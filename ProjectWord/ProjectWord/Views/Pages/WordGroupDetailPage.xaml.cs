#nullable enable

using System.Linq;

using OpenDictionary.DependencyInjection;
using OpenDictionary.Models;
using OpenDictionary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupDetailPage : ContentPage
    {
        private readonly WordGroupDetailViewModel viewModel = default;

        public WordGroupDetailPage()
        {
            InitializeComponent();

            BindingContext = viewModel = DiContainer.Get<WordGroupDetailViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var tapped = viewModel.TappedWord;

            if (tapped is null) //  || viewModel.Words.Collection.Contains(lastSelected) is false
            {
                return;
            }

            var collection = viewModel.Words.Collection;

            var index = collection.IndexOf(word => word.Origin == tapped.Origin);

            if (index < 0)
            {
                return;
            }

            collectionView.ScrollTo(index, position: ScrollToPosition.Start, animate: false);
        }
    }
}