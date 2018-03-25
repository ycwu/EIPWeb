using EIPWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EIPWeb.Controllers
{
    public class EmployeeController : ApiController
    {
        private EIPWebEntities db = new EIPWebEntities();
        APIResult fooResult = new APIResult();

        public EmployeeController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // 取得所有員工檔 GET: api/Employee
        //public APIResult Get()
        //{
        //    fooResult.Success = true;
        //    fooResult.Message = $"";
        //    fooResult.TokenFail = false;
        //    fooResult.Payload = db.Employee.Where(m => m.ResignDate == null).Select(m=> new Employee()
        //    {
        //        ID = m.ID,
        //        EmployeeID = m.EmployeeID,
        //        Name = m.Name,
        //        SimplifiedName = m.SimplifiedName,
        //        EnglishName = m.EnglishName,
        //        TitleID = m.TitleID,
        //        Birthday = m.Birthday,
        //        EMail = m.EMail,
        //        TelNO = m.TelNO,
        //        CellPhone = m.CellPhone,
        //        ResignDate = m.ResignDate,
        //        DepartmentID = m.DepartmentID,
        //        ExtensionNO = m.ExtensionNO,
        //        SupervisorID = m.SupervisorID,
        //        ADLoginID = m.ADLoginID,
        //        IsAdmin = m.IsAdmin,
        //        Available = m.Available,
        //        Gender = m.Gender,
        //        ModifyTime = m.ModifyTime,
        //        ModifyUserID = m.ModifyUserID
        //    });
        //    return fooResult;
        //}

        public APIResult Get()
        {
            fooResult.Success = true;
            fooResult.Message = $"";
            fooResult.TokenFail = false;
            fooResult.Payload = db.Employee.Where(m => m.ResignDate == null).ToList();
            return fooResult;
        }

        // 查詢某筆工作日誌資料 GET: api/WorkingLog/5
        public APIResult Get(int id)
        {
            var fooItem = db.Employee.FirstOrDefault(x => x.ID == id);
            if (fooItem != null)
            {
                fooResult.Success = true;
                fooResult.Message = $"";
                fooResult.TokenFail = false;
                fooResult.Payload = fooItem;
            }
            else
            {
                fooResult.Success = true;
                fooResult.Message = $"";
                fooResult.TokenFail = false;
                fooResult.Payload = null;
            }
            return fooResult;
        }

        [Route("api/Employee/{departmentID}/Department")]
        public APIResult GetByDepartmentID(string departmentID)
        {
            fooResult.Success = true;
            fooResult.Message = $"";
            fooResult.TokenFail = false;
            fooResult.Payload = db.Employee.Where(m => m.DepartmentID == departmentID).ToList();
            return fooResult;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
