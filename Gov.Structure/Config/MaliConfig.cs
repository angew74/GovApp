using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Config
{
    public class MailConfig
    {
        public string mailFrom { get; set; }
        public string host { get; set; }
        public string password { get; set; }
        public int portNo { get; set; }

        public string senderName { get; set; }
    }
}
