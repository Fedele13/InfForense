using CsvHelper;
using GoogleMeetLogsNavigator.Interface;
using GoogleMeetLogsNavigator.TO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleMeetLogsNavigator.GoogleParser
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleMeetCSVReader : ICSVReader<GoogleMeetingTO>
    {
        /// <summary>
        /// 
        /// </summary>
        private CsvReader _csvReader = null;

        /// <summary>
        /// 
        /// </summary>
        private IDictionary<string, GoogleMeetingTO> _meetingDictionary = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvStream"></param>
        public GoogleMeetCSVReader(StreamReader csvStream)
        {
            if (csvStream == null)
            {
                throw new ArgumentNullException("csvStream is null");
            }
            
            this._meetingDictionary = new Dictionary<string, GoogleMeetingTO>();
            this._csvReader = new CsvReader(csvStream, System.Globalization.CultureInfo.InvariantCulture);
            this._csvReader.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.InsideQuotes;
            this._csvReader.Configuration.HeaderValidated = null;
            this._csvReader.Configuration.MissingFieldFound = null;


            IList<GoogleMeetLogTO> recordsList = this._csvReader.GetRecords<GoogleMeetLogTO>().ToList();
            IList<string> meetingCodesList = recordsList.Select(item => item.MeetingCode).Distinct().ToList();

            foreach (string meetingCode in meetingCodesList)
            {
                this._meetingDictionary.Add(meetingCode, new GoogleMeetingTO(meetingCode, recordsList.Where(item => item.MeetingCode == meetingCode).ToList()));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, GoogleMeetingTO> ICSVReader<GoogleMeetingTO>.MeetingDictionary { get => this._meetingDictionary; }
    }
}
