using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using EIPWeb.Models.Chat;

namespace EIPWeb.Models
{   
	public  class ChatMessageRepository : EFRepository<ChatMessage>, IChatMessageRepository
	{
        public bool SaveChanges(ChatMessage chatMessage)
        {
            bool bResult = false;
            try
            {
                if (chatMessage.CreateUser == null)
                {
                    chatMessage.CreateTime = DateTime.Now;
                    chatMessage.CreateUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
                    base.Add(chatMessage);
                }
                else
                {
                    chatMessage.ModifyTime = DateTime.Now;
                    chatMessage.ModifyUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
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

	public  interface IChatMessageRepository : IRepository<ChatMessage>
	{

	}
}