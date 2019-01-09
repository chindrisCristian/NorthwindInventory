using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindInventory.Models.Services
{
	public static class CustomerService
	{
		/// <summary>
		/// Returns the list of customers from the database.
		/// </summary>
		/// <returns></returns>
		public static List<CustomerModel> GetCustomers()
		{
			using(var context = new NorthwindEntities())
			{
				var customers = (from c in context.Customers
								 select new CustomerModel
								 {
									 CustomerID = c.CustomerID,
									 CompanyName = c.CompanyName,
									 ContactName = c.ContactName,
									 ContactTitle = c.ContactTitle,
									 Address = c.Address + ", " +
												 c.City + ", " +
												 c.PostalCode + ", " +
												 c.Country,
												 Phone = c.Phone + ((c.Fax != null)? " (Fax: " + c.Fax + " )" : "")
								 }).ToList();
				return customers;
			}
		}

		/// <summary>
		/// Returns the orders for the selected customer.
		/// </summary>
		/// <param name="customerID">The customer's id whom's orders are returned.</param>
		/// <returns></returns>
		public static List<OrderModel> GetOrders(string customerID)
		{
			using(var context = new NorthwindEntities())
			{
				List<OrderModel> ordersPerCustomer = (from o in context.Orders
													  where o.CustomerID == customerID
													  select new OrderModel
													  {
														  OrderID = o.OrderID,
														  CustomerID = customerID,
														  EmployeeID = o.EmployeeID,
														  ShipperID = o.ShipVia,
														  OrderDate = o.OrderDate,
														  RequiredDate = o.RequiredDate,
														  ShippedDate = o.ShippedDate,
														  Freight = o.Freight,
														  ShippAddress = o.ShipAddress + ", " +
																		o.ShipCity + ", " +
																		o.ShipPostalCode + ", " + 
																		o.ShipCountry
													  }).ToList();

				foreach (var order in ordersPerCustomer)
				{
					order.CustomerCompany = context.Customers.Where(x => x.CustomerID == order.CustomerID).Select(x => x.CompanyName).First();
					order.EmployeeName = context.Employees.Where(x => x.EmployeeID == order.EmployeeID).Select(x => (x.FirstName + " " + x.LastName)).First();
					order.ShipperCompany = context.Shippers.Where(x => x.ShipperID == order.ShipperID).Select(x => x.CompanyName).First();
				}

				return ordersPerCustomer.ToList();
			}
		}

		public static string RemoveCustomer(string customerID)
		{
			string result = "The customer with the id " + customerID +
				" was successfully removed with all his orders.";

			using(var context = new NorthwindEntities())
			{
				try
				{
					var customer = context.Customers.Where(x => x.CustomerID == customerID).FirstOrDefault();
					context.Customers.Remove(customer);
					context.SaveChanges();
				}
				catch(Exception e)
				{
					result = e.Message;
				}
			}

			return result;
		}
	}
}
