using GoogleMeetLogsNavigator.GoogleParser.GoogleEnum;
using System.Collections.Generic;

namespace GoogleMeetLogsNavigator.GoogleParser.nteface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICSVWriter<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delimiter"></param>
        void SetCSVDelimiter(string delimiter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationDictioanry"></param>
        void SetConfiguration(IDictionary<CSVHeaderEnum, bool> configurationDictionary);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logs"></param>
        string ToGoogleMeetCsv(IList<T> logs);
    }
}
