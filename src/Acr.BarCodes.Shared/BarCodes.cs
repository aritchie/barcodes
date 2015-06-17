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
        public static void Init(Func<Activity> getActivity) {
            Instance = new BarCodesImpl(getActivity);
        }


        public static Func<View> CustomOverlayFactory { get; set; }

#elif __UNIFIED__

        public static Func<UIView> CustomOverlayFactory { get; set; }


        public static void Init() {
			Instance = new BarCodesImpl();
        }

#elif WINDOWS_PHONE
        public static Func<UIElement> CustomOverlayFactory { get; set; }


        public static void Init() {
			Instance = new BarCodesImpl();
        }

#else
        [Obsolete("You must call the Init() method from the platform project, not this PCL version")]
        public static void Init() {
			throw new ArgumentException("You must call the Init() method from the platform project, not this PCL version");
        }
#endif


        public static IBarCodes Instance { get; set; }
    }
}
