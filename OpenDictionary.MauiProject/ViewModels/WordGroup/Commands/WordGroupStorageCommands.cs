
using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels.WordGroups.Commands;

public sealed class WordGroupStorageCommands
{
    private readonly WordGroupEditViewModel viewModel;
    private readonly IDatabaseConnection<AppDatabaseContext> connection;
    private readonly INavigationService navigation;

    public AsyncRelayCommand SaveCommand { get; }

    public WordGroupStorageCommands(WordGroupEditViewModel viewModel, IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation)
    {
        this.viewModel = viewModel;
        this.connection = connection;
        this.navigation = navigation;

        SaveCommand = new(Save, ValidateCanSave);
    }

    private async Task Save()
    {
        await using AppDatabaseContext context = connection.Open();

        if (string.IsNullOrWhiteSpace(viewModel.Id))
        {
            WordGroup group = new WordGroup()
            {
                Date = DateTime.Now,
                Name = viewModel.Name!,
                Words = viewModel.Words.ToList()
            };

            await context.WordGroups.AddAsync(group);
        }
        else
        {
            var id = Guid.Parse(viewModel.Id);

            WordGroup group = (await context.WordGroups.GetById(id))!;

            group.Name = viewModel.Name!;
            group.Words = viewModel.Words.ToList();

            context.WordGroups.Update(group);
        }
        await context.SaveChangesAsync();

        await navigation.GoBackAsync();
    }

    private bool ValidateCanSave() => string.IsNullOrWhiteSpace(viewModel.Name) == false;
}
