namespace EIPWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ChatroomMetaData))]
    public partial class Chatroom
    {
    }
    
    public partial class ChatroomMetaData
    {
        [Required]
        public System.Guid ChatroomID { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string ChatroomName { get; set; }
        
        [StringLength(1, ErrorMessage="欄位長度不得大於 1 個字元")]
        [Required]
        public string ChatroomType { get; set; }
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
    
        public virtual ICollection<ChatMessage> ChatMessage { get; set; }
        public virtual ICollection<ChatroomDetail> ChatroomDetail { get; set; }
    }
}
