using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Databases;
using OpenDictionary.ExternalAppTranslation;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.WordGroups.ViewModels;

public sealed class WordGroupEditCommands
{
    private readonly INavigationService navigation;

    public WordGroupCollectionCommands Collection { get; }
    public WordGroupCopyPasteCommands CopyPaste { get; }
    public WordGroupStorageCommands StorageOperation { get; }
    public WordGroupTranslateCommands Translate { get; }

    public AsyncRelayCommand OnDiscardCommand { get; }

    public WordGroupEditCommands(
        WordGroupEditViewModel viewModel,
        IDatabaseConnection<AppDatabaseContext> connection,
        INavigationService navigation, IToastMessageService toast, IExternalTranslator translator)
    {
        this.navigation = navigation;
        Collection = new(viewModel);
        CopyPaste = new(viewModel, toast);
        StorageOperation = new(viewModel, connection, navigation);
        Translate = new(viewModel, translator);

        OnDiscardCommand = new(OnDiscard);
    }

    private Task OnDiscard()
    {
        return navigation.GoBackAsync();
    }
}