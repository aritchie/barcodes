using System;
#if __ANDROID__
using Android.App;
#endif


namespace Acr.BarCodes {

    public static class BarCodes {

#if __ANDROID__
        public static void Init(Func<Activity> getActivity) {
            Instance = new BarCodesImpl(getActivity);
        }

#elif __PLATFORM__
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
