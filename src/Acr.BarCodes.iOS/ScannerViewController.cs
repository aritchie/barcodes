using System;
using System.Collections.Generic;
using System.Linq;
using AVFoundation;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using UIKit;


namespace Acr.BarCodes {

    public class ScannerViewController : UIViewController {
        readonly AVCaptureDevice device;
        readonly AVCaptureSession session;


        // TODO: pass event when closing and item scanned
        // TODO: pass event for calling for light
        public ScannerViewController(BarCodeReadConfiguration config, Action<BarCodeResult> onScan) {
            this.device = AVCaptureDevice.Devices.FirstOrDefault(x => x.HasMediaType(AVMediaType.Video));
            this.session = new AVCaptureSession {
                SessionPreset = AVCaptureSession.Preset640x480
            };

            // TODO: need top/bottom text, no camera text,
            // TODO: cancel and flash text
   //         config = config ?? BarCodeReadConfiguration.Default;

			//var captureDevice = AVCaptureDevice
   //             .DevicesWithMediaType(AVMediaType.Video)
   //             .FirstOrDefault(x => x.Position == AVCaptureDevicePosition.Back);

   //         var session = new AVCaptureSession {
			//	SessionPreset = AVCaptureSession.Preset640x480
			//};
   //         var metadata = new AVCaptureMetadataOutput();
   //         metadata.MetadataObjectTypes = AVMetadataObjectType.Code128Code;

   //         metadata.SetDelegate(new BarCodeCaptureDelegate(onScan), DispatchQueue.MainQueue);
   //         session.AddOutput(metadata);

        }


        public override void ViewDidLoad() {
            base.ViewDidLoad();
            if (this.device == null)
                return;

            NSError error;
            var input = new AVCaptureDeviceInput(this.device, out error);
            this.session.AddInput(input);

            var output = new AVCaptureMetadataOutput();

            // TODO: configure codes
            this.session.AddOutput(output);

            var preview = new AVCaptureVideoPreviewLayer(this.session) {
                VideoGravity = AVLayerVideoGravity.ResizeAspectFill,
                Frame = new CGRect(0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height)
            };
            this.View.Layer.InsertSublayer(preview, 0);
        }


        public override void ViewWillAppear(bool animated) {
            base.ViewWillAppear(animated);

            if (this.device == null)
                new UIAlertView().Show(); // TODO
        }


        void OnCapture(IEnumerable<AVMetadataObject> metaDataObjects) {
			var result = metaDataObjects.FirstOrDefault() as AVMetadataMachineReadableCodeObject;
            if (result == null)
                return;

            var format = result.Type.ToString();
            var code = result.StringValue;
            // TODO: call callback
        }


//         (NSUInteger)supportedInterfaceOrientations;
//{
//    return UIInterfaceOrientationMaskLandscape;
//}

//- (BOOL)shouldAutorotate;
//{
//    return (UIDeviceOrientationIsLandscape([[UIDevice currentDevice] orientation]));
//}

//- (void)didRotateFromInterfaceOrientation:(UIInterfaceOrientation)fromInterfaceOrientation;
//{
//    if([[UIDevice currentDevice] orientation] == UIDeviceOrientationLandscapeLeft) {
//        AVCaptureConnection *con = self.preview.connection;
//        con.videoOrientation = AVCaptureVideoOrientationLandscapeRight;
//    } else {
//        AVCaptureConnection *con = self.preview.connection;
//        con.videoOrientation = AVCaptureVideoOrientationLandscapeLeft;
//    }
//}

    }
}
