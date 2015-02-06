using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Acr.BarCodes;


namespace Samples.iOS {

    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            Forms.Init();
            BarCodes.Init();
            this.LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
