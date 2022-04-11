using ProjectWord.Services.Configurators;

using Xamarin.Forms;

namespace ProjectWord
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DatabaseConfigurator.Configure();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
