using GoogleMeetLogsNavigator.BO;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.Utility
{
    public static class GoogleMeetLogTOExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleMeetLogTO"></param>
        /// <param name="dataToUpdate"></param>
        /// <returns></returns>
        public static GoogleMeetLogModel MapTransferObjectInModel(this GoogleMeetLogTO googleMeetLogTO, GoogleMeetingDataCalculator.DataResult dataToUpdate)
        {
            return new GoogleMeetLogModel
            {
                Date = DateTime.Parse(googleMeetLogTO.Date),
                EventName = googleMeetLogTO.EventName,
                EventDescription = googleMeetLogTO.EventDescription,
                MeetingCode = googleMeetLogTO.MeetingCode,
                PartecipantIdentifier = googleMeetLogTO.PartecipantIdentifier,
                ExternalPartecipantIdentifier = googleMeetLogTO.ExternalPartecipantIdentifier == "YES",
                ClientType = googleMeetLogTO.ClientType,
                MeetingOwnerEmail = googleMeetLogTO.MeetingOwnerEmail,
                ProductType = googleMeetLogTO.ProductType,
                Duration = new TimeSpan(0, 0, int.Parse(googleMeetLogTO.Duration)),
                CallEvaluationOn5 = googleMeetLogTO.CallEvaluationOn5,
                PartecipantName = googleMeetLogTO.PartecipantName,
                IPAddress = googleMeetLogTO.IPAddress,
                City = googleMeetLogTO.City,
                Nation = googleMeetLogTO.Nation,
                ActionCause = googleMeetLogTO.ActionCause,
                ActionDescription = googleMeetLogTO.ActionDescription,
                VisualizedDestinationName = googleMeetLogTO.VisualizedDestinationName,
                DetinationEmailsAddresses = googleMeetLogTO.DetinationEmailsAddresses,
                DestinationPhoneNumber = googleMeetLogTO.DestinationPhoneNumber,
                CalendarEventIdentifier = googleMeetLogTO.CalendarEventIdentifier,
                ConferenceID = googleMeetLogTO.ConferenceID,
                NETRoundTrip = googleMeetLogTO.NETRoundTrip,
                TransportProtocol = googleMeetLogTO.TransportProtocol,
                PredictedBandWidthLoading = int.Parse(googleMeetLogTO.PredictedBandWidthLoading),
                PredictedBandWidthUploading = int.Parse(googleMeetLogTO.PredictedBandWidthUploading),
                MaxReceptionAudioPacketsLost = int.Parse(googleMeetLogTO.MaxReceptionAudioPacketsLost),
                AverageReceptionAudioPacketsLost = int.Parse(googleMeetLogTO.AverageReceptionAudioPacketsLost),
                AudioReceptionDuration = int.Parse(googleMeetLogTO.AudioReceptionDuration),
                BitRationAudioSending = int.Parse(googleMeetLogTO.BitRationAudioSending),
                MaxSendingAudioPacketsLost = int.Parse(googleMeetLogTO.MaxSendingAudioPacketsLost),
                AverageSendingAudioPacketsLost = int.Parse(googleMeetLogTO.AverageSendingAudioPacketsLost),
                AudioSendingDuration = int.Parse(googleMeetLogTO.AudioSendingDuration),
                AverageReceptionFlickering = int.Parse(googleMeetLogTO.AverageReceptionFlickering),
                MaxReceptionFilckering = int.Parse(googleMeetLogTO.MaxReceptionFilckering),
                AverageSendingFlickering = int.Parse(googleMeetLogTO.AverageSendingFlickering),
                BitRationScreencastReception = int.Parse(googleMeetLogTO.BitRationScreencastReception),
                AverageScreecastReception = int.Parse(googleMeetLogTO.AverageScreecastReception),
                LongSideMedianScreencastReception = int.Parse(googleMeetLogTO.LongSideMedianScreencastReception),
                MaxReceptionScreencastPacketsLost = int.Parse(googleMeetLogTO.MaxReceptionScreencastPacketsLost),
                AverageReceptionScreencastPacketsLost = int.Parse(googleMeetLogTO.AverageReceptionScreencastPacketsLost),
                ScreencastReceptionDuration = int.Parse(googleMeetLogTO.ScreencastReceptionDuration),
                ShortSideMedianScreencastReception = int.Parse(googleMeetLogTO.ShortSideMedianScreencastReception),
                BitRationScreencastSending = int.Parse(googleMeetLogTO.BitRationScreencastSending),
                AverageScreecastSending = int.Parse(googleMeetLogTO.AverageScreecastSending),
                LongSideMedianScreencastSending = int.Parse(googleMeetLogTO.LongSideMedianScreencastSending),
                MaxSendingScreencastPacketsLost = int.Parse(googleMeetLogTO.MaxSendingScreencastPacketsLost),
                AverageSendingScreencastPacketsLost = int.Parse(googleMeetLogTO.AverageSendingScreencastPacketsLost),
                ScreencastSendingDuration = int.Parse(googleMeetLogTO.ScreencastSendingDuration),
                ShortSideMedianScreencastSending = int.Parse(googleMeetLogTO.ShortSideMedianScreencastSending),
                AverageVideoReception = int.Parse(googleMeetLogTO.AverageVideoReception),
                LongSideMedianVideoReception = int.Parse(googleMeetLogTO.LongSideMedianVideoReception),
                MaxVideoReceptionPacketsLost = int.Parse(googleMeetLogTO.MaxVideoReceptionPacketsLost),
                AverageVideoReceptionPacketsLost = int.Parse(googleMeetLogTO.AverageVideoReceptionPacketsLost),
                ReceptionVideoDuration = int.Parse(googleMeetLogTO.ReceptionVideoDuration),
                ShortSideMedianVideoReception = int.Parse(googleMeetLogTO.ShortSideMedianVideoReception),
                BitRationVideoSending = int.Parse(googleMeetLogTO.BitRationVideoSending),
                AverageVideoSending = int.Parse(googleMeetLogTO.AverageVideoSending),
                LongSideMedianVideoSending = int.Parse(googleMeetLogTO.LongSideMedianVideoSending),
                MaxSendingVideoPacketsLost = int.Parse(googleMeetLogTO.MaxSendingVideoPacketsLost),
                VideoSenfingDuration = int.Parse(googleMeetLogTO.VideoSenfingDuration),
                ShortSideMedianVideoSending = int.Parse(googleMeetLogTO.ShortSideMedianVideoSending),
                NetworkCongestion = googleMeetLogTO.NetworkCongestion,
                MeetingStartDate = dataToUpdate.MeetingStartDate,
                MeetingEndDate = dataToUpdate.MeetingEndDate,
                MeetingEnteringDate = dataToUpdate.MeetingEnteringDate,
                TotalMeetingUserPartecipation = dataToUpdate.TotalMeetingUserPartecipation
            };
        }
    }
}
