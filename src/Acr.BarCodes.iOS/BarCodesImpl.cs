using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Acr.Support.iOS;
using UIKit;


namespace Acr.BarCodes {

    public class BarCodesImpl : IBarCodes {

        public Stream Create(BarCodeCreateConfiguration cfg) {
    //        var writer = new BarcodeWriter {
				//Format = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), cfg.Format.ToString()),
    //            Encoder = new MultiFormatWriter(),
    //            Options = new EncodingOptions {
				//	Height = cfg.Height,
				//	Margin = cfg.Margin,
				//	Width = cfg.Height,
				//	PureBarcode = cfg.PureBarcode
    //            }
    //        };
    //        return this.ToImageStream(writer, cfg);
            return null;
        }


        public void Cancel() {
            // TODO: cancel any active tasks?
            if (this.scanController == null)
                return;

            this.scanController.DismissViewController(true, null);
            this.scanController.Dispose();
            this.scanController = null;
        }


        public void ContinuousScan(Action<BarCodeResult> onScan, BarCodeReadConfiguration config, CancellationToken? cancelToken) {
            cancelToken?.Register(this.Cancel);
            this.StartScan(config, onScan);
        }


        public Task<BarCodeResult> Scan(BarCodeReadConfiguration config, CancellationToken? cancelToken) {
            var tcs = new TaskCompletionSource<BarCodeResult>();
            cancelToken?.Register(() => tcs.TrySetResult(null));
            this.StartScan(config, x => {
                this.Cancel();
                tcs.TrySetResult(x);
            });
            return tcs.Task;
        }


        ScannerViewController scanController;
        void StartScan(BarCodeReadConfiguration config, Action<BarCodeResult> onScan) {
            if (this.scanController != null)
                throw new ArgumentException("There is already a scanner active");

            this.scanController = new ScannerViewController(config, onScan);
            UIApplication.SharedApplication.GetTopViewController().PresentViewController(this.scanController, true, null);
        }
    }
}


/*   self.device = [AVCaptureDevice defaultDeviceWithMediaType:AVMediaTypeVideo];

    self.input = [AVCaptureDeviceInput deviceInputWithDevice:self.device error:nil];

    self.session = [[AVCaptureSession alloc] init];

    self.output = [[AVCaptureMetadataOutput alloc] init];
    [self.session addOutput:self.output];
    [self.session addInput:self.input];

    [self.output setMetadataObjectsDelegate:self queue:dispatch_get_main_queue()];
    self.output.metadataObjectTypes = @[AVMetadataObjectTypeQRCode];

    self.preview = [AVCaptureVideoPreviewLayer layerWithSession:self.session];
    self.preview.videoGravity = AVLayerVideoGravityResizeAspectFill;
    self.preview.frame = CGRectMake(0, 0, self.view.frame.size.width, self.view.frame.size.height);

    AVCaptureConnection *con = self.preview.connection;

    con.videoOrientation = AVCaptureVideoOrientationLandscapeLeft;

    [self.view.layer insertSublayer:self.preview atIndex:0];
*/