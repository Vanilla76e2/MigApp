using Microsoft.EntityFrameworkCore;
using MigApp.Core.Session;

namespace MigApp.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbContextProvider _provider;
        private MigDatabaseContext _context;
        private readonly ISecurityService _securityService;
        private readonly IAppLogger _logger;
        private readonly IUserSession _userSession;
        private readonly IUINotificationService _ui;

        public AuthService(IDbContextProvider provider, ISecurityService securityService, IAppLogger logger, IUserSession userSession, IUINotificationService ui)
        {
            _provider = provider;
            _context = _provider.GetContext();
            _securityService = securityService;
            _userSession = userSession;
            _logger = logger;
            _ui = ui;
        }

        public async Task<AuthResult> AuthorizeUserAsync(string username, string password)
        {
            _logger.LogDebug($"Проверка входных параметров: username={username}, password={(string.IsNullOrEmpty(password) ? "пустой" : "задан")}");
            ArgumentNullException.ThrowIfNull(username, nameof(username));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            _context = _provider.GetContext();

            try
            {
				_logger.LogInformation($"Начата попытка авторизации пользователя: {username}");
                // Проверяем наличие пользователя в базе данных
                var userProfile = await _context.UsersProfiles.FirstOrDefaultAsync(u => u.Username == username);
                if (userProfile == null)
                {
                    _logger.LogWarning($"Пользователь {username} не найден в базе данных");
                    return new AuthResult(false, "Неверное имя пользователя или пароль.", null);
                }
                _logger.LogInformation($"Пользователь {username} найден в базе данных");

                _logger.LogInformation($"Начата проверка пароля для пользователя: {username}");
                // Проверяем пароль
                if (string.IsNullOrEmpty(userProfile.UserPassword))
                {
                    _logger.LogDebug($"Пароль пользователя {username} не установлен");
                    // Задаём новый пароль, если пароль в БД оказался пустым
                    if (await ChangePasswordAsync(username, password))
                    {
                        await _ui.ShowInfoAsync("Пароль успешно сохранён.");
                        return new AuthResult(false, "Требуется повторная авторизация.", null);
                    }
                    else
                    {
                        return new AuthResult(false, null, null);
                    }
                }
                else if (!_securityService.VerifyHash(password, userProfile.UserPassword))
                {
                    _logger.LogWarning($"Неверный пароль для пользователя: {username}");
                    return new AuthResult(false, "Неверное имя пользователя или пароль.", null);
                }
                _logger.LogInformation($"Пароль для пользователя {username} верный");

                _logger.LogInformation($"Сбор данных пользователя {username} в сессию");
                // Собираем данные пользователя
                var _role = await _context.Roles.FindAsync(userProfile.Role);
                if (_role == null)
                {
                    _logger.LogError($"Роль для пользователя {username} не найдена");
                    throw new NullReferenceException("Роль не найдена");
                }
                UserRole role = new UserRole(_role.Id.ToString(), _role.RoleName, _role.IsAdministrator, _role.EmployeesAccesslevel, _role.TechnicsAccesslevel, _role.FurnitureAccesslevel);
                UserSession session = _userSession.StartSession(userProfile.Id.ToString(), username, role);
                _logger.LogInformation($"Сессия для пользователя {username} успешно собрана");

                // Возвращаем успешный вход
                _logger.LogInformation($"Выполнен успешный вход пользователя: {username}");
                return new AuthResult(true, "Успешный вход", session);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, $"Данные для пользователя {username} неполные");
                return new AuthResult(false, "Ошибка при попытке входа.", null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Критическая ошибка при авторизации пользователя: {username}");
                return new AuthResult(false, "Критическая ошибка при попытке входа.", null);
            }
        }

        public async Task<bool> ChangePasswordAsync(string username, string password)
        {
            _logger.LogDebug($"Начата попытка смены пароля для пользователя: {username}");
            ArgumentNullException.ThrowIfNull(username, nameof(username));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            try
            {
                _logger.LogInformation($"Отправлено предложение на смену пароля для пользователя: {username}");
                // Если пользователь согласился
                if (await _ui.ShowConfirmation("Ваш пароль был сброшен.\nЖелаете сохранить введённый пароль, как новый пароль?"))
                {
                    _logger.LogInformation($"Пользователь {username} решил сохранить новый пароль");
                    if (password.Length < 8)
                    {
                        _logger.LogWarning("Пароль оказался короче 8 символов");
                        await _ui.ShowWarningAsync("Пароль должен быть длиной 8 символов или больше!");
                        return false;
                    }

                    _logger.LogDebug($"Получение записи пользователя: {username}");
                    var userProfile = await _context.UsersProfiles.FirstOrDefaultAsync(u => u.Username == username);

                    if (userProfile != null)
                    {
                        _logger.LogDebug($"Обновление пароля для пользователя: {username}");
                        userProfile.UserPassword = _securityService.HashText(password);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation($"Новый пароль успешно сохранён для пользователя: {username}");
                        return true;
                    }
                    else
                    {
                        _logger.LogError($"Не удалось найти пользователя: {username}");
                        throw new NullReferenceException(nameof(username));
                    }
                }
                else
                {
                    _logger.LogInformation($"Пользователь {username} отказался сохранять новый пароль");
                    return false;
                }
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, $"Пользователь {username} не найден в базе данных");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Критическая ошибка при смене пароля для пользователя: {username}");
                throw;
            }
        }

        public void Logout()
        {
            _logger.LogInformation("Очищаем сессию.");
            _userSession.DisposeSession();
        }
    }
}
