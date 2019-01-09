using NorthwindInventory.ViewModel;

namespace NorthwindInventory.Helpers
{
	public static class Messager
	{
		public static void ShowMessage(string message)
		{
			var locator = (ViewModelLocator)System.Windows.Application.Current.Resources["Locator"];
			locator.Main.FlyOutMessage = message;
			locator.Main.IsMessageFlyOutOpen = true;
		}
	}
}
