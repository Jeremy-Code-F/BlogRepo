using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMVC
{
    public class PasswordData
    {
        public string PassWord { get; set; }
        public string Salt { get; set; }

        public PasswordData(string pass, string salt)
        {
            PassWord = pass;
            Salt = salt;
        }
    }
}
