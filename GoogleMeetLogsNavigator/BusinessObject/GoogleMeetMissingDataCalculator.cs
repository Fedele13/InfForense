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
        /// The reader
        /// </summary>
        private ICSVReader<GoogleMeetingTO> _reader = null;

        #endregion

        #region .ctor

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="reader">The ICSVReader instance</param>
        /// <exception cref="BusinessObject.Exception.CalculationException"></exception>
        public GoogleMeetMissingDataCalculator(ICSVReader<GoogleMeetingTO> reader)
        {
            this._reader = reader;

            this.MeetingLogsDictionary = new Dictionary<string, IList<GoogleMeetLogModel>>();
            foreach (string meetingKey in reader.MeetingDictionary.Keys)
            {
                IList<GoogleMeetLogModel> logsModel = new List<GoogleMeetLogModel>();
                try
                {
                    IDictionary<string, IGoogleMeetLogTO> resultAdditionalDataDictionary = calculateAdditionalData(reader.MeetingDictionary[meetingKey]);

                    foreach (string keyLog in resultAdditionalDataDictionary.Keys)
                    {
                        try
                        {
                            string[] keysLog = splitKeyLog(keyLog);

                            IGoogleMeetLogTO log = reader.MeetingDictionary[meetingKey].
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

            IList<IGoogleMeetLogTO> filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExit).ToList();
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

                            if (isDateTimeToUpdate(partecipantLog.MeetingStartDate))
                                partecipantLog.MeetingStartDate = meetingStartDate;

                            if (isDateTimeToUpdate(partecipantLog.MeetingEndDate))
                                partecipantLog.MeetingEndDate = meetingEndDate;

                            if (isDateTimeToUpdate(partecipantLog.MeetingEnteringDate))
                                partecipantLog.MeetingEnteringDate = partecipantLog.Date.ConvertGooogleMeetDataInDateTime(out string cet2).AddSeconds(-int.Parse(partecipantLog.Duration)).ConvertGoogleDateTimeInString(cet2);

                            if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipation))
                                partecipantLog.TotalMeetingUserPartecipation = partecipantsLogs.Sum(item => int.Parse(item.Duration)).ToString();

                            if (string.IsNullOrEmpty(partecipantLog.CommonEuropeanTimeType))
                                partecipantLog.CommonEuropeanTimeType = cet;

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
