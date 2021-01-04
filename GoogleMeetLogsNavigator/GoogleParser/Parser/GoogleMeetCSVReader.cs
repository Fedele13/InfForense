using CsvHelper;
using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
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
    public class GoogleMeetCSVReader :  ICSVReader<IList<IGoogleMeetLogTO>> 
    {
        #region private attribute 
        /// <summary>
        /// The CSV Reader instance
        /// </summary>
        private CsvReader _csvReader = null;

        /// <summary>
        /// Used Laguage
        /// </summary>
        private string _langauge = Constants.Langauges.ITA;

        /// <summary>
        /// The meeting dictionary
        /// </summary>
        private IDictionary<string, IList<IGoogleMeetLogTO>> _meetingDictionary = null;

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
        public GoogleMeetCSVReader(StreamReader csvStream, string csvDelimiter = ",", string langauge = Constants.Langauges.ITA)
        {
            if (csvStream == null)
            {
                throw new ArgumentNullException("csvStream is null");
            }

            this._encoding = csvStream.CurrentEncoding;
            this._meetingDictionary = new Dictionary<string, IList<IGoogleMeetLogTO>>();
            this._csvReader = new CsvReader(csvStream, System.Globalization.CultureInfo.InvariantCulture);
            this._csvReader.Configuration.IgnoreQuotes = false;
            this._csvReader.Configuration.HeaderValidated = null;
            this._csvReader.Configuration.MissingFieldFound = null;
            this._csvReader.Configuration.Delimiter = csvDelimiter;
            this._langauge = langauge;

            try
            {
                IList<IGoogleMeetLogTO> recordsList = null;
                if (string.IsNullOrEmpty(this._langauge) || this._langauge == Constants.Langauges.ITA)
                {
                    recordsList = getTransferObjectListITA(this._csvReader).ToList().ConvertAll(item => (IGoogleMeetLogTO)item);
                }
                else if (this._langauge == Constants.Langauges.EN)
                {
                    recordsList = getTransferObjectListEN(this._csvReader).ToList().ConvertAll(item => (IGoogleMeetLogTO)item);
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
        public IDictionary<string, IList<IGoogleMeetLogTO>> MeetingDictionary => this._meetingDictionary;

        /// <summary>
        /// The Encoding
        /// </summary>
        public Encoding CSVTextEncoding { get => this._encoding; }

        #endregion

        #region private method

        /// <summary>
        /// Get the GoogleMeetLogTO ITA
        /// </summary>
        /// <param name="_csvReader">The CSV reader</param>
        /// <returns>List of GoogleMeetLogTO ITA</returns>
        private IList<GoogleMeetLogTO> getTransferObjectListITA(CsvReader csvReader)
        {
            IList<GoogleMeetLogTO> toList = new List<GoogleMeetLogTO>();

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
                GoogleMeetLogTO to = new GoogleMeetLogTO();

                /*Mandatory*/
                to.Date = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeaderITA.Date)) ? throw new ArgumentException("Il campo Data non può essere vuoto") : csvReader.GetField(Constants.CSVHeaderITA.Date).Replace(",", "");
                if (to.Date.StartsWith("\"") && to.Date.EndsWith("\""))
                {
                    to.Date = to.Date.Replace("\"", "");
                }
                to.EventName = csvReader.GetField(Constants.CSVHeaderITA.EventName);
                to.EventDescription = csvReader.GetField(Constants.CSVHeaderITA.EventDescription);
                to.MeetingCode = csvReader.GetField(Constants.CSVHeaderITA.MeetingCode);
                to.PartecipantIdentifier = csvReader.GetField(Constants.CSVHeaderITA.PartecipantIdentifier);
                to.ExternalPartecipantIdentifier = csvReader.GetField(Constants.CSVHeaderITA.ExternalPartecipantIdentifier);
                to.ClientType = csvReader.GetField(Constants.CSVHeaderITA.ClientType);
                /*Mandatory*/
                    
                to.MeetingOwnerEmail = csvReader.GetField(Constants.CSVHeaderITA.MeetingOwnerEmail);
                     
                to.ProductType = csvReader.GetField(Constants.CSVHeaderITA.ProductType);
                    
                //Mandatory
                to.Duration = csvReader.GetField(Constants.CSVHeaderITA.Duration);
                to.EffectiveMeetingDurationInSeconds = csvReader.GetField(Constants.CSVHeaderITA.EffectiveMeetingDurationInSeconds);
                to.EffectiveMeetingDurationInMinutes = csvReader.GetField(Constants.CSVHeaderITA.EffectiveMeetingDurationInMinutes);
                to.EffectiveMeetingDurationInHours = csvReader.GetField(Constants.CSVHeaderITA.EffectiveMeetingDurationInHours);

                to.CallEvaluationOn5 = csvReader.GetField(Constants.CSVHeaderITA.CallEvaluationOn5);
                    
                to.PartecipantName = csvReader.GetField(Constants.CSVHeaderITA.PartecipantName);
                       
                to.IPAddress = csvReader.GetField(Constants.CSVHeaderITA.IPAddress);
                       
                to.City = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeaderITA.City))
                    ? csvReader.GetField(Constants.CSVHeaderITA.City2)
                    : csvReader.GetField(Constants.CSVHeaderITA.City);
                       
                to.Country = csvReader.GetField(Constants.CSVHeaderITA.Country);
                    
                to.NETRoundTrip = csvReader.GetField(Constants.CSVHeaderITA.NETRoundTrip);
           
                to.TransportProtocol = csvReader.GetField(Constants.CSVHeaderITA.TransportProtocol);
                        
                to.EstimatedUploadBandwidthInkbps = csvReader.GetField(Constants.CSVHeaderITA.EstimatedUploadBandwidthInkbps);
                        
                to.EstimatedDownloadBandwidthInkbps = csvReader.GetField(Constants.CSVHeaderITA.EstimatedDownloadBandwidthInkbps);
                        
                to.AudioReceivePacketLossMax = csvReader.GetField(Constants.CSVHeaderITA.AudioReceivePacketLossMax);
                        
                to.AudioReceivePacketLossMean = csvReader.GetField(Constants.CSVHeaderITA.AudioReceivePacketLossMean);
                        
                to.AudioReceiveDuration = csvReader.GetField(Constants.CSVHeaderITA.AudioReceiveDuration);
                        
                to.BitRatioAudioSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeaderITA.BitRatioAudioSending))
                        ? csvReader.GetField(Constants.CSVHeaderITA.BitRatioAudioSending2)
                        : csvReader.GetField(Constants.CSVHeaderITA.BitRatioAudioSending);
                        
                to.AudioSendPacketLossMax = csvReader.GetField(Constants.CSVHeaderITA.AudioSendPacketLossMax);
                        
                to.AudioSendPacketLossMean = csvReader.GetField(Constants.CSVHeaderITA.AudioSendPacketLossMean);
                        
                to.AudioSendDuration = csvReader.GetField(Constants.CSVHeaderITA.AudioSendDuration);
                        
                to.CalendarEventIdentifier = csvReader.GetField(Constants.CSVHeaderITA.CalendarEventIdentifier);
                        
                to.ConferenceID = csvReader.GetField(Constants.CSVHeaderITA.ConferenceID);
                        
                to.NetworkRecvJitterMeaninms = csvReader.GetField(Constants.CSVHeaderITA.NetworkRecvJitterMeaninms);
                        
                to.NetworkRecvJitterMaxinms = csvReader.GetField(Constants.CSVHeaderITA.NetworkRecvJitterMaxinms);
                        
                to.NetworkSendJitterMeaninms = csvReader.GetField(Constants.CSVHeaderITA.NetworkRecvJitterMeaninms);
                        
                to.BitRatioScreencastReception = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeaderITA.BitRatioScreencastReception))
                        ? csvReader.GetField(Constants.CSVHeaderITA.BitRatioScreencastReception2)
                        : csvReader.GetField(Constants.CSVHeaderITA.BitRatioScreencastReception);
                        
                to.ScreencastReceiveFPSMean = csvReader.GetField(Constants.CSVHeaderITA.ScreencastReceiveFPSMean);
                        
                to.ScreencastReceiveLongSideMedian = csvReader.GetField(Constants.CSVHeaderITA.ScreencastReceiveLongSideMedian);
                        
                to.ScreencastReceivePacketLossMax = csvReader.GetField(Constants.CSVHeaderITA.ScreencastReceivePacketLossMax);
                        
                to.ScreencastReceivePacketLossMean = csvReader.GetField(Constants.CSVHeaderITA.ScreencastReceivePacketLossMean);
                        
                to.ScreencastReceiveDuration = csvReader.GetField(Constants.CSVHeaderITA.ScreencastReceiveDuration);
                        
                to.ScreencastReceiveShortSideMedian = csvReader.GetField(Constants.CSVHeaderITA.ScreencastReceiveShortSideMedian);
                        
                to.BitRatioScreencastSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeaderITA.BitRatioScreencastSending))
                        ? csvReader.GetField(Constants.CSVHeaderITA.BitRatioScreencastSending2)
                        : csvReader.GetField(Constants.CSVHeaderITA.BitRatioScreencastSending);
                        
                to.ScreencastSendFPSMean = csvReader.GetField(Constants.CSVHeaderITA.ScreencastSendFPSMean);
                        
                to.ScreencastSendLongSideMedian = csvReader.GetField(Constants.CSVHeaderITA.ScreencastSendLongSideMedian);
                        
                to.ScreencastSendPacketLossMax = csvReader.GetField(Constants.CSVHeaderITA.ScreencastSendPacketLossMax);
                        
                to.ScreencastSendPacketLossMean = csvReader.GetField(Constants.CSVHeaderITA.ScreencastSendPacketLossMean);
                        
                to.ScreencastSendDuration = csvReader.GetField(Constants.CSVHeaderITA.ScreencastSendDuration);
                        
                to.ScreencastSendShortSideMedian = csvReader.GetField(Constants.CSVHeaderITA.ScreencastSendShortSideMedian);
                        
                to.VideoReceiveFPSMean = csvReader.GetField(Constants.CSVHeaderITA.VideoReceiveFPSMean);
                        
                to.VideoReceiveLongSideMedian = csvReader.GetField(Constants.CSVHeaderITA.VideoReceiveLongSideMedian);
                        
                to.VideoReceivePacketLossMax = csvReader.GetField(Constants.CSVHeaderITA.VideoReceivePacketLossMax);
                        
                to.VideoReceivePacketLossMean = csvReader.GetField(Constants.CSVHeaderITA.VideoReceivePacketLossMean);
                        
                to.VideoReceiveDuration = csvReader.GetField(Constants.CSVHeaderITA.VideoReceiveDuration);
                        
                to.VideoReceiveShortSideMedian = csvReader.GetField(Constants.CSVHeaderITA.VideoReceiveShortSideMedian);
                        
                to.NetworkCongestionRatio = csvReader.GetField(Constants.CSVHeaderITA.NetworkCongestionRatio);
                        
                to.BitRatioVideoSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeaderITA.BitRatioVideoSending))
                        ? csvReader.GetField(Constants.CSVHeaderITA.BitRatioVideoSending2)
                        : csvReader.GetField(Constants.CSVHeaderITA.BitRatioVideoSending);
                        
                to.VideoSendFPSMean = csvReader.GetField(Constants.CSVHeaderITA.VideoSendFPSMean);
                        
                to.VideoSendLongSideMedian = csvReader.GetField(Constants.CSVHeaderITA.VideoSendLongSideMedian);
                        
                to.VideoSendPacketLossMax = csvReader.GetField(Constants.CSVHeaderITA.VideoSendPacketLossMax);
                        
                to.VideoSendPacketLossMean = csvReader.GetField(Constants.CSVHeaderITA.VideoSendPacketLossMean);
                        
                to.VideoSendDuration = csvReader.GetField(Constants.CSVHeaderITA.VideoSendDuration);
                        
                to.VideoSendShortSideMedian = csvReader.GetField(Constants.CSVHeaderITA.VideoSendShortSideMedian);
                        
                to.ActionReason = csvReader.GetField(Constants.CSVHeaderITA.ActionReason);
                        
                to.ActionDescription = csvReader.GetField(Constants.CSVHeaderITA.ActionDescription);
                        
                to.TargetDisplayNames = csvReader.GetField(Constants.CSVHeaderITA.TargetDisplayNames);
                        
                to.TargetEmail = csvReader.GetField(Constants.CSVHeaderITA.TargetEmail);
                        
                to.TargetPhoneNumber = csvReader.GetField(Constants.CSVHeaderITA.TargetPhoneNumber);
                        
                to.MeetingStartDate = csvReader.GetField(Constants.CSVHeaderITA.MeetingStartDate);

                to.EffectiveMeetingStartDate = csvReader.GetField(Constants.CSVHeaderITA.EffectiveMeetingStartDate);

                to.MeetingEndDate = csvReader.GetField(Constants.CSVHeaderITA.MeetingEndDate);

                to.EffectiveMeetingEndDate = csvReader.GetField(Constants.CSVHeaderITA.EffectiveMeetingEndDate);

                to.MeetingEnteringDate = csvReader.GetField(Constants.CSVHeaderITA.MeetingEnteringDate);
                        
                to.TotalMeetingUserPartecipationInDecimal = csvReader.GetField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInDecimal);

                to.TotalMeetingUserPartecipationInHours = csvReader.GetField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInHours);

                to.TotalMeetingUserPartecipationInMinutes = csvReader.GetField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInMinutes);

                to.TotalMeetingUserPartecipationInSeconds = csvReader.GetField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInSeconds);

                to.TimeZone = csvReader.GetField(Constants.CSVHeaderITA.TimeZone);

                toList.Add(to);
            }

            Debug.WriteLine("Record: " + (i-1));
            return toList;
        }

        /// <summary>
        /// Get the GoogleMeetLogTO EN
        /// </summary>
        /// <param name="_csvReader">The CSV reader</param>
        /// <returns>List of GoogleMeetLogTO EN</returns>
        private IList<GoogleMeetLogTO> getTransferObjectListEN(CsvReader csvReader)
        {
            IList<GoogleMeetLogTO> toList = new List<GoogleMeetLogTO>();

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
                GoogleMeetLogTO to = new GoogleMeetLogTO();

                var c = csvReader.GetField(Constants.CSVHeaderEN.Date);
                /*Mandatory*/
                to.Date = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeaderEN.Date)) ? throw new ArgumentException("Il campo Data non può essere vuoto") : csvReader.GetField(Constants.CSVHeaderEN.Date).Replace(",", "");
                if (to.Date.StartsWith("\"") && to.Date.EndsWith("\""))
                {
                    to.Date = to.Date.Replace("\"", "");
                }
                to.EventName = csvReader.GetField(Constants.CSVHeaderEN.EventName);
                to.EventDescription = csvReader.GetField(Constants.CSVHeaderEN.EventDescription);
                to.MeetingCode = csvReader.GetField(Constants.CSVHeaderEN.MeetingCode);
                to.PartecipantIdentifier = csvReader.GetField(Constants.CSVHeaderEN.PartecipantIdentifier);
                to.ExternalPartecipantIdentifier = csvReader.GetField(Constants.CSVHeaderEN.ExternalPartecipantIdentifier);
                to.ClientType = csvReader.GetField(Constants.CSVHeaderEN.ClientType);
                /*Mandatory*/

                to.MeetingOwnerEmail = csvReader.GetField(Constants.CSVHeaderEN.MeetingOwnerEmail);

                to.ProductType = csvReader.GetField(Constants.CSVHeaderEN.ProductType);

                //Mandatory
                to.Duration = csvReader.GetField(Constants.CSVHeaderEN.Duration);
                to.EffectiveMeetingDurationInSeconds = csvReader.GetField(Constants.CSVHeaderEN.EffectiveMeetingDurationInSeconds);
                to.EffectiveMeetingDurationInMinutes = csvReader.GetField(Constants.CSVHeaderEN.EffectiveMeetingDurationInMinutes);
                to.EffectiveMeetingDurationInHours = csvReader.GetField(Constants.CSVHeaderEN.EffectiveMeetingDurationInHours);

                to.CallEvaluationOn5 = csvReader.GetField(Constants.CSVHeaderEN.CallEvaluationOn5);

                to.PartecipantName = csvReader.GetField(Constants.CSVHeaderEN.PartecipantName);

                to.IPAddress = csvReader.GetField(Constants.CSVHeaderEN.IPAddress);

                to.City = csvReader.GetField(Constants.CSVHeaderEN.City);

                to.Country = csvReader.GetField(Constants.CSVHeaderEN.Country);

                to.NETRoundTrip = csvReader.GetField(Constants.CSVHeaderEN.NETRoundTrip);

                to.TransportProtocol = csvReader.GetField(Constants.CSVHeaderEN.TransportProtocol);

                to.EstimatedUploadBandwidthInkbps = csvReader.GetField(Constants.CSVHeaderEN.EstimatedUploadBandwidthInkbps);

                to.EstimatedDownloadBandwidthInkbps = csvReader.GetField(Constants.CSVHeaderEN.EstimatedDownloadBandwidthInkbps);

                to.AudioReceivePacketLossMax = csvReader.GetField(Constants.CSVHeaderEN.AudioReceivePacketLossMax);

                to.AudioReceivePacketLossMean = csvReader.GetField(Constants.CSVHeaderEN.AudioReceivePacketLossMean);

                to.AudioReceiveDuration = csvReader.GetField(Constants.CSVHeaderEN.AudioReceiveDuration);

                to.BitRatioAudioSending = csvReader.GetField(Constants.CSVHeaderEN.BitRatioAudioSending);

                to.AudioSendPacketLossMax = csvReader.GetField(Constants.CSVHeaderEN.AudioSendPacketLossMax);

                to.AudioSendPacketLossMean = csvReader.GetField(Constants.CSVHeaderEN.AudioSendPacketLossMean);

                to.AudioSendDuration = csvReader.GetField(Constants.CSVHeaderEN.AudioSendDuration);

                to.CalendarEventIdentifier = csvReader.GetField(Constants.CSVHeaderEN.CalendarEventIdentifier);

                to.ConferenceID = csvReader.GetField(Constants.CSVHeaderEN.ConferenceID);

                to.NetworkRecvJitterMeaninms = csvReader.GetField(Constants.CSVHeaderEN.NetworkRecvJitterMeaninms);

                to.NetworkRecvJitterMaxinms = csvReader.GetField(Constants.CSVHeaderEN.NetworkRecvJitterMaxinms);

                to.NetworkSendJitterMeaninms = csvReader.GetField(Constants.CSVHeaderEN.NetworkRecvJitterMeaninms);

                to.BitRatioScreencastReception = csvReader.GetField(Constants.CSVHeaderEN.BitRatioScreencastReception);

                to.ScreencastReceiveFPSMean = csvReader.GetField(Constants.CSVHeaderEN.ScreencastReceiveFPSMean);

                to.ScreencastReceiveLongSideMedian = csvReader.GetField(Constants.CSVHeaderEN.ScreencastReceiveLongSideMedian);

                to.ScreencastReceivePacketLossMax = csvReader.GetField(Constants.CSVHeaderEN.ScreencastReceivePacketLossMax);

                to.ScreencastReceivePacketLossMean = csvReader.GetField(Constants.CSVHeaderEN.ScreencastReceivePacketLossMean);

                to.ScreencastReceiveDuration = csvReader.GetField(Constants.CSVHeaderEN.ScreencastReceiveDuration);

                to.ScreencastReceiveShortSideMedian = csvReader.GetField(Constants.CSVHeaderEN.ScreencastReceiveShortSideMedian);

                to.BitRatioScreencastSending = csvReader.GetField(Constants.CSVHeaderEN.BitRatioScreencastSending);

                to.ScreencastSendFPSMean = csvReader.GetField(Constants.CSVHeaderEN.ScreencastSendFPSMean);

                to.ScreencastSendLongSideMedian = csvReader.GetField(Constants.CSVHeaderEN.ScreencastSendLongSideMedian);

                to.ScreencastSendPacketLossMax = csvReader.GetField(Constants.CSVHeaderEN.ScreencastSendPacketLossMax);

                to.ScreencastSendPacketLossMean = csvReader.GetField(Constants.CSVHeaderEN.ScreencastSendPacketLossMean);

                to.ScreencastSendDuration = csvReader.GetField(Constants.CSVHeaderEN.ScreencastSendDuration);

                to.ScreencastSendShortSideMedian = csvReader.GetField(Constants.CSVHeaderEN.ScreencastSendShortSideMedian);

                to.VideoReceiveFPSMean = csvReader.GetField(Constants.CSVHeaderEN.VideoReceiveFPSMean);

                to.VideoReceiveLongSideMedian = csvReader.GetField(Constants.CSVHeaderEN.VideoReceiveLongSideMedian);

                to.VideoReceivePacketLossMax = csvReader.GetField(Constants.CSVHeaderEN.VideoReceivePacketLossMax);

                to.VideoReceivePacketLossMean = csvReader.GetField(Constants.CSVHeaderEN.VideoReceivePacketLossMean);

                to.VideoReceiveDuration = csvReader.GetField(Constants.CSVHeaderEN.VideoReceiveDuration);

                to.VideoReceiveShortSideMedian = csvReader.GetField(Constants.CSVHeaderEN.VideoReceiveShortSideMedian);

                to.NetworkCongestionRatio = csvReader.GetField(Constants.CSVHeaderEN.NetworkCongestionRatio);

                to.BitRatioVideoSending = csvReader.GetField(Constants.CSVHeaderEN.BitRatioVideoSending);

                to.VideoSendFPSMean = csvReader.GetField(Constants.CSVHeaderEN.VideoSendFPSMean);

                to.VideoSendLongSideMedian = csvReader.GetField(Constants.CSVHeaderEN.VideoSendLongSideMedian);

                to.VideoSendPacketLossMax = csvReader.GetField(Constants.CSVHeaderEN.VideoSendPacketLossMax);

                to.VideoSendPacketLossMean = csvReader.GetField(Constants.CSVHeaderEN.VideoSendPacketLossMean);

                to.VideoSendDuration = csvReader.GetField(Constants.CSVHeaderEN.VideoSendDuration);

                to.VideoSendShortSideMedian = csvReader.GetField(Constants.CSVHeaderEN.VideoSendShortSideMedian);

                to.ActionReason = csvReader.GetField(Constants.CSVHeaderEN.ActionReason);

                to.ActionDescription = csvReader.GetField(Constants.CSVHeaderEN.ActionDescription);

                to.TargetDisplayNames = csvReader.GetField(Constants.CSVHeaderEN.TargetDisplayNames);

                to.TargetEmail = csvReader.GetField(Constants.CSVHeaderEN.TargetEmail);

                to.TargetPhoneNumber = csvReader.GetField(Constants.CSVHeaderEN.TargetPhoneNumber);

                to.MeetingStartDate = csvReader.GetField(Constants.CSVHeaderEN.MeetingStartDate);

                to.EffectiveMeetingStartDate = csvReader.GetField(Constants.CSVHeaderEN.EffectiveMeetingStartDate);

                to.MeetingEndDate = csvReader.GetField(Constants.CSVHeaderEN.MeetingEndDate);

                to.EffectiveMeetingEndDate = csvReader.GetField(Constants.CSVHeaderEN.EffectiveMeetingEndDate);

                to.MeetingEnteringDate = csvReader.GetField(Constants.CSVHeaderEN.MeetingEnteringDate);

                to.TotalMeetingUserPartecipationInDecimal = csvReader.GetField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInDecimal);

                to.TotalMeetingUserPartecipationInHours = csvReader.GetField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInHours);

                to.TotalMeetingUserPartecipationInMinutes = csvReader.GetField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInMinutes);

                to.TotalMeetingUserPartecipationInSeconds = csvReader.GetField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInSeconds);

                to.TimeZone = csvReader.GetField(Constants.CSVHeaderEN.TimeZone);

                toList.Add(to);
            }

            Debug.WriteLine("Record: " + (i - 1));
            return toList;
        }

        #endregion

    }
}
