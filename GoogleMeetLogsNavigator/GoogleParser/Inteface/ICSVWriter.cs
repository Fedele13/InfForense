using GoogleMeetLogsNavigator.GoogleParser.GoogleEnum;
using System.Collections.Generic;

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
        void SetCSVDelimiter(string delimiter);

        /// <summary>
        /// Set the configuration dictionary to build the csv file with selected columns
        /// </summary>
        /// <param name="configurationDictioanry">The Configuration Dictionary <columnName, true/false></param>
        void SetConfiguration(IDictionary<CSVHeaderEnum, bool> configurationDictionary);

        /// <summary>
        /// Create csv contetn
        /// </summary>
        /// <param name="logs">Logs to write in csv file</param>
        /// <return>The string content in csv format</return>
        string ToGoogleMeetCsv(IList<T> logs);
    }
}
