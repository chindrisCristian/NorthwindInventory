using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;
using MahApps.Metro.Controls.Dialogs;
using NorthwindInventory.Helpers;
using NorthwindInventory.Services;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="ShippersPage"/>.
	/// </summary>
	public class ShippersViewModel : ViewModelBase
	{
		#region Constructor

		public ShippersViewModel()
		{
			//Field section
			_dialogCoordinator = DialogCoordinator.Instance;
			_unmodifiedShippers = ShipperService.GetShippers();
			_Shippers = new ObservableCollection<Shipper>(_unmodifiedShippers);


			//Command section
			SearchCommand = new RelayCommand(Search);

			//Messenger section
			MessengerInstance.Register<NotificationMessage<MessengerTypes>>(this, NotifyMe);
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The mode through i can play with dialogs from the view model.
		/// </summary>
		private IDialogCoordinator _dialogCoordinator;



		/// <summary>
		/// The list of Shippers.
		/// </summary>
		private List<Shipper> _unmodifiedShippers;
		private ObservableCollection<Shipper> _Shippers;
		public ObservableCollection<Shipper> Shippers
		{
			get => _Shippers;
			set => Set(ref _Shippers, value);
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
		/// The string to search for in the list of Shippers.
		/// </summary>
		private string _Shippersearch;
		public string Shippersearch
		{
			get => _Shippersearch;
			set => Set(ref _Shippersearch, value);
		}

		#endregion

		#region Commands specific for the options page



		/// <summary>
		/// The command that updates the datagrid accordingly to the user input's search.
		/// </summary>
		public RelayCommand SearchCommand { get; set; }
		private void Search()
		{
			if (Shippersearch == string.Empty)
				Shippers = new ObservableCollection<Shipper>(_unmodifiedShippers);
			else
			{
				Shippers = new ObservableCollection<Shipper>(_unmodifiedShippers.Where(x => x.CompanyName.Contains(Shippersearch)));
			}
		}

		#endregion

		#region Messenger section

		/// <summary>
		/// Accepts and responds to messages that come from another viewmodels.
		/// </summary>
		/// <param name="notificationMessage"></param>
		private void NotifyMe(NotificationMessage<MessengerTypes> notificationMessage)
		{

			Messager.ShowMessage(notificationMessage.Notification);
			switch (notificationMessage.Content)
			{
				case MessengerTypes.Refresh:
					break;
				default:
					break;
			}
		}

		#endregion
	}
}