using CommunityToolkit.Maui.Markup;
using Jellyfin.Maui.DataTemplates;
using Jellyfin.Maui.Pages.Facades;
using Jellyfin.Maui.ViewModels;

namespace Jellyfin.Maui.Pages;

/// <summary>
/// Library page.
/// </summary>
public class LibraryPage : BaseContentIdPage<LibraryViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LibraryPage"/> class.
    /// </summary>
    /// <param name="viewModel">Instance of the <see cref="LibraryViewModel"/>.</param>
    public LibraryPage(LibraryViewModel viewModel)
        : base(viewModel, Strings.Library)
    {
    }

    /// <inheritdoc />
    protected override void InitializeLayout()
    {
        Content = new StackLayout
        {
            Children =
            {
                /*
                 * TODO how to make this work?
                new Label()
                    .Bind(Label.TextProperty, nameof(ViewModel.Item.Name), source: ViewModel.Item, mode: BindingMode.OneWay),
                */
                new Label()
                    .Bind(Label.TextProperty, "Item.Name", mode: BindingMode.OneWay),
                new ScrollView
                {
                    Padding = 16,
                    Content = new FlexLayout
                    {
                        Wrap = Microsoft.Maui.Layouts.FlexWrap.Wrap,
                        Direction = Microsoft.Maui.Layouts.FlexDirection.Row,
                    }
                    .ItemTemplate(new PosterCardTemplate())
                    .Bind(BindableLayout.ItemsSourceProperty, nameof(ViewModel.LibraryItemsCollection))
                }
            }
        };
    }
}
