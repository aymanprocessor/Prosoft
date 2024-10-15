using ProSoft.EF.IRepositories.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.SMS
{
    public class WhySMSRepo : ISMSService
    {
        private readonly string _type;
        private readonly string _authToken;
        private readonly string _senderId;

        public WhySMSRepo( string authToken, string senderId, string type = "plain")
        {
            _type = type;
            _authToken = authToken;
            _senderId = senderId;
        }

        public void SendSMS(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }

        public void SendSMS(List<string> phoneNumberList, string message)
        {
            throw new NotImplementedException();
        }
    }
}
