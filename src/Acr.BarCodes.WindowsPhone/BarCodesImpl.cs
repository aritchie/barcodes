using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Mobile;


namespace Acr.BarCodes {

    public class BarCodesImpl : AbstractBarCodesImpl {

        protected override Stream ToImageStream(ZXing.BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
            var ms = new MemoryStream();
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                var bytes = writer.Write(cfg.BarCode);
                bitmap.SaveJpeg(ms, cfg.Width, cfg.Height, 0, 100);
                ms.Seek(0, SeekOrigin.Begin);
            });

            return ms;
        }


        protected override MobileBarcodeScanner GetInstance() {
            var scanner = new MobileBarcodeScanner(Deployment.Current.Dispatcher);
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
