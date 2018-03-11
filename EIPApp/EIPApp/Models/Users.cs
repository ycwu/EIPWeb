using System;
using System.Collections.Generic;
using System.Text;

namespace EIPApp.Models
{
    /// <summary>
    /// 使用者
    /// </summary>
    public class Users : ICloneable
    {
        public int MyUserId { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }
        public string UserName { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerId { get; set; }
        public bool IsManager { get; set; }
        //public DateTimeOffset? CreatedAt { get; set; }
        //public DateTimeOffset? UpdatedAt { get; set; }

        public Users Clone()
        {
            return this.MemberwiseClone() as Users;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
