using GoogleMeetLogsNavigator.Inteface;
using GoogleMeetLogsNavigator.Model;
using System;
using System.Collections.Generic;

namespace GoogleMeetLogsNavigator.GoogleParser
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleMeetCSVWriter : ICSVWriter<GoogleMeetLogModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logs"></param>
        public void ToGoogleMeetCsv(IList<GoogleMeetLogModel> logs)
        {
            throw new NotImplementedException();
        }
    }
}
