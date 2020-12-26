using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.TransferObject.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGoogleMeetLogTO
    {
        #region Mandatory Data

        string Date { get; set; }
        string EventName { get; set; }
        string EventDescription { get; set; }
        string MeetingCode { get; set; }
        string PartecipantIdentifier { get; set; }
        string ExternalPartecipantIdentifier { get; set; }
        string ClientType { get; set; }
        string MeetingOwnerEmail { get; set; }
        string ProductType { get; set; }
        string Duration { get; set; }
        string CallEvaluationOn5 { get; set; }
        string PartecipantName { get; set; }
        string IPAddress { get; set; }
        string City { get; set; }
        string Nation { get; set; }
        string ActionCause { get; set; }
        string ActionDescription { get; set; }
        string VisualizedDestinationNames { get; set; }
        string DestinationEmailsAddresses { get; set; }
        string DestinationPhoneNumber { get; set; }
        string CalendarEventIdentifier { get; set; }
        string ConferenceID { get; set; }

        #endregion Mandatory Data

        #region Optional Data

        string NETRoundTrip { get; set; }
        string TransportProtocol { get; set; }
        string PredictedBandWidthLoading { get; set; }
        string PredictedBandWidthUploading { get; set; }
        string MaxReceptionAudioPacketsLost { get; set; }
        string AverageReceptionAudioPacketsLost { get; set; }
        string AudioReceptionDuration { get; set; }
        string BitRatioAudioSending { get; set; }
        string MaxSendingAudioPacketsLost { get; set; }
        string AverageSendingAudioPacketsLost { get; set; }
        string AudioSendingDuration { get; set; }
        string AverageReceptionFlickering { get; set; }
        string MaxReceptionFilckering { get; set; }
        string AverageSendingFlickering { get; set; }
        string BitRatioScreencastReception { get; set; }
        string AverageScreecastReception { get; set; }
        string LongSideMedianScreencastReception { get; set; }
        string MaxReceptionScreencastPacketsLost { get; set; }
        string AverageReceptionScreencastPacketsLost { get; set; }
        string ScreencastReceptionDuration { get; set; }
        string ShortSideMedianScreencastReception { get; set; }
        string BitRatioScreencastSending { get; set; }
        string AverageScreecastSending { get; set; }
        string LongSideMedianScreencastSending { get; set; }
        string MaxSendingScreencastPacketsLost { get; set; }
        string AverageSendingScreencastPacketsLost { get; set; }
        string ScreencastSendingDuration { get; set; }
        string ShortSideMedianScreencastSending { get; set; }
        string AverageVideoReception { get; set; }
        string LongSideMedianVideoReception { get; set; }
        string MaxVideoReceptionPacketsLost { get; set; }
        string AverageVideoReceptionPacketsLost { get; set; }
        string ReceptionVideoDuration { get; set; }
        string ShortSideMedianVideoReception { get; set; }
        string BitRatioVideoSending { get; set; }
        string AverageVideoSending { get; set; }
        string LongSideMedianVideoSending { get; set; }
        string MaxSendingVideoPacketsLost { get; set; }
        string AverageSendingVideoPacketsLost { get; set; }
        string VideoSendingDuration { get; set; }
        string ShortSideMedianVideoSending { get; set; }
        string NetworkCongestion { get; set; }
        string MeetingStartDate { get; set; }
        string MeetingEndDate { get; set; }
        string MeetingEnteringDate { get; set; }
        string TotalMeetingUserPartecipation { get; set; }
        string CommonEuropeanTimeType { get; set; }

        #endregion
    }
}
