
using System;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using OpenDictionary.Databases;
using OpenDictionary.ExternalAppTranslation;
using OpenDictionary.Services.Globalization;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels.Shared;


namespace OpenDictionary.WordGroups.ViewModels;

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

    public WordGroupEditState State { get; }

    public CultureInfoViewModel CultureInfo { get; }

    public WordGroupEditCommands Commands { get; }

    public WordGroupEditViewModel(
        IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation,
        IToastMessageService toast, IExternalTranslator translator, ICultureInfoService cultureService)
    {
        this.connection = connection;

        CultureInfo = new(cultureService);

        State = new();

        State.AsEditInfo();

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
            State.AsEditInfo();

            return;
        }

        Guid guid = Guid.Parse(Id);

        var group = connection.Open(context => context.WordGroups.FirstOrDefault(entity => entity.Id == guid));

        if (group == null)
        {
            return;
        }

        State.AsAddWords();

        Name = group.Name;
        OriginCulture = group.OriginCulture;
        TranslationCulture = group.TranslationCulture;
    }
}