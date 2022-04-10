
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectWord.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardTestContentView : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(
            "CardTitle",                  // the name of the bindable property
            typeof(string),                 // the bindable property type
            typeof(CardTestContentView),    // the parent object type
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: Change,
            propertyChanging: Changing);                  // the default value for the property

        private static void Changing(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private static void Change(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public string CardTitle
        {
            get => (string)GetValue(CardTestContentView.CardTitleProperty);
            set => SetValue(CardTestContentView.CardTitleProperty, value);
        }

        public CardTestContentView()
        {
            InitializeComponent();
        }
    }
}