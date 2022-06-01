using OpenDictionary.Animations;
using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupListPage : ContentPage
    {
        private readonly ShakingAnimation animation;

        private readonly WordGroupInfoList viewModel;

        public WordGroupListPage()
        {
            InitializeComponent();

            animation = new ShakingAnimation(animationView);

            BindingContext = viewModel = DiContainer.Get<WordGroupInfoList>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.Groups.Load();
        }

        private async void AnimationView_Clicked(object sender, System.EventArgs e)
        {
            await animation.Shake();
        }
    }
}