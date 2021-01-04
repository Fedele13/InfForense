using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleMeetLogsNavigator.BO
{
    /// <summary>
    /// This class calculates the missing data in csv file
    /// </summary>
    public class GoogleMeetMissingDataCalculator
    {
        #region private

        /// <summary>
        /// The reader dictionary
        /// </summary>
        private IDictionary<string, IList<IGoogleMeetLogTO>> _readerDictionary = null;

        /// <summary>
        /// True force update, no otherwise
        /// </summary>
        private bool _forceUpdate = false;

        /// <summary>
        /// 
        /// </summary>
        private string _language = Constants.Langauges.ITA;

        #endregion

        #region .ctor

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="reader">The ICSVReader instance</param>
        /// <exception cref="BusinessObject.Exception.CalculationException"></exception>
        public GoogleMeetMissingDataCalculator(IDictionary<string, IList<IGoogleMeetLogTO>> readerDictionary, string language = Constants.Langauges.ITA, bool forceUpdate = false)
        {
            if (readerDictionary == null)
            {
                throw new ArgumentNullException("readerDictionary is null");
            }
            this._readerDictionary = readerDictionary;
            this._forceUpdate = forceUpdate;
            this._language = language;
            this.MeetingLogsDictionary = new Dictionary<string, IList<GoogleMeetLogModel>>();
            foreach (string meetingKey in this._readerDictionary.Keys)
            {
                IList<GoogleMeetLogModel> logsModel = new List<GoogleMeetLogModel>();
                try
                {
                    IDictionary<string, IGoogleMeetLogTO> resultAdditionalDataDictionary = calculateAdditionalData(this._readerDictionary[meetingKey]);

                    foreach (string keyLog in resultAdditionalDataDictionary.Keys)
                    {
                        try
                        {
                            string[] keysLog = splitKeyLog(keyLog);

                            IGoogleMeetLogTO log = this._readerDictionary[meetingKey].
                                Where(item => item.Date == keysLog[0] && getPartecipantLogIdentifier(item) == keysLog[1] && item.ClientType == keysLog[2]).FirstOrDefault();

                            logsModel.Add(log.MapTransferObjectInModel(this._language));
                        }
                        catch (Exception ex)
                        {
                            throw new BusinessObject.Exception.CalculationException($"Errore durante il calcolo dei dati mancanti del log con chiave: {keyLog}", ex);
                        }
                    }
                    this.MeetingLogsDictionary.Add(meetingKey, logsModel.OrderByDescending(item => item.Date).ToList());
                }
                catch (Exception ex)
                {
                    throw new BusinessObject.Exception.CalculationException(ex.Message, ex);
                }
            }
        }

        #endregion

        #region private

        /// <summary>
        /// Private method used to calculate the missing data
        /// </summary>
        /// <param name="logs">Logs of the same meeting</param>
        /// <returns>IDictionary<string, IGoogleMeetLogTO></returns>
        /// <exception cref="BusinessObject.Exception.CalculationException"></exception>
        private IDictionary<string, IGoogleMeetLogTO> calculateAdditionalData(IList<IGoogleMeetLogTO> logs)
        {
            if (logs.Select(item => item.MeetingCode).Distinct().Count() > 1) 
                throw new InvalidOperationException("The input logs are not of the same google meeting");

            IDictionary<string, IGoogleMeetLogTO> resultDictionary = new Dictionary<string, IGoogleMeetLogTO>();

            IList<IGoogleMeetLogTO> filteredLogs = new List<IGoogleMeetLogTO>();

            string triedLangauge = this._language;
            if (string.IsNullOrEmpty(this._language) || this._language == Constants.Langauges.ITA)
            {
                filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExitITA).ToList();
            }
            else if (this._language == Constants.Langauges.EN)
            {
                filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExitEN).ToList();
            }

            //questa parte di codice è un po' azzardata ma ci proviamo
            if (filteredLogs.Count == 0)
            {
                if (triedLangauge == Constants.Langauges.ITA)
                {
                    filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExitEN).ToList();
                }
                else if (triedLangauge == Constants.Langauges.EN)
                {
                    filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExitITA).ToList();
                }
                else
                {
                    throw new BusinessObject.Exception.CalculationException("Non è stato possibile trovare elementi per il calcolo dei dati in nessuna lingua al momento supportata");
                }
            }
          
            string timeZone = string.Empty;
            DateTime minDateTime = filteredLogs.Select(item =>item.Date.ConvertGooogleMeetDataInDateTime(this._language, out timeZone)).Min();
            int meetingDurationInSeconds = int.Parse(filteredLogs.Where(item => item.Date == minDateTime.ConvertGoogleDateTimeInString(timeZone, this._language)).Select(item => item.Duration).FirstOrDefault());
            string meetingStartDate = minDateTime.ConvertGoogleDateTimeInString(timeZone, this._language); ;
            string meetingEndDate = string.Empty;
            if (meetingDurationInSeconds > 0)
            {
                meetingStartDate = minDateTime.AddSeconds(-meetingDurationInSeconds).ConvertGoogleDateTimeInString(timeZone, this._language);
            }

            meetingEndDate = filteredLogs.Select(item => item.Date.ConvertGooogleMeetDataInDateTime(this._language, out timeZone)).Max().ConvertGoogleDateTimeInString(timeZone, this._language);

            foreach (IGoogleMeetLogTO log in filteredLogs)
            {
                try
                {
                    string partecipantIdentifier = getPartecipantLogIdentifier(log);
                    IList<IGoogleMeetLogTO> partecipantsLogs = filteredLogs.Where(item => partecipantIdentifier == getPartecipantLogIdentifier(item)).ToList();

                    foreach (IGoogleMeetLogTO partecipantLog in partecipantsLogs)
                    {
                        try
                        {
                            string key = createLogkey(partecipantLog);
                            if (resultDictionary.ContainsKey(key))
                            {
                                continue;
                            }

                            if (isDateTimeToUpdate(partecipantLog.MeetingStartDate) || this._forceUpdate)
                                partecipantLog.MeetingStartDate = meetingStartDate;

                            if (isDateTimeToUpdate(partecipantLog.MeetingEndDate) || this._forceUpdate)
                                partecipantLog.MeetingEndDate = meetingEndDate;

                            if (isDateTimeToUpdate(partecipantLog.MeetingEnteringDate) || this._forceUpdate)
                                partecipantLog.MeetingEnteringDate = partecipantLog.Date.ConvertGooogleMeetDataInDateTime(this._language, out string timeZone2).AddSeconds(-int.Parse(partecipantLog.Duration)).ConvertGoogleDateTimeInString(timeZone2, this._language);

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInSeconds) || this._forceUpdate)
                            {
                                if (string.IsNullOrEmpty(partecipantLog.EffectiveMeetingStartDate))
                                {
                                    partecipantLog.TotalMeetingUserPartecipationInSeconds = partecipantsLogs.Sum(item => int.Parse(item.Duration)).ToString();
                                }
                                else
                                {
                                    DateTime PartecipantMeetingEndDate = partecipantLog.Date.ConvertGooogleMeetDataInDateTime(this._language);
                                    DateTime EffectiveMeetingStartDate = partecipantLog.EffectiveMeetingStartDate.ConvertGooogleMeetDataInDateTime(this._language);
                                    if (PartecipantMeetingEndDate > EffectiveMeetingStartDate)
                                    {
                                        TimeSpan result = PartecipantMeetingEndDate.Subtract(EffectiveMeetingStartDate);
                                        partecipantLog.TotalMeetingUserPartecipationInSeconds = result.TotalSeconds.ToString();
                                    }
                                    else
                                    {
                                        partecipantLog.TotalMeetingUserPartecipationInSeconds = 0.ToString();
                                    }

                                    if (partecipantLog.EffectiveMeetingEndDate.ConvertGooogleMeetDataInDateTime(this._language).Subtract(EffectiveMeetingStartDate).TotalSeconds < double.Parse(partecipantLog.TotalMeetingUserPartecipationInSeconds))
                                    {
                                        partecipantLog.TotalMeetingUserPartecipationInSeconds = partecipantLog.EffectiveMeetingEndDate.ConvertGooogleMeetDataInDateTime(this._language).Subtract(EffectiveMeetingStartDate).TotalSeconds.ToString();
                                    }
                                }
                            }

                            if (isDateTimeToUpdate(partecipantLog.EffectiveMeetingStartDate))
                                partecipantLog.EffectiveMeetingStartDate = partecipantLog.MeetingStartDate;

                            if (isDateTimeToUpdate(partecipantLog.EffectiveMeetingEndDate))
                                partecipantLog.EffectiveMeetingEndDate = partecipantLog.MeetingEndDate;

                            TimeSpan time = TimeSpan.FromSeconds(double.Parse(partecipantLog.TotalMeetingUserPartecipationInSeconds));

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInMinutes) || this._forceUpdate)
                                partecipantLog.TotalMeetingUserPartecipationInMinutes = time.TotalMinutes.ToString();

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInHours) || this._forceUpdate)
                                partecipantLog.TotalMeetingUserPartecipationInHours = time.ToString();

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInDecimal) || this._forceUpdate)
                            {
                                partecipantLog.TotalMeetingUserPartecipationInDecimal = time.TotalHours.ToString();
                            }

                            if (string.IsNullOrEmpty(partecipantLog.EffectiveMeetingDurationInSeconds) || this._forceUpdate)
                            {
                                string effectiveMeetingEndDate = meetingEndDate;
                                if (string.IsNullOrEmpty(partecipantLog.EffectiveMeetingEndDate) == false)
                                {
                                    effectiveMeetingEndDate = partecipantLog.EffectiveMeetingEndDate;
                                }
                                string effectiveMeetingStartDate = meetingStartDate;
                                if (string.IsNullOrEmpty(partecipantLog.EffectiveMeetingStartDate) == false)
                                {
                                    effectiveMeetingStartDate = partecipantLog.EffectiveMeetingStartDate;
                                }

                                time = effectiveMeetingEndDate.ConvertGooogleMeetDataInDateTime(this._language).Subtract(effectiveMeetingStartDate.ConvertGooogleMeetDataInDateTime(this._language));
                                partecipantLog.EffectiveMeetingDurationInSeconds = time.TotalSeconds.ToString();
                            }

                            if (string.IsNullOrEmpty(partecipantLog.EffectiveMeetingDurationInMinutes) || this._forceUpdate)
                                partecipantLog.EffectiveMeetingDurationInMinutes = time.TotalMinutes.ToString();

                            if (string.IsNullOrEmpty(partecipantLog.EffectiveMeetingDurationInHours) || this._forceUpdate)
                                partecipantLog.EffectiveMeetingDurationInHours = time.ToString();

                            if (string.IsNullOrEmpty(partecipantLog.TimeZone) || this._forceUpdate)
                                partecipantLog.TimeZone = timeZone;

                            resultDictionary.Add(key, partecipantLog);
                        }
                        catch (Exception ex)
                        {
                            throw new BusinessObject.Exception.CalculationException(ex.Message, ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new BusinessObject.Exception.CalculationException(ex.Message, ex);
                }
            }

            return resultDictionary;
        }

        /// <summary>
        /// Private method to create key of treated log
        /// </summary>
        /// <param name="log">The Log</param>
        /// <returns>Log's key</returns>
        private string createLogkey(IGoogleMeetLogTO log)
        {
            return log.Date + "_" + getPartecipantLogIdentifier(log) + "_" + log.ClientType;
        }

        /// <summary>
        /// Private method to split the log's key
        /// </summary>
        /// <param name="key">The log's key</param>
        /// <returns>Splitted key</returns>
        private string[] splitKeyLog(string key)
        {
            return key.Split('_');
        }

        /// <summary>
        /// Private method to get partecipant identifier
        /// </summary>
        /// <param name="log">The Log</param>
        /// <returns>The partecipant identifier</returns>
        private string getPartecipantLogIdentifier(IGoogleMeetLogTO log)
        {
            return string.IsNullOrEmpty(log.PartecipantIdentifier) ? log.PartecipantName : log.PartecipantIdentifier;
        }

        /// <summary>
        /// Private method to understand if the date must be updated
        /// </summary>
        /// <param name="dateTime">The datetim in string format</param>
        /// <returns>Trye if the input must be updated, false otherwise</returns>
        private bool isDateTimeToUpdate(string dateTime)
        {
            return string.IsNullOrEmpty(dateTime) || dateTime == Constants.ConstantsValue.MinValue;
        }


        #endregion

        #region prop

        /// <summary>
        /// The Meeting Dictionary with complete data <meetingKey, Logs>
        /// </summary>
        public IDictionary<string, IList<GoogleMeetLogModel>> MeetingLogsDictionary { get; } = null;

        #endregion
    }
}
