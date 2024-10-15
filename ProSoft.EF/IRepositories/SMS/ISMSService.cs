using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.SMS
{
    public interface ISMSService
    {
        void SendSMS(string phoneNumber, string message);
        void SendSMS(List<string> phoneNumberList, string message);


    }
}
