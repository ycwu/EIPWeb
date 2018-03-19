using EIPWeb.Models;
using EIPWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace EIPWeb.Controllers
{
    public class BaseController : Controller
    {
        #region AD驗證
        public bool IsAuthenticated(LoginViewModel user)
        {
            bool bResult = false;
            string domainAndUsername = @"office\" + user.UserID;
            try
            {
#if !DEBUG
                System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://192.168.222.5", domainAndUsername, user.Password);
                Object obj = entry.NativeObject;
                System.DirectoryServices.DirectorySearcher search = new System.DirectoryServices.DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + user.UserID + ")";
                search.PropertiesToLoad.Add("cn");
                System.DirectoryServices.SearchResult result = search.FindOne();
                if (result == null)
                    bResult = false;
                else
                    bResult = true;
#endif
#if DEBUG
                    bResult = true;            
#endif
            }
            catch //(Exception ex)
            {
                bResult = false;
            }
            finally
            {
            }
            return bResult;
        }
        #endregion

        #region 取得員工檔清單for DropDownList
        internal List<SelectListItem> GetEmployeeSelectList(string employeeSelected)
        {
            EmployeeRepository repoEmployee = RepositoryHelper.GetEmployeeRepository();
            var query = repoEmployee.All();
            List<SelectListItem> slResult = new List<SelectListItem>();
            foreach (var c in query)
            {
                slResult.Add(new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.ADLoginID,
                    Selected = c.ADLoginID.Equals(employeeSelected)
                });
            }
            repoEmployee.UnitOfWork.Context.Dispose();
            return slResult;
        }
        #endregion

        #region 取得部門檔清單for DropDownList
        internal List<SelectListItem> GetDepartmentSelectList(string departmentSelected)
        {
            DepartmentRepository repoDepartment = RepositoryHelper.GetDepartmentRepository();
            var query = repoDepartment.All();
            List<SelectListItem> slResult = new List<SelectListItem>();
            foreach (var c in query)
            {
                slResult.Add(new SelectListItem()
                {
                    Text = c.DepartmentName,
                    Value = c.DepartmentID,
                    Selected = c.DepartmentID.Equals(departmentSelected)
                });
            }
            repoDepartment.UnitOfWork.Context.Dispose();
            return slResult;
        }
        #endregion
    }
}