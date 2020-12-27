using CsvHelper;
using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.TransferObject.ToITA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleMeetLogsNavigator.GoogleParser.Parser
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
        private string _langauge = "it";

        /// <summary>
        /// 
        /// </summary>
        private IDictionary<string, GoogleMeetingTO> _meetingDictionary = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvStream"></param>
        public GoogleMeetCSVReader(StreamReader csvStream, string csvDelimiter = ",", string langauge = "it")
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
            this._csvReader.Configuration.Delimiter = csvDelimiter;
            this._langauge = langauge;
            IList<IGoogleMeetLogTO> recordsList = null;
            if (this._langauge == "it" || string.IsNullOrEmpty(this._langauge))
            {
                recordsList = this._csvReader.GetRecords<GoogleMeetLogTOITA>().ToList().ConvertAll(item => (IGoogleMeetLogTO)item);
            }
            else
            {
                throw new InvalidOperationException("No other langauge supported for the moment");
            }
            
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
