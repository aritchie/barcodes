using System;
using Xamarin.Forms;
using Acr.BarCodes;


namespace Samples.Pages {

    public class CreatePage : ContentPage {

        public CreatePage() {
			var btnCreate = new Button { Text = "Create" };
			var imgCode = new Image();
			var txtBarcode = new EntryCell { Label = "Bar Code" };

			btnCreate.Clicked += (sender, e) =>
				imgCode.Source = ImageSource.FromStream(() => BarCodes.Instance.Create(new BarCodeCreateConfiguration {
					Format = BarCodeFormat.QR_CODE,
					BarCode = txtBarcode.Text.Trim(),
					Width = 200,
					Height = 200
				}
			));

            this.Content = new StackLayout {
                Children = {
					btnCreate,
					imgCode,
					new TableView(new TableRoot {
						new TableSection {
							txtBarcode
						}
					})
                }
            };
        }
    }
}
