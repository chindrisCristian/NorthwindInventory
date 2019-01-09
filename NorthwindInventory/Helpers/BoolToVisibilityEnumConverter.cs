using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NorthwindInventory.ViewModel;
using NorthwindInventory.Views;

namespace NorthwindInventory.Helpers
{
	/// <summary>
	/// A converter for <see cref="bool"/> to <see cref="Visibility"/> enum.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BoolToVisibilityEnumConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? Visibility.Visible : Visibility.Collapsed;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
