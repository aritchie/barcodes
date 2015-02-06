using System;
using Samples.Pages;
using Xamarin.Forms;


namespace Samples {

    public class App : Application {

        public App() {
            this.MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart() {}
        protected override void OnSleep() {}
        protected override void OnResume() {}
    }
}
