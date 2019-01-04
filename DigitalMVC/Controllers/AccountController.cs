using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMVC.Controllers
{
    public class AccountController : Controller
    {
        //http://localhost:53579/api/createaccount <- Returns success now (when running)
        [HttpPost]
        [Route("/api/CreateAccount")]
        public bool CreateAccount([FromBody] LoginModel login)
        {
            DBConnectionHelper dbHelp = new DBConnectionHelper("digitaloceanmvc");
       
    
            Tuple<string, string> loginInfo = SHAHash.HashPassword(login.username, login.password);
            Console.WriteLine(loginInfo.Item1 + loginInfo.Item2);// Item 1 Contains the password * item2 contains the salt

            //store username, password, email, salt in db
            dbHelp.IsConnect();
            try
            {
                dbHelp.InsertUser(login.username, loginInfo.Item1, login.email, loginInfo.Item2);
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            dbHelp.Connection.Close();

            return true;
        }
    }
}