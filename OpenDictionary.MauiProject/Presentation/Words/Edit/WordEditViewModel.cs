#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;


using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Words.ViewModels;

[Microsoft.Maui.Controls.QueryProperty(nameof(Id), nameof(Id))]
public partial class WordEditViewModel : WordViewModel
{
    private readonly IDatabaseConnection<DatabaseContext> connection;
    private readonly INavigationService navigation;

    public WordEditViewModel(IDatabaseConnection<DatabaseContext> connection, INavigationService navigation, IToastMessageService toast)
        : base(connection, navigation, toast)
    {
        this.connection = connection;
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

        await using DatabaseContext context = connection.Open();

        Word word = await context.Set<Word>().GetById(guid) ?? throw new Exception();

        word.Origin = Word.Origin;
        word.Translation = Word.Translation;

        await context.SaveChangesAsync();

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