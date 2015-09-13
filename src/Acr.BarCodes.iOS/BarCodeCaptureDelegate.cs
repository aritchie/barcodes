using System;
using System.Linq;
using AVFoundation;


namespace Acr.BarCodes {

    public class BarCodeCaptureDelegate : AVCaptureMetadataOutputObjectsDelegate {
        readonly Action<BarCodeResult> onCapture;


        public BarCodeCaptureDelegate(Action<BarCodeResult> onCapture) {
            this.onCapture = onCapture;
        }


        public override void DidOutputMetadataObjects(AVCaptureMetadataOutput captureOutput, AVMetadataObject[] metadataObjects, AVCaptureConnection connection) {
           var data = metadataObjects?.FirstOrDefault() as AVMetadataMachineReadableCodeObject;
            if (data == null)
                return;

            //data.StringValue;
            //data.Type;
        }
    }
}
