#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Alerts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

[Microsoft.Maui.Controls.QueryProperty(nameof(Id), nameof(Id))]
public partial class WordEditViewModel : WordViewModel
{
    private readonly IStorage<Word> wordStorage;
    private readonly INavigationService navigation;

    public WordEditViewModel(IStorage<Word> wordStorage, INavigationService navigation, IAlertMessageService alert) : base(wordStorage, navigation, alert)
    {
        this.wordStorage = wordStorage;
        this.navigation = navigation;

        Word.PropertyChanged += (_, __) =>
        {
            SaveCommand.NotifyCanExecuteChanged();
        };
    }

    [RelayCommand(CanExecute = nameof(ValidateSave))]
    private async Task Save()
    {
        Guid guid = Guid.Parse(Id);

        Word word = await wordStorage.Query().GetById(guid);

        word.Origin = Word.Origin;
        word.Translation = Word.Translation;

        await wordStorage.UpdateAsync(word);

        // This will pop the current page off the navigation stack
        await navigation.GoBackAsync();
    }
    [RelayCommand]
    private async Task Discard()
    {
        // This will pop the current page off the navigation stack
        await navigation.GoBackAsync();
    }

    private bool ValidateSave()
    {
        return string.IsNullOrWhiteSpace(Word.Translation) == false;
    }
}