namespace OpenDictionary.Services.Navigations.Routes
{
    public sealed class GameRouteGroup
    {
        public string OriginToTranslation { get; } = nameof(OriginToTranslation);
        public string TranslationToOrigin { get; } = nameof(TranslationToOrigin);
        public string List { get; } = nameof(List);
    }
}