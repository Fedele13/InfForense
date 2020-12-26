using System.Collections.Generic;

namespace GoogleMeetLogsNavigator.GoogleParser.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICSVReader<T>
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, T> MeetingDictionary { get; }
    }
}
