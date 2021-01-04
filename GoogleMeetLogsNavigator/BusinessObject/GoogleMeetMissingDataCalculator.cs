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

        #endregion

        #region .ctor

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="reader">The ICSVReader instance</param>
        /// <exception cref="BusinessObject.Exception.CalculationException"></exception>
        public GoogleMeetMissingDataCalculator(IDictionary<string, IList<IGoogleMeetLogTO>> readerDictionary, bool forceUpdate = false)
        {
            if (readerDictionary == null)
            {
                throw new ArgumentNullException("readerDictionary is null");
            }
            this._readerDictionary = readerDictionary;
            this._forceUpdate = forceUpdate;
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

                            logsModel.Add(log.MapTransferObjectITAInModel());
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

            IList<IGoogleMeetLogTO> filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExitITA).ToList();
            string cet = string.Empty;
            DateTime minDateTime = filteredLogs.Select(item =>item.Date.ConvertGooogleMeetDataInDateTime(out cet)).Min();
            int meetingDurationInSeconds = int.Parse(filteredLogs.Where(item => item.Date == minDateTime.ConvertGoogleDateTimeInString(cet)).Select(item => item.Duration).FirstOrDefault());
            string meetingStartDate = minDateTime.ConvertGoogleDateTimeInString(cet); ;
            string meetingEndDate = string.Empty;
            if (meetingDurationInSeconds > 0)
            {
                meetingStartDate = minDateTime.AddSeconds(-meetingDurationInSeconds).ConvertGoogleDateTimeInString(cet);
            }

            meetingEndDate = filteredLogs.Select(item => item.Date.ConvertGooogleMeetDataInDateTime()).Max().ConvertGoogleDateTimeInString(cet);

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

                            DateTime effectiveMeetingStartDateString = partecipantLog.EffectiveMeetingStartDate.ConvertGooogleMeetDataInDateTime();

                            if (isDateTimeToUpdate(partecipantLog.MeetingEndDate) || this._forceUpdate)
                                partecipantLog.MeetingEndDate = meetingEndDate;

                            if (isDateTimeToUpdate(partecipantLog.MeetingEnteringDate) || this._forceUpdate)
                                partecipantLog.MeetingEnteringDate = partecipantLog.Date.ConvertGooogleMeetDataInDateTime(out string cet2).AddSeconds(-int.Parse(partecipantLog.Duration)).ConvertGoogleDateTimeInString(cet2);

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInSeconds) || this._forceUpdate)
                                partecipantLog.TotalMeetingUserPartecipationInSeconds = partecipantsLogs.Sum(item => int.Parse(item.Duration)).ToString();

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInMinutes) || this._forceUpdate)
                                partecipantLog.TotalMeetingUserPartecipationInMinutes = int.Parse(partecipantLog.TotalMeetingUserPartecipationInSeconds) <= 0 ? 0.ToString() : (int.Parse(partecipantLog.TotalMeetingUserPartecipationInSeconds) / 60).ToString();

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInHours) || this._forceUpdate)
                                partecipantLog.TotalMeetingUserPartecipationInHours = int.Parse(partecipantLog.TotalMeetingUserPartecipationInMinutes) <= 0 ? 0.ToString() : (int.Parse(partecipantLog.TotalMeetingUserPartecipationInMinutes) / 60).ToString();

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipationInDecimal) || this._forceUpdate)
                            {
                                TimeSpan time = TimeSpan.FromSeconds(double.Parse(partecipantLog.TotalMeetingUserPartecipationInSeconds));
                                double dec = time.Hours + (time.Minutes / 60) + (time.Seconds / 3600);
                                partecipantLog.TotalMeetingUserPartecipationInHours = dec.ToString();
                            }
                                
                            if (string.IsNullOrEmpty(partecipantLog.TimeZone) || this._forceUpdate)
                                partecipantLog.TimeZone = cet;

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
