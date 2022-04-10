using System;

using ProjectWord.Views.Controls.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectWord.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardContentView : StackLayout
    {
        public static readonly BindableProperty TitleProperty =
            BindableHelper.CreateString<CardContentView>(nameof(Title), OnTitlePropertyChanged);
        public static readonly BindableProperty CaptionProperty =
            BindableHelper.CreateString<CardContentView>(nameof(Caption), OnCaptionPropertyChanged);
        public static readonly BindableProperty ImageSourceProperty =
            BindableHelper.CreateString<CardContentView>(nameof(ImageSource), OnSourcePropertyChanged);

        public static readonly BindableProperty ImageHeightRequestProperty =
            BindableHelper.Create<int, CardContentView>(nameof(ImageHeightRequest), OnImageHeightRequestPropertyChanged);

        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }
        public string Caption { get => GetValue(CaptionProperty) as string; set => SetValue(CaptionProperty, value); }
        public string ImageSource { get => GetValue(ImageSourceProperty) as string; set => SetValue(ImageSourceProperty, value); }

        public int ImageHeightRequest { get => (int)GetValue(ImageHeightRequestProperty); set => SetValue(ImageHeightRequestProperty, value); }

        public CardContentView()
        {
            InitializeComponent();
        }

        private static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var card = bindable as CardContentView;

            card.title.Text = newValue as string;
        }
        private static void OnCaptionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var card = bindable as CardContentView;

            card.caption.Text = newValue as string;
        }
        private static void OnSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var card = bindable as CardContentView;

            string path = newValue as string;

            card.sourse.Uri = new Uri(path);
        }
        private static void OnImageHeightRequestPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var card = bindable as CardContentView;

            card.image.HeightRequest = (int)newValue;
        }
    }
}