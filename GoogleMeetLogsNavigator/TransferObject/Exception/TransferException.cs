namespace GoogleMeetLogsNavigator.TransferObject.Exception
{
    /// <summary>
    /// The Transfer Exception
    /// Raised in <see cref="GoogleMeetLogModel">
    /// </summary>
    public class TransferException : System.Exception
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        public TransferException(string message) : base(message)
        {

        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        /// <param name="exception">The parent exception</param>
        public TransferException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
