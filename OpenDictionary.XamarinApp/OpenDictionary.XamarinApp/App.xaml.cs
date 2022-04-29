
using OpenDictionary.DependencyInjection;

using Xamarin.Forms;

namespace OpenDictionary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DiContainer.Init();

            MainPage = new AppShell();
        }



        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}