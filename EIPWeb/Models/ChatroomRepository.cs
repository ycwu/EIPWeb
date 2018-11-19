using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Web;

namespace EIPWeb.Models
{   
	public  class ChatroomRepository : EFRepository<Chatroom>, IChatroomRepository
	{
        public Chatroom Find(string roomName)
        {
            return base.All().FirstOrDefault(p => p.ChatroomName == roomName.Trim());
        }
        public Chatroom Find(int roomID)
        {
            return base.All().FirstOrDefault(p => p.ChatroomID == roomID);
        }
        public bool SaveChanges(Chatroom chatroom)
        {
            bool bResult = false;
            try
            {
                if (chatroom.CreateUser == null)
                {
                    chatroom.CreateTime = DateTime.Now;
                    chatroom.CreateUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
                    chatroom.IsEnable = true;
                    base.Add(chatroom);
                }
                else
                {
                    chatroom.ModifyTime = DateTime.Now;
                    chatroom.ModifyUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
                    var db = UnitOfWork.Context;
                    db.Entry(chatroom).State = EntityState.Modified;
                }
                base.UnitOfWork.Commit();
                bResult = true;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                    foreach (var vErrors in entityErrors.ValidationErrors)
                        throw new DbEntityValidationException(vErrors.PropertyName + ":" + vErrors.ErrorMessage);
            }
            return bResult;
        }

        public bool RemoveRoom(Chatroom chatroom)
        {
            bool bResult = false;
            try
            {
                chatroom.IsEnable = false;
                var db = UnitOfWork.Context;
                db.Entry(chatroom).State = EntityState.Modified;
                base.UnitOfWork.Commit();
                bResult = true;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                    foreach (var vErrors in entityErrors.ValidationErrors)
                        throw new DbEntityValidationException(vErrors.PropertyName + ":" + vErrors.ErrorMessage);
            }
            return bResult;
        }
    }

	public  interface IChatroomRepository : IRepository<Chatroom>
	{

	}
}