#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.Observables.Words;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public partial class WordViewModel
{
    private string id;

    [ObservableProperty]
    private WordObservable word;
    [ObservableProperty]
    private WordMetadataObservable metadata;

    private readonly IStorage<Word> wordStorage;
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

    public WordViewModel(IStorage<Word> wordStorage, INavigationService navigation, IToastMessageService toast)
    {
        this.wordStorage = wordStorage;
        this.navigation = navigation;
        this.toast = toast;

        id = string.Empty;

        word = new();
        metadata = new();
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
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new NotSupportedException();
            }

            Guid guid = Guid.Parse(id);

            Word? loaded = await wordStorage
                .Query()
                .GetById(guid);

            if (loaded is null)
            {
                throw new Exception($"Cant load word by id: {id ?? "Empty"}");
            }

            word.Origin = loaded.Origin;
            word.Translation = loaded.Translation;

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