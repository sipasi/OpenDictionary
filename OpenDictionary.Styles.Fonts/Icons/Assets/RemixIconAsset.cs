
namespace OpenDictionary.Styles.Fonts.Icons;

public sealed class RemixIconAsset : IIconAsset
{
    public const string Alias = nameof(RemixIconAsset);
    public string FontFamily => Alias;

    public string Edit => RemixIconAlias.EditLine;
    public string DeleteBin => RemixIconAlias.DeleteBinLine;
    public string AddNewEntity => RemixIconAlias.FileAddLine;
    public string Gamepad => RemixIconAlias.GameLine;
    public string Settings => RemixIconAlias.SettingsLine;

    public string Copy => RemixIconAlias.FileCopyLine;
    public string Paste => RemixIconAlias.ClipboardLine;

    public string Save => RemixIconAlias.SaveLine;

    public string DarkMode => RemixIconAlias.MoonClearLine;
    public string LightMode => RemixIconAlias.SunLine;

    public string Download => RemixIconAlias.Download2Line;
    public string Upload => RemixIconAlias.Upload2Line;

    public string PreinstalledDictionaries => RemixIconAlias.InstallLine;
    public string DeleteAllDictionaries => RemixIconAlias.UninstallLine;

    public string Play => RemixIconAlias.PlayLine;

    public string ArrowGoBack => RemixIconAlias.ArrowGoBackLine;

    public string Cloud => RemixIconAlias.CloudOffLine;
    public string CloudDone => RemixIconAlias.CloudLine;

    public string CheckboxIndeterminateCircle => RemixIconAlias.IndeterminateCircleLine;
    public string CheckboxBlankCircle => RemixIconAlias.CheckboxBlankCircleLine;
    public string CheckboxCircle => RemixIconAlias.CheckboxCircleLine;

    public string Translate => RemixIconAlias.Translate;
}