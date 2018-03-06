using System;
using System.Collections.Generic;
using System.Text;
using EIPApp.Helpers;
using EIPApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EIPApp.Repositories
{
    public class SystemStatusRepository
    {
        public SystemStatus Item { get; set; }
        /// <summary>
        /// 將資料寫入到檔案內
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            string data = JsonConvert.SerializeObject(Item);
            await StorageUtility.WriteToDataFileAsync("", MainHelper.資料主目錄, MainHelper.SystemStatusFileName, data);
        }

        /// <summary>
        /// 將資料讀取出來
        /// </summary>
        /// <returns></returns>
        public async Task<SystemStatus> ReadAsync()
        {
            string data = "";
            data = await StorageUtility.ReadFromDataFileAsync("", MainHelper.資料主目錄, MainHelper.SystemStatusFileName);
            Item = JsonConvert.DeserializeObject<SystemStatus>(data);
            if (Item == null)
            {
                Item = new SystemStatus();
            }
            return Item;
        }
    }
}
