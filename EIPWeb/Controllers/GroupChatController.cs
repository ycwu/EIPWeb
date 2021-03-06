﻿using EIPWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIPWeb.Controllers
{
    public class GroupChatController : BaseController
    {
        // GET: GroupChat
        public ActionResult Index()
        {
            GroupChatViewModel model = new GroupChatViewModel();
            ViewBag.Department = GetDepartmentSelectList(null);
            ViewBag.Employee = GetEmployeeSelectList(null);
            return View(model);
        }
    }
}