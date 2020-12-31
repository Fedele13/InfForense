namespace GoogleMeetLogsNavigator.GoogleParser.Exception
{
    /// <summary>
    /// The Reader Exception.
    /// Raised in <see cref="GoogleMeetCSVWriter">
    /// </summary>
    public class WriterException : System.Exception
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        public WriterException(string message) : base(message)
        {

        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        /// <param name="exception">The parent exception</param>
        public WriterException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
