
using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Devices;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

public sealed partial class WordGroupEditViewModel : WordGroupViewModel
{
    [ObservableProperty]
    private string? origin;
    [ObservableProperty]
    private string? translation;

    private readonly IStorage<WordGroup> wordGroupStorage;
    private readonly IStorage<Word> wordStorage;
    private readonly INavigationService navigation;
    private readonly IToastMessageService toast;

    public WordGroupEditViewModel(IStorage<WordGroup> wordGroupStorage, IStorage<Word> wordStorage, INavigationService navigation, IToastMessageService toast)
    {
        this.wordGroupStorage = wordGroupStorage;
        this.wordStorage = wordStorage;
        this.navigation = navigation;
        this.toast = toast;

        PropertyChanged += (_, __) =>
        {
            AddWordCommand.NotifyCanExecuteChanged();
            SaveCommand.NotifyCanExecuteChanged();
        };
    }

    protected override void Load()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            return;
        }

        Guid guid = Guid.Parse(Id);

        var group = wordGroupStorage
            .Query()
            .Where(entity => entity.Id == guid)
            .Select(entity => new { entity.Name })
            .FirstOrDefault();

        Name = group?.Name ?? throw new Exception();
    }

    [RelayCommand(CanExecute = nameof(ValidateCanAdd))]
    private void AddWord()
    {
        Word word = new Word
        {
            Date = DateTime.Now,
            Origin = origin!,
            Translation = translation!
        };

        Origin = null;
        Translation = null;

        Words.Add(word);
    }

    [RelayCommand]
    private void DeleteWord(Word word)
    {
        if (word is null)
        {
            return;
        }

        Words.Remove(word);
    }

    [RelayCommand(CanExecute = nameof(ValidateCanSave))]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            WordGroup group = new WordGroup()
            {
                Date = DateTime.Now,
                Name = Name!,
                Words = Words.ToList()
            };

            await wordGroupStorage.AddAsync(group);
        }
        else
        {
            var id = Guid.Parse(Id);

            WordGroup group = await wordGroupStorage.Query().GetById(id);

            group.Name = Name;
            group.Words = Words.ToList();

            await wordGroupStorage.UpdateAsync(group);
        }


        await navigation.GoBackAsync();
    }

    [RelayCommand]
    private Task OnDiscard()
    {
        return navigation.GoBackAsync();
    }


    [RelayCommand]
    private Task OriginCopy() => Copy(origin, toast);
    [RelayCommand]
    private Task OriginPaste() => Paste(text => Origin = text);

    [RelayCommand]
    private Task TranslationCopy() => Copy(translation, toast);
    [RelayCommand]
    private Task TranslationPaste() => Paste(text => Translation = text);

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

    private bool ValidateCanAdd()
    {
        return string.IsNullOrWhiteSpace(origin) == false &&
               string.IsNullOrWhiteSpace(translation) == false;
    }
    private bool ValidateCanSave()
    {
        return string.IsNullOrWhiteSpace(Name) == false;
    }
}