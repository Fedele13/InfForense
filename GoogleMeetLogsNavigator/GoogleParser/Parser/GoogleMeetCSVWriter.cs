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

        #endregion

        #region .ctor

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="configurationDictionary">The Configuration Dictionary</param>
        public GoogleMeetCSVWriter(IDictionary<CSVHeaderEnum, bool> configurationDictionary)
        {
            if (configurationDictionary != null)
            {
                this._configurationDictionary = configurationDictionary;
            }
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="delimiter">The delimiter to use to build csv file</param>
        public GoogleMeetCSVWriter(string delimiter = ",")
        {
            this._csvDelimiter = delimiter;
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="encoding"></param>
        public GoogleMeetCSVWriter(Encoding encoding)
        {
            if (encoding != null)
            {
                this._encoding = encoding;
            }
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="configurationDictionary">The Configuration Dictionary</param>
        /// <param name="delimiter">The delimiter to use to build csv file</param>
        public GoogleMeetCSVWriter(IDictionary<CSVHeaderEnum, bool> configurationDictionary, Encoding encoding, string delimiter = ",")
        {

            if (configurationDictionary != null)
            {
                this._configurationDictionary = configurationDictionary;
            }

            if (encoding != null)
            {
                this._encoding = encoding;
            }

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
        /// Create csv contetn
        /// </summary>
        /// <param name="logs">Logs to write in csv file</param>
        /// <return>The string content in csv format</return>
        /// <exception cref="Exception.WriterException">
        public string ToGoogleMeetCsv(IList<IGoogleMeetLogTO> logs)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.SanitizeForInjection = true;
                csvWriter.Configuration.Delimiter = this._csvDelimiter;
                csvWriter.Configuration.ShouldQuote = (field, context) => 
                { 
                    Debug.WriteLine(field); 
                    Debug.WriteLine(field.Contains(_csvDelimiter)); 
                    return field.Contains(_csvDelimiter); 
                };

                if (this._configurationDictionary == null)
                {
                    this._configurationDictionary = GetDefaultConfiguration();
                }

                try
                {
                    /*Mandatory*/
                    csvWriter.WriteField(Constants.CSVHeader.Date);
                    csvWriter.WriteField(Constants.CSVHeader.EventName);
                    csvWriter.WriteField(Constants.CSVHeader.EventDescription);
                    csvWriter.WriteField(Constants.CSVHeader.MeetingCode);
                    csvWriter.WriteField(Constants.CSVHeader.PartecipantIdentifier);
                    csvWriter.WriteField(Constants.CSVHeader.ExternalPartecipantIdentifier);
                    csvWriter.WriteField(Constants.CSVHeader.ClientType);
                    /*Mandatory*/

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingOwnerEmail) && this._configurationDictionary[CSVHeaderEnum.MeetingOwnerEmail])
                        csvWriter.WriteField(Constants.CSVHeader.MeetingOwnerEmail);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ProductType) && this._configurationDictionary[CSVHeaderEnum.ProductType])
                        csvWriter.WriteField(Constants.CSVHeader.ProductType);

                    //Mandatory
                    csvWriter.WriteField(Constants.CSVHeader.Duration);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CallEvaluationOn5) && this._configurationDictionary[CSVHeaderEnum.CallEvaluationOn5])
                        csvWriter.WriteField(Constants.CSVHeader.CallEvaluationOn5);

                    csvWriter.WriteField(Constants.CSVHeader.PartecipantName);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.IPAddress) && this._configurationDictionary[CSVHeaderEnum.IPAddress])
                        csvWriter.WriteField(Constants.CSVHeader.IPAddress);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.City) && this._configurationDictionary[CSVHeaderEnum.City])
                        csvWriter.WriteField(Constants.CSVHeader.City2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.Nation) && this._configurationDictionary[CSVHeaderEnum.Nation])
                        csvWriter.WriteField(Constants.CSVHeader.Nation);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NETRoundTrip) && this._configurationDictionary[CSVHeaderEnum.NETRoundTrip])
                        csvWriter.WriteField(Constants.CSVHeader.NETRoundTrip);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TransportProtocol) && this._configurationDictionary[CSVHeaderEnum.TransportProtocol])
                        csvWriter.WriteField(Constants.CSVHeader.TransportProtocol);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.PredictedBandWidthLoading) && this._configurationDictionary[CSVHeaderEnum.PredictedBandWidthLoading])
                        csvWriter.WriteField(Constants.CSVHeader.PredictedBandWidthLoading);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.PredictedBandWidthUploading) && this._configurationDictionary[CSVHeaderEnum.PredictedBandWidthUploading])
                        csvWriter.WriteField(Constants.CSVHeader.PredictedBandWidthUploading);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxReceptionAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxReceptionAudioPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.MaxReceptionAudioPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageReceptionAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageReceptionAudioPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.AverageReceptionAudioPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceptionDuration) && this._configurationDictionary[CSVHeaderEnum.AudioReceptionDuration])
                        csvWriter.WriteField(Constants.CSVHeader.AudioReceptionDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioAudioSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioAudioSending])
                        csvWriter.WriteField(Constants.CSVHeader.BitRatioAudioSending2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxSendingAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxSendingAudioPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.MaxSendingAudioPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageSendingAudioPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.AverageSendingAudioPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendingDuration) && this._configurationDictionary[CSVHeaderEnum.AudioSendingDuration])
                        csvWriter.WriteField(Constants.CSVHeader.AudioSendingDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CalendarEventIdentifier) && this._configurationDictionary[CSVHeaderEnum.CalendarEventIdentifier])
                        csvWriter.WriteField(Constants.CSVHeader.CalendarEventIdentifier);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ConferenceID) && this._configurationDictionary[CSVHeaderEnum.ConferenceID])
                        csvWriter.WriteField(Constants.CSVHeader.ConferenceID);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageReceptionFlickering) && this._configurationDictionary[CSVHeaderEnum.AverageReceptionFlickering])
                        csvWriter.WriteField(Constants.CSVHeader.AverageReceptionFlickering);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxReceptionFilckering) && this._configurationDictionary[CSVHeaderEnum.MaxReceptionFilckering])
                        csvWriter.WriteField(Constants.CSVHeader.MaxReceptionFilckering);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingFlickering) && this._configurationDictionary[CSVHeaderEnum.AverageSendingFlickering])
                        csvWriter.WriteField(Constants.CSVHeader.AverageSendingFlickering);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastReception) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastReception])
                        csvWriter.WriteField(Constants.CSVHeader.BitRatioScreencastReception2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageScreecastReception) && this._configurationDictionary[CSVHeaderEnum.AverageScreecastReception])
                        csvWriter.WriteField(Constants.CSVHeader.AverageScreecastReception);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianScreencastReception) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianScreencastReception])
                        csvWriter.WriteField(Constants.CSVHeader.LongSideMedianScreencastReception);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxReceptionScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxReceptionScreencastPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.MaxReceptionScreencastPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageReceptionScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageReceptionScreencastPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.AverageReceptionScreencastPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceptionDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceptionDuration])
                        csvWriter.WriteField(Constants.CSVHeader.ScreencastReceptionDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianScreencastReception) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianScreencastReception])
                        csvWriter.WriteField(Constants.CSVHeader.ShortSideMedianScreencastReception);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastSending])
                        csvWriter.WriteField(Constants.CSVHeader.BitRatioScreencastSending2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageScreecastSending) && this._configurationDictionary[CSVHeaderEnum.AverageScreecastSending])
                        csvWriter.WriteField(Constants.CSVHeader.AverageScreecastSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianScreencastSending) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianScreencastSending])
                        csvWriter.WriteField(Constants.CSVHeader.LongSideMedianScreencastSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxSendingScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxSendingScreencastPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.MaxSendingScreencastPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageSendingScreencastPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.AverageSendingScreencastPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendingDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendingDuration])
                        csvWriter.WriteField(Constants.CSVHeader.ScreencastSendingDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianScreencastSending) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianScreencastSending])
                        csvWriter.WriteField(Constants.CSVHeader.ShortSideMedianScreencastSending);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageVideoReception) && this._configurationDictionary[CSVHeaderEnum.AverageVideoReception])
                        csvWriter.WriteField(Constants.CSVHeader.AverageVideoReception);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianVideoReception) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianVideoReception])
                        csvWriter.WriteField(Constants.CSVHeader.LongSideMedianVideoReception);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxVideoReceptionPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxVideoReceptionPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.MaxVideoReceptionPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageVideoReceptionPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageVideoReceptionPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.AverageVideoReceptionPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ReceptionVideoDuration) && this._configurationDictionary[CSVHeaderEnum.ReceptionVideoDuration])
                        csvWriter.WriteField(Constants.CSVHeader.ReceptionVideoDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianVideoReception) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianVideoReception])
                        csvWriter.WriteField(Constants.CSVHeader.ShortSideMedianVideoReception);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkCongestion) && this._configurationDictionary[CSVHeaderEnum.NetworkCongestion])
                        csvWriter.WriteField(Constants.CSVHeader.NetworkCongestion);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioVideoSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioVideoSending])
                        csvWriter.WriteField(Constants.CSVHeader.BitRatioVideoSending2);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageVideoSending) && this._configurationDictionary[CSVHeaderEnum.AverageVideoSending])
                        csvWriter.WriteField(Constants.CSVHeader.AverageVideoSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianVideoSending) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianVideoSending])
                        csvWriter.WriteField(Constants.CSVHeader.LongSideMedianVideoSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxSendingVideoPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxSendingVideoPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.MaxSendingVideoPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingVideoPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageSendingVideoPacketsLost])
                        csvWriter.WriteField(Constants.CSVHeader.AverageSendingVideoPacketsLost);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendingDuration) && this._configurationDictionary[CSVHeaderEnum.VideoSendingDuration])
                        csvWriter.WriteField(Constants.CSVHeader.VideoSendingDuration);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianVideoSending) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianVideoSending])
                        csvWriter.WriteField(Constants.CSVHeader.ShortSideMedianVideoSending);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionCause) && this._configurationDictionary[CSVHeaderEnum.ActionCause])
                        csvWriter.WriteField(Constants.CSVHeader.ActionCause);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionDescription) && this._configurationDictionary[CSVHeaderEnum.ActionDescription])
                        csvWriter.WriteField(Constants.CSVHeader.ActionDescription);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VisualizedDestinationNames) && this._configurationDictionary[CSVHeaderEnum.VisualizedDestinationNames])
                        csvWriter.WriteField(Constants.CSVHeader.VisualizedDestinationNames);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.DestinationEmailsAddresses) && this._configurationDictionary[CSVHeaderEnum.DestinationEmailsAddresses])
                        csvWriter.WriteField(Constants.CSVHeader.DestinationEmailsAddresses);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.DestinationPhoneNumber) && this._configurationDictionary[CSVHeaderEnum.DestinationPhoneNumber])
                        csvWriter.WriteField(Constants.CSVHeader.DestinationPhoneNumber);

                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.MeetingStartDate])
                        csvWriter.WriteField(Constants.CSVHeader.MeetingStartDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEndDate])
                        csvWriter.WriteField(Constants.CSVHeader.MeetingEndDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEnteringDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEnteringDate])
                        csvWriter.WriteField(Constants.CSVHeader.MeetingEnteringDate);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipation) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipation])
                        csvWriter.WriteField(Constants.CSVHeader.TotalMeetingUserPartecipation);
                    if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CommonEuropeanTimeType) && this._configurationDictionary[CSVHeaderEnum.CommonEuropeanTimeType])
                        csvWriter.WriteField(Constants.CSVHeader.CommonEuropeanTimeType);
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
                        csvWriter.WriteField(log.Date.GetSafeString().Contains(this._csvDelimiter) ? "\"" + log.Date.GetSafeString() + "\"" : log.Date.GetSafeString());
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

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CallEvaluationOn5) && this._configurationDictionary[CSVHeaderEnum.CallEvaluationOn5])
                            csvWriter.WriteField(log.CallEvaluationOn5.GetSafeString());

                        csvWriter.WriteField(log.PartecipantName.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.IPAddress) && this._configurationDictionary[CSVHeaderEnum.IPAddress])
                            csvWriter.WriteField(log.IPAddress.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.City) && this._configurationDictionary[CSVHeaderEnum.City])
                            csvWriter.WriteField(log.City.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.Nation) && this._configurationDictionary[CSVHeaderEnum.Nation])
                            csvWriter.WriteField(log.Nation.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NETRoundTrip) && this._configurationDictionary[CSVHeaderEnum.NETRoundTrip])
                            csvWriter.WriteField(log.NETRoundTrip.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TransportProtocol) && this._configurationDictionary[CSVHeaderEnum.TransportProtocol])
                            csvWriter.WriteField(log.TransportProtocol.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.PredictedBandWidthLoading) && this._configurationDictionary[CSVHeaderEnum.PredictedBandWidthLoading])
                            csvWriter.WriteField(log.PredictedBandWidthLoading.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.PredictedBandWidthUploading) && this._configurationDictionary[CSVHeaderEnum.PredictedBandWidthUploading])
                            csvWriter.WriteField(log.PredictedBandWidthUploading.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxReceptionAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxReceptionAudioPacketsLost])
                            csvWriter.WriteField(log.MaxReceptionAudioPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageReceptionAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageReceptionAudioPacketsLost])
                            csvWriter.WriteField(log.AverageReceptionAudioPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioReceptionDuration) && this._configurationDictionary[CSVHeaderEnum.AudioReceptionDuration])
                            csvWriter.WriteField(log.AudioReceptionDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioAudioSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioAudioSending])
                            csvWriter.WriteField(log.BitRatioAudioSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxSendingAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxSendingAudioPacketsLost])
                            csvWriter.WriteField(log.MaxSendingAudioPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingAudioPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageSendingAudioPacketsLost])
                            csvWriter.WriteField(log.AverageSendingAudioPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AudioSendingDuration) && this._configurationDictionary[CSVHeaderEnum.AudioSendingDuration])
                            csvWriter.WriteField(log.AudioSendingDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CalendarEventIdentifier) && this._configurationDictionary[CSVHeaderEnum.CalendarEventIdentifier])
                            csvWriter.WriteField(log.CalendarEventIdentifier.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ConferenceID) && this._configurationDictionary[CSVHeaderEnum.ConferenceID])
                            csvWriter.WriteField(log.ConferenceID.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageReceptionFlickering) && this._configurationDictionary[CSVHeaderEnum.AverageReceptionFlickering])
                            csvWriter.WriteField(log.AverageReceptionFlickering.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxReceptionFilckering) && this._configurationDictionary[CSVHeaderEnum.MaxReceptionFilckering])
                            csvWriter.WriteField(log.MaxReceptionFilckering.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingFlickering) && this._configurationDictionary[CSVHeaderEnum.AverageSendingFlickering])
                            csvWriter.WriteField(log.AverageSendingFlickering.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastReception) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastReception])
                            csvWriter.WriteField(log.BitRatioScreencastReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageScreecastReception) && this._configurationDictionary[CSVHeaderEnum.AverageScreecastReception])
                            csvWriter.WriteField(log.AverageScreecastReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianScreencastReception) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianScreencastReception])
                            csvWriter.WriteField(log.LongSideMedianScreencastReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxReceptionScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxReceptionScreencastPacketsLost])
                            csvWriter.WriteField(log.MaxReceptionScreencastPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageReceptionScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageReceptionScreencastPacketsLost])
                            csvWriter.WriteField(log.AverageReceptionScreencastPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastReceptionDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastReceptionDuration])
                            csvWriter.WriteField(log.ScreencastReceptionDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianScreencastReception) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianScreencastReception])
                            csvWriter.WriteField(log.ShortSideMedianScreencastReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioScreencastSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioScreencastSending])
                            csvWriter.WriteField(log.BitRatioScreencastSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageScreecastSending) && this._configurationDictionary[CSVHeaderEnum.AverageScreecastSending])
                            csvWriter.WriteField(log.AverageScreecastSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianScreencastSending) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianScreencastSending])
                            csvWriter.WriteField(log.LongSideMedianScreencastSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxSendingScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxSendingScreencastPacketsLost])
                            csvWriter.WriteField(log.MaxSendingScreencastPacketsLost);
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingScreencastPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageSendingScreencastPacketsLost])
                            csvWriter.WriteField(log.AverageSendingScreencastPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ScreencastSendingDuration) && this._configurationDictionary[CSVHeaderEnum.ScreencastSendingDuration])
                            csvWriter.WriteField(log.ScreencastSendingDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianScreencastSending) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianScreencastSending])
                            csvWriter.WriteField(log.ShortSideMedianScreencastSending.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageVideoReception) && this._configurationDictionary[CSVHeaderEnum.AverageVideoReception])
                            csvWriter.WriteField(log.AverageVideoReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianVideoReception) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianVideoReception])
                            csvWriter.WriteField(log.LongSideMedianVideoReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxVideoReceptionPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxVideoReceptionPacketsLost])
                            csvWriter.WriteField(log.MaxVideoReceptionPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageVideoReceptionPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageVideoReceptionPacketsLost])
                            csvWriter.WriteField(log.AverageVideoReceptionPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ReceptionVideoDuration) && this._configurationDictionary[CSVHeaderEnum.ReceptionVideoDuration])
                            csvWriter.WriteField(log.ReceptionVideoDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianVideoReception) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianVideoReception])
                            csvWriter.WriteField(log.ShortSideMedianVideoReception.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.NetworkCongestion) && this._configurationDictionary[CSVHeaderEnum.NetworkCongestion])
                            csvWriter.WriteField(log.NetworkCongestion.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.BitRatioVideoSending) && this._configurationDictionary[CSVHeaderEnum.BitRatioVideoSending])
                            csvWriter.WriteField(log.BitRatioVideoSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageVideoSending) && this._configurationDictionary[CSVHeaderEnum.AverageVideoSending])
                            csvWriter.WriteField(log.AverageVideoSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.LongSideMedianVideoSending) && this._configurationDictionary[CSVHeaderEnum.LongSideMedianVideoSending])
                            csvWriter.WriteField(log.LongSideMedianVideoSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MaxSendingVideoPacketsLost) && this._configurationDictionary[CSVHeaderEnum.MaxSendingVideoPacketsLost])
                            csvWriter.WriteField(log.MaxSendingVideoPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.AverageSendingVideoPacketsLost) && this._configurationDictionary[CSVHeaderEnum.AverageSendingVideoPacketsLost])
                            csvWriter.WriteField(log.AverageSendingVideoPacketsLost.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VideoSendingDuration) && this._configurationDictionary[CSVHeaderEnum.VideoSendingDuration])
                            csvWriter.WriteField(log.VideoSendingDuration.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ShortSideMedianVideoSending) && this._configurationDictionary[CSVHeaderEnum.ShortSideMedianVideoSending])
                            csvWriter.WriteField(log.ShortSideMedianVideoSending.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionCause) && this._configurationDictionary[CSVHeaderEnum.ActionCause])
                            csvWriter.WriteField(log.ActionCause.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.ActionDescription) && this._configurationDictionary[CSVHeaderEnum.ActionDescription])
                            csvWriter.WriteField(log.ActionDescription.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.VisualizedDestinationNames) && this._configurationDictionary[CSVHeaderEnum.VisualizedDestinationNames])
                            csvWriter.WriteField(log.VisualizedDestinationNames.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.DestinationEmailsAddresses) && this._configurationDictionary[CSVHeaderEnum.DestinationEmailsAddresses])
                            csvWriter.WriteField(log.DestinationEmailsAddresses.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.DestinationPhoneNumber) && this._configurationDictionary[CSVHeaderEnum.DestinationPhoneNumber])
                            csvWriter.WriteField(log.DestinationPhoneNumber.GetSafeString());

                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingStartDate) && this._configurationDictionary[CSVHeaderEnum.MeetingStartDate])
                            csvWriter.WriteField(log.MeetingStartDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEndDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEndDate])
                            csvWriter.WriteField(log.MeetingEndDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.MeetingEnteringDate) && this._configurationDictionary[CSVHeaderEnum.MeetingEnteringDate])
                            csvWriter.WriteField(log.MeetingEnteringDate.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.TotalMeetingUserPartecipation) && this._configurationDictionary[CSVHeaderEnum.TotalMeetingUserPartecipation])
                            csvWriter.WriteField(log.TotalMeetingUserPartecipation.GetSafeString());
                        if (this._configurationDictionary.ContainsKey(CSVHeaderEnum.CommonEuropeanTimeType) && this._configurationDictionary[CSVHeaderEnum.CommonEuropeanTimeType])
                            csvWriter.WriteField(log.CommonEuropeanTimeType.GetSafeString());

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
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDictionary<CSVHeaderEnum, bool> GetDefaultConfiguration()
        {
            IDictionary<CSVHeaderEnum, bool> defaultConfigurationDictionary = new Dictionary<CSVHeaderEnum, bool>();

            for (int headerEnum = 9; headerEnum < Enum.GetValues(typeof(CSVHeaderEnum)).Length; ++headerEnum)
            {
                defaultConfigurationDictionary.Add((CSVHeaderEnum)headerEnum, true);
            }
            return defaultConfigurationDictionary;
        }

        #endregion
    }
}
