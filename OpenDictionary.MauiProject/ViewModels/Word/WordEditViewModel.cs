#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

[Microsoft.Maui.Controls.QueryProperty(nameof(Id), nameof(Id))]
public partial class WordEditViewModel : WordViewModel
{
    private readonly IStorage<Word> wordStorage;
    private readonly IStorage<WordMetadata> metadataStorage;
    private readonly INavigationService navigation;

    public WordEditViewModel(IStorage<Word> wordStorage, IStorage<WordMetadata> metadataStorage, INavigationService navigation, IToastMessageService toast) : base(wordStorage, navigation, toast)
    {
        this.wordStorage = wordStorage;
        this.metadataStorage = metadataStorage;
        this.navigation = navigation;

        Word.PropertyChanged += (_, __) =>
        {
            SaveCommand.NotifyCanExecuteChanged();
        };
    }

    protected override async Task OnWordLoaded()
    {
        if (Word.Origin is null)
        {
            return;
        }

        var stored = await metadataStorage
            .Query()
            .IncludeAll()
            .GetByWord(Word.Origin);

        if (stored is null)
        {
            return;
        }

        Metadata.Set(stored);
    }

    [RelayCommand(CanExecute = nameof(ValidateSave))]
    private async Task Save()
    {
        Guid guid = Guid.Parse(Id);

        Word? word = await wordStorage
            .Query()
            .GetById(guid)
            ?? throw new Exception();

        word.Origin = Word.Origin;
        word.Translation = Word.Translation;

        await wordStorage.UpdateAsync(word);

        WordMetadata? loaded = await metadataStorage
            .Query()
            .Select(entity => new WordMetadata { Id = entity.Id, Value = entity.Value })
            .FirstOrDefaultAsync(entity => entity.Value == word.Origin);

        if (loaded is not null)
        {
            await metadataStorage.DeleteAsync(loaded);
        }

        WordMetadata newMetadata = Metadata.AsMetadata();

        newMetadata.Value = Word.Origin;

        await metadataStorage.AddAsync(newMetadata);

        await navigation.GoBackAsync();
    }
    [RelayCommand]
    private async Task Discard()
    {
        // This will pop the current page off the navigation stack
        await navigation.GoBackAsync();
    }

    [RelayCommand]
    private void AddPhonetic()
    {
        Metadata.Phonetics.Add(new PhoneticObservable());
    }
    [RelayCommand]
    private void AddMeaning()
    {
        Metadata.Meanings.Add(new MeaningObservable());
    }
    [RelayCommand]
    private void AddDefinition(MeaningObservable meaning)
    {
        meaning.Definitions.Add(new DefinitionObservable());
    }

    [RelayCommand]
    private void DeletePhonetic(PhoneticObservable phonetic)
    {
        Metadata.Phonetics.Remove(phonetic);
    }
    [RelayCommand]
    private void DeleteMeaning(MeaningObservable meaning)
    {
        Metadata.Meanings.Remove(meaning);
    }
    [RelayCommand]
    private void DeleteDefinition(DefinitionObservable definition)
    {
        var collection = Metadata.Meanings;

        for (int i = 0; i < collection.Count; i++)
        {
            var definitions = collection[i].Definitions;

            if (definitions.Contains(definition))
            {
                definitions.Remove(definition);
            }
        }
    }

    private bool ValidateSave()
    {
        return string.IsNullOrWhiteSpace(Word.Translation) == false;
    }
}