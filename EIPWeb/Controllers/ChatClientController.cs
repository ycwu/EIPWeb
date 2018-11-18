using EIPWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EIPWeb.Models;

namespace EIPWeb.Controllers
{
    public class ChatClientController : ApiController
    {
        private EIPWebEntities db = new EIPWebEntities();
        APIResult fooResult = new APIResult();

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
