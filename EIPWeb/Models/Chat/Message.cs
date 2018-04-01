﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIPWeb.Models.Chat
{
    public class Message
    {
        public Guid MessageID { get; set; }
        public DateTime MessageTime { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string RoomID { get; set; }
        public string ConnectionID { get; set; }
        public string MessageText { get; set; }
    }
}