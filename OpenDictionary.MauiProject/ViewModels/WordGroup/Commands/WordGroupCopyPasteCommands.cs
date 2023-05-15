
using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Devices;

using OpenDictionary.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels.WordGroups.Commands;

public sealed class WordGroupCopyPasteCommands
{
    private readonly WordGroupEditViewModel viewModel;
    private readonly IToastMessageService toast;

    public AsyncRelayCommand OriginCopyCommand { get; }
    public AsyncRelayCommand OriginPasteCommand { get; }

    public AsyncRelayCommand TranslationCopyCommand { get; }
    public AsyncRelayCommand TranslationPasteCommand { get; }

    public WordGroupCopyPasteCommands(WordGroupEditViewModel viewModel, IToastMessageService toast)
    {
        this.viewModel = viewModel;
        this.toast = toast;

        OriginCopyCommand = new(OriginCopy);
        OriginPasteCommand = new(OriginPaste);

        TranslationCopyCommand = new(TranslationCopy);
        TranslationPasteCommand = new(TranslationPaste);
    }

    private Task OriginCopy() => Copy(viewModel.Origin, toast);
    private Task OriginPaste() => Paste(text => viewModel.Origin = text);


    private Task TranslationCopy() => Copy(viewModel.Translation, toast);
    private Task TranslationPaste() => Paste(text => viewModel.Translation = text);

    private static async Task Copy(string? text, IToastMessageService toast)
    {
        if (text is null)
        {
            return;
        }

        await Clipboard.Default.SetTextAsync(text);

        TryVibrate();

        await toast.Show("Text has been copied");
    }

    private static async Task Paste(Action<string> propertySet)
    {
        string? text = await Clipboard.Default.GetTextAsync();

        if (text is null)
        {
            return;
        }

        propertySet.Invoke(text);

        TryVibrate();
    }

    private static void TryVibrate()
    {
        var vibration = HapticFeedback.Default;

        if (vibration.IsSupported is false)
        {
            return;
        }

        vibration.Perform(HapticFeedbackType.Click);
    }
}