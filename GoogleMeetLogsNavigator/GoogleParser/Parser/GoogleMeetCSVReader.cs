using CsvHelper;
using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.TransferObject.ToITA;
using GoogleMeetLogsNavigator.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GoogleMeetLogsNavigator.GoogleParser.Parser
{
    /// <summary>
    /// The GoogleMeetCSVReader
    /// Implements <see cref="ICSVReader">
    /// </summary>
    public class GoogleMeetCSVReader : ICSVReader<GoogleMeetingTO>
    {
        #region private attribute 
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
        /// The CSV Text Encoding
        /// </summary>
        private Encoding _encoding = null;

        #endregion

        #region .ctor

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

            this._encoding = csvStream.CurrentEncoding;
            this._meetingDictionary = new Dictionary<string, GoogleMeetingTO>();
            this._csvReader = new CsvReader(csvStream, System.Globalization.CultureInfo.InvariantCulture);
            this._csvReader.Configuration.IgnoreQuotes = false;
            this._csvReader.Configuration.HeaderValidated = null;
            this._csvReader.Configuration.MissingFieldFound = null;
            this._csvReader.Configuration.Delimiter = csvDelimiter;
            this._langauge = langauge;

            try
            {
                IList<IGoogleMeetLogTO> recordsList = null;
                if (string.IsNullOrEmpty(this._langauge) || this._langauge == "it")
                {
                    recordsList = getTransferObjectListITA(this._csvReader).ToList().ConvertAll(item => (IGoogleMeetLogTO)item);
                }
                else
                {
                    throw new InvalidOperationException("La lingua selezionata non è supportata");
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
                throw new Exception.ReaderException("Errore durante la lettura del file Controlla che il delimitatore scelto sia corretto.\n" + ex.Message, ex);
            }
        }

        #endregion

        #region prop

        /// <summary>
        /// The Meeting Dictionary with complete data
        /// </summary>
        public IDictionary<string, GoogleMeetingTO> MeetingDictionary => this._meetingDictionary;

        /// <summary>
        /// The Encoding
        /// </summary>
        public Encoding CSVTextEncoding { get => this._encoding; }

        #endregion

        #region private method

        /// <summary>
        /// Get the GoogleMeetLogTOITA
        /// </summary>
        /// <param name="_csvReader">The CSV reader</param>
        /// <returns>List of GoogleMeetLogTOITA</returns>
        private IList<GoogleMeetLogTOITA> getTransferObjectListITA(CsvReader csvReader)
        {
            IList<GoogleMeetLogTOITA> toList = new List<GoogleMeetLogTOITA>();

            int i = 0;
            while (csvReader.Read())
            {
                if (i == 0)
                {
                    csvReader.ReadHeader();
                    ++i;
                    continue;
                }
                ++i;
                GoogleMeetLogTOITA to = new GoogleMeetLogTOITA();

                /*Mandatory*/
                to.Date = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.Date)) ? throw new ArgumentException("Il campo Data non può essere vuoto") : csvReader.GetField(Constants.CSVHeader.Date).Replace(",", "");
                if (to.Date.StartsWith("\"") && to.Date.EndsWith("\""))
                {
                    to.Date = to.Date.Replace("\"", "");
                }
                to.EventName = csvReader.GetField(Constants.CSVHeader.EventName);
                to.EventDescription = csvReader.GetField(Constants.CSVHeader.EventDescription);
                to.MeetingCode = csvReader.GetField(Constants.CSVHeader.MeetingCode);
                to.PartecipantIdentifier = csvReader.GetField(Constants.CSVHeader.PartecipantIdentifier);
                to.ExternalPartecipantIdentifier = csvReader.GetField(Constants.CSVHeader.ExternalPartecipantIdentifier);
                to.ClientType = csvReader.GetField(Constants.CSVHeader.ClientType);
                /*Mandatory*/
                    
                to.MeetingOwnerEmail = csvReader.GetField(Constants.CSVHeader.MeetingOwnerEmail);
                     
                to.ProductType = csvReader.GetField(Constants.CSVHeader.ProductType);
                    
                //Mandatory
                to.Duration = csvReader.GetField(Constants.CSVHeader.Duration);
                        
                to.CallEvaluationOn5 = csvReader.GetField(Constants.CSVHeader.CallEvaluationOn5);
                    
                to.PartecipantName = csvReader.GetField(Constants.CSVHeader.PartecipantName);
                       
                to.IPAddress = csvReader.GetField(Constants.CSVHeader.IPAddress);
                       
                to.City = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.City))
                    ? csvReader.GetField(Constants.CSVHeader.City2)
                    : csvReader.GetField(Constants.CSVHeader.City);
                       
                to.Nation = csvReader.GetField(Constants.CSVHeader.Nation);
                    
                to.NETRoundTrip = csvReader.GetField(Constants.CSVHeader.NETRoundTrip);
           
                to.TransportProtocol = csvReader.GetField(Constants.CSVHeader.TransportProtocol);
                        
                to.PredictedBandWidthLoading = csvReader.GetField(Constants.CSVHeader.PredictedBandWidthLoading);
                        
                to.PredictedBandWidthUploading = csvReader.GetField(Constants.CSVHeader.PredictedBandWidthUploading);
                        
                to.MaxReceptionAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxReceptionAudioPacketsLost);
                        
                to.AverageReceptionAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageReceptionAudioPacketsLost);
                        
                to.AudioReceptionDuration = csvReader.GetField(Constants.CSVHeader.AudioReceptionDuration);
                        
                to.BitRatioAudioSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioAudioSending))
                        ? csvReader.GetField(Constants.CSVHeader.BitRatioAudioSending2)
                        : csvReader.GetField(Constants.CSVHeader.BitRatioAudioSending);
                        
                to.MaxSendingAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxSendingAudioPacketsLost);
                        
                to.AverageSendingAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageSendingAudioPacketsLost);
                        
                to.AudioSendingDuration = csvReader.GetField(Constants.CSVHeader.AudioSendingDuration);
                        
                to.CalendarEventIdentifier = csvReader.GetField(Constants.CSVHeader.CalendarEventIdentifier);
                        
                to.ConferenceID = csvReader.GetField(Constants.CSVHeader.ConferenceID);
                        
                to.AverageReceptionFlickering = csvReader.GetField(Constants.CSVHeader.AverageReceptionFlickering);
                        
                to.MaxReceptionFilckering = csvReader.GetField(Constants.CSVHeader.MaxReceptionFilckering);
                        
                to.AverageSendingFlickering = csvReader.GetField(Constants.CSVHeader.AverageSendingFlickering);
                        
                to.BitRatioScreencastReception = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioScreencastReception))
                        ? csvReader.GetField(Constants.CSVHeader.BitRatioScreencastReception2)
                        : csvReader.GetField(Constants.CSVHeader.BitRatioScreencastReception);
                        
                to.AverageScreecastReception = csvReader.GetField(Constants.CSVHeader.AverageScreecastReception);
                        
                to.LongSideMedianScreencastReception = csvReader.GetField(Constants.CSVHeader.LongSideMedianScreencastReception);
                        
                to.MaxReceptionScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxReceptionScreencastPacketsLost);
                        
                to.AverageReceptionScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageReceptionScreencastPacketsLost);
                        
                to.ScreencastReceptionDuration = csvReader.GetField(Constants.CSVHeader.ScreencastReceptionDuration);
                        
                to.ShortSideMedianScreencastReception = csvReader.GetField(Constants.CSVHeader.ShortSideMedianScreencastReception);
                        
                to.BitRatioScreencastSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioScreencastSending))
                        ? csvReader.GetField(Constants.CSVHeader.BitRatioScreencastSending2)
                        : csvReader.GetField(Constants.CSVHeader.BitRatioScreencastSending);
                        
                to.AverageScreecastSending = csvReader.GetField(Constants.CSVHeader.AverageScreecastSending);
                        
                to.LongSideMedianScreencastSending = csvReader.GetField(Constants.CSVHeader.LongSideMedianScreencastSending);
                        
                to.MaxSendingScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxSendingScreencastPacketsLost);
                        
                to.AverageSendingScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageSendingScreencastPacketsLost);
                        
                to.ScreencastSendingDuration = csvReader.GetField(Constants.CSVHeader.ScreencastSendingDuration);
                        
                to.ShortSideMedianScreencastSending = csvReader.GetField(Constants.CSVHeader.ShortSideMedianScreencastSending);
                        
                to.AverageVideoReception = csvReader.GetField(Constants.CSVHeader.AverageVideoReception);
                        
                to.LongSideMedianVideoReception = csvReader.GetField(Constants.CSVHeader.LongSideMedianVideoReception);
                        
                to.MaxVideoReceptionPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxVideoReceptionPacketsLost);
                        
                to.AverageVideoReceptionPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageVideoReceptionPacketsLost);
                        
                to.ReceptionVideoDuration = csvReader.GetField(Constants.CSVHeader.ReceptionVideoDuration);
                        
                to.ShortSideMedianVideoReception = csvReader.GetField(Constants.CSVHeader.ShortSideMedianVideoReception);
                        
                to.NetworkCongestion = csvReader.GetField(Constants.CSVHeader.NetworkCongestion);
                        
                to.BitRatioVideoSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioVideoSending))
                        ? csvReader.GetField(Constants.CSVHeader.BitRatioVideoSending2)
                        : csvReader.GetField(Constants.CSVHeader.BitRatioVideoSending);
                        
                to.AverageVideoSending = csvReader.GetField(Constants.CSVHeader.AverageVideoSending);
                        
                to.LongSideMedianVideoSending = csvReader.GetField(Constants.CSVHeader.LongSideMedianVideoSending);
                        
                to.MaxSendingVideoPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxSendingVideoPacketsLost);
                        
                to.AverageSendingVideoPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageSendingVideoPacketsLost);
                        
                to.VideoSendingDuration = csvReader.GetField(Constants.CSVHeader.VideoSendingDuration);
                        
                to.ShortSideMedianVideoSending = csvReader.GetField(Constants.CSVHeader.ShortSideMedianVideoSending);
                        
                to.ActionCause = csvReader.GetField(Constants.CSVHeader.ActionCause);
                        
                to.ActionDescription = csvReader.GetField(Constants.CSVHeader.ActionDescription);
                        
                to.VisualizedDestinationNames = csvReader.GetField(Constants.CSVHeader.VisualizedDestinationNames);
                        
                to.DestinationEmailsAddresses = csvReader.GetField(Constants.CSVHeader.DestinationEmailsAddresses);
                        
                to.DestinationPhoneNumber = csvReader.GetField(Constants.CSVHeader.DestinationPhoneNumber);
                        
                to.MeetingStartDate = csvReader.GetField(Constants.CSVHeader.MeetingStartDate);
                        
                to.MeetingEndDate = csvReader.GetField(Constants.CSVHeader.MeetingEndDate);
                        
                to.MeetingEnteringDate = csvReader.GetField(Constants.CSVHeader.MeetingEnteringDate);
                        
                to.TotalMeetingUserPartecipation = csvReader.GetField(Constants.CSVHeader.TotalMeetingUserPartecipation);
                        
                to.CommonEuropeanTimeType = csvReader.GetField(Constants.CSVHeader.CommonEuropeanTimeType);

                toList.Add(to);
            }

            Debug.WriteLine("Record: " + (i-1));
            return toList;
        }

        #endregion

    }
}
