using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Web;

namespace EIPWeb.Models
{   
	public  class ChatClientRepository : EFRepository<ChatClient>, IChatClientRepository
	{
        public ChatClient Find(Guid connectionID)
        {
            return base.All().FirstOrDefault(p => p.ConnectionID == connectionID);
        }

        public IQueryable<ChatClient> Get(string userID)
        {
            return base.All().Where(p => p.UserID == userID);
        }

        public bool SaveChanges(ChatClient chatClient)
        {
            bool bResult = false;
            try
            {
                if (chatClient.CreateUser == null)
                {
                    chatClient.CreateTime = DateTime.Now;
                    chatClient.CreateUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
                    chatClient.IsEnable = true;
                    base.Add(chatClient);
                }
                else
                {
                    chatClient.ModifyTime = DateTime.Now;
                    chatClient.ModifyUser = (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ? Environment.MachineName : HttpContext.Current.User.Identity.Name;
                    var db = UnitOfWork.Context;
                    db.Entry(chatClient).State = EntityState.Modified;
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

	public  interface IChatClientRepository : IRepository<ChatClient>
	{

	}
}