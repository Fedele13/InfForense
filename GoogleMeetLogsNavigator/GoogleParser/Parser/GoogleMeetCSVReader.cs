using CsvHelper;
using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.TransferObject.ToITA;
using GoogleMeetLogsNavigator.Utility;
using System;
using System.Collections.Generic;
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
                    //recordsList = this._csvReader.GetRecords<GoogleMeetLogTOITA>().ToList().ConvertAll(item => (IGoogleMeetLogTO)item);
                }
                else
                {
                    throw new InvalidOperationException("Non sono supportate altre lingue oltre all'italiano");
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
                string s = csvReader.GetField(Constants.CSVHeader.Date);
                string s2 = csvReader.GetField(0);
                s2 = csvReader.GetField(1);

                toList.Add
                (

                    new GoogleMeetLogTOITA
                    {
                        /*Mandatory*/
                        Date = csvReader.GetField(Constants.CSVHeader.Date),
                        EventName = csvReader.GetField(Constants.CSVHeader.EventName),
                        EventDescription = csvReader.GetField(Constants.CSVHeader.EventDescription),
                        MeetingCode = csvReader.GetField(Constants.CSVHeader.MeetingCode),
                        PartecipantIdentifier = csvReader.GetField(Constants.CSVHeader.PartecipantIdentifier),
                        ExternalPartecipantIdentifier = csvReader.GetField(Constants.CSVHeader.ExternalPartecipantIdentifier),
                        ClientType = csvReader.GetField(Constants.CSVHeader.ClientType),
                        /*Mandatory*/

                        MeetingOwnerEmail = csvReader.GetField(Constants.CSVHeader.MeetingOwnerEmail),

                        ProductType = csvReader.GetField(Constants.CSVHeader.ProductType),

                        //Mandatory
                        Duration = csvReader.GetField(Constants.CSVHeader.Duration),

                        CallEvaluationOn5 = csvReader.GetField(Constants.CSVHeader.CallEvaluationOn5),

                        PartecipantName = csvReader.GetField(Constants.CSVHeader.PartecipantName),

                        IPAddress = csvReader.GetField(Constants.CSVHeader.IPAddress),

                        City = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.City))
                            ? csvReader.GetField(Constants.CSVHeader.City2)
                            : csvReader.GetField(Constants.CSVHeader.City),

                        Nation = csvReader.GetField(Constants.CSVHeader.Nation),

                        NETRoundTrip = csvReader.GetField(Constants.CSVHeader.NETRoundTrip),

                        TransportProtocol = csvReader.GetField(Constants.CSVHeader.TransportProtocol),

                        PredictedBandWidthLoading = csvReader.GetField(Constants.CSVHeader.PredictedBandWidthLoading),

                        PredictedBandWidthUploading = csvReader.GetField(Constants.CSVHeader.PredictedBandWidthUploading),

                        MaxReceptionAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxReceptionAudioPacketsLost),

                        AverageReceptionAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageReceptionAudioPacketsLost),

                        AudioReceptionDuration = csvReader.GetField(Constants.CSVHeader.AudioReceptionDuration),

                        BitRatioAudioSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioAudioSending))
                                ? csvReader.GetField(Constants.CSVHeader.BitRatioAudioSending2)
                                : csvReader.GetField(Constants.CSVHeader.BitRatioAudioSending),

                        MaxSendingAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxSendingAudioPacketsLost),

                        AverageSendingAudioPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageSendingAudioPacketsLost),

                        AudioSendingDuration = csvReader.GetField(Constants.CSVHeader.AudioSendingDuration),

                        CalendarEventIdentifier = csvReader.GetField(Constants.CSVHeader.CalendarEventIdentifier),

                        ConferenceID = csvReader.GetField(Constants.CSVHeader.ConferenceID),

                        AverageReceptionFlickering = csvReader.GetField(Constants.CSVHeader.AverageReceptionFlickering),

                        MaxReceptionFilckering = csvReader.GetField(Constants.CSVHeader.MaxReceptionFilckering),

                        AverageSendingFlickering = csvReader.GetField(Constants.CSVHeader.AverageSendingFlickering),

                        BitRatioScreencastReception = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioScreencastReception))
                                ? csvReader.GetField(Constants.CSVHeader.BitRatioScreencastReception2)
                                : csvReader.GetField(Constants.CSVHeader.BitRatioScreencastReception),

                        AverageScreecastReception = csvReader.GetField(Constants.CSVHeader.AverageScreecastReception),

                        LongSideMedianScreencastReception = csvReader.GetField(Constants.CSVHeader.LongSideMedianScreencastReception),

                        MaxReceptionScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxReceptionScreencastPacketsLost),

                        AverageReceptionScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageReceptionScreencastPacketsLost),

                        ScreencastReceptionDuration = csvReader.GetField(Constants.CSVHeader.ScreencastReceptionDuration),

                        ShortSideMedianScreencastReception = csvReader.GetField(Constants.CSVHeader.ShortSideMedianScreencastReception),

                        BitRatioScreencastSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioScreencastSending))
                                ? csvReader.GetField(Constants.CSVHeader.BitRatioScreencastSending2)
                                : csvReader.GetField(Constants.CSVHeader.BitRatioScreencastSending),

                        AverageScreecastSending = csvReader.GetField(Constants.CSVHeader.AverageScreecastSending),

                        LongSideMedianScreencastSending = csvReader.GetField(Constants.CSVHeader.LongSideMedianScreencastSending),

                        MaxSendingScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxSendingScreencastPacketsLost),

                        AverageSendingScreencastPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageSendingScreencastPacketsLost),

                        ScreencastSendingDuration = csvReader.GetField(Constants.CSVHeader.ScreencastSendingDuration),

                        ShortSideMedianScreencastSending = csvReader.GetField(Constants.CSVHeader.ShortSideMedianScreencastSending),

                        AverageVideoReception = csvReader.GetField(Constants.CSVHeader.AverageVideoReception),

                        LongSideMedianVideoReception = csvReader.GetField(Constants.CSVHeader.LongSideMedianVideoReception),

                        MaxVideoReceptionPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxVideoReceptionPacketsLost),

                        AverageVideoReceptionPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageVideoReceptionPacketsLost),

                        ReceptionVideoDuration = csvReader.GetField(Constants.CSVHeader.ReceptionVideoDuration),

                        ShortSideMedianVideoReception = csvReader.GetField(Constants.CSVHeader.ShortSideMedianVideoReception),

                        NetworkCongestion = csvReader.GetField(Constants.CSVHeader.NetworkCongestion),

                        BitRatioVideoSending = string.IsNullOrEmpty(csvReader.GetField(Constants.CSVHeader.BitRatioVideoSending))
                                ? csvReader.GetField(Constants.CSVHeader.BitRatioVideoSending2)
                                : csvReader.GetField(Constants.CSVHeader.BitRatioVideoSending),

                        AverageVideoSending = csvReader.GetField(Constants.CSVHeader.AverageVideoSending),

                        LongSideMedianVideoSending = csvReader.GetField(Constants.CSVHeader.LongSideMedianVideoSending),

                        MaxSendingVideoPacketsLost = csvReader.GetField(Constants.CSVHeader.MaxSendingVideoPacketsLost),

                        AverageSendingVideoPacketsLost = csvReader.GetField(Constants.CSVHeader.AverageSendingVideoPacketsLost),

                        VideoSendingDuration = csvReader.GetField(Constants.CSVHeader.VideoSendingDuration),

                        ShortSideMedianVideoSending = csvReader.GetField(Constants.CSVHeader.ShortSideMedianVideoSending),

                        ActionCause = csvReader.GetField(Constants.CSVHeader.ActionCause),

                        ActionDescription = csvReader.GetField(Constants.CSVHeader.ActionDescription),

                        VisualizedDestinationNames = csvReader.GetField(Constants.CSVHeader.VisualizedDestinationNames),

                        DestinationEmailsAddresses = csvReader.GetField(Constants.CSVHeader.DestinationEmailsAddresses),

                        DestinationPhoneNumber = csvReader.GetField(Constants.CSVHeader.DestinationPhoneNumber),

                        MeetingStartDate = csvReader.GetField(Constants.CSVHeader.MeetingStartDate),

                        MeetingEndDate = csvReader.GetField(Constants.CSVHeader.MeetingEndDate),

                        MeetingEnteringDate = csvReader.GetField(Constants.CSVHeader.MeetingEnteringDate),

                        TotalMeetingUserPartecipation = csvReader.GetField(Constants.CSVHeader.TotalMeetingUserPartecipation),

                        CommonEuropeanTimeType = csvReader.GetField(Constants.CSVHeader.CommonEuropeanTimeType),
                    }
                ) ;
            }

            return toList;
        }

        #endregion

    }
}
