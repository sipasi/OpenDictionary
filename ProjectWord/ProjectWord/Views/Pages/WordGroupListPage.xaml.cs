
using System.Linq;

using OpenDictionary.Animations;
using OpenDictionary.Collections.Storages;
using OpenDictionary.DependencyInjection;
using OpenDictionary.Models;
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

            //DatabaseContext database = new DatabaseContext(DiContainer.Get<IDatabasePath>().Path);

            //database.Words.RemoveRange(database.Words.AsEnumerable());
            //database.SaveChanges();

            var groupStorage = DiContainer.Get<IStorage<WordGroup>>();
            var wordStorage = DiContainer.Get<IStorage<Word>>();
            var metadataStorage = DiContainer.Get<IStorage<WordMetadata>>();

            var c_1 = groupStorage.Query().Count();
            var c_2 = wordStorage.Query().Count();
            var c_3 = metadataStorage.Query().Count();
        }

        private async void AnimationView_Clicked(object sender, System.EventArgs e)
        {
            await animation.Shake();
        }
    }
}