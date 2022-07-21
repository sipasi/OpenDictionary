
namespace OpenDictionary.Fonts.Icons;

internal interface IIconAsset
{
    string FontFamily { get; }

    string Edit { get; }
    string DeleteBin { get; }
    string AddNewEntity { get; }
    string Gamepad { get; }
    string Settings { get; }

    string DarkMode { get; }
    string LightMode { get; }

    string Copy { get; }
    string Paste { get; }

    string Save { get; }

    string Download { get; }
    string Upload { get; }

    string PreinstalledDictionaries { get; }
    string DeleteAllDictionaries { get; }

    string Play { get; }

    string ArrowGoBack { get; }

    string Cloud { get; }
    string CloudDone { get; }
}
