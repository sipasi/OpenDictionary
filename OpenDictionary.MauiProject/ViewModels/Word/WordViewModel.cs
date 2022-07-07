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
using OpenDictionary.Services.Messages.Alerts;
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
    private readonly IAlertMessageService alert;

    public string Id
    {
        get => id;
        set
        {
            SetProperty(ref id, value);

            LoadWordDataCommand.ExecuteAsync(default);
        }
    }

    public WordViewModel(IStorage<Word> wordStorage, INavigationService navigation, IAlertMessageService alert)
    {
        this.wordStorage = wordStorage;
        this.navigation = navigation;
        this.alert = alert;

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

            Word loaded = await wordStorage.Query().GetById(guid);

            word.Origin = loaded!.Origin;
            word.Translation = loaded!.Translation;

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
        string title = GetType().Name;
        string message = $"{exception.Message}. Inner: {exception.InnerException?.Message ?? "Non"}";
        string cancel = "Close";

        return alert.Show(title, message, cancel);
    }
}