using System;
using System.Linq;
using System.Collections.Generic;
	
namespace EIPWeb.Models
{   
	public  class EmployeeRepository : EFRepository<Employee>, IEmployeeRepository
	{
        //public IQueryable<Employee> GetList()
        //{
        //    return base.All().Where(m => m.ResignDate == null);
        //}
    }

	public  interface IEmployeeRepository : IRepository<Employee>
	{

	}
}