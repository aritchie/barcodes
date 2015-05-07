@echo off
del *.nupkg
nuget pack Acr.BarCodes.nuspec
nuget pack Acr.MvvmCross.Plugins.BarCodeScanner.nuspec
pause