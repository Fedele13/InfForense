﻿using CsvHelper;
using GMCSVReaderPrototype.TO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GMCSVReaderPrototype.GoogleReader
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleCSVReader : ICSVReader<GoogleMeetingTO>
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
        public GoogleCSVReader(StreamReader csvStream)
        {
            if (csvStream == null)
            {
                throw new ArgumentNullException("csvStream is null");
            }

            this._meetingDictionary = new Dictionary<string, GoogleMeetingTO>();
            this._csvReader = new CsvReader(csvStream, System.Globalization.CultureInfo.InvariantCulture);

            IList<GoogleMeetLogTO> recordsList = this._csvReader.GetRecords<GoogleMeetLogTO>().ToList();
            IList<string> meetingCodesList = recordsList.Select(item => item.MeetingCode).ToList();

            foreach (string meetingCode in meetingCodesList)
            {
                this._meetingDictionary.Add(meetingCode, new GoogleMeetingTO(meetingCode, recordsList.Where(item => item.MeetingCode == meetingCode).ToList()));
            }
        }

       
        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, GoogleMeetingTO> ICSVReader<GoogleMeetingTO>.MeetingDictionary => this._meetingDictionary;
    }
}
