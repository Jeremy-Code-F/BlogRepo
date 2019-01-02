using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMVC.Controllers
{
    public class AccountController : Controller
    {
        //http://localhost:53579/api/createaccount <- Returns success now
        [HttpPost]
        [Route("/api/CreateAccount")]
        public bool CreateAccount([FromBody] string userName, string password)
        {
            //TODO: Way to send this that CANNOT be replicated from someone else
    
            Tuple<string, string> loginInfo = SHAHash.HashPassword(userName, password);
            Console.WriteLine(loginInfo.Item1);//Contains the username * item2 contains the password
            
            return true;
        }
    }
}