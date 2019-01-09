using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindInventory.Helpers;
using NorthwindInventory.ViewModel;

namespace NorthwindInventory.Services
{
    public static class NavigationService
    {
		#region Private fields

		/// <summary>
		/// The root view model that contains connections to the rest of the view models, and also the pages.
		/// </summary>
		private readonly static ViewModelLocator _locator = (ViewModelLocator)App.Current.Resources["Locator"];

		/// <summary>
		/// The queue that contains all the pages for going back.
		/// </summary>
		private static Stack<PageType> _pagesForReturn = new Stack<PageType>();

		#endregion


		#region Public methods

		/// <summary>
		/// The method used for navigation to other pages.
		/// </summary>
		/// <param name="newPage">The new page to navigate to.</param>
		public static void NavigateTo(PageType newPage)
		{
			_locator.Main.RightSidePage = newPage;
		}

		/// <summary>
		/// The method that saves the pages where the user can go back to
		/// and then navigates to the new page.
		/// </summary>
		/// <param name="currentPage">The page that needs to be saved for returning purposes.</param>
		/// <param name="newPage">The page that is needed to navigate to.</param>
		public static void NavigateWithReturn(PageType currentPage, PageType newPage)
		{
			//Saves the currentPage.
			//If the current page is the same with the one where we navigate to
			//we don't save it.
			if (GetCurrentPage() != newPage)
				_pagesForReturn.Push(currentPage);
			//Navigate to the new page.
			NavigateTo(newPage);
		}

		/// <summary>
		/// The method that navigates to the previous page.
		/// </summary>
		public static void NavigateBack()
		{
			if (_pagesForReturn.Count > 0)
				_locator.Main.RightSidePage = _pagesForReturn.Pop();
			else
				Messager.ShowMessage("The is no other page to return to.");
		}

		/// <summary>
		/// Returns the current page.
		/// </summary>
		/// <returns></returns>
		public static PageType GetCurrentPage()
		{
			return _locator.Main.RightSidePage;
		}

		/// <summary>
		/// Clears the return page stack.
		/// </summary>
		public static void ClearStack()
		{
			_pagesForReturn.Clear();
		}

		#endregion
	}
}
