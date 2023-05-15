namespace OpenDictionary.ExternalAppTranslation;

public interface IAppOrBrowserLauncher
{
    ValueTask OpenAsync(Uri uri);
}