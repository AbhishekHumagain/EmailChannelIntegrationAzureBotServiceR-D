using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office365EmailIntegrationAPI.Model
{
    public class ResourceInformation
    {
        public string ResourceGroupName { get; set; }
        public string BotName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string SubscriptionId { get; set; }
    }
}
