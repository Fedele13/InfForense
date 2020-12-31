namespace GoogleMeetLogsNavigator.GoogleParser.Exception
{
    /// <summary>
    /// The Reader Exception.
    /// Raised in <see cref="GoogleMeetCSVReader">
    /// </summary>
    public class ReaderException : System.Exception
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        public ReaderException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        /// <param name="exception">The parent exception</param>
        public ReaderException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
