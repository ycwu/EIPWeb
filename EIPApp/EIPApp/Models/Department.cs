using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EIPApp.Models
{
    public partial class Department : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentEngName { get; set; }
        public string Leader { get; set; }
        public string UpDepartmentID { get; set; }
        public Nullable<bool> Available { get; set; }
        public bool IsSelected { get; set; }
    }
}
