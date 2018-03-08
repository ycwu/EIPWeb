using System;
using System.Collections.Generic;
using System.Text;

namespace EIPApp.Helpers
{
    /// <summary>
    /// 這是整個應用程式都可以存取的一個支援類別屬性與方法
    /// </summary>
    public class MainHelper
    {
        #region 常用的變數字串
        #region 向 Azure Mobile App 服務的主要網址
        //public const string MainURL = "http://xamarinlobform.azurewebsites.net/";
        public const string MainURL = "http://169.254.80.80/EIPWeb/";
        #endregion

        #region 呼叫 API 的最上層名稱
        public static string BaseAPIUrl = $"{MainURL}api/";
        #endregion

        #region 使用者身分驗證的 API 名稱
        public static string UserLoginAPIName = $"Login";
        public static string UserLoginAPIUrl = $"{BaseAPIUrl}{UserLoginAPIName}";
        #endregion

        #region 員工檔的API名稱
        public static string EmployeeAPIName = $"Employee";
        public static string EmployeeAPIUrl = $"{BaseAPIUrl}{EmployeeAPIName}";
        #endregion

        #region 部門檔的API名稱
        public static string DepartmentAPIName = $"Department";
        public static string DepartmentAPIUrl = $"{BaseAPIUrl}{DepartmentAPIName}";
        #endregion
        //#region 專案清單所有紀錄的 API 名稱
        //public static string ProjectAPIName = $"Project";
        //public static string ProjectAPIUrl = $"{BaseAPIUrl}{ProjectAPIName}";
        //#endregion

        //#region 請假類別清單所有紀錄的 API 名稱
        //public static string LeaveCategoryAPIName = $"LeaveCategory";
        //public static string LeaveCategoryAPIUrl = $"{BaseAPIUrl}{LeaveCategoryAPIName}";
        //#endregion

        //#region On-Call清單所有紀錄的 API 名稱
        //public static string OnCallPhoneAPIName = $"OnCallPhone";
        //public static string OnCallPhoneAPIUrl = $"{BaseAPIUrl}{OnCallPhoneAPIName}";
        //#endregion

        //#region 工作日誌清單所有紀錄的 API 名稱
        //public static string WorkingLogByUserIDAPIName = $"WorkingLog/ByUserID";
        //public static string WorkingLogAPIName = $"WorkingLog";
        //public static string WorkingLogByUserIDAPIUrl = $"{BaseAPIUrl}{WorkingLogByUserIDAPIName}";
        //public static string WorkingLogAPIUrl = $"{BaseAPIUrl}{WorkingLogAPIName}";
        //#endregion

        //#region 請假類清單所有紀錄的 API 名稱
        //public static string LeaveAppFormManagerMode = $"manager";
        //public static string LeaveAppFormUserMode = $"user";
        //public static string LeaveAppFormByUserIDAPIName = $"LeaveAppFormByUser";
        //public static string LeaveAppFormAPIName = $"LeaveAppForm";
        //public static string LeaveAppFormByUserIDAPIUrl = $"{BaseAPIUrl}{LeaveAppFormByUserIDAPIName}";
        //public static string LeaveAppFormAPIUrl = $"{BaseAPIUrl}{LeaveAppFormAPIName}";
        //#endregion

        #region 系統運作狀態的存取檔案名稱
        public static string SystemStatusFileName = $"SystemStatus";
        #endregion

        #region 寫入裝置內的檔案所在的主目錄名稱
        public static string 資料主目錄 = $"Data";
        #endregion

        #endregion

        #region 其他常用字串
        public static string Prism__NavigationMode { get; set; } = "__NavigationMode";
        public static string CRUDItemKeyName { get; set; } = "CRUDItem";
        public static string CRUDKeyName { get; set; } = "CRUDMode";
        public static string CRUDFromDetailKeyName { get; set; } = "DetailAction";
        public static string CRUD_Create { get; set; } = "Create";
        public static string CRUD_Update { get; set; } = "Update";
        public static string CRUD_Delete { get; set; } = "Delete";
        #endregion

        #region Repository (此處為方便開發，所以，所有的 Repository 皆為全域靜態可存取)
        #endregion

    }
}
