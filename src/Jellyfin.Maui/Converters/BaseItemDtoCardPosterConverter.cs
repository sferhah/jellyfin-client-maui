using CommunityToolkit.Maui.Converters;
using Jellyfin.Maui.Services;

namespace Jellyfin.Maui.Converters;

/// <summary>
/// Gets the poster url from the base item.
/// </summary>
public class BaseItemDtoCardPosterConverter : BaseConverterOneWay<BaseItemDto?, ImageSource?>
{
    private readonly ImageType _imageType;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseItemDtoCardPosterConverter"/> class.
    /// </summary>
    /// <param name="imageType">The image type to fetch.</param>
    public BaseItemDtoCardPosterConverter(ImageType imageType)
    {
        _imageType = imageType;
    }

    /// <inheritdoc/>
    public override ImageSource? ConvertFrom(BaseItemDto? value)
    {
        if (value is null)
        {
            return null;
        }

        var host = InternalServiceProvider.GetService<IStateService>().GetHost();

        var itemId = value.Id;
        if (value.Type == BaseItemKind.Episode)
        {
            itemId = value.SeriesId ?? value.Id;
        }

        return ImageSource.FromUri(new Uri($"{host}/Items/{itemId}/Images/{_imageType}?maxHeight=480"));
    }
}
