using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="logs"></param>
        void ToGoogleMeetCsv(IList<T> logs);
    }
}
