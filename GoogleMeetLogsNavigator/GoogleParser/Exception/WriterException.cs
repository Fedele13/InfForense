using System;

namespace GoogleMeetLogsNavigator.GoogleParser.Exception
{
    public class WriterException : System.Exception
    {
        public WriterException(string message) : base(message)
        {

        }

        public WriterException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
