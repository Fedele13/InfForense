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
    /// 
    /// </summary>
    public class GoogleMeetMissingDataCalculator
    {
        #region private

        /// <summary>
        /// 
        /// </summary>
        private ICSVReader<GoogleMeetingTO> _reader = null;

        #endregion

        #region .ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
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
        /// 
        /// </summary>
        /// <param name="logs">Logs of the same meeting</param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private string createLogkey(IGoogleMeetLogTO log)
        {
            return log.Date + "_" + getPartecipantLogIdentifier(log) + "_" + log.ClientType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string[] splitKeyLog(string key)
        {
            return key.Split('_');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private string getPartecipantLogIdentifier(IGoogleMeetLogTO log)
        {
            return string.IsNullOrEmpty(log.PartecipantIdentifier) ? log.PartecipantName : log.PartecipantIdentifier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private bool isDateTimeToUpdate(string dateTime)
        {
            return string.IsNullOrEmpty(dateTime) || dateTime == Constants.ConstantsValue.MinValue;
        }


        #endregion

        #region prop

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, IList<GoogleMeetLogModel>> MeetingLogsDictionary { get; } = null;

        #endregion
    }
}
