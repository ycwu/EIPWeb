﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIPWeb.Models
{
    /// <summary>
    /// 使用者
    /// </summary>
    public class MyUser
    {
        public int MyUserId { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public string Password { get; set; }
        public string SupervisorID { get; set; }
        //public bool IsManager { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}