using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EIPWeb.Models.ViewModel
{
    public class LoginViewModel
    {
        public int? UniqueID { get; set; }

        //[StringLength(30, ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserIDStringLength")]
        //[Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserIDRequired")]
        //[Display(Name = "UserID", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
        public string UserID { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "PasswordRequired")]
        //[StringLength(30, ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "PasswordStringLength")]
        //[Display(Name = "Password", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "RememberMe", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
        public bool RememberMe { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserGroupRequired")]
        //[Display(Name = "UserGroup", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
        public string UserGroup { get; set; }

        public string PasswordSHA1 { get; set; }
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }
        public string LoginIP { get; set; }
        public bool LoginStatus { get; set; }
        public string Remark { get; set; }
        public string ReturnUrl { get; set; }
    }

    //public class RegisterViewModel
    //{
    //    public Guid VendorID { get; set; }

    //    [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "CompanyCodeRequired")]
    //    [Display(Name = "CompanyCode", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    public string CompanyCode { get; set; }

    //    [StringLength(30, ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserIDStringLength")]
    //    [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserIDRequired")]
    //    [Display(Name = "UserID", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    public string UserID { get; set; }

    //    [StringLength(30, ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserNameStringLength")]
    //    [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserNameRequired")]
    //    [Display(Name = "UserName", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    public string UserName { get; set; }

    //    [StringLength(30, ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "PasswordStringLength")]
    //    [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "PasswordRequired")]
    //    [Display(Name = "Password", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserGroupRequired")]
    //    [Display(Name = "UserGroup", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    public string UserGroup { get; set; }

    //    [Display(Name = "Remark", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    public string Remark { get; set; }
    //}

    //public class ChangePasswordViewModel
    //{
    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "OldPassword", ResourceType = typeof(Resources.Models.ViewModel.ChangePasswordViewModel))]
    //    public string OldPassword { get; set; }

    //    [Required]
    //    [StringLength(30, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.Models.ViewModel.ChangePasswordViewModel), ErrorMessageResourceName = "NewPasswordStringLength")]
    //    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{1,30}$", ErrorMessageResourceType = typeof(Resources.Models.ViewModel.ChangePasswordViewModel), ErrorMessageResourceName = "NewPasswordRegularExpression")]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "NewPassword", ResourceType = typeof(Resources.Models.ViewModel.ChangePasswordViewModel))]
    //    public string NewPassword { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Models.ViewModel.ChangePasswordViewModel))]
    //    [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Models.ViewModel.ChangePasswordViewModel), ErrorMessageResourceName = "ConfirmPasswordCompare")]
    //    public string ConfirmPassword { get; set; }
    //}

    //public class RoleViewModel
    //{
    //    public string RoleCode { get; set; }
    //    public string RoleName { get; set; }
    //}

    //public class AgreementViewModel
    //{
    //    public string VendorCode { get; set; }
    //    public string VendorName { get; set; }
    //}

    //public class ResetViewModel
    //{
    //    [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "UserIDRequired")]
    //    [Display(Name = "UserID", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    public string UserID { get; set; }

    //    [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "PasswordRequired")]
    //    [Display(Name = "Password", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [Compare("Password", ErrorMessageResourceType = typeof(Resources.Models.ViewModel.AccountViewModel), ErrorMessageResourceName = "PasswordConfirmCompare")]
    //    [Display(Name = "PasswordConfirm", ResourceType = typeof(Resources.Models.ViewModel.AccountViewModel))]
    //    [DataType(DataType.Password)]
    //    public string PasswordConfirm { get; set; }
    //}

    //public class SetPasswordViewModel
    //{
    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "New password")]
    //    public string NewPassword { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm new password")]
    //    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}
}
