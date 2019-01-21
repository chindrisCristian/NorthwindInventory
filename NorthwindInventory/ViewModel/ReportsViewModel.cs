using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using NorthwindInventory.Helpers;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;
using NorthwindInventory.Services;

namespace NorthwindInventory.ViewModel
{

	/// <summary>
	/// The view model for the <see cref="ReportsPage"/>.
	/// </summary>
	public class ReportsViewModel : ViewModelBase
	{
		#region Constructor

		public ReportsViewModel()
		{

			ShowReportCommand = new RelayCommand(ShowReport);
			_stocksData = new ObservableCollection<ShowStocksReportModel>(ReportsService.GetStocks());

		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The available types of reports.
		/// </summary>
		public ObservableCollection<string> ReportsType { get; set; } = new ObservableCollection<string> { "Show stocks" };

		/// <summary>
		/// The selected option.
		/// </summary>
		public string SelectedReport { get; set; }

		/// <summary>
		/// The content of the report.
		/// </summary>
		private PageType _contentSideFrame;
		public PageType ContentSideFrame
		{
			get => _contentSideFrame;
			set => Set(ref _contentSideFrame, value);
		}
		#endregion

		#region Commands

		/// <summary>
		/// The command and it's associated method for showing a report.
		/// </summary>
		public RelayCommand ShowReportCommand { get; set; }
		private void ShowReport()
		{
			switch (SelectedReport)
			{
				case "Show stocks":
					ContentSideFrame = PageType.ReportShowStock;
					break;
				default:
					ContentSideFrame = PageType.FirstPage;
					break;
			}

		}

		#endregion

		#region Show stocks report

		/// <summary>
		/// The current stocks.
		/// </summary>
		private ObservableCollection<ShowStocksReportModel> _stocksData;
		public ObservableCollection<ShowStocksReportModel> StocksData
		{
			get => _stocksData;
			set => Set(ref _stocksData, value);
		}

		#endregion
	}

}