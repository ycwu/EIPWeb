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
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string SimplifiedName { get; set; }
        public string EnglishName { get; set; }
        public string PassportEnglishName { get; set; }
        public string IdentityCardID { get; set; }
        public Nullable<int> TitleID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string EMail { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }
        public string TelNO { get; set; }
        public string CellPhone { get; set; }
        public string MailingAddress { get; set; }
        public string ResidenceAddress { get; set; }
        public string SkillNote { get; set; }
        public string HobbyNote { get; set; }
        public string GraduatedSchool { get; set; }
        public string GraduatedDepartment { get; set; }
        public string HighestDegree { get; set; }
        public string Birthplace { get; set; }
        public string BloodType { get; set; }
        public string MarriageStatus { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<System.DateTime> ResignDate { get; set; }
        public string DepartmentID { get; set; }
        public string ExtensionNO { get; set; }
        public string SupervisorID { get; set; }
        public string UpSupervisorID { get; set; }
        public string JobAgentID { get; set; }
        public string ADLoginID { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactTitle { get; set; }
        public string EmergencyContactTelNO { get; set; }
        public string EmergencyContactCellPhone { get; set; }
        public string EmergencyContactAddress { get; set; }
        public string Guarantor { get; set; }
        public string GuarantorTitle { get; set; }
        public string GuarantorTelNo { get; set; }
        public string GuarantorCellPhone { get; set; }
        public string GuarantorAddress { get; set; }
        public string RevisedRecord { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string JobGroupID { get; set; }
        public Nullable<int> SortingID { get; set; }
        public Nullable<bool> UseConstructionForm { get; set; }
        public Nullable<bool> UseSS { get; set; }
        public Nullable<bool> UseCS { get; set; }
        public Nullable<bool> Available { get; set; }
        public string ID { get; set; }
        public string ContactNumber { get; set; }
        public string LiveAddress { get; set; }
        public string CensusRegisterAddress { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserID { get; set; }
        public string ModifyIP { get; set; }
        public string Note { get; set; }
        public string ShiftID { get; set; }
        public string EmployeeIdentityTypeID { get; set; }
        public string EmployeeIdentityTypeNote { get; set; }
        public string WorkStatusTypeID { get; set; }
        public string CostCenterID { get; set; }
    
        public virtual Department Department { get; set; }
    }
}
