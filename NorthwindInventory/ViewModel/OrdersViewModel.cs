using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using NorthwindInventory.Helpers;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="OrdersPage"/>.
	/// </summary>
	public class OrdersViewModel : ViewModelBase
    {
		#region Constructor

		public OrdersViewModel()
		{
			_itemsToLoad = 50;
			GoToFirstRecords();

			RemoveSupplierCommand = new RelayCommand(RemoveSupplierAsync);
			RefreshCommand = new RelayCommand(Refresh);
			SearchCommand = new RelayCommand(Search);
			GoToFirstRecordsCommand = new RelayCommand(GoToFirstRecords);
			GetNextRecordsCommand = new RelayCommand(GetNextRecords);
			GetPreviousRecordsCommand = new RelayCommand(GetPreviousRecords);
			SearchCustomerIDCommand = new RelayCommand(SearchByCustomerID);

			_dialogCoordinator = DialogCoordinator.Instance;
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The dialog coordinator.
		/// </summary>
		IDialogCoordinator _dialogCoordinator;

		/// <summary>
		/// Used for memorizing the index in the orders records.
		/// </summary>
		private int _currentIndex;
		public int CurrentIndex
		{
			get => _currentIndex;
			set
			{
				Set(ref _currentIndex, value);

				IsPreviousButtonEnabled = (value > 50) ? true : false;

				IsNextButtonEnabled = (value % _itemsToLoad == 0) ? true : false;
			}
		}
		private int _itemsToLoad;

		/// <summary>
		/// A identifier for the current status of some buttons.
		/// </summary>
		private bool _isPreviousButtonEnabled;
		public bool IsPreviousButtonEnabled
		{
			get => _isPreviousButtonEnabled;
			set => Set(ref _isPreviousButtonEnabled, value);
		}

		/// <summary>
		/// A identifier for the current status of some buttons.
		/// </summary>
		private bool _isNextButtonEnabled;
		public bool IsNextButtonEnabled
		{
			get => _isNextButtonEnabled;
			set => Set(ref _isNextButtonEnabled, value);
		}

		/// <summary>
		/// The collection of all orders.
		/// </summary>
		private List<OrderModel> _unmodifiedOrders;
		private ObservableCollection<OrderModel> _orders;
		public ObservableCollection<OrderModel> Orders
		{
			get => _orders;
			set => Set(ref _orders, value);
		}

		/// <summary>
		/// The selected order.
		/// </summary>
		private OrderModel _selectedModel;
		public OrderModel SelectedOrder
		{
			get => _selectedModel;
			set
			{
				Set(ref _selectedModel, value);
				if(value != null)
				{
					OrderDetails = new ObservableCollection<OrderDetailModel>(OrderService.GetOrderDetails(value.OrderID));
					IsRemoveButtonEnabled = true;
				}
				else
				{
					OrderDetails = new ObservableCollection<OrderDetailModel>();
				}
			}
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

		/// <summary>
		/// The status of the remove button:
		/// is visible or not.
		/// </summary>
		private bool _isRemoveButtonEnabled;
		public bool IsRemoveButtonEnabled
		{
			get => _isRemoveButtonEnabled;
			set => Set(ref _isRemoveButtonEnabled, value);
		}

		/// <summary>
		/// Used to search for a specific order.
		/// </summary>
		private string _orderSearch;
		public string OrderSearch
		{
			get => _orderSearch;
			set => Set(ref _orderSearch, value);
		}

		/// <summary>
		/// Used to as a filter for customer ID.
		/// </summary>
		private string _customerIDSearch;
		public string CustomerIDSearch
		{
			get => _customerIDSearch;
			set => Set(ref _customerIDSearch, value);
		}

		#endregion

		#region Commands

		/// <summary>
		/// The command for removing a supplier. 
		/// </summary>
		public RelayCommand RemoveSupplierCommand { get; set; }
		private async void RemoveSupplierAsync()
		{
			string result = string.Empty;
			if (OrderDetails.Count > 0)
			{
				if (await _dialogCoordinator.ShowMessageAsync(this, "Remove action", "Are you sure you want to remove this order and all it's data, like the details about it?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
				{
					result = OrderService.RemoveOrder(SelectedOrder.OrderID);
					Refresh();
				}
			}
			else
			{
				result = OrderService.RemoveOrder(SelectedOrder.OrderID);
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
			_unmodifiedOrders = OrderService.GetOrders(_itemsToLoad, _currentIndex);
			Orders = new ObservableCollection<OrderModel>(_unmodifiedOrders);
			SelectedOrder = null;
			IsRemoveButtonEnabled = false;
			OrderSearch = string.Empty;
		}

		/// <summary>
		/// The command that updates the datagrid accordingly to the user input's search.
		/// </summary>
		public RelayCommand SearchCommand { get; set; }
		private void Search()
		{
			if (OrderSearch == string.Empty)
				Orders = new ObservableCollection<OrderModel>(_unmodifiedOrders);
			else
			{
				Int32.TryParse(OrderSearch, out int id);
				if (id > 0)
				{
					SelectedOrder = null;
					OrderDetails = new ObservableCollection<OrderDetailModel>(OrderService.GetOrderDetails(id));
				}
			}
		}

		/// <summary>
		/// The command to bring the first <see cref="_itemsToLoad"/> records.
		/// </summary>
		public RelayCommand GoToFirstRecordsCommand { get; set; }
		private void GoToFirstRecords()
		{
			_unmodifiedOrders = OrderService.GetOrders(_itemsToLoad, 0);
			Orders = new ObservableCollection<OrderModel>(_unmodifiedOrders);
			CurrentIndex = _unmodifiedOrders.Count();
			SelectedOrder = null;
		}

		/// <summary>
		/// The command to bring the next <see cref="_itemsToLoad"/> records.
		/// </summary>
		public RelayCommand GetNextRecordsCommand { get; set; }
		private void GetNextRecords()
		{
			_unmodifiedOrders = OrderService.GetOrders(_itemsToLoad, CurrentIndex);
			Orders = new ObservableCollection<OrderModel>(_unmodifiedOrders);
			SelectedOrder = null;
			CurrentIndex += _unmodifiedOrders.Count;
		}

		/// <summary>
		/// The command to bring the previous <see cref="_itemsToLoad"/> records.
		/// </summary>
		public RelayCommand GetPreviousRecordsCommand { get; set; }
		private void GetPreviousRecords()
		{
			CurrentIndex -= (CurrentIndex % _itemsToLoad == 0) ? (2 * _itemsToLoad) : (_itemsToLoad + CurrentIndex % _itemsToLoad);
			_unmodifiedOrders = OrderService.GetOrders(_itemsToLoad, CurrentIndex);
			Orders = new ObservableCollection<OrderModel>(_unmodifiedOrders);
			SelectedOrder = null;
			CurrentIndex += _unmodifiedOrders.Count;
		}


		public RelayCommand SearchCustomerIDCommand { get; set; }
		private void SearchByCustomerID()
		{
			SelectedOrder = null;
			if (CustomerIDSearch != string.Empty && CustomerIDSearch != null)
			{
				Orders = new ObservableCollection<OrderModel>(OrderService.GetOrdersByCustomerID(CustomerIDSearch));
			}
			else
				Orders = new ObservableCollection<OrderModel>(_unmodifiedOrders);
		}
		#endregion
	}

}