using System;
using System.Linq;
using System.Threading.Tasks;

using Framework.States;

using MvvmHelpers;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Tools;
using OpenDictionary.Games.WordConformities.Observables;
using OpenDictionary.Games.WordConformities.States;
using OpenDictionary.Models;
using OpenDictionary.ViewModels;


namespace OpenDictionary.Games.WordConformities.ViewModels
{
    public abstract class WordConformityViewModel : ViewModel
    {
        private string id;

        private readonly IStorage<WordGroup> storage;

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

        public ObservableRangeCollection<Word> Words { get; }
        public GamePropertiesObservable Properties { get; }
        public AnswerButtonCollection Buttons { get; }

        public WordConformityViewModel(IStorage<WordGroup> storage)
        {
            this.storage = storage;

            Buttons = new AnswerButtonCollection(OnTapped);

            Words = new ObservableRangeCollection<Word>();

            Properties = new GamePropertiesObservable();

            events = new GameEvents();

            var ui = new UiUpdater(Words, Properties, Buttons, GetQuestionText, GetButtonText);

            var creator = new StateMachineCreator();

            machine = creator.Create(Properties, events, ui);
        }

        private void Load()
        {
            Guid guid = Guid.Parse(id);

            WordGroup group = storage
                .Query()
                .Where(x => x.Id == guid)
                .Select(entity => new WordGroup { Name = entity.Name, Words = entity.Words })
                .First();

            Word[] array = group.Words.ToArray();

            ListTool.Randomize(array);

            Words.AddRange(array);

            Properties.Clear();

            Properties.GroupName = group.Name;
            Properties.Total = group.Words.Count;
        }

        private Task OnTapped(AnswerButtonObservable info)
        {
            events.InvokeAnswered(info);

            return Task.CompletedTask;
        }

        protected abstract string GetQuestionText(Word word);
        protected abstract string GetButtonText(Word word);
    }
}