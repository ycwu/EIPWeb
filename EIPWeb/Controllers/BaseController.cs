using EIPWeb.Models.ViewModel;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace EIPWeb.Controllers
{
    public class BaseController : Controller
    {
        #region AD驗證
        public bool IsAuthenticated(LoginViewModel user)
        {
            bool bResult = false;
            string domainAndUsername = @"office\" + user.UserID;
            try
            {
#if !DEBUG
                System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://192.168.222.5", domainAndUsername, user.Password);
                Object obj = entry.NativeObject;
                System.DirectoryServices.DirectorySearcher search = new System.DirectoryServices.DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + user.UserID + ")";
                search.PropertiesToLoad.Add("cn");
                System.DirectoryServices.SearchResult result = search.FindOne();
                if (result == null)
                    bResult = false;
                else
                    bResult = true;
#endif
#if DEBUG
                    bResult = true;            
#endif
            }
            catch (Exception ex)
            {
                bResult = false;
            }
            finally
            {
            }
            return bResult;
        }
#endregion
    }
}