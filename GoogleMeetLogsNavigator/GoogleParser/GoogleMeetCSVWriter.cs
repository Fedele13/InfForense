using CsvHelper;
using GoogleMeetLogsNavigator.Inteface;
using GoogleMeetLogsNavigator.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace GoogleMeetLogsNavigator.GoogleParser
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleMeetCSVWriter : ICSVWriter<GoogleMeetLogModel>
    {
        private string _destinationPath = string.Empty;

        public GoogleMeetCSVWriter(string destinationPath)
        {
            this._destinationPath = destinationPath;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logs"></param>
        public void ToGoogleMeetCsv(IList<GoogleMeetLogModel> logs)
        {
            using (StreamWriter writer = File.CreateText(_destinationPath))
            {
                CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);
                csv.Configuration.SanitizeForInjection = true;
                csv.WriteRecords(logs);
            }
        }
    }
}
