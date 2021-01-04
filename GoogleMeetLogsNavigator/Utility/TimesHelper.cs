using GoogleMeetLogsNavigator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.Utility
{
    public class TimesHelper
    {
        public static GoogleMeetLogModel changeEffectiveDateTime(GoogleMeetLogModel partecipantLog)
        {
            
            if (partecipantLog.Date > partecipantLog.EffectiveMeetingStartDate)
            {
                TimeSpan result = partecipantLog.Date.Subtract(partecipantLog.EffectiveMeetingStartDate);
                partecipantLog.TotalMeetingUserPartecipationInSeconds = result.TotalSeconds;
            }
            else
            {
                partecipantLog.TotalMeetingUserPartecipationInSeconds = 0;
            }
            TimeSpan meetingTime = partecipantLog.EffectiveMeetingEndDate.Subtract(partecipantLog.EffectiveMeetingStartDate);
            partecipantLog.EffectiveMeetingDurationInSeconds = meetingTime.TotalSeconds;
            partecipantLog.EffectiveMeetingDurationInMinutes = meetingTime.TotalMinutes;
            partecipantLog.EffectiveMeetingDurationInHours = meetingTime;

            if (meetingTime.TotalSeconds < partecipantLog.TotalMeetingUserPartecipationInSeconds)
            {
                partecipantLog.TotalMeetingUserPartecipationInSeconds = meetingTime.TotalSeconds;
            }

            TimeSpan partecipantTime = TimeSpan.FromSeconds(partecipantLog.TotalMeetingUserPartecipationInSeconds);
            partecipantLog.TotalMeetingUserPartecipationInMinutes = partecipantTime.TotalMinutes;
            partecipantLog.TotalMeetingUserPartecipationInHours = partecipantTime;
            partecipantLog.TotalMeetingUserPartecipationInDecimal = partecipantTime.TotalHours;

            return partecipantLog;
        }

    }
}
