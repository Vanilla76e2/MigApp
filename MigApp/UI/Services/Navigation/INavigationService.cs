using MigApp.UI.Base;

namespace MigApp.UI.Services.Navigation
{
    public interface INavigationService
    {
        ViewModel? CurrentView { get; }
        Task NavigateTo<T>() where T : ViewModel;

        Task NavigateToMainWindow();
        Task NavigateToLoginWindow();
    }
}
