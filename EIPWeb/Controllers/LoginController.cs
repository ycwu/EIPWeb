using EIPWeb.Helpers;
using EIPWeb.Models;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EIPWeb.Controllers
{
    public class LoginController : BaseApiController
    {
        private EIPWebEntities db = new EIPWebEntities();
        APIResult fooResult = new APIResult();
        public string Account { get; set; }
        public string Password { get; set; }

        // GET: api/Login
        public APIResult Get()
        {
            if (CanHandleAuthentication(this.Request) == true)
            {
                #region 檢查帳號與密碼是否正確
                // 這裡可以修改成為與後端資料庫內的使用者資料表進行比對
                var expectAccount = Account;
                var expectPassword = Password;
                if (IsAuthenticated(expectAccount, expectPassword))
                {
                    var myUser = db.Employee.FirstOrDefault(x => x.ADLoginID == expectAccount);
                    if (myUser != null)
                    {
                        #region 產生這次通過身分驗證的存取權杖 Access Token
                        string secretKey = MainHelper.SecretKey;
                        #region 設定該存取權杖的有效期限
                        IDateTimeProvider provider = new UtcDateTimeProvider();
                        // 這個 Access Token只有一個小時有效
                        var now = provider.GetNow().AddHours(1);
                        var unixEpoch = UnixEpoch.Value; // 1970-01-01 00:00:00 UTC
                        var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
                        #endregion

                        string[] fooRole;
                        if (myUser.IsManager() == true)
                        {
                            fooRole = new string[] { "Manager" };
                        }
                        else
                        {
                            fooRole = new string[0];
                        }
                        var jwtToken = new JwtBuilder()
                              .WithAlgorithm(new HMACSHA256Algorithm())
                              .WithSecret(secretKey)
                              .AddClaim("iss", Account)
                              .AddClaim("exp", secondsSinceEpoch)
                              .AddClaim("role", fooRole)
                              .AddClaim("manager", myUser.IsManager())
                              .Build();
                        #endregion

                        // 帳號與密碼比對正確，回傳帳密比對正確
                        this.Request.CreateResponse(HttpStatusCode.OK);
                        fooResult = new APIResult()
                        {
                            Success = true,
                            Message = $"",
                            TokenFail = false,
                            Payload = new UserLoginResultModel()
                            {
                                AccessToken = $"{jwtToken}",
                                MyUser = myUser.ToMyUsers()
                            }
                        };
                    }
                }
                else
                {
                    fooResult.Success = false;
                    fooResult.Message = $"使用者不存在或者帳號、密碼不正確";
                    fooResult.TokenFail = false;
                    fooResult.Payload = null;
                }
                #endregion
            }
            else
            {
                // 沒有收到正確格式的 Authorization 內容，回傳無法驗證訊息
                this.Request.CreateResponse(HttpStatusCode.Unauthorized);
                fooResult = new APIResult()
                {
                    Success = false,
                    TokenFail = false,
                    Message = $"",
                    Payload = "沒有收到帳號與密碼"
                };
            }
            return fooResult;
        }

        //POST: api/Login
        public APIResult Post([FromBody] UserLoginModel UserLoginModel)
        {
            var account = UserLoginModel.Account;
            var password = UserLoginModel.Password;
            if (IsAuthenticated(account, password))
            {
                var employee = db.Employee.FirstOrDefault(x => x.EmployeeID == account);
                if (employee != null)
                {
                    #region 產生這次通過身分驗證的存取權杖 Access Token
                    string secretKey = MainHelper.SecretKey;
                    #region 設定該存取權杖的有效期限
                    IDateTimeProvider provider = new UtcDateTimeProvider();
                    // 這個 Access Token只有一個小時有效
                    var now = provider.GetNow().AddHours(1);
                    var unixEpoch = UnixEpoch.Value; // 1970-01-01 00:00:00 UTC
                    var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
                    #endregion

                    string[] fooRole;
                    if (employee.IsManager())
                    {
                        fooRole = new string[] { "Manager" };
                    }
                    else
                    {
                        fooRole = new string[0];
                    }
                    var jwtToken = new JwtBuilder()
                          .WithAlgorithm(new HMACSHA256Algorithm())
                          .WithSecret(secretKey)
                          .AddClaim("iss", UserLoginModel.Account)
                          .AddClaim("exp", secondsSinceEpoch)
                          .AddClaim("role", fooRole)
                          .AddClaim("manager", employee.IsManager())
                          .Build();
                    #endregion

                    // 帳號與密碼比對正確，回傳帳密比對正確
                    this.Request.CreateResponse(HttpStatusCode.OK);
                    fooResult = new APIResult()
                    {
                        Success = true,
                        Message = $"",
                        TokenFail = false,
                        Payload = new UserLoginResultModel()
                        {
                            AccessToken = $"{jwtToken}",
                            MyUser = employee.ToMyUsers()
                        }
                    };
                }
                else
                {
                    fooResult.Success = false;
                    fooResult.Message = $"使用者不存在或者帳號、密碼不正確";
                    fooResult.TokenFail = false;
                    fooResult.Payload = null;
                }
            }
            return fooResult;
        }

        /// <summary>
        /// 檢查與解析 Authorization 標頭是否存在與解析用戶端傳送過來的帳號與密碼
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool CanHandleAuthentication(HttpRequestMessage request)
        {
            // 驗證結果是否正確
            bool isSuccess = false;

            #region 檢查是否有使用 Authorization: Basic 傳送帳號與密碼到 Web API 伺服器
            if ((request.Headers != null
                    && request.Headers.Authorization != null
                    && request.Headers.Authorization.Scheme.ToLowerInvariant() == "basic"))
            {
                #region 取出帳號與密碼，帳號與密碼格式為 帳號:密碼
                var authHeader = request.Headers.Authorization;

                // 取出有 Base64 編碼的帳號與密碼
                var encodedCredentials = authHeader.Parameter;
                // 進行 Base64 解碼
                var credentialBytes = Convert.FromBase64String(encodedCredentials);
                // 取得 .NET 字串
                var credentials = Encoding.ASCII.GetString(credentialBytes);
                // 判斷格式是否正確
                var credentialParts = credentials.Split(':');

                if (credentialParts.Length == 2)
                {
                    // 取出使用者傳送過來的帳號與密碼
                    Account = credentialParts[0];
                    Password = credentialParts[1];
                    isSuccess = true;
                }

                #endregion
            }
            #endregion
            return isSuccess;
        }
    }
}
