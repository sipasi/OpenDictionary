namespace OpenDictionary.Services.Navigations.Routes
{
    public sealed class GameRouteGroup
    {
        public string OriginToTranslation { get; } = "OriginToTranslation";
        public string TranslationToOrigin { get; } = "TranslationToOrigin";
        public string List { get; } = nameof(List);
    }
}