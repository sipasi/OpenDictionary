
namespace OpenDictionary.Fonts.Icons;

internal class AppIcons
{
    public static IIconAsset Asset { get; } = new RemixIconAsset();

    public static string FontFamily => Asset.FontFamily;
    public static string Edit => Asset.Edit;
    public static string DeleteBin => Asset.DeleteBin;
    public static string AddNewEntity => Asset.AddNewEntity;
    public static string Gamepad => Asset.Gamepad;
    public static string Settings => Asset.Settings;
    public static string DarkMode => Asset.DarkMode;
    public static string LightMode => Asset.LightMode;
    public static string Copy => Asset.Copy;
    public static string Paste => Asset.Paste;
    public static string Save => Asset.Save;
    public static string Download => Asset.Download;
    public static string Upload => Asset.Upload;
    public static string PreinstalledDictionaries => Asset.PreinstalledDictionaries;
    public static string DeleteAllDictionaries => Asset.DeleteAllDictionaries;
    public static string Play => Asset.Play;
    public static string ArrowGoBack => Asset.ArrowGoBack;
    public static string Cloud => Asset.Cloud;
    public static string CloudDone => Asset.CloudDone;
}
