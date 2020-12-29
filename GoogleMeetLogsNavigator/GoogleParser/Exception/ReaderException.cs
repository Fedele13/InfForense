using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.GoogleParser.Exception
{
    public class ReaderException : System.Exception
    {
        public ReaderException(string message) : base(message)
        {
            
        }

        public ReaderException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
