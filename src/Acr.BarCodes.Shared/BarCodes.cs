using System;


namespace Acr.BarCodes {

    public static class BarCodes {
        private static bool init;


#if __ANDROID__
        public static void Init(Android.App.Activity activity) {
            if (init)
                return;

            init = true;
            var app = Android.App.Application.Context.ApplicationContext as Android.App.Application;
            if (app == null)
                throw new Exception("Application Context is not an application");

            ActivityMonitor.CurrentTopActivity = activity;
            app.RegisterActivityLifecycleCallbacks(new ActivityMonitor());
            Instance = new BarCodesImpl();
        }
#else
        public static void Init() {
            init = true;
#if __PLATFORM__
            Instance = new BarCodesImpl();
#endif
        }
#endif


        public static IBarCodes Instance { get; set; }
    }
}
