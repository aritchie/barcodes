using System;
#if __ANDROID__
using Android.App;
#endif

namespace Acr.BarCodes {

    public static class BarCodes {

#if __ANDROID__
        public static void Init(Func<Activity> getActivity) {
            if (Instance == null)
                Instance = new BarCodesImpl(getActivity);
        }


        public static void Init(Activity activity) {
            if (Instance != null)
                return;

            var app = Application.Context.ApplicationContext as Application;
            if (app == null)
                throw new Exception("Application Context is not an application");

            ActivityMonitor.CurrentTopActivity = activity;
            app.RegisterActivityLifecycleCallbacks(new ActivityMonitor());
            Instance = new BarCodesImpl(() => ActivityMonitor.CurrentTopActivity);
        }
#else
        public static void Init() {
#if __PLATFORM__
            if (Instance == null)
                Instance = new BarCodesImpl();
#endif
        }
#endif


        public static IBarCodes Instance { get; set; }
    }
}
