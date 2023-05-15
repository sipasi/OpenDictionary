namespace OpenDictionary.ExternalAppTranslation;

public interface IExternalTranslator
{
    ValueTask Open(string text, TranslationOptions options);
}