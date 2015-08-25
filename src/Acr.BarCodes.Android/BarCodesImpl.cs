using System;
using System.IO;
using ZXing.Mobile;
using Android.Graphics;
using ZXing;


namespace Acr.BarCodes {

    public class BarCodesImpl : AbstractBarCodesImpl {

        protected override MobileBarcodeScanner GetInstance() {
            var scanner = new MobileBarcodeScanner();
            if (BarCodes.CustomOverlayFactory != null) {
                var overlay = BarCodes.CustomOverlayFactory();
                if (overlay != null) {
                    scanner.UseCustomOverlay = true;
                    scanner.CustomOverlay = overlay;
                }
            }
            return scanner;
        }


        protected override Stream ToImageStream(BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
			var stream = new MemoryStream();
			var cf = cfg.ImageType == ImageType.Png
				? Bitmap.CompressFormat.Png
				: Bitmap.CompressFormat.Jpeg;

			using (var bitmap = writer.Write(cfg.BarCode))
				bitmap.Compress(cf, 0, stream);

			stream.Position = 0;
			return stream;
        }
    }
}