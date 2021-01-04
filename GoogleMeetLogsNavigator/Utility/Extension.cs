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
        /// <param name="commonEuropeanTime"></param>
        /// <returns></returns>
        public static DateTime ConvertGooogleMeetDataInDateTime(this string googleDate, out string commonEuropeanTime)
        {
            string[] date = googleDate.Split(' ');
            string[] hours = date[3].Split(':');
            int monthInt = GetGoogleMonthIntITA(date[1]);
            commonEuropeanTime = date[4];
            return new DateTime(int.Parse(date[2]), monthInt, int.Parse(date[0]), int.Parse(hours[0]), int.Parse(hours[1]), int.Parse(hours[2]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleDate"></param>
        /// <returns></returns>
        public static DateTime ConvertGooogleMeetDataInDateTime(this string googleDate)
        {
            return googleDate.ConvertGooogleMeetDataInDateTime(out string notUsed);
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
                        return "may";
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
                        return "aug";
                    }
                case 9:
                    {
                        return "sep";
                    }
                case 10:
                    {
                        return "oct";
                    }
                case 11:
                    {
                        return "nov";
                    }
                case 12:
                    {
                        return "dec";
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
                case "may":
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
                case "aug":
                    {
                        return 8;
                    }
                case "sep":
                    {
                        return 9;
                    }
                case "oct":
                    {
                        return 10;
                    }
                case "nov":
                    {
                        return 11;
                    }
                case "dec":
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
        public static GoogleMeetLogModel MapTransferObjectITAInModel(this IGoogleMeetLogTO googleMeetLogTO)
        {
            try 
            { 
                return new GoogleMeetLogModel
                {
                    Date = googleMeetLogTO.Date.ConvertGooogleMeetDataInDateTime(),
                    EventName = googleMeetLogTO.EventName,
                    EventDescription = googleMeetLogTO.EventDescription,
                    MeetingCode = googleMeetLogTO.MeetingCode,
                    PartecipantIdentifier = googleMeetLogTO.PartecipantIdentifier,
                    ExternalPartecipantIdentifier = googleMeetLogTO.ExternalPartecipantIdentifier.ToUpper() == "YES",
                    ClientType = googleMeetLogTO.ClientType,
                    MeetingOwnerEmail = googleMeetLogTO.MeetingOwnerEmail,
                    ProductType = googleMeetLogTO.ProductType,
                    Duration = string.IsNullOrEmpty(googleMeetLogTO.Duration) ? 0 : int.Parse(googleMeetLogTO.Duration),
                    CallEvaluationOn5 = googleMeetLogTO.CallEvaluationOn5,
                    PartecipantName = googleMeetLogTO.PartecipantName,
                    IPAddress = googleMeetLogTO.IPAddress,
                    City = googleMeetLogTO.City,
                    Nation = googleMeetLogTO.Nation,
                    ActionCause = googleMeetLogTO.ActionReason,
                    ActionDescription = googleMeetLogTO.ActionDescription,
                    VisualizedDestinationName = googleMeetLogTO.TargetDisplayNames,
                    DestinationEmailsAddresses = googleMeetLogTO.TargetEmail,
                    DestinationPhoneNumber = googleMeetLogTO.TargetPhoneNumber,
                    CalendarEventIdentifier = googleMeetLogTO.CalendarEventIdentifier,
                    ConferenceID = googleMeetLogTO.ConferenceID,
                    NETRoundTrip = googleMeetLogTO.NETRoundTrip,
                    TransportProtocol = googleMeetLogTO.TransportProtocol,
                    PredictedBandWidthLoading = string.IsNullOrEmpty(googleMeetLogTO.EstimatedUploadBandwidthInkbps) ? 0 : int.Parse(googleMeetLogTO.EstimatedUploadBandwidthInkbps),
                    PredictedBandWidthUploading = string.IsNullOrEmpty(googleMeetLogTO.EstimatedDownloadBandwidthInkbps) ? 0 : int.Parse(googleMeetLogTO.EstimatedDownloadBandwidthInkbps),
                    MaxReceptionAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AudioReceivePacketLossMax) ? 0 : int.Parse(googleMeetLogTO.AudioReceivePacketLossMax),
                    AverageReceptionAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AudioReceivePacketLossMean) ? 0 : double.Parse(googleMeetLogTO.AudioReceivePacketLossMean),
                    AudioReceptionDuration = string.IsNullOrEmpty(googleMeetLogTO.AudioReceiveDuration) ? 0 : int.Parse(googleMeetLogTO.AudioReceiveDuration),
                    BitRatioAudioSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioAudioSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioAudioSending),
                    MaxSendingAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AudioSendPacketLossMax) ? 0 : int.Parse(googleMeetLogTO.AudioSendPacketLossMax),
                    AverageSendingAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AudioSendPacketLossMean) ? 0 : double.Parse(googleMeetLogTO.AudioSendPacketLossMean),
                    AudioSendingDuration = string.IsNullOrEmpty(googleMeetLogTO.AudioSendDuration) ? 0 : int.Parse(googleMeetLogTO.AudioSendDuration),
                    AverageReceptionFlickering = string.IsNullOrEmpty(googleMeetLogTO.NetworkRecvJitterMeaninms) ? 0 : double.Parse(googleMeetLogTO.NetworkRecvJitterMeaninms),
                    MaxReceptionFilckering = string.IsNullOrEmpty(googleMeetLogTO.NetworkRecvJitterMaxinms) ? 0 : int.Parse(googleMeetLogTO.NetworkRecvJitterMaxinms),
                    AverageSendingFlickering = string.IsNullOrEmpty(googleMeetLogTO.NetworkSendJitterMeaninms) ? 0 : double.Parse(googleMeetLogTO.NetworkSendJitterMeaninms),
                    BitRatioScreencastReception = string.IsNullOrEmpty(googleMeetLogTO.BitRatioScreencastReception) ? 0 : double.Parse(googleMeetLogTO.BitRatioScreencastReception),
                    AverageScreecastReception = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveFPSMean) ? 0 : double.Parse(googleMeetLogTO.ScreencastReceiveFPSMean),
                    LongSideMedianScreencastReception = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceiveLongSideMedian),
                    MaxReceptionScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceivePacketLossMax) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceivePacketLossMax),
                    AverageReceptionScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceivePacketLossMean) ? 0 : double.Parse(googleMeetLogTO.ScreencastReceivePacketLossMean),
                    ScreencastReceptionDuration = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveDuration) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceiveDuration),
                    ShortSideMedianScreencastReception = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceiveShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceiveShortSideMedian),
                    BitRatioScreencastSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioScreencastSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioScreencastSending),
                    AverageScreecastSending = string.IsNullOrEmpty(googleMeetLogTO.AverageScreecastSending) ? 0 : double.Parse(googleMeetLogTO.AverageScreecastSending),
                    LongSideMedianScreencastSending = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendLongSideMedian),
                    MaxSendingScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendPacketLossMax) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendPacketLossMax),
                    AverageSendingScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendPacketLossMean) ? 0 : double.Parse(googleMeetLogTO.ScreencastSendPacketLossMean),
                    ScreencastSendingDuration = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendDuration) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendDuration),
                    ShortSideMedianScreencastSending = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendShortSideMedian),
                    AverageVideoReception = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveFPSMean) ? 0 : double.Parse(googleMeetLogTO.VideoReceiveFPSMean),
                    LongSideMedianVideoReception = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoReceiveLongSideMedian),
                    MaxVideoReceptionPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.VideoReceivePacketLossMax) ? 0 : int.Parse(googleMeetLogTO.VideoReceivePacketLossMax),
                    AverageVideoReceptionPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.VideoReceivePacketLossMean) ? 0 : double.Parse(googleMeetLogTO.VideoReceivePacketLossMean),
                    ReceptionVideoDuration = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveDuration) ? 0 : int.Parse(googleMeetLogTO.VideoReceiveDuration),
                    ShortSideMedianVideoReception = string.IsNullOrEmpty(googleMeetLogTO.VideoReceiveShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoReceiveShortSideMedian),
                    BitRatioVideoSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioVideoSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioVideoSending),
                    AverageVideoSending = string.IsNullOrEmpty(googleMeetLogTO.VideoSendFPSMean) ? 0 : double.Parse(googleMeetLogTO.VideoSendFPSMean),
                    LongSideMedianVideoSending = string.IsNullOrEmpty(googleMeetLogTO.VideoSendLongSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoSendLongSideMedian),
                    MaxSendingVideoPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.VideoSendPacketLossMax) ? 0 : int.Parse(googleMeetLogTO.VideoSendPacketLossMax),
                    VideoSendingDuration = string.IsNullOrEmpty(googleMeetLogTO.VideoSendDuration) ? 0 : int.Parse(googleMeetLogTO.VideoSendDuration),
                    ShortSideMedianVideoSending = string.IsNullOrEmpty(googleMeetLogTO.VideoSendShortSideMedian) ? 0 : int.Parse(googleMeetLogTO.VideoSendShortSideMedian),
                    NetworkCongestion = googleMeetLogTO.NetworkCongestionRatio,
                    MeetingStartDate = googleMeetLogTO.MeetingStartDate.GetSafeString().ConvertGooogleMeetDataInDateTime(),
                    MeetingEndDate = googleMeetLogTO.MeetingEndDate.GetSafeString().ConvertGooogleMeetDataInDateTime(),
                    MeetingEnteringDate = googleMeetLogTO.MeetingEnteringDate.GetSafeString().ConvertGooogleMeetDataInDateTime(),
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
        public static IGoogleMeetLogTO MapObjectModelInTransferObjectITA(this GoogleMeetLogModel googleMeetLogModel)
        {
            try
            {
                return new GoogleMeetLogTO
                {
                    Date = googleMeetLogModel.Date.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone),
                    EventName = googleMeetLogModel.EventName,
                    EventDescription = googleMeetLogModel.EventDescription,
                    MeetingCode = googleMeetLogModel.MeetingCode,
                    PartecipantIdentifier = googleMeetLogModel.PartecipantIdentifier,
                    ExternalPartecipantIdentifier = googleMeetLogModel.ExternalPartecipantIdentifier ? "Yes" : "No",
                    ClientType = googleMeetLogModel.ClientType,
                    MeetingOwnerEmail = googleMeetLogModel.MeetingOwnerEmail,
                    ProductType = googleMeetLogModel.ProductType,
                    Duration = googleMeetLogModel.Duration.ToString(),
                    CallEvaluationOn5 = googleMeetLogModel.CallEvaluationOn5,
                    PartecipantName = googleMeetLogModel.PartecipantName,
                    IPAddress = googleMeetLogModel.IPAddress,
                    City = googleMeetLogModel.City,
                    Nation = googleMeetLogModel.Nation,
                    ActionReason = googleMeetLogModel.ActionCause,
                    ActionDescription = googleMeetLogModel.ActionDescription,
                    TargetDisplayNames = googleMeetLogModel.VisualizedDestinationName,
                    TargetEmail = googleMeetLogModel.DestinationEmailsAddresses,
                    TargetPhoneNumber = googleMeetLogModel.DestinationPhoneNumber,
                    CalendarEventIdentifier = string.IsNullOrEmpty(googleMeetLogModel.CalendarEventIdentifier) ? string.Empty : googleMeetLogModel.CalendarEventIdentifier,
                    ConferenceID = googleMeetLogModel.ConferenceID,
                    NETRoundTrip = googleMeetLogModel.NETRoundTrip,
                    TransportProtocol = googleMeetLogModel.TransportProtocol,
                    EstimatedUploadBandwidthInkbps = googleMeetLogModel.PredictedBandWidthLoading.ToString(),
                    EstimatedDownloadBandwidthInkbps = googleMeetLogModel.PredictedBandWidthUploading.ToString(),
                    AudioReceivePacketLossMax = googleMeetLogModel.MaxReceptionAudioPacketsLost.ToString(),
                    AudioReceivePacketLossMean = googleMeetLogModel.AverageReceptionAudioPacketsLost.ToString(),
                    AudioReceiveDuration = googleMeetLogModel.AudioReceptionDuration.ToString(),
                    BitRatioAudioSending = googleMeetLogModel.BitRatioAudioSending.ToString(),
                    AudioSendPacketLossMax = googleMeetLogModel.MaxSendingAudioPacketsLost.ToString(),
                    AudioSendPacketLossMean = googleMeetLogModel.AverageSendingAudioPacketsLost.ToString(),
                    AudioSendDuration = googleMeetLogModel.AudioSendingDuration.ToString(),
                    NetworkRecvJitterMeaninms = googleMeetLogModel.AverageReceptionFlickering.ToString(),
                    NetworkRecvJitterMaxinms = googleMeetLogModel.MaxReceptionFilckering.ToString(),
                    NetworkSendJitterMeaninms = googleMeetLogModel.AverageSendingFlickering.ToString(),
                    BitRatioScreencastReception = googleMeetLogModel.BitRatioScreencastReception.ToString(),
                    ScreencastReceiveFPSMean = googleMeetLogModel.AverageScreecastReception.ToString(),
                    ScreencastReceiveLongSideMedian = googleMeetLogModel.LongSideMedianScreencastReception.ToString(),
                    ScreencastReceivePacketLossMax = googleMeetLogModel.MaxReceptionScreencastPacketsLost.ToString(),
                    ScreencastReceivePacketLossMean = googleMeetLogModel.AverageReceptionScreencastPacketsLost.ToString(),
                    ScreencastReceiveDuration = googleMeetLogModel.ScreencastReceptionDuration.ToString(),
                    ScreencastReceiveShortSideMedian = googleMeetLogModel.ShortSideMedianScreencastReception.ToString(),
                    BitRatioScreencastSending = googleMeetLogModel.BitRatioScreencastSending.ToString(),
                    AverageScreecastSending = googleMeetLogModel.AverageScreecastSending.ToString(),
                    ScreencastSendLongSideMedian = googleMeetLogModel.LongSideMedianScreencastSending.ToString(),
                    ScreencastSendPacketLossMax = googleMeetLogModel.MaxSendingScreencastPacketsLost.ToString(),
                    ScreencastSendPacketLossMean = googleMeetLogModel.AverageSendingScreencastPacketsLost.ToString(),
                    ScreencastSendDuration = googleMeetLogModel.ScreencastSendingDuration.ToString(),
                    ScreencastSendShortSideMedian = googleMeetLogModel.ShortSideMedianScreencastSending.ToString(),
                    VideoReceiveFPSMean = googleMeetLogModel.AverageVideoReception.ToString(),
                    VideoReceiveLongSideMedian = googleMeetLogModel.LongSideMedianVideoReception.ToString(),
                    VideoReceivePacketLossMax = googleMeetLogModel.MaxVideoReceptionPacketsLost.ToString(),
                    VideoReceivePacketLossMean = googleMeetLogModel.AverageVideoReceptionPacketsLost.ToString(),
                    VideoReceiveDuration = googleMeetLogModel.ReceptionVideoDuration.ToString(),
                    VideoReceiveShortSideMedian = googleMeetLogModel.ShortSideMedianVideoReception.ToString(),
                    BitRatioVideoSending = googleMeetLogModel.BitRatioVideoSending.ToString(),
                    VideoSendFPSMean = googleMeetLogModel.AverageVideoSending.ToString(),
                    VideoSendLongSideMedian = googleMeetLogModel.LongSideMedianVideoSending.ToString(),
                    VideoSendPacketLossMax = googleMeetLogModel.MaxSendingVideoPacketsLost.ToString(),
                    VideoSendDuration = googleMeetLogModel.VideoSendingDuration.ToString(),
                    VideoSendShortSideMedian = googleMeetLogModel.ShortSideMedianVideoSending.ToString(),
                    NetworkCongestionRatio = googleMeetLogModel.NetworkCongestion,
                    MeetingStartDate = googleMeetLogModel.MeetingStartDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone),
                    MeetingEndDate = googleMeetLogModel.MeetingEndDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone),
                    MeetingEnteringDate = googleMeetLogModel.MeetingEnteringDate.ConvertGoogleDateTimeInString(googleMeetLogModel.TimeZone),
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
        /// <param name="cet"></param>
        /// <returns></returns>
        public static string ConvertGoogleDateTimeInString(this DateTime googleDateTime, string cet)
        {
            return string.Format("{0} {1} {2} {3}:{4}:{5} {6}", 
                googleDateTime.Day, 
                GetGoogleMonthStringITA(googleDateTime.Month), 
                googleDateTime.Year,
                (googleDateTime.Hour > 9 ? googleDateTime.Hour.ToString() : ("0" + googleDateTime.Hour.ToString())),
                (googleDateTime.Minute > 9 ? googleDateTime.Minute.ToString() : ("0" + googleDateTime.Minute.ToString())),
                (googleDateTime.Second > 9 ? googleDateTime.Second.ToString() : ("0" + googleDateTime.Second.ToString())),
                string.IsNullOrEmpty(cet) ? string.Empty : cet);

        }
        #endregion
    }
}
