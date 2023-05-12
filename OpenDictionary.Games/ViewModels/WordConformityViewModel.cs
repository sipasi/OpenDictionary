using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

using Framework.States;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Games.WordConformities.Observables;
using OpenDictionary.Games.WordConformities.States;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public abstract partial class WordConformityViewModel : ObservableObject
{
    private string id;

    private readonly IDatabaseConnection<DbContext> connection;

    private readonly IStateMachine<ConformityState> machine;

    private readonly GameEvents events;

    public string Id
    {
        get => id;
        set
        {
            SetProperty(ref id, value);

            Load();

            machine.Fire(ConformityState.GameStart);
        }
    }

    public List<Word> Words { get; }
    public GamePropertiesObservable Properties { get; }
    public AnswerButtonCollection Buttons { get; }

    public WordConformityViewModel(IDatabaseConnection<DbContext> connection, IDialogMessageService dialog, INavigationService navigation)
    {
        this.id = string.Empty;

        this.connection = connection;

        Buttons = new(OnTapped);

        Words = new();

        Properties = new(Words);

        events = new();

        UiUpdater ui = new(Words, Properties, Buttons, GetQuestionText, GetButtonText);

        StateMachineCreator creator = new();

        machine = creator.Create(Properties, events, ui, dialog, navigation);
    }

    private async void Load()
    {
        Guid guid = Guid.Parse(id);

        WordGroup group = connection.Open(context => context.Set<WordGroup>()
            .Where(x => x.Id == guid)
            .Select(entity => new WordGroup { Name = entity.Name, Words = entity.Words })
            .First()
        );

        Word[] array = group.Words.ToArray();

        array.Randomize();

        Words.AddRange(array);

        OnPropertyChanged(nameof(Words));

        Properties.GroupName = group.Name;
        Properties.Total = group.Words.Count;
    }

    private Task OnTapped(AnswerButtonObservable? info)
    {
        if (info is null)
        {
            return Task.CompletedTask;
        }

        events.InvokeAnswered(info);

        return Task.CompletedTask;
    }

    protected abstract string GetQuestionText(Word word);
    protected abstract string GetButtonText(Word word);
}