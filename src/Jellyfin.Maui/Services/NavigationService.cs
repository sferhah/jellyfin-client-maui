using Jellyfin.Maui.Pages;
using Jellyfin.Maui.Pages.Facades;
using Jellyfin.Maui.Pages.Login;
using Jellyfin.Mvvm.Services;
using Jellyfin.Mvvm.ViewModels;
using Jellyfin.Mvvm.ViewModels.Facades;

namespace Jellyfin.Maui.Services;

/// <inheritdoc />
public class NavigationService : INavigationService
{
    private readonly Application _application = Application.Current!;
    private NavigationPage? _navigationPage;
    private NavigationPage? _loginNavigationPage;

    /// <inheritdoc />
    public void NavigateToUserSelectPage()
    {
        if (_loginNavigationPage is null)
        {
            NavigateToServerSelectPage();
            return;
        }

        _application.Dispatcher.Dispatch(() =>
        {
            var userSelectPage = InternalServiceProvider.GetService<SelectUserPage>();
            _loginNavigationPage.PushAsync(userSelectPage).SafeFireAndForget();
        });
    }

    /// <inheritdoc />
    public void NavigateToLoginPage()
    {
        if (_loginNavigationPage is null)
        {
            NavigateToServerSelectPage();
            return;
        }

        _application.Dispatcher.Dispatch(() =>
        {
            var loginPage = InternalServiceProvider.GetService<LoginPage>();
            _loginNavigationPage.PushAsync(loginPage).SafeFireAndForget();
        });
    }

    /// <inheritdoc />
    public void NavigateToServerSelectPage()
    {
        if (_loginNavigationPage is null)
        {
            _application.Dispatcher.Dispatch(() =>
            {
                var serverSelectPage = InternalServiceProvider.GetService<SelectServerPage>();
                _application.MainPage = _loginNavigationPage = new NavigationPage(serverSelectPage);
            });
        }
        else
        {
            _application.Dispatcher.Dispatch(() => _loginNavigationPage.PopToRootAsync(true).SafeFireAndForget());
        }
    }

    /// <inheritdoc />
    public void NavigateToAddServerPage()
    {
        if (_loginNavigationPage is null)
        {
            NavigateToServerSelectPage();
            return;
        }

        _application.Dispatcher.Dispatch(() =>
        {
            var addServerPage = InternalServiceProvider.GetService<AddServerPage>();
            _loginNavigationPage.PushAsync(addServerPage).SafeFireAndForget();
        });
    }

    /// <inheritdoc />
    public void NavigateHome()
    {
        _loginNavigationPage = null;

        if (_navigationPage is null // first appearance
            || _application.MainPage != _navigationPage) // after logout
        {
            _application.Dispatcher.Dispatch(() =>
            {
                var homePage = InternalServiceProvider.GetService<HomePage>();
                _application.MainPage = _navigationPage = new NavigationPage(homePage);
            });
        }
        else
        {
            _application.Dispatcher.Dispatch(() => _navigationPage.PopToRootAsync(true).SafeFireAndForget());
        }
    }

    /// <inheritdoc />
    public void NavigateToItemView(BaseItemDto item)
    {
        ArgumentNullException.ThrowIfNull(item);
        switch (item.Type)
        {
            case BaseItemKind.CollectionFolder:
                Navigate<LibraryPage, LibraryViewModel>(item.Id);
                break;
            default:
                Navigate<ItemPage, ItemViewModel>(item.Id);
                break;
        }
    }

    private void Navigate<TPage, TViewModel>(Guid itemId)
        where TViewModel : BaseItemViewModel
        where TPage : BaseContentIdPage<TViewModel>
    {
        if (_navigationPage is null)
        {
            NavigateHome();
            return;
        }

        _application.Dispatcher.Dispatch(() =>
        {
            var resolvedView = InternalServiceProvider.GetService<TPage>();
            resolvedView.Initialize(itemId);
            _navigationPage.PushAsync(resolvedView, true).SafeFireAndForget();
        });
    }
}
