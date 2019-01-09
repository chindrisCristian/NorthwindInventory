using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindInventory.Models.Services
{
	public static class OrderService
	{
		/// <summary>
		/// Returns all the details about a specific order.
		/// </summary>
		/// <param name="orderID">The order's id whose details are needed.</param>
		/// <returns></returns>
		public static List<OrderDetailModel> GetOrderDetails(int orderID)
		{
			using(var context = new NorthwindEntities())
			{
				var details = (from od in context.Order_Details
							   where od.OrderID == orderID
							   select new OrderDetailModel
							   {
								   OrderID = od.OrderID,
								   ProductName = od.Product.ProductName,
								   CategoryName = od.Product.Category.CategoryName,
								   UnitPrice = od.UnitPrice,
								   Discount = od.Discount,
								   Quantity = od.Quantity
							   }).ToList();

				return details;
			}
		}

		/// <summary>
		/// Returns a list of all orders.
		/// </summary>
		/// <returns></returns>
		public static List<OrderModel> GetOrders(int numberOfItemsToLoad, int currentIndex)
		{
			using(var context = new NorthwindEntities())
			{
				var orders = (from o in context.Orders.OrderBy(x => x.OrderID).Skip(currentIndex).Take(numberOfItemsToLoad)
							  select new OrderModel
							  {
								  OrderID = o.OrderID,
								  CustomerID = o.CustomerID,
								  CustomerCompany = o.Customer.CompanyName,
								  EmployeeID = o.EmployeeID,
								  EmployeeName = o.Employee.FirstName + " " +
												o.Employee.LastName,
								  ShipperID = o.ShipVia,
								  ShipperCompany = o.Shipper.CompanyName,
								  OrderDate = o.OrderDate,
								  RequiredDate = o.RequiredDate,
								  ShippedDate = o.ShippedDate,
								  Freight = o.Freight,
								  ShippAddress = o.ShipAddress + ", " +
												o.ShipCity + ", " +
												o.ShipPostalCode + ", " +
												o.ShipCountry
							  }).ToList();

				return orders;
			}
		}

		/// <summary>
		/// Remove an order from the database.
		/// </summary>
		/// <param name="orderID">The order's id to be removed.</param>
		/// <returns>A message whether everithing went well or not.</returns>
		public static string RemoveOrder(int orderID)
		{
			using(var context = new NorthwindEntities())
			{
				string result = $"You have successfully remove the order ( ID: {orderID}).";
				try
				{
					var order = context.Orders.Where(x => x.OrderID == orderID).FirstOrDefault();
					context.Orders.Remove(order);
					context.SaveChanges();
				}
				catch (Exception e)
				{
					result = e.Message;
				}
				return result;
			}
		}

		/// <summary>
		/// Return an order by it's ID.
		/// </summary>
		/// <param name="orderID">The order's id</param>
		/// <returns></returns>
		public static OrderModel GetOrderByID(int orderID)
		{
			using (var context = new NorthwindEntities())
			{
				var order = (from o in context.Orders
							 where o.OrderID == orderID
							  select new OrderModel
							  {
								  OrderID = o.OrderID,
								  CustomerID = o.CustomerID,
								  CustomerCompany = o.Customer.CompanyName,
								  EmployeeID = o.EmployeeID,
								  EmployeeName = o.Employee.FirstName + " " +
												o.Employee.LastName,
								  ShipperID = o.ShipVia,
								  ShipperCompany = o.Shipper.CompanyName,
								  OrderDate = o.OrderDate,
								  RequiredDate = o.RequiredDate,
								  ShippedDate = o.ShippedDate,
								  Freight = o.Freight,
								  ShippAddress = o.ShipAddress + ", " +
												o.ShipCity + ", " +
												o.ShipPostalCode + ", " +
												o.ShipCountry
							  }).FirstOrDefault();

				return order;
			}
		}


		/// <summary>
		/// Returns orders filtered for the customer id.
		/// </summary>
		/// <param name="customerIDSearch">The ID to filter for.</param>
		/// <returns></returns>
		public static List<OrderModel> GetOrdersByCustomerID(string customerIDSearch)
		{
			using (var context = new NorthwindEntities())
			{
				var orders = (from o in context.Orders
							  where o.CustomerID.Contains(customerIDSearch)
							  select new OrderModel
							  {
								  OrderID = o.OrderID,
								  CustomerID = o.CustomerID,
								  CustomerCompany = o.Customer.CompanyName,
								  EmployeeID = o.EmployeeID,
								  EmployeeName = o.Employee.FirstName + " " +
												o.Employee.LastName,
								  ShipperID = o.ShipVia,
								  ShipperCompany = o.Shipper.CompanyName,
								  OrderDate = o.OrderDate,
								  RequiredDate = o.RequiredDate,
								  ShippedDate = o.ShippedDate,
								  Freight = o.Freight,
								  ShippAddress = o.ShipAddress + ", " +
												o.ShipCity + ", " +
												o.ShipPostalCode + ", " +
												o.ShipCountry
							  }).ToList();

				return orders;
			}
		}
	}
}
