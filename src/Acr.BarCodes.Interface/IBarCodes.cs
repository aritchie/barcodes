using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


namespace Acr.BarCodes {

	public interface IBarCodes {

        void ContinuousScan(Action<BarCodeResult> onScan, BarCodeReadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken));
		Task<BarCodeResult> Scan(BarCodeReadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken));
		Stream Create(BarCodeCreateConfiguration config);
	}
}

