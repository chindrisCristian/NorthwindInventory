using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindInventory.Models.Services
{
	public static class ReportsService
	{
		public static List<ShowStocksReportModel> GetStocks()
		{
			using(var context = new NorthwindEntities())
			{
				var stocks = (from p in context.Products
							  join s in context.ScannedProducts
							  on p.ProductID equals s.ProductID
							  join e in context.Employees
							  on s.EmployeeID equals e.EmployeeID
							  select new ShowStocksReportModel
							  {
								  ProductID = p.ProductID,
								  ProductName = p.ProductName,
								  UnitsInStock = p.UnitsInStock,
								  CurrentStock = s.CurrentStock,
								  EmployeeID = s.EmployeeID,
								  EmployeeFullName = e.Title + " " +
													 e.FirstName + " " +
													 e.LastName
							  }).ToList();

				return stocks;
			}
		}
	}
}
