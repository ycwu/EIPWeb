using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EIPWeb.Models.ViewModel;

namespace EIPWeb.Controllers
{
    public class BaseApiController : ApiController
    {
        public bool IsAuthenticated(string userID, string password)
        {
            BaseController baseController = new BaseController();
            LoginViewModel user = new LoginViewModel();
            user.UserID = userID;
            user.Password = password;
            return baseController.IsAuthenticated(user);
        }
    }
}
