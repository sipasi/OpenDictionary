
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectWord.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupContentView : CollectionView
    {
        public WordGroupContentView()
        {
            InitializeComponent();
        }
    }
}