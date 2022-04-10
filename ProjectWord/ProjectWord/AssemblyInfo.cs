using ProjectWord.Fonts.Icons;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont(fontFileName: "Font Awesome 6 Brands-Regular-400.otf", Alias = nameof(FontAwesomeBrands))]
[assembly: ExportFont(fontFileName: "Font Awesome 6 Regular-400.otf", Alias = nameof(FontAwesomeRegular))]
[assembly: ExportFont(fontFileName: "Font Awesome 6 Solid-900.otf", Alias = nameof(FontAwesomeSolid))]
[assembly: ExportFont(fontFileName: "remixicon.ttf", Alias = nameof(RemixIcon))] 