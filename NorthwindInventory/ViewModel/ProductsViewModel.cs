using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using NorthwindInventory.Helpers;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;
using NorthwindInventory.Services;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="ProductsPage"/>.
	/// </summary>
	public class ProductsViewModel : ViewModelBase
    {
		#region Constructor

		public ProductsViewModel()
		{
			NavigateToUriCommand = new RelayCommand(NavigateToExternUrl);
			ModifyNameCommand = new RelayCommand(ModifyNameField);
			SaveNameCommand = new RelayCommand(SaveNameField);
			ModifyAddressCommand = new RelayCommand(ModifyAddressField);
			SaveAddressCommand = new RelayCommand(SaveAddressField);
			SaveOtherDetailsCommand = new RelayCommand(SaveOtherDetails);
			ChangeImageCommand = new RelayCommand(ChangeImage);
			ChangePasswordCommand = new RelayCommand(ChangePasswordAsync);

			_dialogCoordinator = DialogCoordinator.Instance;
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The dialog coordinator.
		/// </summary>
		IDialogCoordinator _dialogCoordinator;

		/// <summary>
		/// The status that indicates whether the currently connected user is a boss or not.
		/// </summary>
		private bool _isTheBoss;
		public bool IsTheBoss
		{
			get => _isTheBoss;
			set => Set(ref _isTheBoss, value);
		}

		/// <summary>
		/// The connected employee.
		/// </summary>
		private EmployeeModel _connectedEmployee;
		public EmployeeModel ConnectedEmployee
		{
			get => _connectedEmployee;
			set
			{
				Set(ref _connectedEmployee, value);
				OrdersHistory = new ObservableCollection<OrderModel>(EmployeeService.GetOrdersFor(_connectedEmployee.EmployeeID));
				IsTheBoss = (ConnectedEmployee.ReportsTo == null) ? true : false;
			}
		}

		/// <summary>
		/// The orders made by the connected employee.
		/// </summary>
		private ObservableCollection<OrderModel> _ordersHistory;
		public ObservableCollection<OrderModel> OrdersHistory
		{
			get => _ordersHistory;
			set => Set(ref _ordersHistory, value);
		}

		/// <summary>
		/// It helps us for the full name visibility or
		/// the modifiers in the page.
		/// </summary>
		private bool _isFullNameVisible = true;
		public bool IsFullNameVisible
		{
			get => _isFullNameVisible;
			set
			{
				Set(ref _isFullNameVisible, value);
				IsNameModifierVisible = !_isFullNameVisible;
			}
		}
		private bool _isNameModifierVisible;
		public bool IsNameModifierVisible
		{
			get => _isNameModifierVisible;
			set => Set(ref _isNameModifierVisible, value);
		}

		/// <summary>
		/// It helps us for the full address visibility or
		/// the modifiers in the page.
		/// </summary>
		private bool _isFullAddressVisible = true;
		public bool IsFullAddressVisible
		{
			get => _isFullAddressVisible;
			set
			{
				Set(ref _isFullAddressVisible, value);
				IsAddressModifierVisible = !_isFullAddressVisible;
			}
		}
		private bool _isAddressModifierVisible;
		public bool IsAddressModifierVisible
		{
			get => _isAddressModifierVisible;
			set => Set(ref _isAddressModifierVisible, value);
		}
		#endregion

		#region Commands

		/// <summary>
		/// The command to navigate to extern url from photo path.
		/// </summary>
		public RelayCommand NavigateToUriCommand { get; set; }
		private void NavigateToExternUrl()
		{
			//Process.Start(new ProcessStartInfo(ConnectedEmployee.PhotoPath));
		}

		/// <summary>
		/// The command to modify name fields.
		/// </summary>
		public RelayCommand ModifyNameCommand { get; set; }
		private void ModifyNameField()
		{
			IsFullNameVisible = false;
		}

		/// <summary>
		/// The command to save the modifies on the name field.
		/// </summary>
		public RelayCommand SaveNameCommand { get; set; }
		private void SaveNameField()
		{
			ConnectedEmployee = EmployeeService.UpdateEmployeeName(ConnectedEmployee.EmployeeID, ConnectedEmployee.TitleOfCourtesy, ConnectedEmployee.FirstName, ConnectedEmployee.LastName);
			MessengerInstance.Send<NotificationMessage<MessengerTypes>>(new NotificationMessage<MessengerTypes>(MessengerTypes.ChangeName, ConnectedEmployee.LastName));
			Messager.ShowMessage("The name was succesfully changed to " + ConnectedEmployee.FullName);

			IsFullNameVisible = true;
		}

		/// <summary>
		/// The command to modify address fields.
		/// </summary>
		public RelayCommand ModifyAddressCommand { get; set; }
		private void ModifyAddressField()
		{
			IsFullAddressVisible = false;
		}

		/// <summary>
		/// The command to save the modifies on the address field.
		/// </summary>
		public RelayCommand SaveAddressCommand { get; set; }
		private void SaveAddressField()
		{
			ConnectedEmployee = EmployeeService.UpdateEmployeeAddress(ConnectedEmployee.EmployeeID, ConnectedEmployee.Address, ConnectedEmployee.City, ConnectedEmployee.PostalCode, ConnectedEmployee.Country);

			Messager.ShowMessage("The address was succesfully changed to " + ConnectedEmployee.FullAddress);

			IsFullAddressVisible = true;
		}

		/// <summary>
		/// The command to save the modifies on the phone and date fields.
		/// </summary>
		public RelayCommand SaveOtherDetailsCommand { get; set; }
		private void SaveOtherDetails()
		{
			ConnectedEmployee = EmployeeService.UpdateEmployeePhoneAndDates(ConnectedEmployee.EmployeeID, ConnectedEmployee.HomePhone, ConnectedEmployee.BirthDate, ConnectedEmployee.HireDate);

			Messager.ShowMessage("The data was successfully saved!");
		}

		/// <summary>
		/// The command to change user's image.
		/// </summary>
		public RelayCommand ChangeImageCommand { get; set; }
		private void ChangeImage()
		{
			OpenFileDialog findImageDialog = new OpenFileDialog()
			{
				Filter = "Image Files|*.jpg;*.jpeg;*.png;",
				Title = "Find image",
				ValidateNames = true,
				ShowReadOnly = true
			};
			findImageDialog.ShowDialog();
			if (findImageDialog.FileName != string.Empty)
			{
				byte[] photo = File.ReadAllBytes(findImageDialog.FileName);
				ConnectedEmployee = EmployeeService.UpdateEmployeePhoto(ConnectedEmployee.EmployeeID, photo);
				MessengerInstance.Send<NotificationMessage<byte[]>>(new NotificationMessage<byte[]>(photo, string.Empty));
				Messager.ShowMessage("You have successfully changed your image.");
			}
			
		}

		/// <summary>
		/// The command to change the user's password.
		/// </summary>
		public RelayCommand ChangePasswordCommand { get; set; }
		private async void ChangePasswordAsync()
		{
			string currentPassword = await _dialogCoordinator.ShowInputAsync(this, "Change password!", "Enter the current password: ");
			if(ConnectedEmployee.Password == currentPassword)
			{
				string newPassword = await _dialogCoordinator.ShowInputAsync(this, "Change password!", "Enter the new password: ");
				EmployeeService.UpdateEmployeePassword(ConnectedEmployee.EmployeeID, newPassword);
				Messager.ShowMessage("You have succesfully changed your password!");
			}

		}
		#endregion
	}

}