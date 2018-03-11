namespace EIPWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
        private EIPWebEntities db = new EIPWebEntities();
        private EmployeeRepository repoEmployee = RepositoryHelper.GetEmployeeRepository();
        private DepartmentRepository repoDepartment = RepositoryHelper.GetDepartmentRepository();

        public MyUser ToMyUsers()
        {
            //DepartmentRepository repo = RepositoryHelper.GetDepartmentRepository();
            //Department department = db.Department.Find(this.DepartmentID);
            return new MyUser()
            {
                //MyUserId = this.ID,
                UserID = this.ADLoginID,
                UserName = this.Name,
                DepartmentID = this.DepartmentID,
                DepartmentName = repoDepartment.GetDepartmentName(this.DepartmentID),
                EmployeeID = this.EmployeeID,
                IsManager = IsManager(),
            };
        }

        public bool IsManager()
        {
            return repoDepartment.IsManager(this.EmployeeID);
        }
    }
    
    public partial class EmployeeMetaData
    {
        public int ID { get; set; }

        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        [Required]
        public string EmployeeID { get; set; }
        
        [StringLength(15, ErrorMessage="欄位長度不得大於 15 個字元")]
        [Required]
        public string Name { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string SimplifiedName { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string EnglishName { get; set; }
        
        public Nullable<int> TitleID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string EMail { get; set; }
              
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string TelNO { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string CellPhone { get; set; }
        
        public Nullable<System.DateTime> ResignDate { get; set; }
        
        [StringLength(5, ErrorMessage="欄位長度不得大於 5 個字元")]
        public string DepartmentID { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        public string ExtensionNO { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string SupervisorID { get; set; }
                     
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string ADLoginID { get; set; }
                       
        public Nullable<bool> IsAdmin { get; set; }
        
        public Nullable<bool> Available { get; set; }
                
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string Gender { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string ModifyUserID { get; set; }
    }
}
