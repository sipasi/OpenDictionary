namespace OpenDictionary.Services.Navigations.Routes;

public static class AppRoutes
{
    public static WordRouteGroup Word { get; } = new WordRouteGroup();
    public static WordGroupRouteGroup WordGroup { get; } = new WordGroupRouteGroup();
    public static GameRouteGroup Game { get; } = new GameRouteGroup();
    public static SettingsRouteGroup Settings { get; } = new SettingsRouteGroup();
}