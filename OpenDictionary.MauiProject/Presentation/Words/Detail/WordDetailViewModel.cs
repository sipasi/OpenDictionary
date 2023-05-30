#nullable enable

using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.Controls;

using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Observables;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.Words.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public sealed partial class WordDetailViewModel : WordViewModel
{
    private readonly IDatabaseConnection<DatabaseContext> connection;
    private readonly INavigationService navigation;
    private readonly IDictionarySource source;

    public WordDetailState State { get; }

    public WordStorageViewModel WordStorage { get; }
    public PhoneticViewModel AudioPlayer { get; }

    public WordDetailViewModel(
        IDatabaseConnection<DatabaseContext> connection,
        INavigationService navigation,
        IDialogMessageService dialog,
        IToastMessageService toast,
        IAudioPlayerServise audioPlayer,
        IPhoneticFilesService phoneticFiles,
        IDictionarySource source) : base(connection, navigation, toast)
    {
        this.connection = connection;
        this.navigation = navigation;
        this.source = source;

        State = new();

        AudioPlayer = new(audioPlayer, phoneticFiles);
        WordStorage = new(connection, navigation, dialog);
    }

    protected override async Task OnWordLoaded()
    {
        WordMetadataLoader metadataLoader = new(connection, source);

        WordMetadata? metadata = await metadataLoader.Load(Word.Origin);

        State.AsSuccess();

        if (metadata is null)
        {
            return;
        }

        Metadata.Copy(from: metadata);
    }

    [RelayCommand]
    private Task RedirectToEdit()
    {
        return navigation.GoToAsync(AppRoutes.Word.Edit, parameter: nameof(IEntity.Id), Id);
    }
}