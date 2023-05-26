#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;
using OpenDictionary.WordGroups.Helpers;

namespace OpenDictionary.Words.ViewModels;

public sealed partial class WordStorageViewModel : ObservableObject
{
    [ObservableProperty]
    private string? id;

    private readonly IDatabaseConnection<DatabaseContext> connection;
    private readonly INavigationService navigation;
    private readonly IDialogMessageService dialog;

    public AsyncRelayCommand DeleteCommand { get; }

    public WordStorageViewModel(IDatabaseConnection<DatabaseContext> connection, INavigationService navigation, IDialogMessageService dialog)
    {
        this.connection = connection;
        this.navigation = navigation;
        this.dialog = dialog;

        DeleteCommand = new(Delete);
    }

    private async Task Delete()
    {
        if (Id == null)
        {
            return;
        }

        await using DatabaseContext context = connection.Open();

        Guid id = Guid.Parse(Id);

        DialogResult result = await EntityDeleteDialog.Show(dialog);

        if (result is DialogResult.Accept)
        {
            Word group = (await context.Set<Word>().GetById(id))!;

            context.Remove(group);

            await context.SaveChangesAsync();

            await navigation.GoBackAsync();
        }
    }
}