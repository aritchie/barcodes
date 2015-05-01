using System;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.Mobile;
using UIKit;


namespace Acr.BarCodes {

    public class BarCodesImpl : AbstractBarCodesImpl {

        protected override Stream ToImageStream(BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
			return (cfg.ImageType == ImageType.Png)
				? writer.Write(cfg.BarCode).AsPNG().AsStream()
				: writer.Write(cfg.BarCode).AsJPEG().AsStream();
        }


        protected override MobileBarcodeScanner GetInstance() {
			var controller = this.GetTopViewController();
			return new MobileBarcodeScanner(controller) { UseCustomOverlay = false };
        }


		protected virtual UIWindow GetTopWindow() {
			return UIApplication.SharedApplication
				.Windows
				.Reverse()
				.FirstOrDefault(x =>
					x.WindowLevel == UIWindowLevel.Normal &&
					!x.Hidden
				);
		}


		protected virtual UIViewController GetTopViewController() {
			var root = this.GetTopWindow().RootViewController;
			var tabs = root as UITabBarController;
			if (tabs != null)
				return tabs.PresentedViewController ?? tabs.SelectedViewController;

			var nav = root as UINavigationController;
			if (nav != null)
				return nav.VisibleViewController;

			if (root.PresentedViewController != null)
				return root.PresentedViewController;

			return root;
		}
    }
}