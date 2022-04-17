using System;
using System.Linq;

using OpenDictionary.Collections.Storages;
using OpenDictionary.DependencyInjection;
using OpenDictionary.Models;
using OpenDictionary.Observables.Games;
using OpenDictionary.ViewModels.Games;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordConformityControl : ContentView
    {
        public static readonly BindableProperty WordConformityProperty = BindableProperty.Create(
            nameof(WordConformity),
            typeof(WordConformityViewModel),
            typeof(WordConformityViewModel),
            propertyChanged: WordConformityChanged);

        public WordConformityViewModel WordConformity { get; set; }

        public WordConformityControl()
        {
            InitializeComponent();
        }

        private static void WordConformityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as WordConformityControl;

            control.WordConformity = newValue as WordConformityViewModel;

            if (control.WordConformity is null)
            {
                return;
            }

            control.Load();
        }

        private void Load()
        {
            var storage = DiContainer.Get<IStorage<WordGroup>>();

            Guid id = storage.Query().First().Id;

            BindButtons();

            WordConformity.Id = id.ToString();
        }

        private void BindButtons()
        {
            var buttons = answerButtons.Children;

            foreach (Button button in buttons)
            {
                AnswerButtonObservable answer = new AnswerButtonObservable();

                BindButton(button, answer);

                WordConformity.Buttons.Collection.Add(answer);
            }
        }
        private void BindButton(Button button, AnswerButtonObservable answer)
        {
            Binding binding = new Binding(path: nameof(AnswerButtonObservable.Text));

            button.SetBinding(Button.TextProperty, binding);

            button.BindingContext = answer;
            button.Command = WordConformity.Buttons.TappedCommand;
            button.CommandParameter = answer;
        }
    }
}