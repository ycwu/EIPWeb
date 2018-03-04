namespace EIPWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
        public MyUser ToMyUsers()
        {
            //DepartmentRepository repo = RepositoryHelper.GetDepartmentRepository();
            //private EIPWebEntities db = new EIPWebEntities();
            return new MyUser()
            {
                //MyUserId = this.MyUserId,
                DepartmentID = this.DepartmentID,
                DepartmentName = this.Department.DepartmentName,
                EmployeeID = this.EmployeeID,
                IsManager = true,
                //ManagerId = this.SupervisorID,
                Name = this.Name,
                //Password = this.Password,
            };
        }

        public bool IsManager()
        {
            bool bResult = false;            
            //Employee employee=
            return bResult;
        }
    }
    
    public partial class EmployeeMetaData
    {        
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
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string PassportEnglishName { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        public string IdentityCardID { get; set; }
        public Nullable<int> TitleID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string EMail { get; set; }
        public byte[] Photo { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string PhotoType { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string TelNO { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string CellPhone { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        public string MailingAddress { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        public string ResidenceAddress { get; set; }
        
        [StringLength(500, ErrorMessage="欄位長度不得大於 500 個字元")]
        public string SkillNote { get; set; }
        
        [StringLength(500, ErrorMessage="欄位長度不得大於 500 個字元")]
        public string HobbyNote { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string GraduatedSchool { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string GraduatedDepartment { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string HighestDegree { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string Birthplace { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string BloodType { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string MarriageStatus { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<System.DateTime> ResignDate { get; set; }
        
        [StringLength(5, ErrorMessage="欄位長度不得大於 5 個字元")]
        public string DepartmentID { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        public string ExtensionNO { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string SupervisorID { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string UpSupervisorID { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string JobAgentID { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string ADLoginID { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string EmergencyContact { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string EmergencyContactTitle { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string EmergencyContactTelNO { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string EmergencyContactCellPhone { get; set; }
        
        [StringLength(500, ErrorMessage="欄位長度不得大於 500 個字元")]
        public string EmergencyContactAddress { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string Guarantor { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string GuarantorTitle { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string GuarantorTelNo { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string GuarantorCellPhone { get; set; }
        
        [StringLength(500, ErrorMessage="欄位長度不得大於 500 個字元")]
        public string GuarantorAddress { get; set; }
        
        [StringLength(2000, ErrorMessage="欄位長度不得大於 2000 個字元")]
        public string RevisedRecord { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string JobGroupID { get; set; }
        public Nullable<int> SortingID { get; set; }
        public Nullable<bool> UseConstructionForm { get; set; }
        public Nullable<bool> UseSS { get; set; }
        public Nullable<bool> UseCS { get; set; }
        public Nullable<bool> Available { get; set; }
        
        [StringLength(15, ErrorMessage="欄位長度不得大於 15 個字元")]
        public string ID { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        public string ContactNumber { get; set; }
        
        [StringLength(200, ErrorMessage="欄位長度不得大於 200 個字元")]
        public string LiveAddress { get; set; }
        
        [StringLength(200, ErrorMessage="欄位長度不得大於 200 個字元")]
        public string CensusRegisterAddress { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string Gender { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string ModifyUserID { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string ModifyIP { get; set; }
        
        [StringLength(4000, ErrorMessage="欄位長度不得大於 4000 個字元")]
        public string Note { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string ShiftID { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string EmployeeIdentityTypeID { get; set; }
        
        [StringLength(500, ErrorMessage="欄位長度不得大於 500 個字元")]
        public string EmployeeIdentityTypeNote { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string WorkStatusTypeID { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string CostCenterID { get; set; }
    }
}
