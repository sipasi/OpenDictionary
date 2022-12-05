using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform;

namespace OpenDictionary.Views.Controls;

public class TextInputLine : Entry
{
    public TextInputLine()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            if (view is TextInputLine)
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#elif IOS || MACCATALYST
                handler.PlatformView..BorderStyle = UITextBorderStyle.None;
                handler.PlatformView.Underline.Enabled = false;
                handler.PlatformView.Underline.DisabledColor = UIColor.Clear;
                handler.PlatformView.Underline.Color = UIColor.Clear;
                handler.PlatformView.Underline.BackgroundColor = UIColor.Clear;
                // Remove underline on focus
                handler.PlatformView.ActiveTextInputController.UnderlineHeightActive = 0f;
                // Remove placeholder background
                handler.PlatformView.PlaceholderLabel.BackgroundColor = UIColor.Clear;
#elif WINDOWS
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
            }
        });
    }
}