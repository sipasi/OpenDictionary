#nullable enable

using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.Controls;

using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.ViewModels.Words.Commands;

namespace OpenDictionary.ViewModels.Words;

[QueryProperty(nameof(Id), nameof(Id))]
public sealed partial class WordDetailViewModel : WordViewModel
{
    private readonly INavigationService navigation;

    private readonly WordMetadataLoader metadataLoader;

    public WordDetailCommands Commands { get; }

    public WordDetailViewModel(
        IDatabaseConnection<AppDatabaseContext> connection,
        INavigationService navigation,
        IDialogMessageService dialog,
        IToastMessageService toast,
        IAudioPlayerServise audioPlayer,
        IPhoneticFilesService phoneticFiles,
        IDictionarySource source) : base(connection, navigation, toast)
    {
        this.navigation = navigation;

        metadataLoader = new(this, source, connection);

        Commands = new()
        {
            AudioPlayer = new(this, audioPlayer, phoneticFiles),
            WordStorage = new(this, connection, navigation, dialog)
        };
    }

    protected override Task OnWordLoaded()
    {
        return metadataLoader.Load();
    }

    [RelayCommand]
    private Task RedirectToEdit()
    {
        return navigation.GoToAsync(AppRoutes.Word.Edit, parameter: nameof(IEntity.Id), Id);
    }
}