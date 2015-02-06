using System;
using Acr.BarCodes;
using Xamarin.Forms;


namespace Samples.Pages {

    public class MainPage : ContentPage {


        public MainPage() {
            var btnScan = new Button {
                Text = "Scan BarCode"
            };
            btnScan.Clicked += async (sender, args) => {
                var result = await BarCodes.Instance.Read();
                if (!result.Success)
                    await this.DisplayAlert("Failed", "Failed to get barcode", "OK");

                else {
                    var msg = String.Format("Barcode: {0} - {1}", result.Format, result.Code);
                    await this.DisplayAlert("Success", msg, "OK");
                }
            };

            var btnCreate = new Button { Text = "Create Barcode" };
            btnCreate.Clicked += async (sender, args) => await this.Navigation.PushAsync(new CreatePage());

            this.Content = new StackLayout {
                Children = {
                    btnCreate,
                    btnScan
                }
            };
        }
    }
}
/*
        <Button
            local:MvxBind="Click Scan"
            android:text="Scan Barcode"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <TextView
            android:text="Top Text"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <EditText
            local:MvxBind="Text Scanner.Configuration.TopText"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <TextView
            android:text="Bottom Text"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <EditText
            local:MvxBind="Text Scanner.Configuration.BottomText"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <TextView
            android:text="Flashlight Text"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <EditText
            local:MvxBind="Text Scanner.Configuration.FlashlightText"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <TextView
            android:text="Cancel Text"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <EditText
            local:MvxBind="Text Scanner.Configuration.CancelText"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <TextView
            android:text="Auto Rotate"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <CheckBox
            local:MvxBind="Checked Scanner.Configuration.AutoRotate"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <TextView
            android:text="Use Front Camera"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <CheckBox
            local:MvxBind="Checked Scanner.Configuration.UseFrontCameraIfAvailable"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <TextView
            android:text="Format"
            android:layout_width="fill_parent"
            android:layout_height="wrap_content" />
        <Mvx.MvxSpinner
            android:layout_width="fill_parent"
            android:layout_height="wrap_content"
            local:MvxBind="ItemsSource Formats; SelectedItem SelectedFormat;" />*/