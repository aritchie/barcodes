using System;

#if __ANDROID__
using Android.App;
using Android.Views;
#elif __UNIFIED__
using UIKit;
#elif WINDOWS_PHONE
using System.Windows;
#endif


namespace Acr.BarCodes {

    public static class BarCodes {

#if __ANDROID__
        public static Func<View> CustomOverlayFactory { get; set; }
#elif __UNIFIED__
        public static Func<UIView> CustomOverlayFactory { get; set; }
#elif WINDOWS_PHONE
        public static Func<UIElement> CustomOverlayFactory { get; set; }
#endif


        static readonly Lazy<IBarCodes> instance = new Lazy<IBarCodes>(() => {
#if PCL
            throw new ArgumentException("This PCL library, not the platform library.  Did you include the nuget package in your project?");
#else
            return new BarCodesImpl();
#endif
        });


        static IBarCodes customInstance;
        public static IBarCodes Instance {
            get { return customInstance ?? instance.Value; }
            set { customInstance = value; }
        }
    }
}
