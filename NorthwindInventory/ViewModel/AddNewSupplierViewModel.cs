using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NorthwindInventory.Helpers;
using NorthwindInventory.Models.Services;
using NorthwindInventory.Services;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="AddNewSupplierPage"/>.
	/// </summary>
	public class AddNewSupplierViewModel : ViewModelBase
    {
		#region Constructor

		public AddNewSupplierViewModel()
		{
			ReturnCommand = new RelayCommand(Return);
			SaveCommand = new RelayCommand(Save);
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The company name, label and watermark.
		/// </summary>
		public string CompanyNameLabel { get; set; } = "Company Name*:";
		public string CompanyNameWatermark { get; set; } = "eg. Tokyo Traders";
		private string _companyName = string.Empty;
		public string CompanyName
		{
			get => _companyName;
			set => Set(ref _companyName, value);
		}

		/// <summary>
		/// The contact name, label and watermark.
		/// </summary>
		public string ContactNameLabel { get; set; } = "Contact Name*:";
		public string ContactNameWatermark { get; set; } = "eg. Joe Black";
		private string _contactName = string.Empty;
		public string ContactName
		{
			get => _contactName;
			set => Set(ref _contactName, value);
		}

		/// <summary>
		/// The contact title, label and watermark.
		/// </summary>
		public string ContactTitleLabel { get; set; } = "Contact Title*:";
		public string ContactTitleWatermark { get; set; } = "eg. CEO";
		private string _contactTitle = string.Empty;
		public string ContactTitle
		{
			get => _contactTitle;
			set => Set(ref _contactTitle, value);
		}

		/// <summary>
		/// The address and it's label and watermark.
		/// </summary>
		public string AddressLabel { get; set; } = "Address*:";
		public string AddressWatermark { get; set; } = "eg. 49 Gilbert Str.";
		private string _address = string.Empty;
		public string Address
		{
			get => _address;
			set => Set(ref _address, value);
		}

		/// <summary>
		/// The city and it's label and watermark.
		/// </summary>
		public string CityLabel { get; set; } = "City*:";
		public string CityWatermark { get; set; } = "eg. Bucharest";
		private string _city = string.Empty;
		public string City
		{
			get => _city;
			set => Set(ref _city, value);
		}

		/// <summary>
		/// The region and it's label and watermark.
		/// </summary>
		public string RegionLabel { get; set; } = "Region:";
		public string RegionWatermark { get; set; } = "eg. B";
		private string _region = string.Empty;
		public string Region
		{
			get => _region;
			set => Set(ref _region, value);
		}

		/// <summary>
		/// The postal code and it's label and watermark.
		/// </summary>
		public string PostalCodeLabel { get; set; } = "PostalCode*:";
		public string PostalCodeWatermark { get; set; } = "eg. 123456";
		private string _postalCode = string.Empty;
		public string PostalCode
		{
			get => _postalCode;
			set => Set(ref _postalCode, value);
		}

		/// <summary>
		/// The country and it's label and watermark.
		/// </summary>
		public string CountryLabel { get; set; } = "Country*:";
		public string CountryWatermark { get; set; } = "eg. RO or Romania";
		private string _country = string.Empty;
		public string Country
		{
			get => _country;
			set => Set(ref _country, value);
		}

		/// <summary>
		/// The phone and it's label and watermark.
		/// </summary>
		public string PhoneLabel { get; set; } = "Phone*:";
		public string PhoneWatermark { get; set; } = "eg. 0731950425";
		private string _phone = string.Empty;
		public string Phone
		{
			get => _phone;
			set => Set(ref _phone, value);
		}

		/// <summary>
		/// The fax and it's label and watermark.
		/// </summary>
		public string FaxLabel { get; set; } = "Fax:";
		public string FaxWatermark { get; set; } = "eg. 0342 44 55";
		private string _fax = string.Empty;
		public string Fax
		{
			get => _fax;
			set => Set(ref _fax, value);
		}

		/// <summary>
		/// The home page and it's label and watermark.
		/// </summary>
		public string HomePageLabel { get; set; } = "HomePage:";
		public string HomePageWatermark { get; set; } = "eg. www.facebook.com";
		private string _homePage = string.Empty;
		public string HomePage
		{
			get => _homePage;
			set => Set(ref _homePage, value);
		}

		#endregion


		#region Commands

		/// <summary>
		/// The command to return to the previous page.
		/// </summary>
		public RelayCommand ReturnCommand { get; set; }
		private void Return()
		{
			NavigationService.NavigateBack();
			Messager.ShowMessage("There were no modifies!");
		}

		/// <summary>
		/// The command to save the new added supplier.
		/// </summary>
		public RelayCommand SaveCommand { get; set; }
		private void Save()
		{
			bool isCorrect = (CompanyName != string.Empty) &&
								(ContactName != string.Empty) &&
								(ContactTitle != string.Empty) &&
								(Address != string.Empty) &&
								(City != string.Empty) &&
								(PostalCode != string.Empty) &&
								(Country != string.Empty) &&
								(Phone != string.Empty);

			if (isCorrect)
			{
				SupplierService.AddSupplier(new Models.Supplier
				{
					SupplierID = -1,
					CompanyName = this.CompanyName,
					ContactName = this.ContactName,
					ContactTitle = this.ContactTitle,
					Address = this.Address,
					City = this.City,
					Region = this.Region,
					PostalCode = this.PostalCode,
					Country = this.Country,
					Phone = this.Phone,
					Fax = this.Fax,
					HomePage = this.HomePage
				});

				MessengerInstance.Send<NotificationMessage<MessengerTypes>>(new NotificationMessage<MessengerTypes>(MessengerTypes.Refresh, "You succesfully added a new supplier, " + this.CompanyName + "!"));
				NavigationService.NavigateBack();
			}
		}
		#endregion
	}

}