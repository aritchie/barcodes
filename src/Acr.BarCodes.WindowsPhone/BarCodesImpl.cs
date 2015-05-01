using System;
using System.IO;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Mobile;


namespace Acr.BarCodes {

    public class BarCodesImpl : AbstractBarCodesImpl {

        protected override Stream ToImageStream(BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
            return new MemoryStream(writer.Write(cfg.BarCode).ToByteArray());
        }


        protected override MobileBarcodeScanner GetInstance() {
            return new MobileBarcodeScanner(System.Windows.Deployment.Current.Dispatcher) { UseCustomOverlay = false };

        }
    }
}
