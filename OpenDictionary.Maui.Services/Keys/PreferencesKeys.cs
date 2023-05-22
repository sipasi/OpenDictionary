#nullable enable

namespace OpenDictionary.Services.Keys;

public static class PreferencesKeys
{
    public static class Theme
    {
        public static string UserAppTheme => "UserAppTheme";
    }

    public static class CultureInfo
    {
        public static string PreferOrigin => nameof(PreferOrigin);
        public static string PreferTranslation => nameof(PreferTranslation);
    }
}