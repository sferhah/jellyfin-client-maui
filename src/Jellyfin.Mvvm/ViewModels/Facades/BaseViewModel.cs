using System.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jellyfin.Mvvm.Services;

namespace Jellyfin.Mvvm.ViewModels.Facades;

/// <summary>
/// Base view model.
/// </summary>
public abstract partial class BaseViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private bool _loading = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Instance of the <see cref="INavigationService"/> interface.</param>
    /// <param name="applicationService">Instance of the <see cref="IApplicationService"/> interface.</param>
    protected BaseViewModel(INavigationService navigationService, IApplicationService applicationService)
    {
        _navigationService = navigationService;
        ApplicationService = applicationService;
    }

    /// <summary>
    /// Gets the ApplicationService.
    /// </summary>
    protected IApplicationService ApplicationService { get; }

    /// <summary>
    /// Gets or sets the selected BaseItemDto.
    /// </summary>
    public BaseItemDto? SelectedItem { get; set; }

    /// <summary>
    /// Initialize the view model.
    /// </summary>
    /// <returns>The task.</returns>
    public abstract ValueTask InitializeAsync();

    /// <summary>
    /// Ensure Observable Collection is thread-safe.
    /// </summary>
    /// <remarks>
    ///  https://codetraveler.io/2019/09/11/using-observablecollection-in-a-multi-threaded-xamarin-forms-application/.
    /// </remarks>
    /// <param name="collection">The collection.</param>
    /// <param name="context">The context.</param>
    /// <param name="accessMethod">The access method.</param>
    /// <param name="writeAccess">Whether to enable write access.</param>
    protected void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
    {
        ApplicationService.Dispatch(accessMethod);
    }

    [RelayCommand]
    private void NavigateToItem(BaseItemDto? item)
    {
        if (item is null)
        {
            return;
        }

        _navigationService.NavigateToItemView(item);
    }
}
