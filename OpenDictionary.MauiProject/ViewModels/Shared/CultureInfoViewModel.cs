using System.Collections.Generic;
using System.Globalization;

using CommunityToolkit.Mvvm.ComponentModel;

using OpenDictionary.Services.Globalization;

namespace OpenDictionary.ViewModels.Shared;

public sealed partial class CultureInfoViewModel : ObservableObject
{
    [ObservableProperty]
    private CultureInfo origin;
    [ObservableProperty]
    private CultureInfo translation;

    public IReadOnlyList<CultureInfo> Cultures { get; }

    public CultureInfoViewModel(ICultureInfoService cultures)
    {
        Cultures = cultures.NeutralCultures;

        origin = cultures.PreferOrigin;
        translation = cultures.PreferTranslation;
    }
}