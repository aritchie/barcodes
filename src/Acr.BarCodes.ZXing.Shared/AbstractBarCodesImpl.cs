using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;


namespace Acr.BarCodes {

    public abstract class AbstractBarCodesImpl : IBarCodes {

        protected AbstractBarCodesImpl() {
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
            var writer = new ZXing.Mobile.BarcodeWriter {
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


        public void ContinuousScan(Action<BarCodeResult> onScan, BarCodeReadConfiguration config, CancellationToken cancelToken) {
            var scanner = this.BuildScanner(config, cancelToken);
            var zxing = this.GetZXingConfig(config);
            scanner.ScanContinuously(zxing, x => {
                var result = this.FromZXing(x);
                onScan(result);
            });
        }


        public async Task<BarCodeResult> Scan(BarCodeReadConfiguration config, CancellationToken cancelToken) {
            var scanner = this.BuildScanner(config, cancelToken);
            var result = await scanner.Scan(this.GetZXingConfig(config));
            return this.FromZXing(result);
        }



        protected virtual BarCodeResult FromZXing(Result result) {
            var format = this.FromZXingFormat(result.BarcodeFormat);
            return new BarCodeResult(result.Text, format);
        }


        protected virtual MobileBarcodeScanner BuildScanner(BarCodeReadConfiguration config, CancellationToken cancelToken) {
			config = config ?? BarCodeReadConfiguration.Default;
            var scanner = this.GetInstance();
			cancelToken.Register(scanner.Cancel);
            scanner.CameraUnsupportedMessage = config.CameraUnsupportedMessage ?? scanner.CameraUnsupportedMessage;
            scanner.BottomText = config.BottomText ?? scanner.BottomText;
            scanner.CancelButtonText = config.CancelText ?? scanner.CancelButtonText;
            scanner.FlashButtonText = config.FlashlightText ?? scanner.FlashButtonText;
            scanner.TopText = config.TopText ?? scanner.TopText;
            return scanner;
        }


        protected virtual BarCodeFormat FromZXingFormat(ZXing.BarcodeFormat format) {
            return (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), format.ToString());
        }


		protected virtual MobileBarcodeScanningOptions GetZXingConfig(BarCodeReadConfiguration cfg) {
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

            if (cfg.Formats != null && cfg.Formats.Any()) {
                opts.PossibleFormats = cfg.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            return opts;
        }


        protected abstract MobileBarcodeScanner GetInstance();
        protected abstract Stream ToImageStream(ZXing.Mobile.BarcodeWriter writer, BarCodeCreateConfiguration cfg);
    }
}