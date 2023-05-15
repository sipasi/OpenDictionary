
using System;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using OpenDictionary.Databases;
using OpenDictionary.ExternalAppTranslation;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels.WordGroups.Commands;

namespace OpenDictionary.ViewModels.WordGroups;

public sealed partial class WordGroupEditViewModel : WordGroupViewModel
{
    [ObservableProperty]
    private string? origin;
    [ObservableProperty]
    private string? translation;

    [ObservableProperty]
    private string? originCulture;
    [ObservableProperty]
    private string? translationCulture;

    private readonly IDatabaseConnection<AppDatabaseContext> connection;

    public WordGroupEditCommands Commands { get; }

    public WordGroupEditViewModel(IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation, IToastMessageService toast, IExternalTranslator translator)
    {
        this.connection = connection;

        Commands = new(this, connection, navigation, toast, translator);

        PropertyChanged += (_, __) =>
        {
            Commands.Collection.AddWordCommand.NotifyCanExecuteChanged();
            Commands.StorageOperation.SaveCommand.NotifyCanExecuteChanged();
        };
    }

    protected override void Load()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            return;
        }

        Guid guid = Guid.Parse(Id);

        var name = connection.Open(context => context.WordGroups
            .Where(entity => entity.Id == guid)
            .Select(entity => entity.Name)
            .FirstOrDefault()
        );

        Name = name ?? throw new Exception();
    }
}