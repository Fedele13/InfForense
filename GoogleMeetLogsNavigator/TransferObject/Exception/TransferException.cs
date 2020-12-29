using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.TransferObject.Exception
{
    public class TransferException : System.Exception
    {
        public TransferException(string message) : base(message)
        {

        }

        public TransferException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
