using System;
#if __ANDROID__
using Android.App;
#endif

namespace Acr.BarCodes {

    public static class BarCodes {

#if __ANDROID__

        public static void Init(Func<Activity> getActivity) {
			if (Instance != null)
				throw new Exception("You have already initialized barcodes");
            
			Instance = new BarCodesImpl(getActivity);
        }


        public static void Init(Activity activity) {
            if (Instance != null)
				throw new Exception("You have already initialized barcodes");

            var app = Application.Context.ApplicationContext as Application;
            if (app == null)
                throw new Exception("Application Context is not an application");

            ActivityMonitor.CurrentTopActivity = activity;
            app.RegisterActivityLifecycleCallbacks(new ActivityMonitor());
            Instance = new BarCodesImpl(() => ActivityMonitor.CurrentTopActivity);
        }

#elif __PLATFORM__

        public static void Init() {
            if (Instance != null)
				throw new Exception("You have already initialized barcodes");
            
			Instance = new BarCodesImpl();
        }

#else

		[Obsolete("This is the PCL version of Init.  You must call Init in your platform project!")]
		public static void Init() {
			throw new Exception("This is the PCL version of Init.  You must call Init in your platform project!");
		}
#endif


        public static IBarCodes Instance { get; set; }
    }
}
