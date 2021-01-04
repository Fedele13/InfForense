using CsvHelper;
using GoogleMeetLogsNavigator.GoogleParser.GoogleEnum;
using GoogleMeetLogsNavigator.GoogleParser.nteface;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GoogleMeetLogsNavigator.GoogleParser.Parser
{
    /// <summary>
    /// The GoogleMeetCSVWriter
    /// Implements <see cref="ICSVWriter">
    /// </summary>
    public class GoogleMeetCSVWriter : ICSVWriter<IGoogleMeetLogTO>
    {
        #region private

        /// <summary>
        /// The configuration Dictionary
        /// </summary>
        private IDictionary<CSVHeaderEnum, bool> _configurationDictionary = null;

        /// <summary>
        /// The delimter to use to build the csv
        /// </summary>
        private string _csvDelimiter = ",";

        /// <summary>
        /// The Encoding
        /// </summary>
        private Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// Language used to find the header of csv file
        /// </summary>
        private string _supportedLanguage = Constants.Langauges.ITA;

        #endregion

        #region .ctor

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="configurationDictionary">The Configuration Dictionary</param>
        /// <param name="language">The language used for csv headers</param>
        public GoogleMeetCSVWriter(IDictionary<CSVHeaderEnum, bool> configurationDictionary, string language = Constants.Langauges.ITA)
        {
            if (configurationDictionary != null)
            {
                this._configurationDictionary = configurationDictionary;
            }
            this._supportedLanguage = language;
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="delimiter">The delimiter to use to build csv file</param>
        /// <param name="language">The language used for csv headers</param>
        public GoogleMeetCSVWriter(string delimiter = ",", string language = Constants.Langauges.ITA)
        {
            this._csvDelimiter = delimiter;
            this._supportedLanguage = language;
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="language">The language used for csv headers</param>
        public GoogleMeetCSVWriter(Encoding encoding, string language = Constants.Langauges.ITA)
        {
            if (encoding != null)
            {
                this._encoding = encoding;
            }
            else
            {
                throw new ArgumentNullException("L'encoder è nullo. Selezionare un encoder valido");
            }
            this._supportedLanguage = language;
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="configurationDictionary">The Configuration Dictionary</param>
        /// <param name="encoding">Encoder to use to create text content into csv file</param>
        /// <param name="delimiter">The delimiter to use to build csv file</param>
        /// <param name="language">The csv header language</param>
        public GoogleMeetCSVWriter(IDictionary<CSVHeaderEnum, bool> configurationDictionary, Encoding encoding, string delimiter = ",", string language = Constants.Langauges.ITA)
        {
            if (configurationDictionary != null)
            {
                this._configurationDictionary = configurationDictionary;
            }

            if (encoding != null)
            {
                this._encoding = encoding;
            }
            else
            {
                throw new ArgumentNullException("L'encoder è nullo. Selezionare un encoder valido");
            }

            this._supportedLanguage = language;
            this._csvDelimiter = delimiter;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Set the delimter used to build the csv file
        /// </summary>
        /// <param name="delimiter">The delimiter</param>
        public void SetCSVDelimiter(string delimiter)
        {
            this._csvDelimiter = delimiter;
        }

        /// <summary>
        /// Set the configuration dictionary to build the csv file with selected columns
        /// </summary>
        /// <param name="configurationDictioanry">The Configuration Dictionary <columnName, true/false></param>
        public void SetConfiguration(IDictionary<CSVHeaderEnum, bool> configurationDictionary)
        {
            this._configurationDictionary = configurationDictionary;
        }

        /// <summary>
        /// The csv Encoding
        /// </summary>
        /// <param name="encoding">The Encoding</param>
        public void SetEncoding(Encoding encoding)
        {
            this._encoding = encoding;
        }
        /// <summary>
        /// Set the Header csv language
        /// </summary>
        /// <param name="language">Header csv language</param>
        public void SetCSVHeaderLanguage(string language)
        {
            this._supportedLanguage = language;
        }


        /// <summary>
        /// Create csv contetn
        /// </summary>
        /// <param name="logs">Logs to write in csv file</param>
        /// <return>The string content in csv format</return>
        /// <exception cref="Exception.WriterException">
        public string ToGoogleMeetCsv(IList<IGoogleMeetLogTO> logs)
        {
            if (this._supportedLanguage == Constants.Langauges.ITA)
                return exportTransferObjectITA(logs);
            if (this._supportedLanguage == Constants.Langauges.EN)
                return exportTransferObjectEN(logs);
            throw new Exception.WriterException("La lingua configurata da usare per l'header del csv non è al momento gestita");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDictionary<CSVHeaderEnum, bool> GetDefaultConfiguration()
        {
            IDictionary<CSVHeaderEnum, bool> defaultConfigurationDictionary = new Dictionary<CSVHeaderEnum, bool>();

            for (int headerEnum = 12; headerEnum < Enum.GetValues(typeof(CSVHeaderEnum)).Length; ++headerEnum)
            {
                defaultConfigurationDictionary.Add((CSVHeaderEnum)headerEnum, true);
            }
            return defaultConfigurationDictionary;
        }

        #endregion

        #region
        /// <summary>
        /// Return the csv content with ITA csv header
        /// </summary>
        /// <param name="logs">The logs</param>
        /// <returns>The csv content with ITA csv header</returns>
        private string exportTransferObjectITA(IList<IGoogleMeetLogTO> logs)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.SanitizeForInjection = true;
                csvWriter.Configuration.Delimiter = this._csvDelimiter;
                csvWriter.Configuration.ShouldQuote = (field, context) =>
                {
                    return field.Contains(_csvDelimiter);
                };

                if (this._configurationDictionary == null)
                {
                    this._configurationDictionary = GetDefaultConfiguration();
                }

                try
                {
                    /*Mandatory*/
                    csvWriter.WriteField(Constants.CSVHeaderITA.Date);
                    csvWriter.WriteField(Constants.CSVHeaderITA.EventName);
                    csvWriter.WriteField(Constants.CSVHeaderITA.EventDescription);
                    csvWriter.WriteField(Constants.CSVHeaderITA.MeetingCode);
                    csvWriter.WriteField(Constants.CSVHeaderITA.PartecipantIdentifier);
                    csvWriter.WriteField(Constants.CSVHeaderITA.ExternalPartecipantIdentifier);
                    csvWriter.WriteField(Constants.CSVHeaderITA.ClientType);
                    /*Mandatory*/

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingOwnerEmail) && this._configurationDictionary[CSVHeaderEnum.MeetingOwnerEmail])
                        csvWriter.WriteField(Constants.CSVHeaderITA.MeetingOwnerEmail);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ProductType) && this._configurationDictionary[CSVHeaderEnum.ProductType])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ProductType);

                    //Mandatory
                    csvWriter.WriteField(Constants.CSVHeaderITA.Duration);
                    csvWriter.WriteField(Constants.CSVHeaderITA.EffectiveMeetingDurationInSeconds);
                    csvWriter.WriteField(Constants.CSVHeaderITA.EffectiveMeetingDurationInMinutes);
                    csvWriter.WriteField(Constants.CSVHeaderITA.EffectiveMeetingDurationInHours);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CallEvaluationOn5) && this._configurationDictionary[CSVHeaderEnum.CallEvaluationOn5])
                        csvWriter.WriteField(Constants.CSVHeaderITA.CallEvaluationOn5);

                    csvWriter.WriteField(Constants.CSVHeaderITA.PartecipantName);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.IPAddress) && this._configurationDictionary[CSVHeaderEnum.IPAddress])
                        csvWriter.WriteField(Constants.CSVHeaderITA.IPAddress);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.City) && this._configurationDictionary[CSVHeaderEnum.City])
                        csvWriter.WriteField(Constants.CSVHeaderITA.City2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.Country) && this._configurationDictionary[CSVHeaderEnum.Country])
                        csvWriter.WriteField(Constants.CSVHeaderITA.Country);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NETRoundTrip) && this._configurationDictionary[CSVHeaderEnum.NETRoundTrip])
                        csvWriter.WriteField(Constants.CSVHeaderITA.NETRoundTrip);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TransportProtocol) && this._configurationDictionary[CSVHeaderEnum.TransportProtocol])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TransportProtocol);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedUploadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedUploadBandwidthInkbps])
                        csvWriter.WriteField(Constants.CSVHeaderITA.EstimatedUploadBandwidthInkbps);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedDownloadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedDownloadBandwidthInkbps])
                        csvWriter.WriteField(Constants.CSVHeaderITA.EstimatedDownloadBandwidthInkbps);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderITA.AudioReceivePacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.AudioReceivePacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.AudioReceiveDuration])
                        csvWriter.WriteField(Constants.CSVHeaderITA.AudioReceiveDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioAudioSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioAudioSending])
                        csvWriter.WriteField(Constants.CSVHeaderITA.BitRatioAudioSending2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderITA.AudioSendPacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.AudioSendPacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendDuration) && this._configurationDictionary[CSVHeaderEnum.AudioSendDuration])
                        csvWriter.WriteField(Constants.CSVHeaderITA.AudioSendDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CalendarEventIdentifier) && this._configurationDictionary[CSVHeaderEnum.CalendarEventIdentifier])
                        csvWriter.WriteField(Constants.CSVHeaderITA.CalendarEventIdentifier);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ConferenceID) && this._configurationDictionary[CSVHeaderEnum.ConferenceID])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ConferenceID);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMeaninms])
                        csvWriter.WriteField(Constants.CSVHeaderITA.NetworkRecvJitterMeaninms);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMaxinms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMaxinms])
                        csvWriter.WriteField(Constants.CSVHeaderITA.NetworkRecvJitterMaxinms);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkSendJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkSendJitterMeaninms])
                        csvWriter.WriteField(Constants.CSVHeaderITA.NetworkSendJitterMeaninms);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastReception) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastReception])
                        csvWriter.WriteField(Constants.CSVHeaderITA.BitRatioScreencastReception2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastReceiveFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastReceiveLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastReceivePacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastReceivePacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveDuration])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastReceiveDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastReceiveShortSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastSending])
                        csvWriter.WriteField(Constants.CSVHeaderITA.BitRatioScreencastSending2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastSendFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastSendLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastSendPacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastSendPacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendDuration])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastSendDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ScreencastSendShortSideMedian);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoReceiveFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoReceiveLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoReceivePacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoReceivePacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveDuration])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoReceiveDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoReceiveShortSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkCongestionRatio) && this._configurationDictionary[CSVHeaderEnum.NetworkCongestionRatio])
                        csvWriter.WriteField(Constants.CSVHeaderITA.NetworkCongestionRatio);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioVideoSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioVideoSending])
                        csvWriter.WriteField(Constants.CSVHeaderITA.BitRatioVideoSending2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoSendFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoSendLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoSendPacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoSendPacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendDuration) && this._configurationDictionary[CSVHeaderEnum.VideoSendDuration])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoSendDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderITA.VideoSendShortSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionReason) && this._configurationDictionary[CSVHeaderEnum.ActionReason])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ActionReason);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionDescription) && this._configurationDictionary[CSVHeaderEnum.ActionDescription])
                        csvWriter.WriteField(Constants.CSVHeaderITA.ActionDescription);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetDisplayNames) && this._configurationDictionary[CSVHeaderEnum.TargetDisplayNames])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TargetDisplayNames);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetEmail) && this._configurationDictionary[CSVHeaderEnum.TargetEmail])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TargetEmail);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetPhoneNumber) && this._configurationDictionary[CSVHeaderEnum.TargetPhoneNumber])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TargetPhoneNumber);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.MeetingStartDate])
                        csvWriter.WriteField(Constants.CSVHeaderITA.MeetingStartDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingStartDate])
                        csvWriter.WriteField(Constants.CSVHeaderITA.EffectiveMeetingStartDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEndDate])
                        csvWriter.WriteField(Constants.CSVHeaderITA.MeetingEndDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingEndDate])
                        csvWriter.WriteField(Constants.CSVHeaderITA.EffectiveMeetingEndDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEnteringDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEnteringDate])
                        csvWriter.WriteField(Constants.CSVHeaderITA.MeetingEnteringDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInDecimal);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInHours) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInHours])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInHours);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInMinutes);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TotalMeetingUserPartecipationInSeconds);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CommonEuropeanTimeType) && this._configurationDictionary[CSVHeaderEnum.CommonEuropeanTimeType])
                        csvWriter.WriteField(Constants.CSVHeaderITA.TimeZone);
                }
                catch (System.Exception ex)
                {
                    throw new Exception.WriterException("Errore durante la scrittura dell'header del contenuto del csv", ex);
                }

                csvWriter.NextRecord();

                for (int i = 0; i < logs.Count; ++i)
                {
                    IGoogleMeetLogTO log = logs[i];

                    try
                    {
                        /*Mandatory*/
                        csvWriter.WriteField(log.Date.GetSafeString());
                        csvWriter.WriteField(log.EventName.GetSafeString());
                        csvWriter.WriteField(log.EventDescription.GetSafeString());
                        csvWriter.WriteField(log.MeetingCode.GetSafeString());
                        csvWriter.WriteField(log.PartecipantIdentifier.GetSafeString());
                        csvWriter.WriteField(log.ExternalPartecipantIdentifier.GetSafeString());
                        csvWriter.WriteField(log.ClientType.GetSafeString());
                        /*Mandatory*/

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingOwnerEmail) && this._configurationDictionary[CSVHeaderEnum.MeetingOwnerEmail])
                            csvWriter.WriteField(log.MeetingOwnerEmail.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ProductType) && this._configurationDictionary[CSVHeaderEnum.ProductType])
                            csvWriter.WriteField(log.ProductType.GetSafeString());

                        //Mandatory
                        csvWriter.WriteField(log.Duration);
                        csvWriter.WriteField(log.EffectiveMeetingDurationInSeconds);
                        csvWriter.WriteField(log.EffectiveMeetingDurationInMinutes);
                        csvWriter.WriteField(log.EffectiveMeetingDurationInHours);

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CallEvaluationOn5) && this._configurationDictionary[CSVHeaderEnum.CallEvaluationOn5])
                            csvWriter.WriteField(log.CallEvaluationOn5.GetSafeString());

                        csvWriter.WriteField(log.PartecipantName.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.IPAddress) && this._configurationDictionary[CSVHeaderEnum.IPAddress])
                            csvWriter.WriteField(log.IPAddress.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.City) && this._configurationDictionary[CSVHeaderEnum.City])
                            csvWriter.WriteField(log.City.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.Country) && this._configurationDictionary[CSVHeaderEnum.Country])
                            csvWriter.WriteField(log.Country.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NETRoundTrip) && this._configurationDictionary[CSVHeaderEnum.NETRoundTrip])
                            csvWriter.WriteField(log.NETRoundTrip.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TransportProtocol) && this._configurationDictionary[CSVHeaderEnum.TransportProtocol])
                            csvWriter.WriteField(log.TransportProtocol.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedUploadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedUploadBandwidthInkbps])
                            csvWriter.WriteField(log.EstimatedUploadBandwidthInkbps.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedDownloadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedDownloadBandwidthInkbps])
                            csvWriter.WriteField(log.EstimatedDownloadBandwidthInkbps.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMax])
                            csvWriter.WriteField(log.AudioReceivePacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMean])
                            csvWriter.WriteField(log.AudioReceivePacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.AudioReceiveDuration])
                            csvWriter.WriteField(log.AudioReceiveDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioAudioSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioAudioSending])
                            csvWriter.WriteField(log.BitRatioAudioSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMax])
                            csvWriter.WriteField(log.AudioSendPacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMean])
                            csvWriter.WriteField(log.AudioSendPacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendDuration) && this._configurationDictionary[CSVHeaderEnum.AudioSendDuration])
                            csvWriter.WriteField(log.AudioSendDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CalendarEventIdentifier) && this._configurationDictionary[CSVHeaderEnum.CalendarEventIdentifier])
                            csvWriter.WriteField(log.CalendarEventIdentifier.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ConferenceID) && this._configurationDictionary[CSVHeaderEnum.ConferenceID])
                            csvWriter.WriteField(log.ConferenceID.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMeaninms])
                            csvWriter.WriteField(log.NetworkRecvJitterMeaninms.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMaxinms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMaxinms])
                            csvWriter.WriteField(log.NetworkRecvJitterMaxinms.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkSendJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkSendJitterMeaninms])
                            csvWriter.WriteField(log.NetworkSendJitterMeaninms.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastReception) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastReception])
                            csvWriter.WriteField(log.BitRatioScreencastReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveFPSMean])
                            csvWriter.WriteField(log.ScreencastReceiveFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveLongSideMedian])
                            csvWriter.WriteField(log.ScreencastReceiveLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMax])
                            csvWriter.WriteField(log.ScreencastReceivePacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMean])
                            csvWriter.WriteField(log.ScreencastReceivePacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveDuration])
                            csvWriter.WriteField(log.ScreencastReceiveDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveShortSideMedian])
                            csvWriter.WriteField(log.ScreencastReceiveShortSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastSending])
                            csvWriter.WriteField(log.BitRatioScreencastSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendFPSMean])
                            csvWriter.WriteField(log.ScreencastSendFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendLongSideMedian])
                            csvWriter.WriteField(log.ScreencastSendLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMax])
                            csvWriter.WriteField(log.ScreencastSendPacketLossMax);
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMean])
                            csvWriter.WriteField(log.ScreencastSendPacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendDuration])
                            csvWriter.WriteField(log.ScreencastSendDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendShortSideMedian])
                            csvWriter.WriteField(log.ScreencastSendShortSideMedian.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveFPSMean])
                            csvWriter.WriteField(log.VideoReceiveFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveLongSideMedian])
                            csvWriter.WriteField(log.VideoReceiveLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMax])
                            csvWriter.WriteField(log.VideoReceivePacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMean])
                            csvWriter.WriteField(log.VideoReceivePacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveDuration])
                            csvWriter.WriteField(log.VideoReceiveDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveShortSideMedian])
                            csvWriter.WriteField(log.VideoReceiveShortSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkCongestionRatio) && this._configurationDictionary[CSVHeaderEnum.NetworkCongestionRatio])
                            csvWriter.WriteField(log.NetworkCongestionRatio.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioVideoSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioVideoSending])
                            csvWriter.WriteField(log.BitRatioVideoSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendFPSMean])
                            csvWriter.WriteField(log.VideoSendFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendLongSideMedian])
                            csvWriter.WriteField(log.VideoSendLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMax])
                            csvWriter.WriteField(log.VideoSendPacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMean])
                            csvWriter.WriteField(log.VideoSendPacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendDuration) && this._configurationDictionary[CSVHeaderEnum.VideoSendDuration])
                            csvWriter.WriteField(log.VideoSendDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendShortSideMedian])
                            csvWriter.WriteField(log.VideoSendShortSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionReason) && this._configurationDictionary[CSVHeaderEnum.ActionReason])
                            csvWriter.WriteField(log.ActionReason.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionDescription) && this._configurationDictionary[CSVHeaderEnum.ActionDescription])
                            csvWriter.WriteField(log.ActionDescription.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetDisplayNames) && this._configurationDictionary[CSVHeaderEnum.TargetDisplayNames])
                            csvWriter.WriteField(log.TargetDisplayNames.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetEmail) && this._configurationDictionary[CSVHeaderEnum.TargetEmail])
                            csvWriter.WriteField(log.TargetEmail.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetPhoneNumber) && this._configurationDictionary[CSVHeaderEnum.TargetPhoneNumber])
                            csvWriter.WriteField(log.TargetPhoneNumber.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.MeetingStartDate])
                            csvWriter.WriteField(log.MeetingStartDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingStartDate])
                            csvWriter.WriteField(log.EffectiveMeetingStartDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEndDate])
                            csvWriter.WriteField(log.MeetingEndDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingEndDate])
                            csvWriter.WriteField(log.EffectiveMeetingEndDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEnteringDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEnteringDate])
                            csvWriter.WriteField(log.MeetingEnteringDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInDecimal.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInSeconds.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInMinutes.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInHours) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInHours])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInHours.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CommonEuropeanTimeType) && this._configurationDictionary[CSVHeaderEnum.CommonEuropeanTimeType])
                            csvWriter.WriteField(log.TimeZone.GetSafeString());

                        csvWriter.NextRecord();
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception.WriterException($"Errore durante la scrittura dell {i + 1}-esimo record del contenuto del csv", ex);
                    }
                }

                writer.Flush();

                return this._encoding.GetString(mem.ToArray());

            }
        }

        /// <summary>
        /// Return the csv content with EN csv header
        /// </summary>
        /// <param name="logs">The logs</param>
        /// <returns>The csv content with EN csv header</returns>
        private string exportTransferObjectEN(IList<IGoogleMeetLogTO> logs)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.SanitizeForInjection = true;
                csvWriter.Configuration.Delimiter = this._csvDelimiter;
                csvWriter.Configuration.ShouldQuote = (field, context) =>
                {
                    return field.Contains(_csvDelimiter);
                };

                if (this._configurationDictionary == null)
                {
                    this._configurationDictionary = GetDefaultConfiguration();
                }

                try
                {
                    /*Mandatory*/
                    csvWriter.WriteField(Constants.CSVHeaderEN.Date);
                    csvWriter.WriteField(Constants.CSVHeaderEN.EventName);
                    csvWriter.WriteField(Constants.CSVHeaderEN.EventDescription);
                    csvWriter.WriteField(Constants.CSVHeaderEN.MeetingCode);
                    csvWriter.WriteField(Constants.CSVHeaderEN.PartecipantIdentifier);
                    csvWriter.WriteField(Constants.CSVHeaderEN.ExternalPartecipantIdentifier);
                    csvWriter.WriteField(Constants.CSVHeaderEN.ClientType);
                    /*Mandatory*/

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingOwnerEmail) && this._configurationDictionary[CSVHeaderEnum.MeetingOwnerEmail])
                        csvWriter.WriteField(Constants.CSVHeaderEN.MeetingOwnerEmail);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ProductType) && this._configurationDictionary[CSVHeaderEnum.ProductType])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ProductType);

                    //Mandatory
                    csvWriter.WriteField(Constants.CSVHeaderEN.Duration);
                    csvWriter.WriteField(Constants.CSVHeaderEN.EffectiveMeetingDurationInSeconds);
                    csvWriter.WriteField(Constants.CSVHeaderEN.EffectiveMeetingDurationInMinutes);
                    csvWriter.WriteField(Constants.CSVHeaderEN.EffectiveMeetingDurationInHours);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CallEvaluationOn5) && this._configurationDictionary[CSVHeaderEnum.CallEvaluationOn5])
                        csvWriter.WriteField(Constants.CSVHeaderEN.CallEvaluationOn5);

                    csvWriter.WriteField(Constants.CSVHeaderEN.PartecipantName);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.IPAddress) && this._configurationDictionary[CSVHeaderEnum.IPAddress])
                        csvWriter.WriteField(Constants.CSVHeaderEN.IPAddress);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.City) && this._configurationDictionary[CSVHeaderEnum.City])
                        csvWriter.WriteField(Constants.CSVHeaderEN.City);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.Country) && this._configurationDictionary[CSVHeaderEnum.Country])
                        csvWriter.WriteField(Constants.CSVHeaderEN.Country);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NETRoundTrip) && this._configurationDictionary[CSVHeaderEnum.NETRoundTrip])
                        csvWriter.WriteField(Constants.CSVHeaderEN.NETRoundTrip);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TransportProtocol) && this._configurationDictionary[CSVHeaderEnum.TransportProtocol])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TransportProtocol);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedUploadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedUploadBandwidthInkbps])
                        csvWriter.WriteField(Constants.CSVHeaderEN.EstimatedUploadBandwidthInkbps);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedDownloadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedDownloadBandwidthInkbps])
                        csvWriter.WriteField(Constants.CSVHeaderEN.EstimatedDownloadBandwidthInkbps);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderEN.AudioReceivePacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.AudioReceivePacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.AudioReceiveDuration])
                        csvWriter.WriteField(Constants.CSVHeaderEN.AudioReceiveDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioAudioSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioAudioSending])
                        csvWriter.WriteField(Constants.CSVHeaderEN.BitRatioAudioSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderEN.AudioSendPacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.AudioSendPacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendDuration) && this._configurationDictionary[CSVHeaderEnum.AudioSendDuration])
                        csvWriter.WriteField(Constants.CSVHeaderEN.AudioSendDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CalendarEventIdentifier) && this._configurationDictionary[CSVHeaderEnum.CalendarEventIdentifier])
                        csvWriter.WriteField(Constants.CSVHeaderEN.CalendarEventIdentifier);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ConferenceID) && this._configurationDictionary[CSVHeaderEnum.ConferenceID])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ConferenceID);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMeaninms])
                        csvWriter.WriteField(Constants.CSVHeaderEN.NetworkRecvJitterMeaninms);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMaxinms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMaxinms])
                        csvWriter.WriteField(Constants.CSVHeaderEN.NetworkRecvJitterMaxinms);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkSendJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkSendJitterMeaninms])
                        csvWriter.WriteField(Constants.CSVHeaderEN.NetworkSendJitterMeaninms);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastReception) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastReception])
                        csvWriter.WriteField(Constants.CSVHeaderEN.BitRatioScreencastReception);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastReceiveFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastReceiveLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastReceivePacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastReceivePacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveDuration])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastReceiveDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastReceiveShortSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastSending])
                        csvWriter.WriteField(Constants.CSVHeaderEN.BitRatioScreencastSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastSendFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastSendLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastSendPacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastSendPacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendDuration])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastSendDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ScreencastSendShortSideMedian);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoReceiveFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoReceiveLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoReceivePacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoReceivePacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveDuration])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoReceiveDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoReceiveShortSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkCongestionRatio) && this._configurationDictionary[CSVHeaderEnum.NetworkCongestionRatio])
                        csvWriter.WriteField(Constants.CSVHeaderEN.NetworkCongestionRatio);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioVideoSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioVideoSending])
                        csvWriter.WriteField(Constants.CSVHeaderEN.BitRatioVideoSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendFPSMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoSendFPSMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendLongSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoSendLongSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMax])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoSendPacketLossMax);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMean])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoSendPacketLossMean);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendDuration) && this._configurationDictionary[CSVHeaderEnum.VideoSendDuration])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoSendDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendShortSideMedian])
                        csvWriter.WriteField(Constants.CSVHeaderEN.VideoSendShortSideMedian);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionReason) && this._configurationDictionary[CSVHeaderEnum.ActionReason])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ActionReason);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionDescription) && this._configurationDictionary[CSVHeaderEnum.ActionDescription])
                        csvWriter.WriteField(Constants.CSVHeaderEN.ActionDescription);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetDisplayNames) && this._configurationDictionary[CSVHeaderEnum.TargetDisplayNames])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TargetDisplayNames);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetEmail) && this._configurationDictionary[CSVHeaderEnum.TargetEmail])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TargetEmail);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetPhoneNumber) && this._configurationDictionary[CSVHeaderEnum.TargetPhoneNumber])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TargetPhoneNumber);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.MeetingStartDate])
                        csvWriter.WriteField(Constants.CSVHeaderEN.MeetingStartDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingStartDate])
                        csvWriter.WriteField(Constants.CSVHeaderEN.EffectiveMeetingStartDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEndDate])
                        csvWriter.WriteField(Constants.CSVHeaderEN.MeetingEndDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingEndDate])
                        csvWriter.WriteField(Constants.CSVHeaderEN.EffectiveMeetingEndDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEnteringDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEnteringDate])
                        csvWriter.WriteField(Constants.CSVHeaderEN.MeetingEnteringDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInDecimal);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInHours) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInHours])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInHours);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInMinutes);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TotalMeetingUserPartecipationInSeconds);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CommonEuropeanTimeType) && this._configurationDictionary[CSVHeaderEnum.CommonEuropeanTimeType])
                        csvWriter.WriteField(Constants.CSVHeaderEN.TimeZone);
                }
                catch (System.Exception ex)
                {
                    throw new Exception.WriterException("Errore durante la scrittura dell'header del contenuto del csv", ex);
                }

                csvWriter.NextRecord();

                for (int i = 0; i < logs.Count; ++i)
                {
                    IGoogleMeetLogTO log = logs[i];

                    try
                    {
                        /*Mandatory*/
                        csvWriter.WriteField(log.Date.GetSafeString());
                        csvWriter.WriteField(log.EventName.GetSafeString());
                        csvWriter.WriteField(log.EventDescription.GetSafeString());
                        csvWriter.WriteField(log.MeetingCode.GetSafeString());
                        csvWriter.WriteField(log.PartecipantIdentifier.GetSafeString());
                        csvWriter.WriteField(log.ExternalPartecipantIdentifier.GetSafeString());
                        csvWriter.WriteField(log.ClientType.GetSafeString());
                        /*Mandatory*/

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingOwnerEmail) && this._configurationDictionary[CSVHeaderEnum.MeetingOwnerEmail])
                            csvWriter.WriteField(log.MeetingOwnerEmail.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ProductType) && this._configurationDictionary[CSVHeaderEnum.ProductType])
                            csvWriter.WriteField(log.ProductType.GetSafeString());

                        //Mandatory
                        csvWriter.WriteField(log.Duration);
                        csvWriter.WriteField(log.EffectiveMeetingDurationInSeconds);
                        csvWriter.WriteField(log.EffectiveMeetingDurationInMinutes);
                        csvWriter.WriteField(log.EffectiveMeetingDurationInHours);

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CallEvaluationOn5) && this._configurationDictionary[CSVHeaderEnum.CallEvaluationOn5])
                            csvWriter.WriteField(log.CallEvaluationOn5.GetSafeString());

                        csvWriter.WriteField(log.PartecipantName.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.IPAddress) && this._configurationDictionary[CSVHeaderEnum.IPAddress])
                            csvWriter.WriteField(log.IPAddress.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.City) && this._configurationDictionary[CSVHeaderEnum.City])
                            csvWriter.WriteField(log.City.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.Country) && this._configurationDictionary[CSVHeaderEnum.Country])
                            csvWriter.WriteField(log.Country.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NETRoundTrip) && this._configurationDictionary[CSVHeaderEnum.NETRoundTrip])
                            csvWriter.WriteField(log.NETRoundTrip.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TransportProtocol) && this._configurationDictionary[CSVHeaderEnum.TransportProtocol])
                            csvWriter.WriteField(log.TransportProtocol.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedUploadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedUploadBandwidthInkbps])
                            csvWriter.WriteField(log.EstimatedUploadBandwidthInkbps.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EstimatedDownloadBandwidthInkbps) && this._configurationDictionary[CSVHeaderEnum.EstimatedDownloadBandwidthInkbps])
                            csvWriter.WriteField(log.EstimatedDownloadBandwidthInkbps.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMax])
                            csvWriter.WriteField(log.AudioReceivePacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioReceivePacketLossMean])
                            csvWriter.WriteField(log.AudioReceivePacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.AudioReceiveDuration])
                            csvWriter.WriteField(log.AudioReceiveDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioAudioSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioAudioSending])
                            csvWriter.WriteField(log.BitRatioAudioSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMax])
                            csvWriter.WriteField(log.AudioSendPacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.AudioSendPacketLossMean])
                            csvWriter.WriteField(log.AudioSendPacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendDuration) && this._configurationDictionary[CSVHeaderEnum.AudioSendDuration])
                            csvWriter.WriteField(log.AudioSendDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CalendarEventIdentifier) && this._configurationDictionary[CSVHeaderEnum.CalendarEventIdentifier])
                            csvWriter.WriteField(log.CalendarEventIdentifier.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ConferenceID) && this._configurationDictionary[CSVHeaderEnum.ConferenceID])
                            csvWriter.WriteField(log.ConferenceID.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMeaninms])
                            csvWriter.WriteField(log.NetworkRecvJitterMeaninms.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkRecvJitterMaxinms) && this._configurationDictionary[CSVHeaderEnum.NetworkRecvJitterMaxinms])
                            csvWriter.WriteField(log.NetworkRecvJitterMaxinms.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkSendJitterMeaninms) && this._configurationDictionary[CSVHeaderEnum.NetworkSendJitterMeaninms])
                            csvWriter.WriteField(log.NetworkSendJitterMeaninms.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastReception) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastReception])
                            csvWriter.WriteField(log.BitRatioScreencastReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveFPSMean])
                            csvWriter.WriteField(log.ScreencastReceiveFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveLongSideMedian])
                            csvWriter.WriteField(log.ScreencastReceiveLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMax])
                            csvWriter.WriteField(log.ScreencastReceivePacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceivePacketLossMean])
                            csvWriter.WriteField(log.ScreencastReceivePacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveDuration])
                            csvWriter.WriteField(log.ScreencastReceiveDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceiveShortSideMedian])
                            csvWriter.WriteField(log.ScreencastReceiveShortSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastSending])
                            csvWriter.WriteField(log.BitRatioScreencastSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendFPSMean])
                            csvWriter.WriteField(log.ScreencastSendFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendLongSideMedian])
                            csvWriter.WriteField(log.ScreencastSendLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMax])
                            csvWriter.WriteField(log.ScreencastSendPacketLossMax);
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendPacketLossMean])
                            csvWriter.WriteField(log.ScreencastSendPacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendDuration])
                            csvWriter.WriteField(log.ScreencastSendDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendShortSideMedian])
                            csvWriter.WriteField(log.ScreencastSendShortSideMedian.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveFPSMean])
                            csvWriter.WriteField(log.VideoReceiveFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveLongSideMedian])
                            csvWriter.WriteField(log.VideoReceiveLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMax])
                            csvWriter.WriteField(log.VideoReceivePacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceivePacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoReceivePacketLossMean])
                            csvWriter.WriteField(log.VideoReceivePacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveDuration) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveDuration])
                            csvWriter.WriteField(log.VideoReceiveDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoReceiveShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoReceiveShortSideMedian])
                            csvWriter.WriteField(log.VideoReceiveShortSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkCongestionRatio) && this._configurationDictionary[CSVHeaderEnum.NetworkCongestionRatio])
                            csvWriter.WriteField(log.NetworkCongestionRatio.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioVideoSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioVideoSending])
                            csvWriter.WriteField(log.BitRatioVideoSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendFPSMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendFPSMean])
                            csvWriter.WriteField(log.VideoSendFPSMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendLongSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendLongSideMedian])
                            csvWriter.WriteField(log.VideoSendLongSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMax) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMax])
                            csvWriter.WriteField(log.VideoSendPacketLossMax.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendPacketLossMean) && this._configurationDictionary[CSVHeaderEnum.VideoSendPacketLossMean])
                            csvWriter.WriteField(log.VideoSendPacketLossMean.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendDuration) && this._configurationDictionary[CSVHeaderEnum.VideoSendDuration])
                            csvWriter.WriteField(log.VideoSendDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendShortSideMedian) && this._configurationDictionary[CSVHeaderEnum.VideoSendShortSideMedian])
                            csvWriter.WriteField(log.VideoSendShortSideMedian.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionReason) && this._configurationDictionary[CSVHeaderEnum.ActionReason])
                            csvWriter.WriteField(log.ActionReason.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionDescription) && this._configurationDictionary[CSVHeaderEnum.ActionDescription])
                            csvWriter.WriteField(log.ActionDescription.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetDisplayNames) && this._configurationDictionary[CSVHeaderEnum.TargetDisplayNames])
                            csvWriter.WriteField(log.TargetDisplayNames.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetEmail) && this._configurationDictionary[CSVHeaderEnum.TargetEmail])
                            csvWriter.WriteField(log.TargetEmail.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TargetPhoneNumber) && this._configurationDictionary[CSVHeaderEnum.TargetPhoneNumber])
                            csvWriter.WriteField(log.TargetPhoneNumber.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.MeetingStartDate])
                            csvWriter.WriteField(log.MeetingStartDate.GetSafeString());
                        if(this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingStartDate])
                            csvWriter.WriteField(log.EffectiveMeetingStartDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEndDate])
                            csvWriter.WriteField(log.MeetingEndDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.EffectiveMeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.EffectiveMeetingEndDate])
                            csvWriter.WriteField(log.EffectiveMeetingEndDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEnteringDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEnteringDate])
                            csvWriter.WriteField(log.MeetingEnteringDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInDecimal])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInDecimal.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInSeconds])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInSeconds.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInMinutes])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInMinutes.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipationInHours) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipationInHours])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipationInHours.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CommonEuropeanTimeType) && this._configurationDictionary[CSVHeaderEnum.CommonEuropeanTimeType])
                            csvWriter.WriteField(log.TimeZone.GetSafeString());

                        csvWriter.NextRecord();
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception.WriterException($"Errore durante la scrittura dell {i + 1}-esimo record del contenuto del csv", ex);
                    }
                }

                writer.Flush();

                return this._encoding.GetString(mem.ToArray());

            }
        }
        #endregion
    }
}
