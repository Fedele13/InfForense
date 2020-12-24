using GoogleMeetLogsNavigator.Interface;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TO;
using GoogleMeetLogsNavigator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.BO
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleMeetingDataCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        public class DataResult
        {
            public string PartecipantIdentifier { get; set; }
            public DateTime MeetingStartDate { get; set; }
            public DateTime MeetingEndDate { get; set; }
            public DateTime MeetingEnteringDate { get; set; }
            public int TotalMeetingUserPartecipation { get; set; }
        }

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
                //Date -> to add data
                IDictionary<string, DataResult> resultAdditionalDataDictionary = calculateAdditionalData(reader.MeetingDictionary[meetingKey]);

                foreach (string dateLog in resultAdditionalDataDictionary.Keys)
                {
                    GoogleMeetLogTO log = reader.MeetingDictionary[meetingKey].
                        Where(item => item.Date == dateLog && getLogIdentifier(item) == resultAdditionalDataDictionary[dateLog].PartecipantIdentifier).FirstOrDefault();

                    logsModel.Add(log.MapTransferObjectInModel(resultAdditionalDataDictionary[dateLog]));
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
        private IDictionary<string, DataResult> calculateAdditionalData(IList<GoogleMeetLogTO> logs)
        {
            if (logs.Select(item => item.MeetingCode).Distinct().Count() > 1) throw new InvalidOperationException("The input logs are not of the same google meeting");

            IDictionary<string, DataResult> resultDictionary = new Dictionary<string, DataResult>();

            IList<GoogleMeetLogTO> filteredLogs = logs.Where(item => item.EventName == Constants.EventsToConsider.CallExit).ToList();

            DateTime minDateTime = filteredLogs.Select(item => DateTime.Parse(item.Date)).Min();
            int meetingDurationInSeconds = filteredLogs.Where(item => DateTime.Parse(item.Date) == minDateTime).Select(item => int.Parse(item.Duration)).FirstOrDefault();
            DateTime meetingStartData = minDateTime.Subtract(new TimeSpan(0, 0, meetingDurationInSeconds));
            DateTime meetingEndData = filteredLogs.Select(item => DateTime.Parse(item.Date)).Max(); ;  //select max data to calculate meeting end data

            foreach (GoogleMeetLogTO log in filteredLogs)
            {
                string identifier = getLogIdentifier(log);
                IList<GoogleMeetLogTO> partecipantsLogs = filteredLogs.Where(item => identifier == getLogIdentifier(item)).ToList();

                DateTime meetingEnteringData = DateTime.MinValue;

                foreach (GoogleMeetLogTO partecipantLog in partecipantsLogs)
                {
                    meetingEnteringData = DateTime.Parse(partecipantLog.Date).Subtract(new TimeSpan(0, 0, int.Parse(partecipantLog.Duration)));

                    resultDictionary.Add(partecipantLog.Date,
                    new DataResult
                    {
                        PartecipantIdentifier = identifier,
                        MeetingStartDate = meetingStartData,
                        MeetingEndDate = meetingEndData,
                        MeetingEnteringDate = meetingEnteringData,
                        TotalMeetingUserPartecipation = partecipantsLogs.Sum(item => int.Parse(item.Duration))
                    });
                }
            }

            return resultDictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private string getLogIdentifier(GoogleMeetLogTO log)
        {
            return log.PartecipantIdentifier == string.Empty ? log.PartecipantName : log.PartecipantIdentifier;
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
