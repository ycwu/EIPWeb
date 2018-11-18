using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Web;

namespace EIPWeb.Models
{   
	public  class ChatroomDetailRepository : EFRepository<ChatroomDetail>, IChatroomDetailRepository
	{
        public ChatroomDetail Find(Guid chatroomID, Guid connectionID)
        {
            return base.All().FirstOrDefault(p => p.ChatroomID == chatroomID && p.ConnectionID == connectionID);
        }

        public bool SaveChanges(ChatroomDetail chatroomDetail)
        {
            bool bResult = false;
            try
            {
                if (chatroomDetail.CreateUser == null)
                {
                    chatroomDetail.CreateTime = DateTime.Now;
                    chatroomDetail.CreateUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
                    chatroomDetail.IsEnable = true;
                    base.Add(chatroomDetail);
                }
                else
                {
                    chatroomDetail.ModifyTime = DateTime.Now;
                    chatroomDetail.ModifyUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
                    var db = UnitOfWork.Context;
                    db.Entry(chatroomDetail).State = EntityState.Modified;
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
    }

	public  interface IChatroomDetailRepository : IRepository<ChatroomDetail>
	{

	}
}