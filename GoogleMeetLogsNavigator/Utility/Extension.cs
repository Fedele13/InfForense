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
                ExternalPartecipantIdentifier = googleMeetLogTO.ExternalPartecipantIdentifier == "YES",
                ClientType = googleMeetLogTO.ClientType,
                MeetingOwnerEmail = googleMeetLogTO.MeetingOwnerEmail,
                ProductType = googleMeetLogTO.ProductType,
                Duration = int.Parse(googleMeetLogTO.Duration),
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
                PredictedBandWidthLoading = int.Parse(googleMeetLogTO.PredictedBandWidthLoading),
                PredictedBandWidthUploading = int.Parse(googleMeetLogTO.PredictedBandWidthUploading),
                MaxReceptionAudioPacketsLost = int.Parse(googleMeetLogTO.MaxReceptionAudioPacketsLost),
                AverageReceptionAudioPacketsLost = double.Parse(googleMeetLogTO.AverageReceptionAudioPacketsLost),
                AudioReceptionDuration = int.Parse(googleMeetLogTO.AudioReceptionDuration),
                BitRationAudioSending = double.Parse(googleMeetLogTO.BitRatioAudioSending),
                MaxSendingAudioPacketsLost = int.Parse(googleMeetLogTO.MaxSendingAudioPacketsLost),
                AverageSendingAudioPacketsLost = double.Parse(googleMeetLogTO.AverageSendingAudioPacketsLost),
                AudioSendingDuration = int.Parse(googleMeetLogTO.AudioSendingDuration),
                AverageReceptionFlickering = double.Parse(googleMeetLogTO.AverageReceptionFlickering),
                MaxReceptionFilckering = int.Parse(googleMeetLogTO.MaxReceptionFilckering),
                AverageSendingFlickering = double.Parse(googleMeetLogTO.AverageSendingFlickering),
                BitRationScreencastReception = double.Parse(googleMeetLogTO.BitRatioScreencastReception),
                AverageScreecastReception = double.Parse(googleMeetLogTO.AverageScreecastReception),
                LongSideMedianScreencastReception = int.Parse(googleMeetLogTO.LongSideMedianScreencastReception),
                MaxReceptionScreencastPacketsLost = int.Parse(googleMeetLogTO.MaxReceptionScreencastPacketsLost),
                AverageReceptionScreencastPacketsLost = double.Parse(googleMeetLogTO.AverageReceptionScreencastPacketsLost),
                ScreencastReceptionDuration = int.Parse(googleMeetLogTO.ScreencastReceptionDuration),
                ShortSideMedianScreencastReception = int.Parse(googleMeetLogTO.ShortSideMedianScreencastReception),
                BitRationScreencastSending = double.Parse(googleMeetLogTO.BitRatioScreencastSending),
                AverageScreecastSending = double.Parse(googleMeetLogTO.AverageScreecastSending),
                LongSideMedianScreencastSending = int.Parse(googleMeetLogTO.LongSideMedianScreencastSending),
                MaxSendingScreencastPacketsLost = int.Parse(googleMeetLogTO.MaxSendingScreencastPacketsLost),
                AverageSendingScreencastPacketsLost = double.Parse(googleMeetLogTO.AverageSendingScreencastPacketsLost),
                ScreencastSendingDuration = int.Parse(googleMeetLogTO.ScreencastSendingDuration),
                ShortSideMedianScreencastSending = int.Parse(googleMeetLogTO.ShortSideMedianScreencastSending),
                AverageVideoReception = double.Parse(googleMeetLogTO.AverageVideoReception),
                LongSideMedianVideoReception = int.Parse(googleMeetLogTO.LongSideMedianVideoReception),
                MaxVideoReceptionPacketsLost = int.Parse(googleMeetLogTO.MaxVideoReceptionPacketsLost),
                AverageVideoReceptionPacketsLost = double.Parse(googleMeetLogTO.AverageVideoReceptionPacketsLost),
                ReceptionVideoDuration = int.Parse(googleMeetLogTO.ReceptionVideoDuration),
                ShortSideMedianVideoReception = int.Parse(googleMeetLogTO.ShortSideMedianVideoReception),
                BitRationVideoSending = double.Parse(googleMeetLogTO.BitRatioVideoSending),
                AverageVideoSending = double.Parse(googleMeetLogTO.AverageVideoSending),
                LongSideMedianVideoSending = int.Parse(googleMeetLogTO.LongSideMedianVideoSending),
                MaxSendingVideoPacketsLost = int.Parse(googleMeetLogTO.MaxSendingVideoPacketsLost),
                VideoSenfingDuration = int.Parse(googleMeetLogTO.VideoSendingDuration),
                ShortSideMedianVideoSending = int.Parse(googleMeetLogTO.ShortSideMedianVideoSending),
                NetworkCongestion = googleMeetLogTO.NetworkCongestion,
                MeetingStartDate = googleMeetLogTO.MeetingStartDate.ConvertGooogleMeetDataInDateTime(),
                MeetingEndDate = googleMeetLogTO.MeetingEndDate.ConvertGooogleMeetDataInDateTime(),
                MeetingEnteringDate = googleMeetLogTO.MeetingEnteringDate.ConvertGooogleMeetDataInDateTime(),
                TotalMeetingUserPartecipation = int.Parse(googleMeetLogTO.TotalMeetingUserPartecipation),
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
                ExternalPartecipantIdentifier = googleMeetLogModel.ExternalPartecipantIdentifier ? "YES" : "NO",
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
                CalendarEventIdentifier = googleMeetLogModel.CalendarEventIdentifier,
                ConferenceID = googleMeetLogModel.ConferenceID,
                NETRoundTrip = googleMeetLogModel.NETRoundTrip,
                TransportProtocol = googleMeetLogModel.TransportProtocol,
                PredictedBandWidthLoading = googleMeetLogModel.PredictedBandWidthLoading.ToString(),
                PredictedBandWidthUploading = googleMeetLogModel.PredictedBandWidthUploading.ToString(),
                MaxReceptionAudioPacketsLost = googleMeetLogModel.MaxReceptionAudioPacketsLost.ToString(),
                AverageReceptionAudioPacketsLost = googleMeetLogModel.AverageReceptionAudioPacketsLost.ToString(),
                AudioReceptionDuration = googleMeetLogModel.AudioReceptionDuration.ToString(),
                BitRatioAudioSending = googleMeetLogModel.BitRationAudioSending.ToString(),
                MaxSendingAudioPacketsLost = googleMeetLogModel.MaxSendingAudioPacketsLost.ToString(),
                AverageSendingAudioPacketsLost = googleMeetLogModel.AverageSendingAudioPacketsLost.ToString(),
                AudioSendingDuration = googleMeetLogModel.AudioSendingDuration.ToString(),
                AverageReceptionFlickering = googleMeetLogModel.AverageReceptionFlickering.ToString(),
                MaxReceptionFilckering = googleMeetLogModel.MaxReceptionFilckering.ToString(),
                AverageSendingFlickering = googleMeetLogModel.AverageSendingFlickering.ToString(),
                BitRatioScreencastReception = googleMeetLogModel.BitRationScreencastReception.ToString(),
                AverageScreecastReception = googleMeetLogModel.AverageScreecastReception.ToString(),
                LongSideMedianScreencastReception = googleMeetLogModel.LongSideMedianScreencastReception.ToString(),
                MaxReceptionScreencastPacketsLost = googleMeetLogModel.MaxReceptionScreencastPacketsLost.ToString(),
                AverageReceptionScreencastPacketsLost = googleMeetLogModel.AverageReceptionScreencastPacketsLost.ToString(),
                ScreencastReceptionDuration = googleMeetLogModel.ScreencastReceptionDuration.ToString(),
                ShortSideMedianScreencastReception = googleMeetLogModel.ShortSideMedianScreencastReception.ToString(),
                BitRatioScreencastSending = googleMeetLogModel.BitRationScreencastSending.ToString(),
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
                BitRatioVideoSending = googleMeetLogModel.BitRationVideoSending.ToString(),
                AverageVideoSending = googleMeetLogModel.AverageVideoSending.ToString(),
                LongSideMedianVideoSending = googleMeetLogModel.LongSideMedianVideoSending.ToString(),
                MaxSendingVideoPacketsLost = googleMeetLogModel.MaxSendingVideoPacketsLost.ToString(),
                VideoSendingDuration = googleMeetLogModel.VideoSenfingDuration.ToString(),
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
                googleDateTime.Day, GetGoogleMonthString(googleDateTime.Month), googleDateTime.Year,
                googleDateTime.Hour, googleDateTime.Minute, googleDateTime.Second,
                string.IsNullOrEmpty(cet) ? string.Empty : cet);

        }
        #endregion
    }
}
