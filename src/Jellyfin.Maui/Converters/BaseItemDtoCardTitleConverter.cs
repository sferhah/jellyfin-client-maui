using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace Jellyfin.Maui.Converters;

/// <summary>
/// BaseItemDto Card Title Converter.
/// </summary>
public class BaseItemDtoCardTitleConverter : BaseConverterOneWay<BaseItemDto?, string?>
{
    /// <inheritdoc/>
    public override string? DefaultConvertReturnValue { get; set; }

    /// <inheritdoc/>
    public override string? ConvertFrom(BaseItemDto? value, CultureInfo? culture)
    {
        if (value is null)
        {
            return null;
        }

        return value.Type switch
        {
            BaseItemKind.Episode => value.SeriesName,
            BaseItemKind.Season => value.SeriesName,
            _ => value.Name?.ToString(culture) ?? string.Empty
        };
    }
}
