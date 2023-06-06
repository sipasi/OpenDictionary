#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.Observables.Words;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Words.ViewModels;

public partial class WordViewModel : ObservableObject
{
    private string id;

    private readonly IDatabaseConnection<DatabaseContext> connection;
    private readonly INavigationService navigation;
    private readonly IToastMessageService toast;

    public string Id
    {
        get => id;
        set
        {
            SetProperty(ref id, value);

            LoadWordDataCommand.ExecuteAsync(default);
        }
    }

    public WordObservable Word { get; }
    public WordMetadataObservable Metadata { get; }

    public WordViewModel(IDatabaseConnection<DatabaseContext> connection, INavigationService navigation, IToastMessageService toast)
    {
        this.connection = connection;
        this.navigation = navigation;
        this.toast = toast;

        id = string.Empty;

        Word = new();
        Metadata = new();
    }

    protected virtual Task OnWordLoaded()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task LoadWordData()
    {
        try
        {
            Word.Origin = null!;
            Word.Translation = null!;

            Metadata.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new NotSupportedException();
            }

            Guid guid = Guid.Parse(id);

            Word? loaded = await connection.Open(context => context.Set<Word>().GetById(guid));

            if (loaded is null)
            {
                throw new Exception($"Cant load word by id: {id ?? "Empty"}");
            }

            Word.Origin = loaded.Origin;
            Word.Translation = loaded.Translation;

            await OnWordLoaded();
        }
        catch (Exception e)
        {
            await ErrorMessage(e);

            await navigation.GoBackAsync();
        }
    }

    protected Task ErrorMessage(Exception exception)
    {
        string name = GetType().Name;
        string message = $"ViewModel[{name}]: {exception.Message}. Inner: {exception.InnerException?.Message ?? "Non"}";

        return toast.Show(message);
    }
}