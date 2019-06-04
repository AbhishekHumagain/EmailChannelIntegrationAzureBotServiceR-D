using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office365EmailIntegrationAPI.Models
{
    public class EmailInformation
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string SmtpServer { get; set; }
    }
}
