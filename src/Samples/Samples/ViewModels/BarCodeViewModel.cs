using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Acr.BarCodes;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class BarCodeViewModel : AbstractViewModel {

        public BarCodeViewModel() {
            var list = Enum
                .GetNames(typeof(BarCodeFormat))
                .ToList();
            list.Insert(0, "Any");
            this.Formats = list;
            this.SelectedFormat = "Any";
        }


        public ICommand Scan {
            get {
                return new Command(async () => {
                    var result = await BarCodes.Instance.Read();
                    if (result.Success) {
                    }
                    else {
                    }
                });
            }
        }


        public IList<string> Formats { get; private set; }


        private string selectedFormat;
        public string SelectedFormat {
            get { return this.selectedFormat; }
            set {
                if (this.selectedFormat == value)
                    return;

                this.selectedFormat = value;
				BarCodeReadConfiguration.Default.Formats.Clear();
                if (value != "Any") {
                    var format = (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), value);
					BarCodeReadConfiguration.Default.Formats.Add(format);
                }
                this.OnPropertyChanged();
            }
        }
    }
}
