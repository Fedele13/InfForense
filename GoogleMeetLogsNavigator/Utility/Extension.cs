using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.TransferObject.ToITA;
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
            string[] dateSplitted = googleDate.Split(',');
            string[] date = dateSplitted[0].Split(' ');
            string[] hours = dateSplitted[1].Split(':');
            string[] seconds = hours[2].Split(' ');
            int monthInt = GetGoogleMonthInt(date[1]);
            commonEuropeanTime = seconds[1];
            return new DateTime(int.Parse(date[2]), monthInt, int.Parse(date[0]), int.Parse(hours[0]), int.Parse(hours[1]), int.Parse(seconds[0]));
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

        #endregion

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private static string GetGoogleMonthString(int month)
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
        private static int GetGoogleMonthInt(string month)
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
                ActionCause = googleMeetLogTO.ActionCause,
                ActionDescription = googleMeetLogTO.ActionDescription,
                VisualizedDestinationName = googleMeetLogTO.VisualizedDestinationNames,
                DestinationEmailsAddresses = googleMeetLogTO.DestinationEmailsAddresses,
                DestinationPhoneNumber = googleMeetLogTO.DestinationPhoneNumber,
                CalendarEventIdentifier = googleMeetLogTO.CalendarEventIdentifier,
                ConferenceID = googleMeetLogTO.ConferenceID,
                NETRoundTrip = googleMeetLogTO.NETRoundTrip,
                TransportProtocol = googleMeetLogTO.TransportProtocol,
                PredictedBandWidthLoading = string.IsNullOrEmpty(googleMeetLogTO.PredictedBandWidthLoading) ? 0 : int.Parse(googleMeetLogTO.PredictedBandWidthLoading),
                PredictedBandWidthUploading = string.IsNullOrEmpty(googleMeetLogTO.PredictedBandWidthUploading) ? 0 : int.Parse(googleMeetLogTO.PredictedBandWidthUploading),
                MaxReceptionAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.MaxReceptionAudioPacketsLost) ? 0 : int.Parse(googleMeetLogTO.MaxReceptionAudioPacketsLost),
                AverageReceptionAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AverageReceptionAudioPacketsLost) ? 0 : double.Parse(googleMeetLogTO.AverageReceptionAudioPacketsLost),
                AudioReceptionDuration = string.IsNullOrEmpty(googleMeetLogTO.AudioReceptionDuration) ? 0 : int.Parse(googleMeetLogTO.AudioReceptionDuration),
                BitRatioAudioSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioAudioSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioAudioSending),
                MaxSendingAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.MaxSendingAudioPacketsLost) ? 0 : int.Parse(googleMeetLogTO.MaxSendingAudioPacketsLost),
                AverageSendingAudioPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AverageSendingAudioPacketsLost) ? 0 : double.Parse(googleMeetLogTO.AverageSendingAudioPacketsLost),
                AudioSendingDuration = string.IsNullOrEmpty(googleMeetLogTO.AudioSendingDuration) ? 0 : int.Parse(googleMeetLogTO.AudioSendingDuration),
                AverageReceptionFlickering = string.IsNullOrEmpty(googleMeetLogTO.AverageReceptionFlickering) ? 0 : double.Parse(googleMeetLogTO.AverageReceptionFlickering),
                MaxReceptionFilckering = string.IsNullOrEmpty(googleMeetLogTO.MaxReceptionFilckering) ? 0 : int.Parse(googleMeetLogTO.MaxReceptionFilckering),
                AverageSendingFlickering = string.IsNullOrEmpty(googleMeetLogTO.AverageSendingFlickering) ? 0 : double.Parse(googleMeetLogTO.AverageSendingFlickering),
                BitRatioScreencastReception = string.IsNullOrEmpty(googleMeetLogTO.BitRatioScreencastReception) ? 0 : double.Parse(googleMeetLogTO.BitRatioScreencastReception),
                AverageScreecastReception = string.IsNullOrEmpty(googleMeetLogTO.AverageScreecastReception) ? 0 : double.Parse(googleMeetLogTO.AverageScreecastReception),
                LongSideMedianScreencastReception = string.IsNullOrEmpty(googleMeetLogTO.LongSideMedianScreencastReception) ? 0 : int.Parse(googleMeetLogTO.LongSideMedianScreencastReception),
                MaxReceptionScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.MaxReceptionScreencastPacketsLost) ? 0 : int.Parse(googleMeetLogTO.MaxReceptionScreencastPacketsLost),
                AverageReceptionScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AverageReceptionScreencastPacketsLost) ? 0 : double.Parse(googleMeetLogTO.AverageReceptionScreencastPacketsLost),
                ScreencastReceptionDuration = string.IsNullOrEmpty(googleMeetLogTO.ScreencastReceptionDuration) ? 0 : int.Parse(googleMeetLogTO.ScreencastReceptionDuration),
                ShortSideMedianScreencastReception = string.IsNullOrEmpty(googleMeetLogTO.ShortSideMedianScreencastReception) ? 0 : int.Parse(googleMeetLogTO.ShortSideMedianScreencastReception),
                BitRatioScreencastSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioScreencastSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioScreencastSending),
                AverageScreecastSending = string.IsNullOrEmpty(googleMeetLogTO.AverageScreecastSending) ? 0 : double.Parse(googleMeetLogTO.AverageScreecastSending),
                LongSideMedianScreencastSending = string.IsNullOrEmpty(googleMeetLogTO.LongSideMedianScreencastSending) ? 0 : int.Parse(googleMeetLogTO.LongSideMedianScreencastSending),
                MaxSendingScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.MaxSendingScreencastPacketsLost) ? 0 : int.Parse(googleMeetLogTO.MaxSendingScreencastPacketsLost),
                AverageSendingScreencastPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AverageSendingScreencastPacketsLost) ? 0 : double.Parse(googleMeetLogTO.AverageSendingScreencastPacketsLost),
                ScreencastSendingDuration = string.IsNullOrEmpty(googleMeetLogTO.ScreencastSendingDuration) ? 0 : int.Parse(googleMeetLogTO.ScreencastSendingDuration),
                ShortSideMedianScreencastSending = string.IsNullOrEmpty(googleMeetLogTO.ShortSideMedianScreencastSending) ? 0 : int.Parse(googleMeetLogTO.ShortSideMedianScreencastSending),
                AverageVideoReception = string.IsNullOrEmpty(googleMeetLogTO.AverageVideoReception) ? 0 : double.Parse(googleMeetLogTO.AverageVideoReception),
                LongSideMedianVideoReception = string.IsNullOrEmpty(googleMeetLogTO.LongSideMedianVideoReception) ? 0 : int.Parse(googleMeetLogTO.LongSideMedianVideoReception),
                MaxVideoReceptionPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.MaxVideoReceptionPacketsLost) ? 0 : int.Parse(googleMeetLogTO.MaxVideoReceptionPacketsLost),
                AverageVideoReceptionPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.AverageVideoReceptionPacketsLost) ? 0 : double.Parse(googleMeetLogTO.AverageVideoReceptionPacketsLost),
                ReceptionVideoDuration = string.IsNullOrEmpty(googleMeetLogTO.ReceptionVideoDuration) ? 0 : int.Parse(googleMeetLogTO.ReceptionVideoDuration),
                ShortSideMedianVideoReception = string.IsNullOrEmpty(googleMeetLogTO.ShortSideMedianVideoReception) ? 0 : int.Parse(googleMeetLogTO.ShortSideMedianVideoReception),
                BitRatioVideoSending = string.IsNullOrEmpty(googleMeetLogTO.BitRatioVideoSending) ? 0 : double.Parse(googleMeetLogTO.BitRatioVideoSending),
                AverageVideoSending = string.IsNullOrEmpty(googleMeetLogTO.AverageVideoSending) ? 0 : double.Parse(googleMeetLogTO.AverageVideoSending),
                LongSideMedianVideoSending = string.IsNullOrEmpty(googleMeetLogTO.LongSideMedianVideoSending) ? 0 : int.Parse(googleMeetLogTO.LongSideMedianVideoSending),
                MaxSendingVideoPacketsLost = string.IsNullOrEmpty(googleMeetLogTO.MaxSendingVideoPacketsLost) ? 0 : int.Parse(googleMeetLogTO.MaxSendingVideoPacketsLost),
                VideoSendingDuration = string.IsNullOrEmpty(googleMeetLogTO.VideoSendingDuration) ? 0 : int.Parse(googleMeetLogTO.VideoSendingDuration),
                ShortSideMedianVideoSending = string.IsNullOrEmpty(googleMeetLogTO.ShortSideMedianVideoSending) ? 0 : int.Parse(googleMeetLogTO.ShortSideMedianVideoSending),
                NetworkCongestion = googleMeetLogTO.NetworkCongestion,
                MeetingStartDate = googleMeetLogTO.MeetingStartDate.ConvertGooogleMeetDataInDateTime(),
                MeetingEndDate = googleMeetLogTO.MeetingEndDate.ConvertGooogleMeetDataInDateTime(),
                MeetingEnteringDate = googleMeetLogTO.MeetingEnteringDate.ConvertGooogleMeetDataInDateTime(),
                TotalMeetingUserPartecipation = string.IsNullOrEmpty(googleMeetLogTO.TotalMeetingUserPartecipation) ? 0 : int.Parse(googleMeetLogTO.TotalMeetingUserPartecipation),
                CommonEuropeanTimeType = googleMeetLogTO.CommonEuropeanTimeType
            };
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
            return new GoogleMeetLogTOITA
            {
                Date = googleMeetLogModel.Date.ConvertGoogleDateTimeInString(googleMeetLogModel.CommonEuropeanTimeType),
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
                ActionCause = googleMeetLogModel.ActionCause,
                ActionDescription = googleMeetLogModel.ActionDescription,
                VisualizedDestinationNames = googleMeetLogModel.VisualizedDestinationName,
                DestinationEmailsAddresses = googleMeetLogModel.DestinationEmailsAddresses,
                DestinationPhoneNumber = googleMeetLogModel.DestinationPhoneNumber,
                CalendarEventIdentifier = string.IsNullOrEmpty(googleMeetLogModel.CalendarEventIdentifier) ? string.Empty : googleMeetLogModel.CalendarEventIdentifier,
                ConferenceID = googleMeetLogModel.ConferenceID,
                NETRoundTrip = googleMeetLogModel.NETRoundTrip,
                TransportProtocol = googleMeetLogModel.TransportProtocol,
                PredictedBandWidthLoading = googleMeetLogModel.PredictedBandWidthLoading.ToString(),
                PredictedBandWidthUploading = googleMeetLogModel.PredictedBandWidthUploading.ToString(),
                MaxReceptionAudioPacketsLost = googleMeetLogModel.MaxReceptionAudioPacketsLost.ToString(),
                AverageReceptionAudioPacketsLost = googleMeetLogModel.AverageReceptionAudioPacketsLost.ToString(),
                AudioReceptionDuration = googleMeetLogModel.AudioReceptionDuration.ToString(),
                BitRatioAudioSending = googleMeetLogModel.BitRatioAudioSending.ToString(),
                MaxSendingAudioPacketsLost = googleMeetLogModel.MaxSendingAudioPacketsLost.ToString(),
                AverageSendingAudioPacketsLost = googleMeetLogModel.AverageSendingAudioPacketsLost.ToString(),
                AudioSendingDuration = googleMeetLogModel.AudioSendingDuration.ToString(),
                AverageReceptionFlickering = googleMeetLogModel.AverageReceptionFlickering.ToString(),
                MaxReceptionFilckering = googleMeetLogModel.MaxReceptionFilckering.ToString(),
                AverageSendingFlickering = googleMeetLogModel.AverageSendingFlickering.ToString(),
                BitRatioScreencastReception = googleMeetLogModel.BitRatioScreencastReception.ToString(),
                AverageScreecastReception = googleMeetLogModel.AverageScreecastReception.ToString(),
                LongSideMedianScreencastReception = googleMeetLogModel.LongSideMedianScreencastReception.ToString(),
                MaxReceptionScreencastPacketsLost = googleMeetLogModel.MaxReceptionScreencastPacketsLost.ToString(),
                AverageReceptionScreencastPacketsLost = googleMeetLogModel.AverageReceptionScreencastPacketsLost.ToString(),
                ScreencastReceptionDuration = googleMeetLogModel.ScreencastReceptionDuration.ToString(),
                ShortSideMedianScreencastReception = googleMeetLogModel.ShortSideMedianScreencastReception.ToString(),
                BitRatioScreencastSending = googleMeetLogModel.BitRatioScreencastSending.ToString(),
                AverageScreecastSending = googleMeetLogModel.AverageScreecastSending.ToString(),
                LongSideMedianScreencastSending = googleMeetLogModel.LongSideMedianScreencastSending.ToString(),
                MaxSendingScreencastPacketsLost = googleMeetLogModel.MaxSendingScreencastPacketsLost.ToString(),
                AverageSendingScreencastPacketsLost = googleMeetLogModel.AverageSendingScreencastPacketsLost.ToString(),
                ScreencastSendingDuration = googleMeetLogModel.ScreencastSendingDuration.ToString(),
                ShortSideMedianScreencastSending = googleMeetLogModel.ShortSideMedianScreencastSending.ToString(),
                AverageVideoReception = googleMeetLogModel.AverageVideoReception.ToString(),
                LongSideMedianVideoReception = googleMeetLogModel.LongSideMedianVideoReception.ToString(),
                MaxVideoReceptionPacketsLost = googleMeetLogModel.MaxVideoReceptionPacketsLost.ToString(),
                AverageVideoReceptionPacketsLost = googleMeetLogModel.AverageVideoReceptionPacketsLost.ToString(),
                ReceptionVideoDuration = googleMeetLogModel.ReceptionVideoDuration.ToString(),
                ShortSideMedianVideoReception = googleMeetLogModel.ShortSideMedianVideoReception.ToString(),
                BitRatioVideoSending = googleMeetLogModel.BitRatioVideoSending.ToString(),
                AverageVideoSending = googleMeetLogModel.AverageVideoSending.ToString(),
                LongSideMedianVideoSending = googleMeetLogModel.LongSideMedianVideoSending.ToString(),
                MaxSendingVideoPacketsLost = googleMeetLogModel.MaxSendingVideoPacketsLost.ToString(),
                VideoSendingDuration = googleMeetLogModel.VideoSendingDuration.ToString(),
                ShortSideMedianVideoSending = googleMeetLogModel.ShortSideMedianVideoSending.ToString(),
                NetworkCongestion = googleMeetLogModel.NetworkCongestion,
                MeetingStartDate = googleMeetLogModel.MeetingStartDate.ConvertGoogleDateTimeInString(googleMeetLogModel.CommonEuropeanTimeType),
                MeetingEndDate = googleMeetLogModel.MeetingEndDate.ConvertGoogleDateTimeInString(googleMeetLogModel.CommonEuropeanTimeType),
                MeetingEnteringDate = googleMeetLogModel.MeetingEnteringDate.ConvertGoogleDateTimeInString(googleMeetLogModel.CommonEuropeanTimeType),
                TotalMeetingUserPartecipation = googleMeetLogModel.TotalMeetingUserPartecipation.ToString(),
                CommonEuropeanTimeType = googleMeetLogModel.CommonEuropeanTimeType
            };
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
            return string.Format("{0} {1} {2}, {3}:{4}:{5} {6}", 
                googleDateTime.Day, 
                GetGoogleMonthString(googleDateTime.Month), 
                googleDateTime.Year,
                (googleDateTime.Hour > 9 ? googleDateTime.Hour.ToString() : ("0" + googleDateTime.Hour.ToString())),
                (googleDateTime.Minute > 9 ? googleDateTime.Minute.ToString() : ("0" + googleDateTime.Minute.ToString())),
                (googleDateTime.Second > 9 ? googleDateTime.Second.ToString() : ("0" + googleDateTime.Second.ToString())),
                string.IsNullOrEmpty(cet) ? string.Empty : cet);

        }
        #endregion
    }
}
