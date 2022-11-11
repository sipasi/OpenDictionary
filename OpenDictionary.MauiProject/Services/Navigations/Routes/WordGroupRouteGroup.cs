namespace OpenDictionary.Services.Navigations.Routes;

public sealed class WordGroupRouteGroup : RouteGroup
{
    public WordGroupRouteGroup()
        : base(create: "WordGroupCreate", edit: "WordGroupEdit", delete: "WordGroupDelete", details: "WordGroupDetails") { }
}