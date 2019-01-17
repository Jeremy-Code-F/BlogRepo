using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace DigitalMVC.Models
{
    public class CheckAuthorization
    {
        public static bool CheckAuth(ISession session)
        {
            if (session.GetString("UserName") == null)
            {
                //TODO: Redirect to login view

                return false;
            }

            return true;
        }
    }
}
