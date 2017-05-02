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


        //public void CheckScannerAvailability() {
        //    var frontCamera = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);
        //    var rearCamera = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear);
        //    if (!frontCamera && !rearCamera)
        //        throw;

        //    return await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
        //}


        protected override MobileBarcodeScanner GetInstance() {
			var controller = this.GetTopViewController();
			var scanner = new MobileBarcodeScanner(controller);

            if (BarCodes.CustomOverlayFactory != null) {
                var overlay = BarCodes.CustomOverlayFactory();
                if (overlay != null) {
                    scanner.UseCustomOverlay = true;
                    scanner.CustomOverlay = overlay;
                }
            }
            return scanner;
        }


		protected virtual UIViewController GetTopViewController() {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;

            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            return vc;
		}
    }
}