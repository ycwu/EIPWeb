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
    
    public partial class ChatroomDetail
    {
        public int ChatroomID { get; set; }
        public System.Guid ConnectionID { get; set; }
        public Nullable<System.DateTime> DisconnectTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUser { get; set; }
        public bool IsEnable { get; set; }
    
        public virtual Chatroom Chatroom { get; set; }
    }
}
