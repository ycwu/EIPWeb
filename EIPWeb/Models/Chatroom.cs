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
    
    public partial class Chatroom
    {
        public System.Guid ChatroomID { get; set; }
        public string ChatroomName { get; set; }
        public string ChatroomType { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string CreateDevice { get; set; }
    }
}
