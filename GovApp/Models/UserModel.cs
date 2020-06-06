using Gov.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class UserModel
    {
        public string userName { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string email {get;set;}

        public bool result { get; set; }
        public string sesso { get; set; }
        public string password { get; set; }
        public string codicefiscale { get; set; }
        public string role { get; set; }
        public bool isActive { get; set; }

        public string url { get; set; }
        public string _rowVariant { get { return this.isActive == false ? "danger" : "light"; } }
                
    }
}
