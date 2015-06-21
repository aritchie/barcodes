@echo off
del *.nupkg
nuget pack Acr.BarCodes.nuspec
rem nuget pack Acr.MvvmCross.Plugins.BarCodeScanner.nuspec
pause