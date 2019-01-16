using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace DigitalMVC.Controllers
{
    public class AccountController : Controller
    {
        //http://localhost:53579/api/createaccount <- Returns success now (when running)
        [Route("/api/CreateAccount")]
        [HttpPost] 
        public async Task<string> CreateAccount([FromBody] LoginModel login)
        {
            DBConnectionHelper dbHelp = new DBConnectionHelper("digitaloceanmvc");

            string returnValue;
            Tuple<string, string> loginInfo = SHAHash.HashPassword(login.username, login.password);
            Console.WriteLine(loginInfo.Item1 + loginInfo.Item2);// Item 1 Contains the password * item2 contains the salt

            //store username, password, email, salt in db
            dbHelp.IsConnect();
            try
            {
                returnValue = await dbHelp.InsertUser(login.username, loginInfo.Item1, login.email, loginInfo.Item2);
            
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                returnValue = ex.Message;
            }
            finally
            {
                dbHelp.Connection.Close();
                
            }
            return returnValue;


        }

        [Route("/api/TestEnd")]
        [HttpGet]
        public string TestEnd()
        {
            return "{\"Success\": \"test\"}";
        }


        [Route("/api/LoginUser")]
        [HttpPost]
        public async Task<string> LoginUser([FromBody] LoginModel login)
        {
            DBConnectionHelper dbHelp = new DBConnectionHelper("digitaloceanmvc");

            string returnValue;
            Tuple<string, string> loginInfo = SHAHash.HashPassword(login.username, login.password);
            Console.WriteLine(loginInfo.Item1 + loginInfo.Item2);// Item 1 Contains the password * item2 contains the salt

            //store username, password, email, salt in db
            dbHelp.IsConnect();
            try
            {
                returnValue = await dbHelp.ConfirmPassword(login.username, loginInfo.Item1);
              //
                   try
                {
     
                    HttpContext.Session.SetString("UserName", returnValue);
    
                }catch(Exception ex)
                {
                    return ex.ToString();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                returnValue = null;
            }
            finally
            {
                dbHelp.Connection.Close();

            }
            return returnValue;


        }

    }
}