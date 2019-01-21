using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindInventory.Models
{
	public class ShowStocksReportModel
	{
		public int ProductID { get; set; }

		public string ProductName { get; set; }

		public short? UnitsInStock { get; set; }

		public int CurrentStock { get; set; }

		public int EmployeeID { get; set; }

		public string EmployeeFullName { get; set; }
	}
}
