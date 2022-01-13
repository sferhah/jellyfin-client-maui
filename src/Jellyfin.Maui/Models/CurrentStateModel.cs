using Jellyfin.Sdk;

namespace Jellyfin.Maui.Models;

/// <summary>
/// The state model.
/// </summary>
public class CurrentStateModel
{
    /// <summary>
    /// Gets or sets the current token.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Gets or sets the current host.
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// Gets or sets the current user.
    /// </summary>
    public UserDto? UserDto { get; set; }

    /// <summary>
    /// Gets or sets the server state.
    /// </summary>
    public ServerStateModel? Server { get; set; }
}
