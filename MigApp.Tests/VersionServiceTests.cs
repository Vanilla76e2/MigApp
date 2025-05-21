using Moq;
using Moq.Protected;
using System.Net;
using System.Configuration;
using MigApp.Properties;
using System.Reflection;
using MigApp.Infrastructure.Services.DnsResolver.DnsResolver;
using MigApp.Infrastructure.Services.Version.VersionService;
using MigApp.Core.Services.Internet;
using MigApp.Infrastructure.Services.DnsResolver;
using MigApp.Infrastructure.Services.Version;

namespace MigApp.Tests;

[TestClass]
public class VersionServiceTests
{
    private Mock<InternetService> _mockInternetService = null!;
    private HttpClient _httpClient = null!; 
    private VersionService? _versionService;
    private string? _appConfigPath;

    [TestInitialize]
    public void TestInitialize()
    {
        // Создаем моки
        _mockInternetService = new Mock<InternetService>();
        // Создаем HttpClient с Mock<HttpMessageHandler>
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[]"),
            });

        _httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _versionService = new VersionService(_mockInternetService.Object, _httpClient);

        // Создаем временный App.config
        _appConfigPath = Path.GetTempFileName() + ".config";
        AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", _appConfigPath);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        // Сбрасываем настройки после каждого теста с использованием Reflection
        Settings.Default.GetType().GetMethod("Reset", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(Settings.Default, null);

        // Удаляем временный App.config
        if (File.Exists(_appConfigPath))
        {
            File.Delete(_appConfigPath);
        }

        // Восстанавливаем App.config
        AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", null);
    }

    [TestMethod]
    public void ReadOldConnectionSettings_AllSettingsPresent_ReturnsConnectionSettings()
    {
        // Arrange
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        config.AppSettings.Settings.Add("pgServer", "testServer");
        config.AppSettings.Settings.Add("pgDatabase", "testDatabase");
        config.AppSettings.Settings.Add("pgPassword", "testPassword");
        config.AppSettings.Settings.Add("pgUser", "testUser");
        config.AppSettings.Settings.Add("pgPort", "testPort");
        config.Save(ConfigurationSaveMode.Minimal);
        ConfigurationManager.RefreshSection("appSettings");

        // Act
        MethodInfo? methodInfo = typeof(VersionService).GetMethod("ReadOldConnectionSettings", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.IsNotNull(methodInfo, "Метод ReadOldConnectionSettings не найден.");

        // Получаем тип ConnectionSettings через Reflection
        Type connectionSettingsType = typeof(VersionService).GetNestedType("ConnectionSettings", BindingFlags.NonPublic);
        Assert.IsNotNull(connectionSettingsType, "Тип ConnectionSettings не найден");

        object? settings = null;
        try
        {
            settings = methodInfo?.Invoke(null, null);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Ошибка при вызове метода ReadOldConnectionSettings: {ex.Message}");
        }

        // Assert
        Assert.IsNotNull(settings, "Settings is null");

        // Получаем значения свойств через Reflection
        PropertyInfo? pgServerProperty = connectionSettingsType.GetProperty("pgServer");
        PropertyInfo? pgDatabaseProperty = connectionSettingsType.GetProperty("pgDatabase");
        PropertyInfo? pgPasswordProperty = connectionSettingsType.GetProperty("pgPassword");
        PropertyInfo? pgUserProperty = connectionSettingsType.GetProperty("pgUser");
        PropertyInfo? pgPortProperty = connectionSettingsType.GetProperty("pgPort");

        Assert.IsNotNull(pgServerProperty, "Property pgServer not found");
        Assert.IsNotNull(pgDatabaseProperty, "Property pgDatabase not found");
        Assert.IsNotNull(pgPasswordProperty, "Property pgPassword not found");
        Assert.IsNotNull(pgUserProperty, "Property pgUser not found");
        Assert.IsNotNull(pgPortProperty, "Property pgPort not found");

        Assert.AreEqual("testServer", pgServerProperty?.GetValue(settings, null));
        Assert.AreEqual("testDatabase", pgDatabaseProperty?.GetValue(settings, null));
        Assert.AreEqual("testPassword", pgPasswordProperty?.GetValue(settings, null));
        Assert.AreEqual("testUser", pgUserProperty?.GetValue(settings, null));
        Assert.AreEqual("testPort", pgPortProperty?.GetValue(settings, null));
    }

    [TestMethod]
    public void ReadOldConnectionSettings_MissingSettings_ReturnsNull()
    {
        // Arrange
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        config.AppSettings.Settings.Clear();
        config.AppSettings.Settings.Add("pgServer", "testServer");
        config.AppSettings.Settings.Add("pgDatabase", "testDatabase");
        config.Save(ConfigurationSaveMode.Minimal);
        ConfigurationManager.RefreshSection("appSettings");

        // Act
        MethodInfo? methodInfo = typeof(VersionService).GetMethod("ReadOldConnectionSettings", BindingFlags.NonPublic | BindingFlags.Instance);
        dynamic? settings = methodInfo?.Invoke(_versionService, null);

        // Assert
        Assert.IsNull(settings);
    }

    [TestMethod]
    public async Task GetLatestReleaseInfoAsync_ReturnsCorrectReleaseInfo()
    {
        // Arrange
        string filePath = @"C:\Users\Vanilla76e2\source\repos\MigApp\MigApp.Tests\ApiGithubTest.json";
        // Читаем JSON файл с тестовыми данными
        string json = File.ReadAllText(filePath);
        // Подменяем HTTP-клиент на мок
        var mockHttpMessageHandler = new Mock<HttpClientHandler>();
        mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>
            (
            "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json)
            });
        // Создаём HttpClient с нашим моком
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        // Создаём InternetService
        var mockDnsResolver = new Mock<IDnsResolver>();
        mockDnsResolver.Setup(x => x.GetHostEntryAsync(It.IsAny<string>())).ReturnsAsync(new System.Net.IPHostEntry());
        var internetService = new InternetService(mockDnsResolver.Object);
        // Создаём VersionService
        var versionService = new VersionService(internetService, httpClient);

        // Act
        ReleaseInfo? releaseInfo = await versionService.GetLatestReleaseInfoAsync();

        // Assert
        Assert.IsNotNull(releaseInfo);
        Assert.AreEqual("1.6.1", releaseInfo.Version);
        Assert.AreEqual(false, releaseInfo.IsPreRelease);
        Assert.AreEqual("https://github.com/Vanilla76e2/MigApp/releases/download/v1.6.1/MigApp_Installer_1.6.1.msi", releaseInfo.DownloadUrl);

        // Убеждаемся, что HTTP-запрос был отправлен только один раз
        mockHttpMessageHandler.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    
}
