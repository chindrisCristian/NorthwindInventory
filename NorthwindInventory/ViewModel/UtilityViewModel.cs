using System;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using NorthwindInventory.Helpers;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;
using NorthwindInventory.Services;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="UtilityViewModel"/>.
	/// </summary>
	public class UtilityViewModel : ViewModelBase
    {
		#region Constructor

		public UtilityViewModel()
		{
			ReturnCommand = new RelayCommand(Return);

			_dialogHelper = DialogCoordinator.Instance;
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The dialog coordinator for this window.
		/// </summary>
		private IDialogCoordinator _dialogHelper;
		public IDialogCoordinator DialogHelper => _dialogHelper;

		
		/// <summary>
		/// The title of the main window.
		/// </summary>
		private string _title;
		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}

		/// <summary>
		/// The main window is divided in two frames.
		/// This is the content of the right frame.
		/// </summary>
		private PageType _mainContent;
		public PageType MainContent
		{
			get => _mainContent;
			set => Set(ref _mainContent, value);
		}

		/// <summary>
		/// The minimum width of the window.
		/// </summary>
		private int _minWidth = 900;
		public int MinWidth
		{
			get => _minWidth;
			set => Set(ref _minWidth, value);
		}

		/// <summary>
		/// The minimum height of the window.
		/// </summary>
		private int _minHeight = 700;
		public int MinHeight
		{
			get => _minHeight;
			set => Set(ref _minHeight, value);
		}

		#endregion

		#region Commands and helpers

		/// <summary>
		/// The command to navigate to the previous page.
		/// </summary>
		public RelayCommand ReturnCommand { get; set; }
		private void Return()
		{

			NavigationService.NavigateBack();
		}
		
		#endregion
	}

}