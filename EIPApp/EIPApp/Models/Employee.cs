using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EIPApp.Models
{
    public class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
        public bool IsSelected { get; set; }
    }
}
