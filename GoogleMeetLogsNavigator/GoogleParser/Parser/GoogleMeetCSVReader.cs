﻿using CsvHelper;
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
    /// The GoogleMeetCSVReader
    /// Implements <see cref="ICSVReader">
    /// </summary>
    public class GoogleMeetCSVReader : ICSVReader<GoogleMeetingTO>
    {
        /// <summary>
        /// The CSV Reader instance
        /// </summary>
        private CsvReader _csvReader = null;

        /// <summary>
        /// Used Laguage
        /// </summary>
        private string _langauge = "it";

        /// <summary>
        /// The meeting dictionary
        /// </summary>
        private IDictionary<string, GoogleMeetingTO> _meetingDictionary = null;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="csvStream">The considered stream</param>
        /// <param name="csvDelimiter">The delimiter to use to build the csv</param>
        /// <param name="langauge">Language to use</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception.ReaderException"></exception>
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

            try
            {
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
                    try
                    {
                        this._meetingDictionary.Add(meetingCode, new GoogleMeetingTO(meetingCode, recordsList.Where(item => item.MeetingCode == meetingCode).ToList()));
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception.ReaderException($"Errore durante la lettura del meeting {meetingCode}", ex);   
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.ReaderException(ex.Message, ex);
            }
        }

        /// <summary>
        /// The Meeting Dictionaty with complete data
        /// </summary>
        IDictionary<string, GoogleMeetingTO> ICSVReader<GoogleMeetingTO>.MeetingDictionary { get => this._meetingDictionary; }

    }
}
