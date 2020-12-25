using GoogleMeetLogsNavigator.Interface;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TO;
using GoogleMeetLogsNavigator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleMeetLogsNavigator.BO
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleMeetingDataCalculator
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
        public GoogleMeetingDataCalculator(ICSVReader<GoogleMeetingTO> reader)
        {
            this._reader = reader;

            this.MeetingLogsDictionary = new Dictionary<string, IList<GoogleMeetLogModel>>();
            foreach (string meetingKey in reader.MeetingDictionary.Keys)
            {
                IList<GoogleMeetLogModel> logsModel = new List<GoogleMeetLogModel>();
                
                IDictionary<string, GoogleMeetLogTO> resultAdditionalDataDictionary = calculateAdditionalData(reader.MeetingDictionary[meetingKey]);

                foreach (string dateLog in resultAdditionalDataDictionary.Keys)
                {
                    GoogleMeetLogTO log = reader.MeetingDictionary[meetingKey].
                        Where(item => item.Date == dateLog && getPartecipantLogIdentifier(item) == getPartecipantLogIdentifier(resultAdditionalDataDictionary[dateLog])).FirstOrDefault();

                    logsModel.Add(log.MapTransferObjectInModel());
                }

                this.MeetingLogsDictionary.Add(meetingKey, logsModel);
            }
        }

        #endregion

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logs">Logs of the same meeting</param>
        /// <returns></returns>
        private IDictionary<string, GoogleMeetLogTO> calculateAdditionalData(IList<GoogleMeetLogTO> logs)
        {
            if (logs.Select(item => item.MeetingCode).Distinct().Count() > 1) 
                throw new InvalidOperationException("The input logs are not of the same google meeting");

            IDictionary<string, GoogleMeetLogTO> resultDictionary = new Dictionary<string, GoogleMeetLogTO>();

            IList<GoogleMeetLogTO> filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExit).ToList();
            string cet = string.Empty;
            DateTime minDateTime = filteredLogs.Select(item =>item.Date.ConvertGooogleMeetDataInDateTime(out cet)).Min();
            int meetingDurationInSeconds = int.Parse(filteredLogs.Where(item => item.Date.ConvertGooogleMeetDataInDateTime() == minDateTime).Select(item => item.Duration).FirstOrDefault());
            string meetingStartDate = minDateTime.AddSeconds(-meetingDurationInSeconds).ConvertGoogleDateTimeInString(cet);
            string meetingEndDate = filteredLogs.Select(item => item.Date.ConvertGooogleMeetDataInDateTime()).Max().ToString();

            foreach (GoogleMeetLogTO log in filteredLogs)
            {
                string partecipantIdentifier = getPartecipantLogIdentifier(log);
                IList<GoogleMeetLogTO> partecipantsLogs = filteredLogs.Where(item => partecipantIdentifier == getPartecipantLogIdentifier(item)).ToList();

                DateTime meetingEnteringData = DateTime.MinValue;

                foreach (GoogleMeetLogTO partecipantLog in partecipantsLogs)
                {
                    if (isDateTimeToUpdate(partecipantLog.MeetingStartDate))
                        partecipantLog.MeetingStartDate = meetingStartDate;

                    if (isDateTimeToUpdate(partecipantLog.MeetingEndDate))
                        partecipantLog.MeetingEndDate = meetingEndDate;

                    if (isDateTimeToUpdate(partecipantLog.MeetingEnteringDate))
                        partecipantLog.MeetingEnteringDate = DateTime.Parse(partecipantLog.Date).AddSeconds(-int.Parse(partecipantLog.Duration)).ToString();

                    if (string.IsNullOrEmpty(partecipantLog.TotalMeetingUserPartecipation))
                        partecipantLog.TotalMeetingUserPartecipation = partecipantsLogs.Sum(item => int.Parse(item.Duration)).ToString();

                    if (string.IsNullOrEmpty(partecipantLog.CommonEuropeanTimeType))
                        partecipantLog.CommonEuropeanTimeType = cet;

                    resultDictionary.Add(partecipantLog.Date, partecipantLog);
                }
            }

            return resultDictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private string getPartecipantLogIdentifier(GoogleMeetLogTO log)
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
        IDictionary<string, IList<GoogleMeetLogModel>> MeetingLogsDictionary { get; } = null;

        #endregion
    }
}
