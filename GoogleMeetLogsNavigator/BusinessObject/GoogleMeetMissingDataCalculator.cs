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
                
                IDictionary<string, IGoogleMeetLogTO> resultAdditionalDataDictionary = calculateAdditionalData(reader.MeetingDictionary[meetingKey]);

                foreach (string dateLog in resultAdditionalDataDictionary.Keys)
                {
                    IGoogleMeetLogTO log = reader.MeetingDictionary[meetingKey].
                        Where(item => item.Date == dateLog && getPartecipantLogIdentifier(item) == getPartecipantLogIdentifier(resultAdditionalDataDictionary[dateLog])).FirstOrDefault();

                    logsModel.Add(log.MapTransferObjectITAInModel());
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
        private IDictionary<string, IGoogleMeetLogTO> calculateAdditionalData(IList<IGoogleMeetLogTO> logs)
        {
            if (logs.Select(item => item.MeetingCode).Distinct().Count() > 1) 
                throw new InvalidOperationException("The input logs are not of the same google meeting");

            IDictionary<string, IGoogleMeetLogTO> resultDictionary = new Dictionary<string, IGoogleMeetLogTO>();

            IList<IGoogleMeetLogTO> filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExit).ToList();
            string cet = string.Empty;
            DateTime minDateTime = filteredLogs.Select(item =>item.Date.ConvertGooogleMeetDataInDateTime(out cet)).Min();
            int meetingDurationInSeconds = int.Parse(filteredLogs.Where(item => item.Date.ConvertGooogleMeetDataInDateTime() == minDateTime).Select(item => item.Duration).FirstOrDefault());
            string meetingStartDate = string.Empty;
            string meetingEndDate = string.Empty;
            if (meetingDurationInSeconds > 0)
            {
                meetingStartDate  = minDateTime.AddSeconds(-meetingDurationInSeconds).ConvertGoogleDateTimeInString(cet);
                meetingEndDate = filteredLogs.Select(item => item.Date.ConvertGooogleMeetDataInDateTime()).Max().ConvertGoogleDateTimeInString(cet);
            }
           
            foreach (IGoogleMeetLogTO log in filteredLogs)
            {
                string partecipantIdentifier = getPartecipantLogIdentifier(log);
                IList<IGoogleMeetLogTO> partecipantsLogs = filteredLogs.Where(item => partecipantIdentifier == getPartecipantLogIdentifier(item)).ToList();

                foreach (IGoogleMeetLogTO partecipantLog in partecipantsLogs)
                {
                    if (isDateTimeToUpdate(partecipantLog.MeetingStartDate))
                        partecipantLog.MeetingStartDate = meetingStartDate;

                    if (isDateTimeToUpdate(partecipantLog.MeetingEndDate))
                        partecipantLog.MeetingEndDate = meetingEndDate;

                    if (isDateTimeToUpdate(partecipantLog.MeetingEnteringDate) && int.Parse(partecipantLog.Duration) > 0)
                        partecipantLog.MeetingEnteringDate = partecipantLog.Date.ConvertGooogleMeetDataInDateTime(out string cet2).AddSeconds(-int.Parse(partecipantLog.Duration)).ConvertGoogleDateTimeInString(cet2);

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
        IDictionary<string, IList<GoogleMeetLogModel>> MeetingLogsDictionary { get; } = null;

        #endregion
    }
}
