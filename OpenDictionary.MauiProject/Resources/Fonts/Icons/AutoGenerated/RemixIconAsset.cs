
namespace OpenDictionary.Fonts.Icons;

internal sealed class RemixIconAsset : IIconAsset
{
    public const string Alias = nameof(RemixIconAsset);
    public string FontFamily => Alias;

    public string Edit => RemixIcon.Edit2Line;
    public string DeleteBin => RemixIcon.DeleteBin5Line;
    public string AddNewEntity => RemixIcon.AddFill;
    public string Gamepad => RemixIcon.GamepadLine;
    public string Settings => RemixIcon.Settings3Line;

    public string Copy => RemixIcon.FileCopy2Line;
    public string Paste => RemixIcon.ClipboardLine;

    public string Save => RemixIcon.SaveLine;

    public string DarkMode => RemixIcon.MoonLine;
    public string LightMode => RemixIcon.SunLine;

    public string Download => RemixIcon.Download2Line;
    public string Upload => RemixIcon.Upload2Line;

    public string PreinstalledDictionaries => RemixIcon.InstallLine;
    public string DeleteAllDictionaries => RemixIcon.UninstallLine;

    public string Play => RemixIcon.PlayCircleLine;

    public string ArrowGoBack => RemixIcon.ArrowGoBackLine;

    public string Cloud => RemixIcon.CloudOffLine;
    public string CloudDone => RemixIcon.CloudLine;
}
