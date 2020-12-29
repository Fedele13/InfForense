using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.BusinessObject.Exception
{
    public class CalculationException : System.Exception
    {
        public CalculationException(string message) : base(message)
        {

        }

        public CalculationException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
