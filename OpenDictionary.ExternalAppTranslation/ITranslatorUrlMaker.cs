namespace OpenDictionary.ExternalAppTranslation;

public interface ITranslatorUrlMaker
{
    Uri Make(string text, TranslationOptions options);
}