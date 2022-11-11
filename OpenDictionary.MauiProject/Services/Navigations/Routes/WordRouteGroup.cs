namespace OpenDictionary.Services.Navigations.Routes;

public sealed class WordRouteGroup : RouteGroup
{
    public WordRouteGroup()
        : base(create: "WordCreate", edit: "WordEdit", delete: "WordDelete", details: "WordDetails") { }
}