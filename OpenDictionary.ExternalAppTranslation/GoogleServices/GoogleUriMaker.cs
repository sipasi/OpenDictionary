namespace OpenDictionary.ExternalAppTranslation.GoogleServices;

public sealed class GoogleUriMaker : ITranslatorUrlMaker
{
    public Uri Make(string text, TranslationOptions options)
    {
        string escaped = Uri.EscapeDataString(text);

        string result = string.Concat(
            "https://translate.google.com/",
            "?sl=", options.From.TwoLetterISOLanguageName,
            "&tl=", options.To.TwoLetterISOLanguageName,
            "&text=", escaped,
            "&op=translate");

        return new(result);
    }
}