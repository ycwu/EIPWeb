﻿using System;
using System.Collections.Generic;
using System.Text;
using EIPApp.Helpers;
using EIPApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EIPApp.Repositories
{
    public class EmployeeRepository
    {
        /// <summary>
        /// 呼叫 API 回傳後，回報的結果內容
        /// </summary>
        public APIResult fooAPIResult { get; set; } = new APIResult();
        /// <summary>
        /// 身分驗證成功後的使用者紀錄
        /// </summary>
        public List<Employee> Items { get; set; } = new List<Employee>();


        /// <summary>
        /// 員工檔-全部
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<APIResult> GetAsync()
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        string FooAPIUrl = $"{MainHelper.EmployeeAPIUrl}";
                        HttpResponseMessage response = null;

                        // Accept 用於宣告客戶端要求服務端回應的文件型態 (底下兩種方法皆可任選其一來使用)
                        //client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // 這裡是要存取 Azure Mobile 服務必須要指定的 Header
                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                        #region 傳入 Access Token
                        var fooSystemStatus = new SystemStatusRepository();
                        await fooSystemStatus.ReadAsync();
                        client.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                           fooSystemStatus.Item.AccessToken);
                        #endregion

                        #region  設定相關網址內容
                        var fooFullUrl = $"{FooAPIUrl}";
                        #endregion

                        response = await client.GetAsync(fooFullUrl);
                        #endregion

                        #region 處理呼叫完成 Web API 之後的回報結果
                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode == true)
                            {
                                #region 狀態碼為成功
                                // 取得呼叫完成 API 後的回報內容
                                String strResult = await response.Content.ReadAsStringAsync();
                                fooAPIResult = JsonConvert.DeserializeObject<APIResult>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                                if (fooAPIResult.Success == true)
                                {
                                    #region 讀取成功的回傳資料
                                    // 將 Payload 裡面的內容，反序列化為真實 .NET 要用到的資料
                                    Items = JsonConvert.DeserializeObject<List<Employee>>(fooAPIResult.Payload.ToString());
                                    if (Items == null)
                                    {
                                        Items = new List<Employee>();

                                        fooAPIResult = new APIResult
                                        {
                                            Success = false,
                                            Message = $"回傳的 API 內容不正確 : {fooAPIResult.Payload.ToString()}",
                                            Payload = null,
                                        };
                                    }
                                    else
                                    {
                                        await SaveAsync();
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region API 的狀態碼為 不成功
                                    Items = new List<Employee>();
                                    fooAPIResult = new APIResult
                                    {
                                        Success = false,
                                        Message = fooAPIResult.Message,
                                        Payload = Items,
                                    };
                                    #endregion
                                }
                                //await SaveAsync();
                                #endregion
                            }
                            else
                            {
                                fooAPIResult = new APIResult
                                {
                                    Success = false,
                                    Message = $"應用程式呼叫 API 發生異常{Environment.NewLine}錯誤代碼:{response.StatusCode}{Environment.NewLine}{response.ReasonPhrase}",
                                    TokenFail = false,
                                    Payload = null,
                                };
                            }
                        }
                        else
                        {
                            #region API 的狀態碼為 不成功
                            fooAPIResult = new APIResult
                            {
                                Success = false,
                                Message = $"狀態碼：{response.StatusCode}{Environment.NewLine}{response.ReasonPhrase}",
                                Payload = null,
                            };
                            #endregion
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        fooAPIResult = new APIResult
                        {
                            Success = false,
                            Message = ex.Message,
                            Payload = ex,
                        };
                    }
                }
            }

            return fooAPIResult;
        }

        /// <summary>
        /// 員工檔-全部
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<APIResult> GetByDepartmentIDAsync(string departmentID)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        string FooAPIUrl = $"{MainHelper.EmployeeAPIUrl}?DepartmentID={departmentID}";
                        HttpResponseMessage response = null;

                        // Accept 用於宣告客戶端要求服務端回應的文件型態 (底下兩種方法皆可任選其一來使用)
                        //client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // 這裡是要存取 Azure Mobile 服務必須要指定的 Header
                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                        #region 傳入 Access Token
                        var fooSystemStatus = new SystemStatusRepository();
                        await fooSystemStatus.ReadAsync();
                        client.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                           fooSystemStatus.Item.AccessToken);
                        #endregion

                        #region  設定相關網址內容
                        var fooFullUrl = $"{FooAPIUrl}";
                        #endregion

                        response = await client.GetAsync(fooFullUrl);
                        #endregion

                        #region 處理呼叫完成 Web API 之後的回報結果
                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode == true)
                            {
                                #region 狀態碼為成功
                                // 取得呼叫完成 API 後的回報內容
                                String strResult = await response.Content.ReadAsStringAsync();
                                fooAPIResult = JsonConvert.DeserializeObject<APIResult>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                                if (fooAPIResult.Success == true)
                                {
                                    #region 讀取成功的回傳資料
                                    // 將 Payload 裡面的內容，反序列化為真實 .NET 要用到的資料
                                    Items = JsonConvert.DeserializeObject<List<Employee>>(fooAPIResult.Payload.ToString());
                                    if (Items == null)
                                    {
                                        Items = new List<Employee>();

                                        fooAPIResult = new APIResult
                                        {
                                            Success = false,
                                            Message = $"回傳的 API 內容不正確 : {fooAPIResult.Payload.ToString()}",
                                            Payload = null,
                                        };
                                    }
                                    else
                                    {
                                        await SaveAsync();
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region API 的狀態碼為 不成功
                                    Items = new List<Employee>();
                                    fooAPIResult = new APIResult
                                    {
                                        Success = false,
                                        Message = fooAPIResult.Message,
                                        Payload = Items,
                                    };
                                    #endregion
                                }
                                //await SaveAsync();
                                #endregion
                            }
                            else
                            {
                                fooAPIResult = new APIResult
                                {
                                    Success = false,
                                    Message = $"應用程式呼叫 API 發生異常{Environment.NewLine}錯誤代碼:{response.StatusCode}{Environment.NewLine}{response.ReasonPhrase}",
                                    TokenFail = false,
                                    Payload = null,
                                };
                            }
                        }
                        else
                        {
                            #region API 的狀態碼為 不成功
                            fooAPIResult = new APIResult
                            {
                                Success = false,
                                Message = $"狀態碼：{response.StatusCode}{Environment.NewLine}{response.ReasonPhrase}",
                                Payload = null,
                            };
                            #endregion
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        fooAPIResult = new APIResult
                        {
                            Success = false,
                            Message = ex.Message,
                            Payload = ex,
                        };
                    }
                }
            }

            return fooAPIResult;
        }

        /// <summary>
        /// 將資料寫入到檔案內
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            string data = JsonConvert.SerializeObject(Items);
            await StorageUtility.WriteToDataFileAsync("", MainHelper.資料主目錄, MainHelper.EmployeeAPIName, data);
        }

        /// <summary>
        /// 將資料讀取出來
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> ReadAsync()
        {
            string data = "";
            data = await StorageUtility.ReadFromDataFileAsync("", MainHelper.資料主目錄, MainHelper.EmployeeAPIName);
            Items = JsonConvert.DeserializeObject<List<Employee>>(data);
            if (Items == null)
            {
                Items = new List<Employee>();
            }
            return Items;
        }
    }
}
