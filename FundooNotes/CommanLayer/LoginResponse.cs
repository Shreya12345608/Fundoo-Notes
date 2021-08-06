using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer
{
   public class LoginResponse
    {
        public int Userid { get; set; }
       public string FirstName { get; set; }
        //For Last Name
        public string LastName { get; set; }
        //For Email
        public string UserEmail { get; set; }
    }
}
