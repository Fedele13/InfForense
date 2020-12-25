using GoogleMeetLogsNavigator.GoogleParser;
using System.Collections.Generic;

namespace GoogleMeetLogsNavigator.Inteface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ICSVWriter<T>
    {
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
