
using System;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Models;

namespace OpenDictionary.ViewModels.WordGroups.Commands;

public sealed class WordGroupCollectionCommands
{
    private readonly WordGroupEditViewModel viewModel;

    public RelayCommand AddWordCommand { get; }
    public RelayCommand<Word> DeleteWordCommand { get; }

    public WordGroupCollectionCommands(WordGroupEditViewModel viewModel)
    {
        this.viewModel = viewModel;

        AddWordCommand = new(AddWord, ValidateCanAdd);
        DeleteWordCommand = new(DeleteWord);
    }

    private void AddWord()
    {
        Word word = new()
        {
            Date = DateTime.Now,
            Origin = viewModel.Origin!,
            Translation = viewModel.Translation!
        };

        viewModel.Origin = null;
        viewModel.Translation = null;

        viewModel.Words.Add(word);
    }

    private void DeleteWord(Word? word)
    {
        if (word is null)
        {
            return;
        }

        viewModel.Words.Remove(word);
    }

    private bool ValidateCanAdd()
    {
        return string.IsNullOrWhiteSpace(viewModel.Origin) == false &&
               string.IsNullOrWhiteSpace(viewModel.Translation) == false;
    }
}
