namespace OpenDictionary.Services.Navigations.Routes;

public class RouteGroup
{
    public RouteGroup(string create, string edit, string delete, string details)
    {
        Create = create;
        Edit = edit;
        Delete = delete;
        Detail = details;
    }

    public string Create { get; }
    public string Edit { get; }
    public string Delete { get; }
    public string Detail { get; }
}