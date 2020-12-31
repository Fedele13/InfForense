using System.Collections.Generic;

namespace GoogleMeetLogsNavigator.GoogleParser.Interface
{
    /// <summary>
    /// The ICSV Reader Interface
    /// </summary>
    /// <typeparam name="T">Treated type</typeparam>
    public interface ICSVReader<T>
    {   
        /// <summary>
        /// The Complete Data Dictinary prop
        /// </summary>
        IDictionary<string, T> MeetingDictionary { get; }
    }
}
