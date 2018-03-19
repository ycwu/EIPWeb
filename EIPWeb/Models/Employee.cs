//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EIPWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string SimplifiedName { get; set; }
        public string EnglishName { get; set; }
        public Nullable<int> TitleID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string EMail { get; set; }
        public string TelNO { get; set; }
        public string CellPhone { get; set; }
        public Nullable<System.DateTime> ResignDate { get; set; }
        public string DepartmentID { get; set; }
        public string ExtensionNO { get; set; }
        public string SupervisorID { get; set; }
        public string ADLoginID { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> Available { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserID { get; set; }
    
        public virtual Department Department { get; set; }
    }
}
