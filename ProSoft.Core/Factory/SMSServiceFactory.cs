using ProSoft.Core.Enums;
using ProSoft.Core.Repositories.SMS;
using ProSoft.EF.IRepositories.SMS;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProSoft.Core.Factory
{
    public static class SMSServiceFactory
    {
       
        public static ISMSService CreateSMSService(SMSProvider provider,IConfiguration configuration)
        {
            var configSection = configuration.GetSection($"SMSProvider:{provider.ToString()}");
            var authToken = configSection.GetValue<string>("AuthToken");
            var senderId = configSection.GetValue<string>("SenderId");

            switch (provider)
            {
                case SMSProvider.WhySMS:
                    return new WhySMSRepo("ProviderAAuthToken", "ProviderASenderId");
                
                    
                default:
                    throw new ArgumentException("Invalid provider name");
            }
        }
    }
}
