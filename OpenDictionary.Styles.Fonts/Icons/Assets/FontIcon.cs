using OpenDictionary.CodeAnalysis;

namespace OpenDictionary.Styles.Fonts.Icons;

[EnumToInterface(InterfaceName = "IIconAsset")]
public enum FontIcon
{
    FontFamily,

    Edit,
    DeleteBin,
    AddNewEntity,
    Gamepad,
    Settings,

    DarkMode,
    LightMode,

    Copy,
    Paste,

    Save,

    Download,
    Upload,

    PreinstalledDictionaries,
    DeleteAllDictionaries,

    Play,

    ArrowGoBack,

    Cloud,
    CloudDone,

    CheckboxIndeterminateCircle,
    CheckboxBlankCircle,
    CheckboxCircle,

    Translate,
}