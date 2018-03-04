using System;
using System.Linq;
using System.Collections.Generic;
	
namespace EIPWeb.Models
{   
	public  class EmployeeRepository : EFRepository<Employee>, IEmployeeRepository
	{

	}

	public  interface IEmployeeRepository : IRepository<Employee>
	{

	}
}