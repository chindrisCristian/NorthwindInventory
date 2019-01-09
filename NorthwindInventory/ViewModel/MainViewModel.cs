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
	/// The view model for the <see cref="MainWindow"/>.
	/// </summary>
	public class MainViewModel : ViewModelBase
    {
		#region Constructor

		public MainViewModel()
		{
			_rightSideFrameMargin = _flyOutWidth;
			_rightSideFrameMarginThickness = new Thickness(_rightSideFrameMargin, 0, 0, 0);
			ReturnCommand = new RelayCommand(Return);
			LogInCommand = new RelayCommand(ShowLoginDialogAsync);
			DisconnectCommand = new RelayCommand(Disconnect);

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
		/// Shows whether the message flyout is open or closed.
		/// </summary>
		private bool _isMessageFlyOutOpen = false;
		public bool IsMessageFlyOutOpen
		{
			get => _isMessageFlyOutOpen;
			set => Set(ref _isMessageFlyOutOpen, value);
		}

		/// <summary>
		/// The message for the flyout bar.
		/// </summary>
		private string _flyOutMessage;
		public string FlyOutMessage
		{
			get => _flyOutMessage;
			set => Set(ref _flyOutMessage, value);
		}


		/// <summary>
		/// The title of the main window.
		/// </summary>
		private string _title = "Northwind Inventory";
		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}

		/// <summary>
		/// The main window is divided in two frames.
		/// This is the content of the left frame.
		/// </summary>
		private PageType _leftSidePage = PageType.NavigationPage;
		public PageType LeftSidePage
		{
			get => _leftSidePage;
			set => Set(ref _leftSidePage, value);
		}

		/// <summary>
		/// The main window is divided in two frames.
		/// This is the content of the right frame.
		/// </summary>
		private PageType _rightSidePage = PageType.FirstPage;
		public PageType RightSidePage
		{
			get => _rightSidePage;
			set => Set(ref _rightSidePage, value);
		}

		/// <summary>
		/// The flyout menu opened/ closed state.
		/// </summary>
		private bool _isFlyOutOpen = false;
		public bool IsFlyOutOpen
		{
			get => _isFlyOutOpen;
			set
			{
				if (value)
					RightSideFrameMargin = FlyOutWidth;
				else
					RightSideFrameMargin = 0;
				RightSideFrameMarginThickness = new Thickness(RightSideFrameMargin, 0, 0, 0);
				Set(ref _isFlyOutOpen, value);
			}
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

		/// <summary>
		/// The width of the menu flyout.
		/// </summary>
		private double _flyOutWidth = 300;
		public double FlyOutWidth
		{
			get => _flyOutWidth;
			set => Set(ref _flyOutWidth, value);
		}

		/// <summary>
		/// The margin for the right side frame.
		/// </summary>
		private double _rightSideFrameMargin;
		public double RightSideFrameMargin
		{
			get => _rightSideFrameMargin;
			set => Set(ref _rightSideFrameMargin, value);
		}
		private Thickness _rightSideFrameMarginThickness;
		public Thickness RightSideFrameMarginThickness
		{
			get => new Thickness(_rightSideFrameMargin, 0, 0, 0);
			set => Set(ref _rightSideFrameMarginThickness, value);
		}

		/// <summary>
		/// The status of the user.
		/// </summary>
		private bool _isLoggedIn = false;
		public bool IsLoggedIn
		{
			get => _isLoggedIn;
			set => Set(ref _isLoggedIn, value);
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

		/// <summary>
		/// The command to log into the application.
		/// </summary>
		public RelayCommand LogInCommand { get; set; }
		private async void ShowLoginDialogAsync()
		{
			var mySettings = new LoginDialogSettings
			{
				EnablePasswordPreview = true,
				AnimateShow = true,
				ColorScheme = MetroDialogColorScheme.Theme,
				DialogResultOnCancel = MessageDialogResult.Canceled
			};
			string errorMessage = string.Empty;
			int numberOfTries = 5;
			for(int i = 0; i < numberOfTries; i++)
			{
				LoginDialogData loginDialog = await DialogHelper.ShowLoginAsync(this, "Please log in!", "Press ESC to Cancel (close app)." + "\n\n\nIn order to use this app you need to be logged in!" + $"\nThere are only {numberOfTries} attempts! You have {numberOfTries - i} more attempt(s)." + $"\n{errorMessage}", mySettings);

				if (i == numberOfTries - 1 || loginDialog == null)
				{
					App.Current.MainWindow.Close();
				}
						
				if (loginDialog.Username != string.Empty && loginDialog.Password != string.Empty)
				{
					EmployeeModel currentEmployee = EmployeeService.DoesEmployeeExist(loginDialog.Username, loginDialog.Password);
					if (currentEmployee != null)
					{
						((ViewModelLocator)App.Current.Resources["Locator"]).ConnectedUser.ConnectedEmployee = currentEmployee;
						MessengerInstance.Send<NotificationMessage<MessengerTypes>>(new NotificationMessage<MessengerTypes>(MessengerTypes.ChangeName, currentEmployee.LastName));
						MessengerInstance.Send<NotificationMessage<byte[]>>(new NotificationMessage<byte[]>(currentEmployee.Photo, string.Empty));
						IsFlyOutOpen = true;
						IsLoggedIn = true;
						Messager.ShowMessage($"{currentEmployee.FullName} have successfully connected!");
						break;
					}
					else
					{
						errorMessage = "The username or the password you have introduced is incorrect!";
					}
				}
				else
				{
					errorMessage = "Username and password are required!";
				}
			}
		}

		/// <summary>
		/// The command to disconnect.
		/// </summary>
		public RelayCommand DisconnectCommand { get; set; }
		private void Disconnect()
		{
			IsLoggedIn = false;
			NavigationService.NavigateTo(PageType.FirstPage);
			NavigationService.ClearStack();
			ShowLoginDialogAsync();
		}
		#endregion
	}

}