using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIPWeb.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Simple()
        {
            return View();
        }

        // GET: Chat
        public ActionResult Group()
        {
            return View();
        }
    }
}