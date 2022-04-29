#nullable enable

using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    public class AppThemeObservable : ViewModel
    {
        private OSAppTheme current;

        public OSAppTheme Current
        {
            get => current;
            set
            {
                SetProperty(ref current, value);

                Application.Current.UserAppTheme = value;
            }
        }

        public Command<IViewContainer<View>> UpdateRadioButtonsCommand { get; }

        public AppThemeObservable()
        {
            Current = Application.Current.RequestedTheme;

            UpdateRadioButtonsCommand = new Command<IViewContainer<View>>(UpdateRadioButtonsGroup);
        }

        private void UpdateRadioButtonsGroup(IViewContainer<View> container)
        {
            var children = container.Children;

            foreach (var item in children)
            {
                var radioButton = item as RadioButton;

                if (radioButton is null)
                {
                    return;
                }

                OSAppTheme theme = (OSAppTheme)radioButton.Value;

                if (theme == Current)
                {
                    radioButton.IsChecked = true;

                    Application.Current.UserAppTheme = theme;

                    return;
                }
            }
        }
    }
}