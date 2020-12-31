using GoogleMeetLogsNavigator.GoogleParser.GoogleEnum;
using System.Collections.Generic;
using System.Text;

namespace GoogleMeetLogsNavigator.GoogleParser.nteface
{
    /// <summary>
    /// The ICSV Writer interface
    /// </summary>
    /// <typeparam name="T">Treated type</typeparam>
    public interface ICSVWriter<T>
    {
        /// <summary>
        /// Set the delimter used to build the csv file
        /// </summary>
        /// <param name="delimiter">The delimiter</param>
        public void SetCSVDelimiter(string delimiter);

        /// <summary>
        /// Set the configuration dictionary to build the csv file with selected columns
        /// </summary>
        /// <param name="configurationDictioanry">The Configuration Dictionary <columnName, true/false></param>
        public void SetConfiguration(IDictionary<CSVHeaderEnum, bool> configurationDictionary);

        /// <summary>
        /// Create csv contetn
        /// </summary>
        /// <param name="logs">Logs to write in csv file</param>
        /// <return>The string content in csv format</return>
        public string ToGoogleMeetCsv(IList<T> logs);

        /// <summary>
        /// The csv Encoding
        /// </summary>
        /// <param name="encoding">The Encoding</param>
        public void SetEncoding(Encoding encoding);
    }
}
