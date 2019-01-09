using System;

namespace NorthwindInventory.Models
{
	public class OrderModel
	{
		#region Properties and fields

		public int OrderID { get; set; }
		
		public string CustomerID { get; set; }

		public string CustomerCompany { get; set; }
		
		public int? EmployeeID { get; set; }

		public string EmployeeName { get; set; }
		
		public int? ShipperID { get; set; }

		public string ShipperCompany { get; set; }

		public DateTime? OrderDate { get; set; }

		public DateTime? RequiredDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public decimal? Freight { get; set; }

		public string ShippAddress { get; set; }

		#endregion

		#region Constructor

		public OrderModel()
		{
		
		}

		#endregion
	}
}
