namespace EIPWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ChatroomDetailMetaData))]
    public partial class ChatroomDetail
    {
    }
    
    public partial class ChatroomDetailMetaData
    {
        [Required]
        public System.Guid ChatroomID { get; set; }
        [Required]
        public System.Guid ConnectionID { get; set; }
        public Nullable<System.DateTime> DisconnectTime { get; set; }
        [Required]
        public System.DateTime CreateTime { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string ModifyUser { get; set; }
        [Required]
        public bool IsEnable { get; set; }
    
        public virtual Chatroom Chatroom { get; set; }
    }
}
