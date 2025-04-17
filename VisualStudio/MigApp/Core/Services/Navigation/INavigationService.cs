namespace MigApp.Core.Services
{
    public interface INavigationService
    {
        ViewModel? CurrentView { get; }
        Task NavigateTo<T>() where T : ViewModel;

        Task NavigateToMainWindow();
        Task NavigateToLoginWindow();
    }
}
