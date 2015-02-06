using System;
using System.Windows.Input;
using Acr.BarCodes;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class BarCodeCreateViewModel : AbstractViewModel {

		public BarCodeCreateViewModel() {
            this.Formats = Enum.GetNames(typeof(BarodeFormat));
            this.selectedFormat = "QR_CODE";
			this.width = 200;
			this.height = 200;
        }


        private ICommand create;
        public ICommand Create {
            get {
                this.create = this.create ?? new Command(() => {
					try {
	                    var format = (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), this.SelectedFormat);
						var cfg = new BarCodeCreateConfiguration {
							BarCode = this.BarCode,
							Height = this.Height,
							Width = this.Width,
							Format = format
						};

                        this.Image = ImageSource.FromStream(() => BarCodes.Instance.Create(cfg));
					}
					catch (Exception ex) {
					}
                });
                return this.create;
            }
        }

        #region Properties

        public string[] Formats { get; private set; }


		private bool isBarCodeReady;
		public bool IsBarCodeReady {
			get { return this.isBarCodeReady; }
			set { this.SetProperty(ref this.isBarCodeReady, value); }
		}


        private string selectedFormat;
        public string SelectedFormat {
            get { return this.selectedFormat; }
            set { this.SetProperty(ref this.selectedFormat, value); }
        }


        private string barCode;
        public string BarCode {
			get { return this.barCode; }
			set { this.SetProperty(ref this.barCode, value); }
        }


		private int height;
		public int Height {
			get { return this.height; }
			set { 
				this.height = (value <= 400 && value >= 50) 
					? value
					: 200;

				this.OnPropertyChanged();
			}
		}


		private int width;
		public int Width {
			get { return this.width; }
			set { 
				this.width = (value <= 400 && value >= 50) 
					? value
					: 200;

				this.OnPropertyChanged();
			}
		}


        private ImageSource image;
        public ImageSource Image {
            get { return this.image; }
            private set {
                this.image = value;
                this.OnPropertyChanged();
            }
        }

        #endregion
    }
}
