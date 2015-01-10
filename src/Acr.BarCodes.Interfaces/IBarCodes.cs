using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


namespace Acr.BarCodes {

	public interface IBarCodes {

		Task<BarCodeResult> Read(BarCodeReadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken));
		Stream Create(BarCodeCreateConfiguration config);
	}
}

