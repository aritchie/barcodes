using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Android.Gms.Common;
using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;


namespace Acr.BarCodes {

    public class BarCodesImpl : IBarCodes {


        public Stream Create(BarCodeCreateConfiguration config) {
            return null;
        }


        public void Cancel() {
            // TODO: cancel any active tasks?
            if (this.activity == null)
                return;

            this.activity.Finish();
            this.activity = null;
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


        ScannerActivity activity;
        void StartScan(BarCodeReadConfiguration config, Action<BarCodeResult> onScan) {
            if (this.activity != null)
                throw new ArgumentException("Scanner is already active");

            //new Tracker<Barcode>();
            //new BarcodeDetector();
            var scanner = new BarcodeDetector.Builder(null)
                .SetBarcodeFormats(BarcodeFormat.Pdf417)
                .Build();
        }
    }
}