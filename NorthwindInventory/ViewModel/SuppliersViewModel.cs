using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;
using MahApps.Metro.Controls.Dialogs;
using NorthwindInventory.Helpers;
using NorthwindInventory.Services;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="SuppliersPage"/>.
	/// </summary>
	public class SuppliersViewModel : ViewModelBase
    {
		#region Constructor

		public SuppliersViewModel()
		{
			//Field section
			_dialogCoordinator = DialogCoordinator.Instance;
			_unmodifiedSuppliers = SupplierService.GetSuppliers();	
			_suppliers = new ObservableCollection<Supplier>(_unmodifiedSuppliers);
			ProductsPerSupplier = new ObservableCollection<Product>();


			//Command section
			SaveContextCommand = new RelayCommand(SaveContext);
			AddSupplierCommand = new RelayCommand(AddSupplier);
			RemoveSupplierCommand = new RelayCommand(RemoveSupplierAsync);
			RefreshCommand = new RelayCommand(Refresh);
			SearchCommand = new RelayCommand(Search);

			//Messenger section
			MessengerInstance.Register<NotificationMessage<MessengerTypes>>(this, NotifyMe);
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The mode through i can play with dialogs from the view model.
		/// </summary>
		private IDialogCoordinator _dialogCoordinator;

		/// <summary>
		/// The type for the frame that contains the options for <see cref="SuppliersPage"/>.
		/// </summary>
		private PageType _options = PageType.SupplierOptionsPage;
		public PageType Options => _options;

		/// <summary>
		/// The list of suppliers.
		/// </summary>
		private List<Supplier> _unmodifiedSuppliers;
		private ObservableCollection<Supplier> _suppliers;
		public ObservableCollection<Supplier> Suppliers
		{
			get => _suppliers;
			set => Set(ref _suppliers, value);
		}

		/// <summary>
		/// A list of products for the selected supplier.
		/// </summary>
		private ObservableCollection<Product> _productsPerSupplier;
		public ObservableCollection<Product> ProductsPerSupplier
		{
			get => _productsPerSupplier;
			set => Set(ref _productsPerSupplier, value);
		}

		/// <summary>
		/// The item that is currently selected.
		/// </summary>
		private Supplier _selectedSupplier;
		public Supplier SelectedSupplier
		{
			get => _selectedSupplier;
			set
			{
				Set(ref _selectedSupplier, value);
				if (value != null)
				{
					IsRemoveButtonEnabled = true;
					ProductsPerSupplier = new ObservableCollection<Product>(SupplierService.GetProductsPerSupplier(SelectedSupplier));
				}
				else
					ProductsPerSupplier = new ObservableCollection<Product>();
			}
		}

		#endregion

		#region Properties and fields specific for the options page
		
		/// <summary>
		/// The remove button will be available only if
		/// there is at least one element selected.
		/// </summary>
		private bool _isRemoveButtonEnabled = false;
		public bool IsRemoveButtonEnabled
		{
			get => _isRemoveButtonEnabled;
			set => Set(ref _isRemoveButtonEnabled, value);
		}

		/// <summary>
		/// The string to search for in the list of suppliers.
		/// </summary>
		private string _supplierSearch;
		public string SupplierSearch
		{
			get => _supplierSearch;
			set => Set(ref _supplierSearch, value);
		}

		#endregion

		#region Commands specific for the options page

		/// <summary>
		/// The command for saving the context.
		/// </summary>
		public RelayCommand SaveContextCommand { get; set; }
		private void SaveContext()
		{
			SupplierService.Save(_suppliers.ToList());
		}

		/// <summary>
		/// The command for adding a new supplier.
		/// </summary>
		public RelayCommand AddSupplierCommand { get; set; }
		private void AddSupplier()
		{
			NavigationService.NavigateWithReturn(PageType.SuppliersPage, PageType.AddNewSupplierPage);
		}

		/// <summary>
		/// The command for removing a supplier. 
		/// </summary>
		public RelayCommand RemoveSupplierCommand { get; set; }
		private async void RemoveSupplierAsync()
		{
			string result = string.Empty;
			if (ProductsPerSupplier.Count > 0)
			{
				if (await _dialogCoordinator.ShowMessageAsync(this, "Remove action", "Are you sure you want to remove this supplier and all it's data, like products?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
				{
					result = SupplierService.RemoveSupplier(SelectedSupplier);
					Refresh();
				}
			}
			else
			{
				result = SupplierService.RemoveSupplier(SelectedSupplier);
				Refresh();
			}
			Messager.ShowMessage(result);
		}

		/// <summary>
		/// Update the data grid from the database.
		/// </summary>
		public RelayCommand RefreshCommand { get; set; }
		private void Refresh()
		{
			_unmodifiedSuppliers = SupplierService.GetSuppliers();
			Suppliers = new ObservableCollection<Supplier>(_unmodifiedSuppliers);
			SelectedSupplier = null;
			IsRemoveButtonEnabled = false;
			SupplierSearch = string.Empty;
		}

		/// <summary>
		/// The command that updates the datagrid accordingly to the user input's search.
		/// </summary>
		public RelayCommand SearchCommand { get; set; }
		private void Search()
		{
			if (SupplierSearch == string.Empty)
				Suppliers = new ObservableCollection<Supplier>(_unmodifiedSuppliers);
			else
			{
				Suppliers = new ObservableCollection<Supplier>(_unmodifiedSuppliers.Where(x => x.CompanyName.Contains(SupplierSearch)));
			}
		}

		#endregion

		#region Messenger section

		/// <summary>
		/// Accepts and responds to messages that come from another viewmodels.
		/// </summary>
		/// <param name="notificationMessage"></param>
		private void NotifyMe(NotificationMessage<MessengerTypes> notificationMessage)
		{

			Messager.ShowMessage(notificationMessage.Notification);
			switch (notificationMessage.Content)
			{
				case MessengerTypes.Refresh:
					Refresh();
					break;
				default:
					break;
			}
		}

		#endregion
	}

}