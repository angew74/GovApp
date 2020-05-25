using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class LoginModel
    {

      /*  [UIHint("email")]       */
        public string Email { get; set; }

      
        public string Password { get; set; }
       
      
        public string Username { get; set; }

/*
        [UIHint("rememberme")]
        public string RememberMe { get; set; }

    */
    /*
        [UIHint("returnurl")]*/
        public string ReturnUrl { get; set; }
    }
}
