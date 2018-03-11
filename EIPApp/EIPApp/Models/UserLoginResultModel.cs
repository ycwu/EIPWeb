using System;
using System.Collections.Generic;
using System.Text;

namespace EIPApp.Models
{
    public class UserLoginResultModel
    {
        public string AccessToken { get; set; } = "";
        public Users MyUser { get; set; } = new Users();
    }
}
