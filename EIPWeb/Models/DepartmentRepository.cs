using System;
using System.Linq;
using System.Collections.Generic;
	
namespace EIPWeb.Models
{   
	public  class DepartmentRepository : EFRepository<Department>, IDepartmentRepository
	{
        public bool IsManager(string employeeID)
        {
            bool bResult = false;
            Department department = All().FirstOrDefault(m => m.Leader == employeeID);
            if (department != null)
                bResult = true;
            return bResult;
        }

        public string GetDepartmentName(string departmentID)
        {
            string result = "";
            Department department= base.All().FirstOrDefault(m => m.DepartmentID == departmentID);
            if (department != null)
                result = department.DepartmentName;
            return result;
        }

    }

	public  interface IDepartmentRepository : IRepository<Department>
	{

	}
}