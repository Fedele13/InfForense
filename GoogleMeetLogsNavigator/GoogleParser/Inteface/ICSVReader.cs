using System.Collections.Generic;
using System.Text;

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
        public IDictionary<string, T> MeetingDictionary { get; }

        /// <summary>
        /// The Encoding
        /// </summary>
        public Encoding CSVTextEncoding { get; }
    }
}
