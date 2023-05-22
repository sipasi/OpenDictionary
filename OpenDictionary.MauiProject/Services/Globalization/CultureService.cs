using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using Microsoft.Maui.Storage;

using OpenDictionary.Services.Keys;

namespace OpenDictionary.Services.Globalization;

public interface ICultureInfoService
{
    public CultureInfo English { get; }
    public CultureInfo System { get; }

    public CultureInfo PreferOrigin { get; }
    public CultureInfo PreferTranslation { get; }

    IReadOnlyList<CultureInfo> NeutralCultures { get; }
}

internal class CultureService : ICultureInfoService
{
    public CultureInfo English { get; }
    public CultureInfo System { get; }

    public CultureInfo PreferOrigin => GetInfo(PreferencesKeys.CultureInfo.PreferOrigin, English);

    public CultureInfo PreferTranslation => GetInfo(PreferencesKeys.CultureInfo.PreferTranslation, System);

    public IReadOnlyList<CultureInfo> NeutralCultures { get; }

    public CultureService()
    {
        CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

        English = CultureInfo.GetCultureInfo("en");
        System = cultures.FirstOrDefault(IsCurrentCulture) ?? English;

        NeutralCultures = cultures;
    }

    private static bool IsCurrentCulture(CultureInfo culture)
    {
        return culture.TwoLetterISOLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }

    private static CultureInfo GetInfo(string key, CultureInfo defaultValue)
    {
        string? iso = Preferences.Get(key, null);

        if (iso is null)
        {
            return defaultValue;
        }

        try
        {
            return CultureInfo.GetCultureInfo(iso);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);

            return defaultValue;
        }
    }
}