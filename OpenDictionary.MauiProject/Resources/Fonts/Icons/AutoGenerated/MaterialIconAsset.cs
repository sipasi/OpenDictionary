
namespace OpenDictionary.Fonts.Icons;

internal sealed class MaterialIconAsset : IIconAsset
{
    public const string Alias = nameof(MaterialIconAsset);
    public string FontFamily => Alias;

    public string Edit => "\uea01";
    public string DeleteBin => "\ue872";
    public string AddNewEntity => "\ue145";
    public string Gamepad => "\ue338";
    public string Settings => "\ue8b8";

    public string Copy => "\ue14d";
    public string Paste => "\ue14f";

    public string Save => "\ue161";

    public string DarkMode => "\ue51c";
    public string LightMode => "\ue518";

    public string Download => "\uf090";
    public string Upload => "\uf09b";

    public string PreinstalledDictionaries => "\ueb71";
    public string DeleteAllDictionaries => "\ue92b";

    public string Play => "\ue037";

    public string ArrowGoBack => "\ue5c4";

    public string Cloud => "\ue2bd";
    public string CloudDone => "\ue2bf";
}
