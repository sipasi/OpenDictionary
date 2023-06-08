#nullable enable

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.Observables.Words;
using OpenDictionary.Presentation.Shared;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Words.ViewModels;

public partial class WordViewModel : AsyncObservableObject
{
    private readonly IDatabaseConnection<DatabaseContext> connection;
    private readonly INavigationService navigation;
    private readonly IToastMessageService toast;

    public string? Id { get; set; }

    public string GroupName { get; private set; }

    public WordObservable Word { get; }
    public WordMetadataObservable Metadata { get; }

    public WordViewModel(IDatabaseConnection<DatabaseContext> connection, INavigationService navigation, IToastMessageService toast)
    {
        this.connection = connection;
        this.navigation = navigation;
        this.toast = toast;

        GroupName = "Details";

        Word = new();
        Metadata = new();
    }

    protected override async Task OnInitialize()
    {
        Word.Origin = null!;
        Word.Translation = null!;

        Metadata.Clear();

        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new NotSupportedException("Id can't be null");
        }

        if (await LoadWord(Id) is not WordInfo loaded)
        {
            throw new Exception($"Cant load word by id: {Id}");
        }

        GroupName = loaded.GroupName;

        Word.Origin = loaded.Origin;
        Word.Translation = loaded.Translation;

        await OnWordLoaded();
    }
    protected override void OnInitializationFailed(string message, string? inner)
    {
        Debug.WriteLine($"ViewModel[{GetType().Name}]: {message}. Inner: {inner ?? "Non"}");

        navigation.GoBackAsync().SafeFireAndForget();
    }

    private async ValueTask<WordInfo?> LoadWord(string id)
    {
        Guid guid = Guid.Parse(id);

        await using var context = connection.Open();

        var loaded = await context.Set<Word>().Select(static word => new WordInfo
        {
            Id = word.Id,
            Origin = word.Origin,
            Translation = word.Translation,
            GroupName = word.Group.Name,
        }).FirstOrDefaultAsync(x => x.Id == guid);

        return loaded;
    }

    protected virtual Task OnWordLoaded() => Task.CompletedTask;

    private readonly record struct WordInfo(Guid Id, string Origin, string Translation, string GroupName);
}