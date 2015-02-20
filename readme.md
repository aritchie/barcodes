#Xplat Barcode Scanning for Xamarin and Windows

A cross platform barcode scanning and creating library built on top of ZXing.Net.Mobile.  

To initialize, in your xplat platform (NOT YOUR PCL)

    iOS AppDelegate: BarCodes.Init()
    Android: BarCodes.Init(Activity); or BarCodes.Init(() => you supply activity);
    WP8: BarCodes.Init();