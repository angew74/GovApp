using Gov.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class UserModel
    {
        public string name { get; set; }
        public string email {get;set;}
        public string role { get; set; }
    }
}
