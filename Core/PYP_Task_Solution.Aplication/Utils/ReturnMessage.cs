using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Utils
{
    public class ReturnMessage
    {
        static public Dictionary<string, string> SendMessage
        {
            get
            {
                var message = new Dictionary<string, string>()
            {
             {"NoData", "No data found in the range you specified"},
             {"GenarateExcelError","Error occurred while generating excel file"},
             {"EmailSendingError","An error occurred while sending the report by mail"},
             {"RaportSucceded","The report has been sent to your e-mail address"},
            };
                return message;
            }
        }
    }
}
