using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls.Dialogs;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// THIS IS SOMEHOW THE ROOT OF MY APP.
	/// </summary>
	public class ViewModelLocator
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<FirstViewModel>();
            SimpleIoc.Default.Register<NavigationViewModel>();
			SimpleIoc.Default.Register<SuppliersViewModel>();
			SimpleIoc.Default.Register<AddNewSupplierViewModel>();
			SimpleIoc.Default.Register<CustomersViewModel>();
			SimpleIoc.Default.Register<ShippersViewModel>();
			SimpleIoc.Default.Register<ConnectedUserViewModel>();
			SimpleIoc.Default.Register<ProductsViewModel>();
			SimpleIoc.Default.Register<OrdersViewModel>();
			SimpleIoc.Default.Register<ReportsViewModel>();
        }

		//The viewmodel getters.
		public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

		public FirstViewModel First => ServiceLocator.Current.GetInstance<FirstViewModel>();

		public NavigationViewModel Navigation => ServiceLocator.Current.GetInstance<NavigationViewModel>();

		public SuppliersViewModel Suppliers => ServiceLocator.Current.GetInstance<SuppliersViewModel>();

		public AddNewSupplierViewModel AddNewSupplier => ServiceLocator.Current.GetInstance<AddNewSupplierViewModel>();

		public CustomersViewModel Customers => ServiceLocator.Current.GetInstance<CustomersViewModel>();

		public ShippersViewModel Shippers => ServiceLocator.Current.GetInstance<ShippersViewModel>();

		public ConnectedUserViewModel ConnectedUser => ServiceLocator.Current.GetInstance<ConnectedUserViewModel>();

		public ProductsViewModel Products => ServiceLocator.Current.GetInstance<ProductsViewModel>();

		public OrdersViewModel Orders => ServiceLocator.Current.GetInstance<OrdersViewModel>();

		public ReportsViewModel Reports => ServiceLocator.Current.GetInstance<ReportsViewModel>();

		public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}