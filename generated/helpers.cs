namespace CrudUi.Pages.Configuration;

/// <summary>
/// API endpoints class.
/// Endpoints are returned as <see cref="Uri"/> instead of <see cref="string"/> to prevent problems with trailing slashes when combining uri segments.
/// </summary>
public class Endpoints
{
    // Note: Make sure to add configuration in program.cs when adding new properties!

    /// <summary>
    /// URI of the Cartons SignalR Hub.
    /// </summary>
    public Uri CartonsHub { get; set; } = null!;

    /// <summary>
    /// Base URI of the Data Service API.
    /// </summary>
    public Uri DataServiceBaseUri { get; set; } = null!;

    /// <summary>
    /// Base URI of the Data Service API.
    /// </summary>
    public Uri AsfService { get; set; } = null!;
}