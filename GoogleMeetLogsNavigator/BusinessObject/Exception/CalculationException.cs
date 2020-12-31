namespace GoogleMeetLogsNavigator.BusinessObject.Exception
{
    /// <summary>
    /// The Calculation exception.
    /// Raised when occurred an error in <see cref="GoogleMeetLogsNavigator.BusinessObject.GoogleMeetMissingDataCalculator"/>
    /// </summary>
    public class CalculationException : System.Exception
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        public CalculationException(string message) : base(message)
        {

        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="message">The Message</param>
        /// <param name="exception">The parent Exception</param>
        public CalculationException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
