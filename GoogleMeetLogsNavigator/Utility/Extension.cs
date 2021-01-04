using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Exception;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using System;

namespace GoogleMeetLogsNavigator.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extension
    {
        #region string

        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleDate"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTime ConvertGooogleMeetDataInDateTime(this string googleDate, string language, out string timeZone)
        {
            if (string.IsNullOrEmpty(language) ||language == "it")
            {
                //16 dic 2020 21:57:35 CET
                string[] date = googleDate.Split(' ');
                string[] hours = date[3].Split(':');
                int monthInt = GetGoogleMonthIntITA(date[1]);
                timeZone = date[4];
                return new DateTime(int.Parse(date[2]), monthInt, int.Parse(date[0]), int.Parse(hours[0]), int.Parse(hours[1]), int.Parse(hours[2]));
            }
            if (language == "en")
            {
                //Nov 27 2020 8:04:52 PM GMT+1
                string[] date = googleDate.Split(' ');
                int monthInt = GetGoogleMonthIntEN(date[0]);
                string[] hours = date[3].Split(':');
                int hour = int.Parse(hours[0]);
                if (date[4] == "PM")
                {
                    hour += 12;
                }
                timeZone = date[5];
                return new DateTime(int.Parse(date[2]), monthInt, int.Parse(date[1]), hour, int.Parse(hours[1]), int.Parse(hours[2]));
            }

            throw new ArgumentException("La lingua in input non è supportata");
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleDate"></param>
        /// <returns></returns>
        public static DateTime ConvertGooogleMeetDataInDateTime(this string googleDate, string language)
        {
            return googleDate.ConvertGooogleMeetDataInDateTime(language, out string notUsed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetSafeString(this string instance)
        {
            return string.IsNullOrEmpty(instance) ? string.Empty : instance;
        }

        #endregion

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private static string GetGoogleMonthStringITA(int month)
        {
            switch (month)
            {
                case 1:
                    {
                        return "gen";
                    }
                case 2:
                    {
                        return "feb";
                    }
                case 3:
                    {
                        return "mar";
                    }
                case 4:
                    {
                        return "apr";
                    }
                case 5:
                    {
                        return "mag";
                    }
                case 6:
                    {
                        return "giu";
                    }
                case 7:
                    {
                        return "lug";
                    }
                case 8:
                    {
                        return "ago";
                    }
                case 9:
                    {
                        return "set";
                    }
                case 10:
                    {
                        return "ott";
                    }
                case 11:
                    {
                        return "nov";
                    }
                case 12:
                    {
                        return "dic";
                    }
                default:
                    throw new InvalidOperationException("Invalid input month");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private static int GetGoogleMonthIntITA(string month)
        {
            switch (month)
            {
                case "gen":
                    {
                        return 1;
                    }
                case "feb":
                    {
                        return 2;
                    }
                case "mar":
                    {
                        return 3;
                    }
                case "apr":
                    {
                        return 4;
                    }
                case "mag":
                    {
                        return 5;
                    }
                case "giu":
                    {
                        return 6;
                    }
                case "lug":
                    {
                        return 7;
                    }
                case "ago":
                    {
                        return 8;
                    }
                case "set":
                    {
                        return 9;
                    }
                case "ott":
                    {
                        return 10;
                    }
                case "nov":
                    {
                        return 11;
                    }
                case "dic":
                    {
                        return 12;
                    }
                default:
                    throw new InvalidOperationException("Invalid input month");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private static string GetGoogleMonthStringEN(int month)
        {
            switch (month)
            {
                case 1:
                    {
                        return "Gen";
                    }
                case 2:
                    {
                        return "Feb";
                    }
                case 3:
                    {
                        return "Mar";
                    }
                case 4:
                    {
                        return "Apr";
                    }
                case 5:
                    {
                        return "May";
                    }
                case 6:
                    {
                        return "Jun";
                    }
                case 7:
                    {
                        return "Jul";
                    }
                case 8:
                    {
                        return "Aug";
                    }
                case 9:
                    {
                        return "Sep";
                    }
                case 10:
                    {
                        return "Oct";
                    }
                case 11:
                    {
                        return "Nov";
                    }
                case 12:
                    {
                        return "Dec";
                    }
                default:
                    throw new InvalidOperationException("Invalid input month");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private static int GetGoogleMonthIntEN(string month)
        {
            switch (month)
            {
                case "Gen":
                    {
                        return 1;
                    }
                case "Feb":
                    {
                        return 2;
                    }
                case "Mar":
                    {
                        return 3;
                    }
                case "Apr":
                    {
                        return 4;
                    }
                case "May":
                    {
                        return 5;
                    }
                case "Jun":
                    {
                        return 6;
                    }
                case "Jul":
                    {
                        return 7;
                    }
                case "Aug":
                    {
                        return 8;
                    }
                case "Sep":
                    {
                        return 9;
                    }
                case "Oct":
                    {
                        return 10;
                    }
                case "Nov":
                    {
                        return 11;
                    }
                case "Dec":
                    {
                        return 12;
                    }
                default:
                    throw new InvalidOperationException("Invalid input month");
            }
        }

        #endregion

        #region GoogleMeetLogTO

        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleMeetLogTO"></param>
        /// <param name="dataToUpdate"></param>
        /// <returns></returns>
        public static GoogleMeetLogModel MapTransferObjectInModel(this IGoogleMeetLogTO googleMeetLogTO, string languageForDateString)
        {
            try 
            { 
                return new GoogleMeetLogModel
                {
                    Date = googleMeetLogTO.Date.ConvertGooogleMeetDataInDateTime(languageForDateString),
                    EventName = googleMeetLogTO.EventName,
                    EventDescription = googleMeetLogTO.EventDescription,
                    MeetingCode = googleMeetLogTO.MeetingCode,
                    PartecipantIdentifier = googleMeetLogTO.PartecipantIdentifier,
                    ExternalPartecipantIdentifier = googleMeetLogTO.ExternalPartecipantIdentifier.ToUpper() == "YES",
                    ClientType = googleMeetLogTO.ClientType,
                    MeetingOwnerEmail = googleMeetLogTO.MeetingOwnerEmail,
                    ProductType = googleMeetLogTO.ProductType,
                    Duration = string.IsNullOrEmpty(googleMeetLogTO.Duration) ? 0 : int.Parse(googleMeetLogTO.Duration),
                    EffectiveMeetingDurationInHours = string.IsNullOrEmpty(googleMeetLogTO.EffectiveMeetingDurationInHours) ? 0 : double.Parse(googleMeetLogTO.EffectiveMeetingDurationInHours),
                    EffectiveMeetingDurationInSeconds = string.IsNullOrEmpty(googleMeetLogTO.EffectiveMeetingDurationInSeconds) ? 0 : double.Parse(googleMeetLogTO.EffectiveMeetingDurationInSeconds),
                    EffectiveMeetingDurationInMinutes = string.IsNullOrEmpty(googleMeetLogTO.EffectiveMeetingDurationInMinutes) ? 0 : double.Parse(googleMeetLogTO.EffectiveMeetingDurationInMinutes),
                    CallEvaluationOn5 = googleMeetLogTO.CallEvaluationOn5,
                    PartecipantName = googleMeetLogTO.PartecipantName,
                    IPAddress = googleMeetLogTO.IPAddress,
                    City = googleMeetLogTO.City,
                    Country = googleMeetLogTO.Country,
                    ActionReason = googleMeetLogTO.ActionReason,
                    ActionDescription = googleMeetLogTO.ActionDescription,
                    TargetDisplayNames = googleMeetLogTO.TargetDisplayNames,
                    TargetEmail = googleMeetLogTO.TargetEmail,
                    TargetPhoneNumber = googleMeetLogTO.TargetPhoneNumber,
                    CalendarEventIdentifier = googleMeetLogTO.CalendarEventIdentifier,
                    ConferenceID = googleMeetLogTO.ConferenceID,
                    NETRoundTrip = googleMeetLogTO.NETRoundTrip,
                    TransportProtocol = googleMeetLogTO.TransportProtocol,
                    EstimatedUploadBandwidthInkbps = string.IsNullOrEmpty(googleMeetLogTO.EstimatedUploadBandwidthInkbps) ? 0 : int.Parse(googleMeetLogTO.EstimatedUploadBandwidthInkbps),
                    EstimatedDownloadBandwidthInkbps = string.IsNullOrEmpty(googleMeetLogTO.EstimatedDownloadBandwidthInkbps) ? 0 : int.Parse(googleMeetLogTO.EstimatedDownloadBandwidthInkbps),
                    AudioReceivePacketLossMax = string.IsNullOrEmpty(googleMeetLogTO.AudioReceivePacketLossMax) ? 0 : int.Parse(googleMeetLogTO.AudioReceivePacketLossMax),
                    AudioReceivePacketLossMean = string.IsNullOrEmpty(googleMeetLogTO.AudioReceivePacketLossMean) ? 0 : double.Parse(googleMeetLogTO.AudioReceivePacketLossMean),
                    AudioReceiveDuration = string.IsNullOrEmpty(googleMeetLogTO.AudioReceiveDuration) ? 0 : int.Parse(googleMeetLogTO.AudioReceiveDuration),
                    BitRatioAudioSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioAudioSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioAudioSending),
                    AudioSendPacketLossMax = string.IsNullOrEmpty(googleMeetLogTO.AudioSendPacketLossMax) ? 0 : int.Parse(googleMeetLogTO.AudioSendPacketLossMax),
                    AudioSendPacketLossMean = string.IsNullOrEmpty(googleMeetLogTO.AudioSendPacketLossMean) ? 0 : double.Parse(googleMeetLogTO.AudioSendPacketLossMean),
                    AudioSendDuration = string.IsNullOrEmpty(googleMeetLogTO.AudioSendDuration) ? 0 : int.Parse(googleMeetLogTO.AudioSendDuration),
                    NetworkRecvJitterMeaninms = string.IsNullOrEmpty(googleMeetLogTO.NetworkRecvJitterMeaninms) ? 0 : double.Parse(googleMeetLogTO.NetworkRecvJitterMeaninms),
                    NetworkRecvJitterMaxinms = string.IsNullOrEmpty(googleMeetLogTO.NetworkRecvJitterMaxinms) ? 0 : int.Parse(googleMeetLogTO.NetworkRecvJitterMaxinms),
                    NetworkSendJitterMeaninms = string.IsNullOrEmpty(googleMeetLogTO.NetworkSendJitterMeaninms) ? 0 : double.Parse(googleMeetLogTO.NetworkSendJitterMeaninms),
                    BitRatioScreencastReception = string.IsNullOrEmpty(googleMeetLogTO.BitRatioScreencastReception) ? 0 : double.Parse(googleMeetLogTO.BitRatioScreencastReception),
                    ScreencastReceiveFPSMean = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveFPSMean) ? 0 : double.Parse(googleMeetLogTO.ScreencastReceiveFPSMean),
                    ScreencastReceiveLongSideMedian = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceiveLongSideMedian),
                    ScreencastReceivePacketLossMax = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceivePacketLossMax) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceivePacketLossMax),
                    ScreencastReceivePacketLossMean = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceivePacketLossMean) ? 0 : double.Parse(googleMeetLogTO.ScreencastReceivePacketLossMean),
                    ScreencastReceiveDuration = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveDuration) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceiveDuration),
                    ScreencastReceiveShortSideMedian = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceiveShortSideMedian),
                    BitRatioScreencastSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioScreencastSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioScreencastSending),
                    ScreencastSendFPSMean = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendFPSMean) ? 0 : double.Parse(googleMeetLogTO.ScreencastSendFPSMean),
                    ScreencastSendLongSideMedian = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendLongSideMedian),
                    ScreencastSendPacketLossMax = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendPacketLossMax) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendPacketLossMax),
                    ScreencastSendPacketLossMean = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendPacketLossMean) ? 0 : double.Parse(googleMeetLogTO.ScreencastSendPacketLossMean),
                    ScreencastSendDuration = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendDuration) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendDuration),
                    ScreencastSendShortSideMedian = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendShortSideMedian),
                    VideoReceiveFPSMean = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveFPSMean) ? 0 : double.Parse(googleMeetLogTO.VideoReceiveFPSMean),
                    VideoReceiveLongSideMedian = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoReceiveLongSideMedian),
                    MaxVideoReceptionPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.VideoReceivePacketLossMax) ? 0 : int.Parse(googleMeetLogTO.VideoReceivePacketLossMax),
                    VideoReceivePacketLossMax = string.IsNullOrEmpty(googleMeetLogTO.VideoReceivePacketLossMean) ? 0 : double.Parse(googleMeetLogTO.VideoReceivePacketLossMean),
                    VideoReceiveDuration = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveDuration) ? 0 : int.Parse(googleMeetLogTO.VideoReceiveDuration),
                    VideoReceiveShortSideMedian = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoReceiveShortSideMedian),
                    BitRatioVideoSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioVideoSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioVideoSending),
                    VideoSendFPSMean = string.IsNullOrEmpty(googleMeetLogTO.VideoSendFPSMean) ? 0 : double.Parse(googleMeetLogTO.VideoSendFPSMean),
                    VideoSendLongSideMedian = string.IsNullOrEmpty(googleMeetLogTO.VideoSendLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoSendLongSideMedian),
                    VideoSendPacketLossMax = string.IsNullOrEmpty(googleMeetLogTO.VideoSendPacketLossMax) ? 0 : int.Parse(googleMeetLogTO.VideoSendPacketLossMax),
                    VideoSendDuration = string.IsNullOrEmpty(googleMeetLogTO.VideoSendDuration) ? 0 : int.Parse(googleMeetLogTO.VideoSendDuration),
                    VideoSendShortSideMedian = string.IsNullOrEmpty(googleMeetLogTO.VideoSendShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoSendShortSideMedian),
                    NetworkCongestionRatio = googleMeetLogTO.NetworkCongestionRatio,
                    MeetingStartDate = googleMeetLogTO.MeetingStartDate.GetSafeString().ConvertGooogleMeetDataInDateTime(languageForDateString),
                    EffectiveMeetingStartDate = string.IsNullOrEmpty(googleMeetLogTO.EffectiveMeetingStartDate) 
                                ? googleMeetLogTO.MeetingStartDate.GetSafeString().ConvertGooogleMeetDataInDateTime(languageForDateString) 
                                : googleMeetLogTO.EffectiveMeetingStartDate.ConvertGooogleMeetDataInDateTime(languageForDateString),
                    MeetingEndDate = googleMeetLogTO.MeetingEndDate.GetSafeString().ConvertGooogleMeetDataInDateTime(languageForDateString),
                    EffectiveMeetingEndDate = string.IsNullOrEmpty(googleMeetLogTO.EffectiveMeetingEndDate) 
                                ? googleMeetLogTO.MeetingEndDate.GetSafeString().ConvertGooogleMeetDataInDateTime(languageForDateString)
                                : googleMeetLogTO.EffectiveMeetingEndDate.ConvertGooogleMeetDataInDateTime(languageForDateString),
                    MeetingEnteringDate = googleMeetLogTO.MeetingEnteringDate.GetSafeString().ConvertGooogleMeetDataInDateTime(languageForDateString),
                    TotalMeetingUserPartecipationInDecimal = string.IsNullOrEmpty(googleMeetLogTO.TotalMeetingUserPartecipationInDecimal) ? 0.0 : double.Parse(googleMeetLogTO.TotalMeetingUserPartecipationInDecimal),
                    TotalMeetingUserPartecipationInSeconds = string.IsNullOrEmpty(googleMeetLogTO.TotalMeetingUserPartecipationInSeconds) ? 0 : int.Parse(googleMeetLogTO.TotalMeetingUserPartecipationInSeconds),
                    TotalMeetingUserPartecipationInMinutes = string.IsNullOrEmpty(googleMeetLogTO.TotalMeetingUserPartecipationInMinutes) ? 0 : int.Parse(googleMeetLogTO.TotalMeetingUserPartecipationInMinutes),
                    TotalMeetingUserPartecipationInHours = string.IsNullOrEmpty(googleMeetLogTO.TotalMeetingUserPartecipationInHours) ? 0 : int.Parse(googleMeetLogTO.TotalMeetingUserPartecipationInHours),
                    TimeZone = googleMeetLogTO.TimeZone
                };
            }
            catch (Exception ex)
            {
                throw new TransferException("Errore durante la conversione", ex);
            }
        }

        #endregion

        #region GoogleMeetLogModel

        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleMeetLogModel"></param>
        /// <returns></returns>
        public static IGoogleMeetLogTO MapObjectModelInTransferObject(this GoogleMeetLogModel googleMeetLogModel, string language)
        {
            try
            {
                return new GoogleMeetLogTO
                {
                    Date = googleMeetLogModel.Date.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone, language),
                    EventName = googleMeetLogModel.EventName,
                    EventDescription = googleMeetLogModel.EventDescription,
                    MeetingCode = googleMeetLogModel.MeetingCode,
                    PartecipantIdentifier = googleMeetLogModel.PartecipantIdentifier,
                    ExternalPartecipantIdentifier = googleMeetLogModel.ExternalPartecipantIdentifier ? "Yes" : "No",
                    ClientType = googleMeetLogModel.ClientType,
                    MeetingOwnerEmail = googleMeetLogModel.MeetingOwnerEmail,
                    ProductType = googleMeetLogModel.ProductType,
                    Duration = googleMeetLogModel.Duration.ToString(),
                    EffectiveMeetingDurationInHours = googleMeetLogModel.EffectiveMeetingDurationInHours.ToString(),
                    EffectiveMeetingDurationInSeconds = googleMeetLogModel.EffectiveMeetingDurationInSeconds.ToString(),
                    EffectiveMeetingDurationInMinutes = googleMeetLogModel.EffectiveMeetingDurationInMinutes.ToString(),
                    CallEvaluationOn5 = googleMeetLogModel.CallEvaluationOn5,
                    PartecipantName = googleMeetLogModel.PartecipantName,
                    IPAddress = googleMeetLogModel.IPAddress,
                    City = googleMeetLogModel.City,
                    Country = googleMeetLogModel.Country,
                    ActionReason = googleMeetLogModel.ActionReason,
                    ActionDescription = googleMeetLogModel.ActionDescription,
                    TargetDisplayNames = googleMeetLogModel.TargetDisplayNames,
                    TargetEmail = googleMeetLogModel.TargetEmail,
                    TargetPhoneNumber = googleMeetLogModel.TargetPhoneNumber,
                    CalendarEventIdentifier = string.IsNullOrEmpty(googleMeetLogModel.CalendarEventIdentifier) ? string.Empty : googleMeetLogModel.CalendarEventIdentifier,
                    ConferenceID = googleMeetLogModel.ConferenceID,
                    NETRoundTrip = googleMeetLogModel.NETRoundTrip,
                    TransportProtocol = googleMeetLogModel.TransportProtocol,
                    EstimatedUploadBandwidthInkbps = googleMeetLogModel.EstimatedUploadBandwidthInkbps.ToString(),
                    EstimatedDownloadBandwidthInkbps = googleMeetLogModel.EstimatedDownloadBandwidthInkbps.ToString(),
                    AudioReceivePacketLossMax = googleMeetLogModel.AudioReceivePacketLossMax.ToString(),
                    AudioReceivePacketLossMean = googleMeetLogModel.AudioReceivePacketLossMean.ToString(),
                    AudioReceiveDuration = googleMeetLogModel.AudioReceiveDuration.ToString(),
                    BitRatioAudioSending = googleMeetLogModel.BitRatioAudioSending.ToString(),
                    AudioSendPacketLossMax = googleMeetLogModel.AudioSendPacketLossMax.ToString(),
                    AudioSendPacketLossMean = googleMeetLogModel.AudioSendPacketLossMean.ToString(),
                    AudioSendDuration = googleMeetLogModel.AudioSendDuration.ToString(),
                    NetworkRecvJitterMeaninms = googleMeetLogModel.NetworkRecvJitterMeaninms.ToString(),
                    NetworkRecvJitterMaxinms = googleMeetLogModel.NetworkRecvJitterMaxinms.ToString(),
                    NetworkSendJitterMeaninms = googleMeetLogModel.NetworkSendJitterMeaninms.ToString(),
                    BitRatioScreencastReception = googleMeetLogModel.BitRatioScreencastReception.ToString(),
                    ScreencastReceiveFPSMean = googleMeetLogModel.ScreencastReceiveFPSMean.ToString(),
                    ScreencastReceiveLongSideMedian = googleMeetLogModel.ScreencastReceiveLongSideMedian.ToString(),
                    ScreencastReceivePacketLossMax = googleMeetLogModel.ScreencastReceivePacketLossMax.ToString(),
                    ScreencastReceivePacketLossMean = googleMeetLogModel.ScreencastReceivePacketLossMean.ToString(),
                    ScreencastReceiveDuration = googleMeetLogModel.ScreencastReceiveDuration.ToString(),
                    ScreencastReceiveShortSideMedian = googleMeetLogModel.ScreencastReceiveShortSideMedian.ToString(),
                    BitRatioScreencastSending = googleMeetLogModel.BitRatioScreencastSending.ToString(),
                    ScreencastSendFPSMean = googleMeetLogModel.ScreencastSendFPSMean.ToString(),
                    ScreencastSendLongSideMedian = googleMeetLogModel.ScreencastSendLongSideMedian.ToString(),
                    ScreencastSendPacketLossMax = googleMeetLogModel.ScreencastSendPacketLossMax.ToString(),
                    ScreencastSendPacketLossMean = googleMeetLogModel.ScreencastSendPacketLossMean.ToString(),
                    ScreencastSendDuration = googleMeetLogModel.ScreencastSendDuration.ToString(),
                    ScreencastSendShortSideMedian = googleMeetLogModel.ScreencastSendShortSideMedian.ToString(),
                    VideoReceiveFPSMean = googleMeetLogModel.VideoReceiveFPSMean.ToString(),
                    VideoReceiveLongSideMedian = googleMeetLogModel.VideoReceiveLongSideMedian.ToString(),
                    VideoReceivePacketLossMax = googleMeetLogModel.MaxVideoReceptionPacketsLost.ToString(),
                    VideoReceivePacketLossMean = googleMeetLogModel.VideoReceivePacketLossMax.ToString(),
                    VideoReceiveDuration = googleMeetLogModel.VideoReceiveDuration.ToString(),
                    VideoReceiveShortSideMedian = googleMeetLogModel.VideoReceiveShortSideMedian.ToString(),
                    BitRatioVideoSending = googleMeetLogModel.BitRatioVideoSending.ToString(),
                    VideoSendFPSMean = googleMeetLogModel.VideoSendFPSMean.ToString(),
                    VideoSendLongSideMedian = googleMeetLogModel.VideoSendLongSideMedian.ToString(),
                    VideoSendPacketLossMax = googleMeetLogModel.VideoSendPacketLossMax.ToString(),
                    VideoSendDuration = googleMeetLogModel.VideoSendDuration.ToString(),
                    VideoSendShortSideMedian = googleMeetLogModel.VideoSendShortSideMedian.ToString(),
                    NetworkCongestionRatio = googleMeetLogModel.NetworkCongestionRatio,
                    MeetingStartDate = googleMeetLogModel.MeetingStartDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone, language),
                    EffectiveMeetingStartDate = googleMeetLogModel.EffectiveMeetingStartDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone, language),
                    MeetingEndDate = googleMeetLogModel.MeetingEndDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone, language),
                    EffectiveMeetingEndDate = googleMeetLogModel.EffectiveMeetingEndDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone, language),
                    MeetingEnteringDate = googleMeetLogModel.MeetingEnteringDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone, language),
                    TotalMeetingUserPartecipationInDecimal = googleMeetLogModel.TotalMeetingUserPartecipationInDecimal.ToString(),
                    TotalMeetingUserPartecipationInSeconds = googleMeetLogModel.TotalMeetingUserPartecipationInSeconds.ToString(),
                    TotalMeetingUserPartecipationInMinutes =googleMeetLogModel.TotalMeetingUserPartecipationInMinutes.ToString(),
                    TotalMeetingUserPartecipationInHours = googleMeetLogModel.TotalMeetingUserPartecipationInHours.ToString(),
                    TimeZone = googleMeetLogModel.TimeZone
                };
            }
            catch (Exception ex)
            {
                throw new TransferException("Errore durante la conversione", ex);
            }
           
        }

        #endregion

        #region DateTime

        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleDateTime"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static string ConvertGoogleDateTimeInString(this DateTime googleDateTime, string timeZone, string language)
        {
            if (googleDateTime == null)
            {
                googleDateTime = DateTime.MinValue;
            }

            if (string.IsNullOrEmpty(language) || language == "it")
            {
               
                return string.Format("{0} {1} {2} {3}:{4}:{5} {6}",
                    googleDateTime.Day,
                    GetGoogleMonthStringITA(googleDateTime.Month),
                    googleDateTime.Year,
                    (googleDateTime.Hour > 9 ? googleDateTime.Hour.ToString() : ("0" + googleDateTime.Hour.ToString())),
                    (googleDateTime.Minute > 9 ? googleDateTime.Minute.ToString() : ("0" + googleDateTime.Minute.ToString())),
                    (googleDateTime.Second > 9 ? googleDateTime.Second.ToString() : ("0" + googleDateTime.Second.ToString())),
                    string.IsNullOrEmpty(timeZone) ? string.Empty : timeZone);
            }
            if (language == "en")
            {
                //Nov 27 2020 8:04:52 PM GMT+1
                string antePostMeridian = googleDateTime.Hour > 12 ? "PM" : "AM";
                int hour = googleDateTime.Hour > 12 ? googleDateTime.Hour - 12 : googleDateTime.Hour;
                
                return string.Format("{0} {1} {2} {3}:{4}:{5} {6} {7}",
                    GetGoogleMonthStringEN(googleDateTime.Month),
                    googleDateTime.Day,
                    googleDateTime.Year,
                   hour,
                   (googleDateTime.Minute > 9 ? googleDateTime.Minute.ToString() : ("0" + googleDateTime.Minute.ToString())),
                   (googleDateTime.Second > 9 ? googleDateTime.Second.ToString() : ("0" + googleDateTime.Second.ToString())),
                   antePostMeridian,
                   string.IsNullOrEmpty(timeZone) ? string.Empty : timeZone);
            }

            throw new ArgumentException("Input language is not valid");

        }
        #endregion
    }
}
