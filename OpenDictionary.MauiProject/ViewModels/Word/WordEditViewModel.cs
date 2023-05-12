#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;


using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

[Microsoft.Maui.Controls.QueryProperty(nameof(Id), nameof(Id))]
public partial class WordEditViewModel : WordViewModel
{
    private readonly IDatabaseConnection<AppDatabaseContext> connection;
    private readonly INavigationService navigation;

    public WordEditViewModel(IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation, IToastMessageService toast) : base(connection, navigation, toast)
    {
        this.connection = connection;
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

        var stored = await connection.Open(context => context.WordMetadatas
            .IncludeAll()
            .GetByWord(Word.Origin));

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

        await using AppDatabaseContext context = connection.Open();

        Word? word = await context.Words.GetById(guid) ?? throw new Exception();

        word.Origin = Word.Origin;
        word.Translation = Word.Translation;

        context.Words.Update(word);

        WordMetadata? loaded = await context.WordMetadatas
            .Select(entity => new WordMetadata
            {
                Id = entity.Id,
                Value = entity.Value
            })
            .FirstOrDefaultAsync(entity => entity.Value == word.Origin);

        if (loaded is not null)
        {
            context.WordMetadatas.Remove(loaded);
        }

        WordMetadata newMetadata = Metadata.AsMetadata();

        newMetadata.Value = Word.Origin;

        await context.WordMetadatas.AddAsync(newMetadata);

        await context.SaveChangesAsync();

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