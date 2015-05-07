using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Acr.BarCodes.BarCodes.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
            Mvx.RegisterSingleton(Acr.BarCodes.BarCodes.Instance);
        }
    }
}