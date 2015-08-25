using System;
using System.IO;
using Acr.Support.iOS;
using ZXing.Mobile;
using UIKit;


namespace Acr.BarCodes {

    public class BarCodesImpl : AbstractBarCodesImpl {

        protected override Stream ToImageStream(ZXing.Mobile.BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
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
			var controller = UIApplication.SharedApplication.GetTopViewController();
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
    }
}