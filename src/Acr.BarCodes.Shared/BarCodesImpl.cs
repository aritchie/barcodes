#if __PLATFORM__
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;
#if __ANDROID__
using Java.Nio;
using Android.App;
using Android.Runtime;
using Android.Graphics;
#elif __IOS__
using UIKit;
#elif WINDOWS_PHONE
using System.Windows.Media.Imaging;
#endif


namespace Acr.BarCodes {

    public class BarCodesImpl : IBarCodes {

#if __ANDROID__
        private readonly Func<Activity> getTopActivity;


		public BarCodesImpl(Func<Activity> getTopActivity) {
            this.getTopActivity = getTopActivity;
#else
        public BarCodesImpl() {
#endif
            var def = MobileBarcodeScanningOptions.Default;

			BarCodeReadConfiguration.Default = new BarCodeReadConfiguration {
				AutoRotate = def.AutoRotate,
				CharacterSet = def.CharacterSet,
				DelayBetweenAnalyzingFrames = def.DelayBetweenAnalyzingFrames,
				InitialDelayBeforeAnalyzingFrames = def.InitialDelayBeforeAnalyzingFrames,
				PureBarcode = def.PureBarcode,
				TryHarder = def.TryHarder,
				TryInverted = def.TryInverted,
				UseFrontCameraIfAvailable = def.UseFrontCameraIfAvailable
            };
        }


		public virtual Stream Create(BarCodeCreateConfiguration cfg) {
            var writer = new BarcodeWriter {
				Format = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), cfg.Format.ToString()),
                Encoder = new MultiFormatWriter(),
                Options = new EncodingOptions {
					Height = cfg.Height,
					Margin = cfg.Margin,
					Width = cfg.Height,
					PureBarcode = cfg.PureBarcode
                }
            };
            return this.ToImageStream(writer, cfg);
        }


		public async Task<BarCodeResult> Read(BarCodeReadConfiguration config, CancellationToken cancelToken) {
			config = config ?? BarCodeReadConfiguration.Default;
            var scanner = this.GetInstance();
			cancelToken.Register(scanner.Cancel);

            var result = await scanner.Scan(this.GetXingConfig(config));
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, FromXingFormat(result.BarcodeFormat))
            );
        }


        private static BarCodeFormat FromXingFormat(ZXing.BarcodeFormat format) {
            return (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), format.ToString());
        }


		private MobileBarcodeScanningOptions GetXingConfig(BarCodeReadConfiguration cfg) {
            var opts = new MobileBarcodeScanningOptions {
                AutoRotate = cfg.AutoRotate,
                CharacterSet = cfg.CharacterSet,
                DelayBetweenAnalyzingFrames = cfg.DelayBetweenAnalyzingFrames,
                InitialDelayBeforeAnalyzingFrames = cfg.InitialDelayBeforeAnalyzingFrames,
                PureBarcode = cfg.PureBarcode,
                TryHarder = cfg.TryHarder,
                TryInverted = cfg.TryInverted,
                UseFrontCameraIfAvailable = cfg.UseFrontCameraIfAvailable
            };

            if (cfg.Formats != null && cfg.Formats.Count > 0) {
                opts.PossibleFormats = cfg.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            return opts;
        }


#if __ANDROID__
        protected virtual MobileBarcodeScanner GetInstance() {
            return new MobileBarcodeScanner(this.getTopActivity());
        }


        protected virtual Stream ToImageStream(BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
			var stream = new MemoryStream();

			var cf = cfg.ImageType == ImageType.Png
				? Bitmap.CompressFormat.Png
				: Bitmap.CompressFormat.Jpeg;

			using (var bitmap = writer.Write(cfg.BarCode))
				bitmap.Compress(cf, 0, stream);

			stream.Position = 0;
			return stream;
        }
#endif

#if WINDOWS_PHONE
        protected virtual Stream ToImageStream(BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
            return new MemoryStream(writer.Write(cfg.BarCode).ToByteArray());
        }


        protected virtual MobileBarcodeScanner GetInstance() {
            return new MobileBarcodeScanner(System.Windows.Deployment.Current.Dispatcher) { UseCustomOverlay = false };

        }
#endif

#if __IOS__

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


        protected virtual Stream ToImageStream(BarcodeWriter writer, BarCodeCreateConfiguration cfg) {
			return (cfg.ImageType == ImageType.Png)
				? writer.Write(cfg.BarCode).AsPNG().AsStream()
				: writer.Write(cfg.BarCode).AsJPEG().AsStream();
        }


        protected virtual MobileBarcodeScanner GetInstance() {
			var controller = this.GetTopViewController();
			return new MobileBarcodeScanner(controller) { UseCustomOverlay = false };
        }
#endif
    }
}
#endif