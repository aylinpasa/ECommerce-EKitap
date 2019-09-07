using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class SessionUserInfo
    {
        public static UserProfile User
        {
            get
            {
                return (UserProfile)HttpContext.Current.Session["SessionUserInfo_User"];
            }
            set
            {
                HttpContext.Current.Session["SessionUserInfo_User"] = value;
            }
        }
        public int UserId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }


    }
}