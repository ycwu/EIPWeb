namespace EIPWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ChatMessageMetaData))]
    public partial class ChatMessage
    {
    }
    
    public partial class ChatMessageMetaData
    {
        [Required]
        public System.Guid MessageID { get; set; }
        [Required]
        public System.Guid ConnectionID { get; set; }
        [Required]
        public System.Guid ChatroomID { get; set; }
        
        [StringLength(4000, ErrorMessage="欄位長度不得大於 4000 個字元")]
        [Required]
        public string Message { get; set; }
        
        [StringLength(1, ErrorMessage="欄位長度不得大於 1 個字元")]
        [Required]
        public string MessageType { get; set; }
        public Nullable<System.Guid> FileID { get; set; }
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
    
        public virtual ChatClient ChatClient { get; set; }
        public virtual Chatroom Chatroom { get; set; }
    }
}
