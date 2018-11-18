using EIPWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EIPWeb.Controllers
{
    public class ChatroomController : ApiController
    {
        private EIPWebEntities db = new EIPWebEntities();
        APIResult fooResult = new APIResult();

        public APIResult Get()
        {
            fooResult.Success = true;
            fooResult.Message = $"";
            fooResult.TokenFail = false;
            fooResult.Payload = db.Chatroom.Where(m => m.IsEnable == true).ToList();
            return fooResult;
        }

        // 查詢某筆聊天室資料 GET: api/Chatroom/聊天室1
        public APIResult Get(string roomName)
        {
            var chatroom = db.Chatroom.FirstOrDefault(x => x.ChatroomName == roomName.Trim());
            if (chatroom != null)
            {
                fooResult.Success = true;
                fooResult.Message = $"";
                fooResult.TokenFail = false;
                fooResult.Payload = chatroom;
            }
            else
            {
                chatroom = new Chatroom();
                chatroom.ChatroomID = Guid.NewGuid();
                chatroom.ChatroomName = roomName;
                chatroom.ChatroomType = "S";
                ChatroomRepository repoChatroom = RepositoryHelper.GetChatroomRepository();
                repoChatroom.SaveChanges(chatroom);
                fooResult.Success = true;
                fooResult.Message = $"";
                fooResult.TokenFail = false;
                fooResult.Payload = chatroom;
            }
            return fooResult;
        }
    }
}
