using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindInventory.Models.Services
{
	public static class EmployeeService
	{
		/// <summary>
		/// An update method for the title and name for a specified employee.
		/// </summary>
		/// <param name="employeeID">The ID of a certain employee.</param>
		/// <param name="title">The new title.</param>
		/// <param name="firstName">The new first name.</param>
		/// <param name="lastName">The new last name.</param>
		/// <returns>The modified employee.</returns>
		public static EmployeeModel UpdateEmployeeName(int employeeID, string titleOfCourtesy, string firstName, string lastName)
		{
			using (var context = new NorthwindEntities())
			{
				var employee = context.Employees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
				employee.TitleOfCourtesy = titleOfCourtesy;
				employee.FirstName = firstName;
				employee.LastName = lastName;

				context.SaveChanges();

				return new EmployeeModel
				{
					EmployeeID = employee.EmployeeID,
					LastName = employee.LastName,
					FirstName = employee.FirstName,
					Title = employee.Title,
					TitleOfCourtesy = employee.TitleOfCourtesy,
					BirthDate = employee.BirthDate,
					HireDate = employee.HireDate,
					Address = employee.Address,
					City = employee.City,
					PostalCode = employee.PostalCode,
					Country = employee.Country,
					HomePhone = employee.HomePhone,
					Photo = employee.Photo,
					Notes = employee.Notes,
					ReportsTo = employee.ReportsTo,
					PhotoPath = employee.PhotoPath,
					Password = employee.Password
				};
			}
		}

		/// <summary>
		/// Returns a list of employee models.
		/// </summary>
		/// <returns></returns>
		public static List<EmployeeModel> GetEmployees()
		{
			using (var context = new NorthwindEntities())
			{
				var employees = (from e in context.Employees
								 select new EmployeeModel
								 {
									 EmployeeID = e.EmployeeID,
									 LastName = e.LastName,
									 FirstName = e.FirstName,
									 Title = e.Title,
									 TitleOfCourtesy = e.TitleOfCourtesy,
									 BirthDate = e.BirthDate,
									 HireDate = e.HireDate,
									 Address = e.Address,
									 City = e.City,
									 PostalCode = e.PostalCode,
									 Country = e.Country,
									 HomePhone = e.HomePhone,
									 Photo = e.Photo,
									 Notes = e.Notes,
									 ReportsTo = e.ReportsTo,
									 PhotoPath = e.PhotoPath,
									 Password = e.Password
								 }).ToList();
				return employees;
			}
		}

		/// <summary>
		/// Returns a list of orders made by a specific employee.
		/// </summary>
		/// <param name="employeeID">The employee's id whose orders are required.</param>
		/// <returns></returns>
		public static List<OrderModel> GetOrdersFor(int employeeID)
		{
			using(var context = new NorthwindEntities())
			{
				var orders = (from o in context.Orders
							  where o.EmployeeID == employeeID
							  select new OrderModel
							  {
								  OrderID = o.OrderID,
								  CustomerID = o.CustomerID,
								  CustomerCompany = o.Customer.CompanyName,
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
		/// Verifies whether a user exists or not.
		/// </summary>
		/// <param name="username">Username used to log in.</param>
		/// <param name="password">Password used to log in.</param>
		/// <returns></returns>
		public static EmployeeModel DoesEmployeeExist(string username, string password)
		{
			var employess = GetEmployees();
			List<EmployeeModel> employee = null;
			employee = employess.Where(x => x.UserName == username && x.Password == password).ToList();
			if (employee.Count > 0)
				return employee[0];
			else
				return null;
		}

		/// <summary>
		/// Updates the address fields of a specific employee.
		/// </summary>
		/// <param name="employeeID">Employee's ID.</param>
		/// <param name="address">Employee's address.</param>
		/// <param name="city">Employee's citty.</param>
		/// <param name="postalCode">Employee's postal code.</param>
		/// <param name="country">Employee's country.</param>
		/// <returns></returns>
		public static EmployeeModel UpdateEmployeeAddress(int employeeID, string address, string city, string postalCode, string country)
		{
			using (var context = new NorthwindEntities())
			{
				var employee = context.Employees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
				employee.Address = address;
				employee.City = city;
				employee.PostalCode = postalCode;
				employee.Country = country;

				context.SaveChanges();

				return new EmployeeModel
				{
					EmployeeID = employee.EmployeeID,
					LastName = employee.LastName,
					FirstName = employee.FirstName,
					Title = employee.Title,
					TitleOfCourtesy = employee.TitleOfCourtesy,
					BirthDate = employee.BirthDate,
					HireDate = employee.HireDate,
					Address = employee.Address,
					City = employee.City,
					PostalCode = employee.PostalCode,
					Country = employee.Country,
					HomePhone = employee.HomePhone,
					Photo = employee.Photo,
					Notes = employee.Notes,
					ReportsTo = employee.ReportsTo,
					PhotoPath = employee.PhotoPath,
					Password = employee.Password
				};
			}
		}

		/// <summary>
		/// Updates the photo of an employee.
		/// </summary>
		/// <param name="employeeID">Employee's id whose photo must be changed.</param>
		/// <param name="photo">The photo in <see cref="byte[]"/></param>
		/// <returns></returns>
		public static EmployeeModel UpdateEmployeePhoto(int employeeID, byte[] photo)
		{
			using (var context = new NorthwindEntities())
			{
				var employee = context.Employees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
				employee.Photo = photo;

				context.SaveChanges();

				return new EmployeeModel
				{
					EmployeeID = employee.EmployeeID,
					LastName = employee.LastName,
					FirstName = employee.FirstName,
					Title = employee.Title,
					TitleOfCourtesy = employee.TitleOfCourtesy,
					BirthDate = employee.BirthDate,
					HireDate = employee.HireDate,
					Address = employee.Address,
					City = employee.City,
					PostalCode = employee.PostalCode,
					Country = employee.Country,
					HomePhone = employee.HomePhone,
					Photo = employee.Photo,
					Notes = employee.Notes,
					ReportsTo = employee.ReportsTo,
					PhotoPath = employee.PhotoPath,
					Password = employee.Password
				};
			}
		}

		/// <summary>
		/// Updates the phone, birth and hire dates for a specific employee.
		/// </summary>
		/// <param name="employeeID">Employee's id.</param>
		/// <param name="homePhone">Employee's home phone.</param>
		/// <param name="birthDate">Employee's birth date.</param>
		/// <param name="hireDate">Employee's hire date.</param>
		/// <returns></returns>
		public static EmployeeModel UpdateEmployeePhoneAndDates(int employeeID, string homePhone, DateTime? birthDate, DateTime? hireDate)
		{
			using (var context = new NorthwindEntities())
			{
				var employee = context.Employees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
				employee.HomePhone = homePhone;
				employee.BirthDate = birthDate;
				employee.HireDate = hireDate;

				context.SaveChanges();

				return new EmployeeModel
				{
					EmployeeID = employee.EmployeeID,
					LastName = employee.LastName,
					FirstName = employee.FirstName,
					Title = employee.Title,
					TitleOfCourtesy = employee.TitleOfCourtesy,
					BirthDate = employee.BirthDate,
					HireDate = employee.HireDate,
					Address = employee.Address,
					City = employee.City,
					PostalCode = employee.PostalCode,
					Country = employee.Country,
					HomePhone = employee.HomePhone,
					Photo = employee.Photo,
					Notes = employee.Notes,
					ReportsTo = employee.ReportsTo,
					PhotoPath = employee.PhotoPath,
					Password = employee.Password
				};
			}
		}

		/// <summary>
		/// Updates employee's password.
		/// </summary>
		/// <param name="employeeID">The employee's ID.</param>
		/// <param name="newPassword">The new password.</param>
		public static void UpdateEmployeePassword(int employeeID, string newPassword)
		{
			using(var context = new NorthwindEntities())
			{
				var employee = context.Employees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
				employee.Password = newPassword;
				context.SaveChanges();
			}
		}
	}
}
