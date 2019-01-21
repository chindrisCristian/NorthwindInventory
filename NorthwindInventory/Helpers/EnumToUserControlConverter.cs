using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using NorthwindInventory.ViewModel;
using NorthwindInventory.Views;

namespace NorthwindInventory.Helpers
{
	/// <summary>
	/// A converter for enums of type <see cref="PageType"/> to <see cref="UserControl"/>.
	/// </summary>
	[ValueConversion(typeof(PageType), typeof(UserControl))]
	public class EnumToUserControlConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			PageType? pageType = value as PageType?;
			switch (pageType)
			{
				case PageType.FirstPage:
					return new FirstPage();
				case PageType.NavigationPage:
					return new NavigationPage();
				case PageType.SuppliersPage:
					return new SuppliersPage();
				case PageType.SupplierOptionsPage:
					return new SupplierOptionsPage();
				case PageType.AddNewSupplierPage:
					return new AddNewSupplierPage();
				case PageType.CustomersPage:
					return new CustomersPage();
				case PageType.ShippersPage:
					return new ShippersPage();
				case PageType.ConnectedUserPage:
					return new ConnectedUserPage();
				case PageType.CustomerOptionsPage:
					return new CustomerOptionsPage();
				case PageType.ProductsPage:
					return new ProductsPage();
				case PageType.OrdersPage:
					return new OrdersPage();
				case PageType.ReportsPage:
					return new ReportsPage();
				case PageType.EmployeesPage:
					return new EmployeesPage();
				case PageType.ReportShowStock:
					return new ReportShowStockPage();
				default:
					return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
