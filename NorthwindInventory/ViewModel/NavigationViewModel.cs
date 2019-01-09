using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NorthwindInventory.Services;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="NavigationPage"/>.
	/// </summary>
	public class NavigationViewModel : ViewModelBase
    {
		#region Constructor

		public NavigationViewModel()
		{
			GoToMainPageCommand = new RelayCommand(GoToMainPage);
			GoToSuppliersPageCommand = new RelayCommand(GoToSuppliersPage);
			GoToCustomersPageCommand = new RelayCommand(GoToCustomersPage);
			GoToShippersPageCommand = new RelayCommand(GoToShippersPage);
			GoToConnectedUserPageCommand = new RelayCommand(GoToConnectedUserPage);
			GoToSettingsPageCommand = new RelayCommand(GoToSettingsPage);
			GoToProductsPageCommand = new RelayCommand(GoToProductsPage);
			GoToOrdersPageCommand = new RelayCommand(GoToOrdersPage);
			GoToReportsPageCommand = new RelayCommand(GoToReportsPage);


			//Messenger section
			MessengerInstance.Register<NotificationMessage<MessengerTypes>>(this, NotifyMe);
			MessengerInstance.Register<NotificationMessage<byte[]>>(this, NotifyChangeImage);
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The text shown to navigate to the first page.
		/// </summary>
		private string _mainPage = "Main";
		public string MainPage
		{
			get => _mainPage;
			set => Set(ref _mainPage, value);
		}

		/// <summary>
		/// The text shown to navigate to the suppliers page.
		/// </summary>
		private string _suppliersPage = "Suppliers";
		public string SuppliersPage
		{
			get => _suppliersPage;
			set => Set(ref _suppliersPage, value);
		}

		/// <summary>
		/// The text shown to navigate to the customers page.
		/// </summary>
		private string _customersPage = "Customers";
		public string CustomersPage
		{
			get => _customersPage;
			set => Set(ref _customersPage, value);
		}

		/// <summary>
		/// The text shown to navigate to the shippers page.
		/// </summary>
		private string _shippersPage = "Shippers";
		public string ShippersPage
		{
			get => _shippersPage;
			set => Set(ref _shippersPage, value);
		}

		/// <summary>
		/// The text and the image shown to navigate to the connected user's page.
		/// </summary>
		private byte[] _connectedUserImage;
		public byte[] ConnectedUserImage
		{
			get => _connectedUserImage;
			set => Set(ref _connectedUserImage, value);
		}
		private string _connectedUserPage;
		public string ConnectedUserPage
		{
			get => _connectedUserPage;
			set => Set(ref _connectedUserPage, value);
		}

		/// <summary>
		/// The text shown to navigate to the settings page.
		/// </summary>
		private string _settingsPage = "Settings";
		public string SettingsPage
		{
			get => _settingsPage;
			set => Set(ref _settingsPage, value);
		}

		/// <summary>
		/// The text shown to navigate to the products page.
		/// </summary>
		private string _productsPage = "Products";
		public string ProductsPage
		{
			get => _productsPage;
			set => Set(ref _productsPage, value);
		}

		/// <summary>
		/// The text shown to navigate to the orders page.
		/// </summary>
		private string _ordersPage = "Orders";
		public string OrdersPage
		{
			get => _ordersPage;
			set => Set(ref _ordersPage, value);
		}

		/// <summary>
		/// The text shown to navigate to the reports page.
		/// </summary>
		private string _reportsPage = "Reports";
		public string ReportsPage
		{
			get => _reportsPage;
			set => Set(ref _reportsPage, value);
		}

		#endregion

		#region Commands

		/// <summary>
		/// The command to navigate to the first page.
		/// </summary>
		public RelayCommand GoToMainPageCommand { get; set; }
		private void GoToMainPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.FirstPage);
		}

		/// <summary>
		/// The command to navigate to the suppliers page.
		/// </summary>
		public RelayCommand GoToSuppliersPageCommand { get; set; }
		private void GoToSuppliersPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.SuppliersPage);
		}

		/// <summary>
		/// The command to navigate to the customers page.
		/// </summary>
		public RelayCommand GoToCustomersPageCommand { get; set; }
		private void GoToCustomersPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.CustomersPage);
		}

		/// <summary>
		/// The command to navigate to the shippers page.
		/// </summary>
		public RelayCommand GoToShippersPageCommand { get; set; }
		private void GoToShippersPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.ShippersPage);
		}

		/// <summary>
		/// The command to navigate to the connected user's page.
		/// </summary>
		public RelayCommand GoToConnectedUserPageCommand { get; set; }
		private void GoToConnectedUserPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.ConnectedUserPage);
		}

		/// <summary>
		/// The command to navigate to the settings page.
		/// </summary>
		public RelayCommand GoToSettingsPageCommand { get; set; }
		private void GoToSettingsPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.SettingsPage);
		}

		/// <summary>
		/// The command to navigate to the products page.
		/// </summary>
		public RelayCommand GoToProductsPageCommand { get; set; }
		private void GoToProductsPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.ProductsPage);
		}

		/// <summary>
		/// The command to navigate to the orders page.
		/// </summary>
		public RelayCommand GoToOrdersPageCommand { get; set; }
		private void GoToOrdersPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.OrdersPage);
		}

		/// <summary>
		/// The command to navigate to the reports page.
		/// </summary>
		public RelayCommand GoToReportsPageCommand { get; set; }
		private void GoToReportsPage()
		{
			NavigationService.NavigateWithReturn(NavigationService.GetCurrentPage(), PageType.ReportsPage);
		}
		#endregion

		#region Messaging

		/// <summary>
		/// The method that resolv the notification messages.
		/// </summary>
		/// <param name="notificationMessage">The message.</param>
		private void NotifyMe(NotificationMessage<MessengerTypes> notificationMessage)
		{
			if (notificationMessage.Content == MessengerTypes.ChangeName)
				ConnectedUserPage = notificationMessage.Notification;
		}

		/// <summary>
		/// The method that resolv the notification messages that are about changing images.
		/// </summary>
		/// <param name="notificationMessage">The notification message.</param>
		private void NotifyChangeImage(NotificationMessage<byte[]> notificationMessage)
		{
			ConnectedUserImage = notificationMessage.Content;
		}
		#endregion
	}

}