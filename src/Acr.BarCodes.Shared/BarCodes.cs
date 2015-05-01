using System;
#if __ANDROID__
using Android.App;
#endif


namespace Acr.BarCodes {

    public static class BarCodes {

        private static readonly Lazy<IBarCodes> instanceInit = new Lazy<IBarCodes>(() => {
#if __ANDROID__
            if (getActivity == null)
                throw new ArgumentException("Android requires that you pass an activity factory function to Init() from your main activity");
            return new BarCodesImpl(getActivity);
#elif __PLATFORM__
            return new BarCodesImpl();
#else
            throw new ArgumentException("No platform implementation found.  Did you install this package into your application project?");
#endif
        }, false);

#if __ANDROID__
        private static Func<Activity> getActivity;
        public static void Init(Func<Activity> activityFactory) {
            getActivity = activityFactory;
        }

#endif


        private static IBarCodes customInstance;
        public static IBarCodes Instance {
            get { return customInstance ?? instanceInit.Value; }
            set { customInstance = value; }
        }
    }
}
