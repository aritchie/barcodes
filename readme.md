#THIS LIBRARY IS NOW DEPRECATED.  THIS WAS NOTHING MORE THAN A PCL BRIDGE FOR ZXING.NET.MOBILE.  ZXING IS NOW PCL OUT OF THE BOX!



#Xplat Barcode Scanning for Xamarin and Windows

A cross platform barcode scanning and creating library built on top of ZXing.Net.Mobile.  

Include the nuget package in all of your mobile projects and your core/PCL library (https://www.nuget.org/packages/Acr.BarCodes/)
To initialize, in your xplat platform (NOT YOUR PCL)

    iOS AppDelegate: BarCodes.Init()
    Android: BarCodes.Init(Activity); or BarCodes.Init(() => you supply activity);
    WP8: BarCodes.Init();

To use in your project, simply use:

    var result = await Barcodes.Instance.Read();
    if (result.Success) {
        var msg = String.Format("Barcode Found.  Type: {0} - Code: {1}", result.Format, result.Code);
        .. display message
    }


Additional Samples: 

Scan: https://github.com/aritchie/barcodes/blob/master/src/Samples/Samples/Pages/MainPage.cs
Create: https://github.com/aritchie/barcodes/blob/master/src/Samples/Samples/Pages/CreatePage.cs
