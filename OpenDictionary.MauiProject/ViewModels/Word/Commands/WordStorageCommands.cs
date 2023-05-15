#nullable enable

using CommunityToolkit.Mvvm.Input;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels.Helpers;
using System.Threading.Tasks;
using System;
using OpenDictionary.Collections.Storages.Extensions;

namespace OpenDictionary.ViewModels.Words.Commands;

public sealed class WordStorageCommands
{
    private readonly WordDetailViewModel viewModel;

    private readonly IDatabaseConnection<AppDatabaseContext> connection;
    private readonly INavigationService navigation;
    private readonly IDialogMessageService dialog;

    public AsyncRelayCommand DeleteCommand { get; }

    public WordStorageCommands(WordDetailViewModel viewModel, IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation, IDialogMessageService dialog)
    {
        this.viewModel = viewModel;
        this.connection = connection;
        this.navigation = navigation;
        this.dialog = dialog;

        DeleteCommand = new(Delete);
    }

    private async Task Delete()
    {
        await using AppDatabaseContext context = connection.Open();

        Guid id = Guid.Parse(viewModel.Id);

        DialogResult result = await EntityDeleteDialog.Show(dialog);

        if (result is DialogResult.Ok)
        {
            Word group = (await context.Words.GetById(id))!;

            context.Remove(group);

            await context.SaveChangesAsync();

            await navigation.GoBackAsync();
        }
    }
}