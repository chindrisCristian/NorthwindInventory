using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using NorthwindInventory.Helpers;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;
using NorthwindInventory.Services;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="CustomersPage"/>.
	/// </summary>
	public class CustomersViewModel : ViewModelBase
    {
		#region Constructor

		public CustomersViewModel()
		{
			_unmodifiedCustomers = CustomerService.GetCustomers();
			Customers = new ObservableCollection<CustomerModel>(_unmodifiedCustomers);

			//Command section
			RemoveCustomerCommand = new RelayCommand(RemoveCustomerAsync);
			RefreshCommand = new RelayCommand(Refresh);
			SearchCommand = new RelayCommand(Search);

			_dialogCoordinator = DialogCoordinator.Instance;
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The dialog coordinator.
		/// </summary>
		private IDialogCoordinator _dialogCoordinator;

		/// <summary>
		/// The options page for the customers page.
		/// </summary>
		private PageType _options = PageType.CustomerOptionsPage;
		public PageType Options
		{
			get => _options;
			set => Set(ref _options, value);
		}

		/// <summary>
		/// The customers from tha database.
		/// </summary>
		private List<CustomerModel> _unmodifiedCustomers;
		private ObservableCollection<CustomerModel> _customers;
		public ObservableCollection<CustomerModel> Customers
		{
			get => _customers;
			set => Set(ref _customers, value);
		}

		/// <summary>
		/// The selected customer from the datagrid.
		/// </summary>
		private CustomerModel _selectedCustomer;
		public CustomerModel SelectedCustomer
		{
			get => _selectedCustomer;
			set
			{
				Set(ref _selectedCustomer, value);
				if (value != null)
				{
					CustomerOrders = new ObservableCollection<OrderModel>(CustomerService.GetOrders(value.CustomerID));
					SelectedOrder = CustomerOrders[0];
					AreCustomerOrdersVisible = true;
					IsRemoveButtonEnabled = true;
				}
				else
				{
					CustomerOrders = new ObservableCollection<OrderModel>();
					IsRemoveButtonEnabled = false;
				}
			}
		}

		/// <summary>
		/// The orders for the selected customer.
		/// </summary>
		private ObservableCollection<OrderModel> _customerOrders;
		public ObservableCollection<OrderModel> CustomerOrders
		{
			get => _customerOrders;
			set => Set(ref _customerOrders, value);
		}

		/// <summary>
		/// The selected order.
		/// </summary>
		private OrderModel _selectedOrder;
		public OrderModel SelectedOrder
		{
			get => _selectedOrder;
			set
			{
				Set(ref _selectedOrder, value);
				OrderDetails = new ObservableCollection<OrderDetailModel>(OrderService.GetOrderDetails(_selectedOrder.OrderID));
			}
		}

		/// <summary>
		/// Determine whether the orders column is visible.
		/// </summary>
		private bool _areCustomerOrdersVisible = false;
		public bool AreCustomerOrdersVisible
		{
			get => _areCustomerOrdersVisible;
			set => Set(ref _areCustomerOrdersVisible, value);
		}

		/// <summary>
		/// The details of the selected order.
		/// </summary>
		private ObservableCollection<OrderDetailModel> _orderDetails;
		public ObservableCollection<OrderDetailModel> OrderDetails
		{
			get => _orderDetails;
			set => Set(ref _orderDetails, value);
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
		private string _customerSearch;
		public string CustomerSearch
		{
			get => _customerSearch;
			set => Set(ref _customerSearch, value);
		}

		#endregion

		#region Commands specific for the options page

		/// <summary>
		/// The command for removing a supplier. 
		/// </summary>
		public RelayCommand RemoveCustomerCommand { get; set; }
		private async void RemoveCustomerAsync()
		{
			string message;
			if(CustomerOrders.Count > 0)
			{
				MessageDialogResult resultDialog = await _dialogCoordinator.ShowMessageAsync(this, "Remove customer", "Are you sure? The selected customer will be removed with all his orders.", MessageDialogStyle.AffirmativeAndNegative);
				if(resultDialog == MessageDialogResult.Affirmative)
				{
					message = CustomerService.RemoveCustomer(SelectedCustomer.CustomerID);
					Refresh();
					Messager.ShowMessage(message);
				}
			}
			else
			{
				message = CustomerService.RemoveCustomer(SelectedCustomer.CustomerID);
				Refresh();
				Messager.ShowMessage(message);
			}

		}

		/// <summary>
		/// Update the data grid from the database.
		/// </summary>
		public RelayCommand RefreshCommand { get; set; }
		private void Refresh()
		{
			_unmodifiedCustomers = CustomerService.GetCustomers();
			Customers = new ObservableCollection<CustomerModel>(_unmodifiedCustomers);
			SelectedCustomer = null;
			IsRemoveButtonEnabled = false;
			CustomerSearch = string.Empty;
		}

		/// <summary>
		/// The command that updates the datagrid accordingly to the user input's search.
		/// </summary>
		public RelayCommand SearchCommand { get; set; }
		private void Search()
		{
			if (CustomerSearch == string.Empty)
				Customers = new ObservableCollection<CustomerModel>(_unmodifiedCustomers);
			else
			{
				Customers = new ObservableCollection<CustomerModel>(_unmodifiedCustomers.Where(x => x.CompanyName.StartsWith(CustomerSearch)));
			}
		}

		#endregion
	}

}