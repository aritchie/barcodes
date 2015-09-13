using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


namespace Acr.BarCodes {

	public interface IBarCodes {

        void Cancel();
        void ContinuousScan(Action<BarCodeResult> onScan, BarCodeReadConfiguration config = null, CancellationToken? cancelToken = null);
		Task<BarCodeResult> Scan(BarCodeReadConfiguration config = null, CancellationToken? cancelToken = null);
		Stream Create(BarCodeCreateConfiguration config);
	}
}

