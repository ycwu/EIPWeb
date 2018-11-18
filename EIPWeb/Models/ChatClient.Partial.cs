namespace EIPWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ChatClientMetaData))]
    public partial class ChatClient
    {
    }
    
    public partial class ChatClientMetaData
    {
        [Required]
        public int ChatClientID { get; set; }
        [Required]
        public System.Guid ConnectionID { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string UserID { get; set; }
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
    }
}
