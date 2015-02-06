#Xplat Barcode Scanning for Xamarin and Windows

To initialize, in your xplat platform (NOT YOUR PCL)

    iOS AppDelegate: BarCodes.Init()
    Android 4: BarCodes.Init(Activity);
    Android 2.3+: BarCodes.Init(() => you supply activity);
