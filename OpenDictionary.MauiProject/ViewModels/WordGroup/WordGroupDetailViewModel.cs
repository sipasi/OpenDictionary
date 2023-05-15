
using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.ViewModels.Helpers;

namespace OpenDictionary.ViewModels.WordGroups;

public sealed partial class WordGroupDetailViewModel : WordGroupViewModel
{
    private readonly IDatabaseConnection<AppDatabaseContext> connection;
    private readonly INavigationService navigation;
    private readonly IDialogMessageService dialog;

    public Word? TappedItem { get; private set; }

    public WordGroupDetailViewModel(IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation, IDialogMessageService dialog)
    {
        this.connection = connection;
        this.navigation = navigation;
        this.dialog = dialog;
    }
    protected override void Load()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            return;
        }

        Guid guid = Guid.Parse(Id);

        var group = connection.Open(context => context.WordGroups
            .Where(entity => entity.Id == guid)
            .Select(entity => new { entity.Name, entity.Words })
            .FirstOrDefault());

        if (group is null)
        {
            throw new Exception();
        }

        Name = group.Name;

        Words.Clear();

        Words.AddRange(group.Words);
    }

    [RelayCommand]
    private Task Tapped(Word word)
    {
        TappedItem = word;

        return navigation.GoToAsync(AppRoutes.Word.Detail, parameter: nameof(Word.Id), value: word.Id.ToString());
    }

    [RelayCommand]
    private Task RedirectToGame()
    {
        return navigation.GoToAsync(AppRoutes.Game.List, parameter: nameof(WordGroup.Id), value: Id!);
    }

    [RelayCommand]
    private Task RedirectToEdit()
    {
        return navigation.GoToAsync(AppRoutes.WordGroup.Create, parameter: nameof(WordGroup.Id), value: Id!);
    }

    [RelayCommand]
    private async Task Delete()
    {
        Guid guid = Guid.Parse(Id!);

        DialogResult result = await EntityDeleteDialog.Show(dialog);

        if (result is DialogResult.Ok)
        {
            await using AppDatabaseContext context = connection.Open();

            WordGroup group = (await context.WordGroups.IncludeAll().GetById(guid))!;

            context.Remove(group);

            await context.SaveChangesAsync();

            await navigation.GoBackAsync();
        }
    }
}