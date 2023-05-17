
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

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

        WordGroup group = new()
        {
            Date = DateTime.Now,
            Name = viewModel.Name?.Trim()!,
            OriginCulture = viewModel.OriginCulture?.Trim()!,
            TranslationCulture = viewModel.TranslationCulture?.Trim()!,
            Words = viewModel.Words.ToList()
        };

        if (string.IsNullOrWhiteSpace(viewModel.Id))
        {
            await context.WordGroups.AddAsync(group);
        }
        else
        {
            group.Id = Guid.Parse(viewModel.Id);

            context.WordGroups.Update(group);
        }

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }


        await navigation.GoBackAsync();
    }

    private bool ValidateCanSave() =>
        IsValideString(viewModel.Name) &&
        IsValideString(viewModel.OriginCulture) &&
        IsValideString(viewModel.TranslationCulture);

    private static bool IsValideString(string? text) => string.IsNullOrWhiteSpace(text) is false;
}