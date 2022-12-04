namespace OpenDictionary.Styles.Fonts.Icons;

public static class IconDictionary
{
    public static string Get(FontIcon icon)
    {
        IIconAsset asset = AppIcons.Asset;

        string glyph = icon switch
        {
            FontIcon.AddNewEntity => asset.AddNewEntity,
            FontIcon.Edit => asset.Edit,
            FontIcon.DeleteBin => asset.DeleteBin,
            FontIcon.Gamepad => asset.Gamepad,
            FontIcon.Settings => asset.Settings,
            FontIcon.DarkMode => asset.DarkMode,
            FontIcon.LightMode => asset.LightMode,
            FontIcon.Copy => asset.Copy,
            FontIcon.Paste => asset.Paste,
            FontIcon.Save => asset.Save,
            FontIcon.Download => asset.Download,
            FontIcon.Upload => asset.Upload,
            FontIcon.PreinstalledDictionaries => asset.PreinstalledDictionaries,
            FontIcon.DeleteAllDictionaries => asset.DeleteAllDictionaries,
            FontIcon.Play => asset.Play,
            FontIcon.ArrowGoBack => asset.ArrowGoBack,
            FontIcon.Cloud => asset.Cloud,
            FontIcon.CloudDone => asset.CloudDone,
            FontIcon.CheckboxIndeterminateCircle => asset.CheckboxIndeterminateCircle,
            FontIcon.CheckboxBlankCircle => asset.CheckboxBlankCircle,
            FontIcon.CheckboxCircle => asset.CheckboxCircle,

            _ => throw new NotImplementedException($"{typeof(IconDictionary).FullName}"),
        };

        return glyph;
    }
}
