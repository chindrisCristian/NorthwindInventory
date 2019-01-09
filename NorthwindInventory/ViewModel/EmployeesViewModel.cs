using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="EmployersPage"/>.
	/// </summary>
	public class EmployeesViewModel : ViewModelBase
    {
		#region Constructor

		public EmployeesViewModel()
		{
			_unmodifiedEmployees = EmployeeService.GetEmployees();
			Employees = new ObservableCollection<EmployeeModel>(_unmodifiedEmployees);
		}

		#endregion

		#region Properties and fields

		/// <summary>
		/// The list of employers.
		/// </summary>
		private List<EmployeeModel> _unmodifiedEmployees;
		private ObservableCollection<EmployeeModel> _employees;
		public ObservableCollection<EmployeeModel> Employees
		{
			get => _employees;
			set => Set(ref _employees, value);
		}

		#endregion

	}

}