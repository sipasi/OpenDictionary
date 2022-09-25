
namespace OpenDictionary.Fonts.Icons;

internal sealed class RemixIconAsset : IIconAsset
{
    public const string Alias = nameof(RemixIconAsset);
    public string FontFamily => Alias;

    public string Edit => "\uec80";
    public string DeleteBin => "\uec24";
    public string AddNewEntity => "\uea12";
    public string Gamepad => "\uedab";
    public string Settings => "\uf0e6";

    public string Copy => "\uecd3";
    public string Paste => "\ueb91";

    public string Save => "\uf0b3";

    public string DarkMode => "\uef75";
    public string LightMode => "\uf1bf";

    public string Download => "\uec54";
    public string Upload => "\uf24a";

    public string PreinstalledDictionaries => "\uee68";
    public string DeleteAllDictionaries => "\uf246";

    public string Play => "\uf009";

    public string ArrowGoBack => "\uea58";

    public string Cloud => "\ueb9f";
    public string CloudDone => "\ueb9d";

    public string CheckboxIndeterminateCircle => "\uee57";
    public string CheckboxBlankCircle => "\ueb7d";
    public string CheckboxCircle => "\ueb81";
}