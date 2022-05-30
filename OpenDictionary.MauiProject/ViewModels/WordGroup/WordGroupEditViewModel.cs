
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

public sealed class WordGroupEditViewModel : WordGroupViewModel
{
    private readonly IStorage<WordGroup> wordGroupStorage;
    private readonly IStorage<Word> wordStorage;
    private readonly INavigationService navigation;

    private string origin;
    private string translation;

    private readonly List<Word> created;
    private readonly List<Word> deleted;

    public string Origin
    {
        get => origin;
        set => SetProperty(ref origin, value);
    }
    public string Translation
    {
        get => translation;
        set => SetProperty(ref translation, value);
    }

    public bool IsNew => string.IsNullOrWhiteSpace(Id);

    public Command AddWordCommand { get; }
    public Command<Word> DeleteWordCommand { get; }

    public Command SaveCommand { get; }
    public Command DiscardCommand { get; }

    public WordGroupEditViewModel(IStorage<WordGroup> wordGroupStorage, IStorage<Word> wordStorage, INavigationService navigation)
        : base(wordGroupStorage, navigation)
    {
        this.wordGroupStorage = wordGroupStorage;
        this.wordStorage = wordStorage;
        this.navigation = navigation;

        created = new List<Word>();
        deleted = new List<Word>();

        AddWordCommand = new Command(OnAddWord, ValidateAdd);
        SaveCommand = new Command(OnSave, ValidateSave);
        DiscardCommand = new Command(OnDiscard);

        DeleteWordCommand = new Command<Word>(OnDeleteWord);

        PropertyChanged += (_, __) =>
        {
            AddWordCommand.ChangeCanExecute();
            SaveCommand.ChangeCanExecute();
        };
    }

    private bool ValidateAdd()
    {
        return string.IsNullOrWhiteSpace(origin) == false &&
               string.IsNullOrWhiteSpace(translation) == false;
    }
    private bool ValidateSave()
    {
        return string.IsNullOrWhiteSpace(Name) == false;
    }

    private void OnAddWord()
    {
        Word word = new Word
        {
            Date = DateTime.Now,
            Origin = origin,
            Translation = translation
        };

        Origin = null;
        Translation = null;

        Words.Collection.Add(word);
        created.Add(word);
    }
    private void OnDeleteWord(Word word)
    {
        if (word is null)
        {
            return;
        }

        Words.Collection.Remove(word);

        if (created.Contains(word) is false)
        {
            deleted.Add(word);
        }
    }

    private async void OnSave()
    {
        if (IsNew)
        {
            WordGroup group = new WordGroup()
            {
                Date = DateTime.Now,
                Name = Name,
                Words = Words.Collection.ToList()
            };

            await wordGroupStorage.AddAsync(group);
        }
        else
        {
            var id = Guid.Parse(Id);

            WordGroup group = await wordGroupStorage.Query().GetById(id);

            group.Words = Words.Collection.ToList();

            foreach (var word in deleted)
            {
                await wordStorage.DeleteAsync(word);
            }

            await wordGroupStorage.UpdateAsync(group);
        }


        await navigation.GoBackAsync();
    }
    private async void OnDiscard()
    {
        await navigation.GoBackAsync();
    }
}