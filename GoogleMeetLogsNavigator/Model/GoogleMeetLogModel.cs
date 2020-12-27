using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GoogleMeetLogsNavigator.Model
{
   
    public class GoogleMeetLogModel
    {
        #region Mandatory Data

        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string MeetingCode { get; set; }
        public string PartecipantIdentifier { get; set; }
        public bool ExternalPartecipantIdentifier { get; set; }
        public string ClientType { get; set; }
        public string MeetingOwnerEmail { get; set; }
        public string ProductType { get; set; }
        public int Duration { get; set; }
        public string CallEvaluationOn5 { get; set; }
        public string PartecipantName { get; set; }
        public string IPAddress { get; set; }
        public string City { get; set; }
        public string Nation { get; set; }
        public string ActionCause { get; set; }
        public string ActionDescription { get; set; }
        public string VisualizedDestinationName { get; set; }
        public string DestinationEmailsAddresses { get; set; }
        public string DestinationPhoneNumber { get; set; }
        public string CalendarEventIdentifier { get; set; }
        public string ConferenceID { get; set; }

        #endregion Mandatory Data

        #region Optional Data

        public string NETRoundTrip { get; set; }
        public string TransportProtocol { get; set; }
        public int PredictedBandWidthLoading { get; set; }
        public int PredictedBandWidthUploading { get; set; }
        public int MaxReceptionAudioPacketsLost { get; set; }
        public double AverageReceptionAudioPacketsLost { get; set; }
        public int AudioReceptionDuration { get; set; }
        public double BitRatioAudioSending { get; set; }
        public int MaxSendingAudioPacketsLost { get; set; }
        public double AverageSendingAudioPacketsLost { get; set; }
        public int AudioSendingDuration { get; set; }
        public double AverageReceptionFlickering { get; set; }
        public int MaxReceptionFilckering { get; set; }
        public double AverageSendingFlickering { get; set; }
        public double BitRatioScreencastReception { get; set; }
        public double AverageScreecastReception { get; set; }
        public int LongSideMedianScreencastReception { get; set; }
        public int MaxReceptionScreencastPacketsLost { get; set; }
        public double AverageReceptionScreencastPacketsLost { get; set; }
        public int ScreencastReceptionDuration { get; set; }
        public int ShortSideMedianScreencastReception { get; set; }
        public double BitRatioScreencastSending { get; set; }
        public double AverageScreecastSending { get; set; }
        public int LongSideMedianScreencastSending { get; set; }
        public int MaxSendingScreencastPacketsLost { get; set; }
        public double AverageSendingScreencastPacketsLost { get; set; }
        public int ScreencastSendingDuration { get; set; }
        public int ShortSideMedianScreencastSending { get; set; }
        public double AverageVideoReception { get; set; }
        public int LongSideMedianVideoReception { get; set; }
        public int MaxVideoReceptionPacketsLost { get; set; }
        public double AverageVideoReceptionPacketsLost { get; set; }
        public int ReceptionVideoDuration { get; set; }
        public int ShortSideMedianVideoReception { get; set; }
        public double BitRatioVideoSending { get; set; }
        public double AverageVideoSending { get; set; }
        public int LongSideMedianVideoSending { get; set; }
        public int MaxSendingVideoPacketsLost { get; set; }
        public double AverageSendingVideoPacketsLost { get; set; }
        public int VideoSendingDuration { get; set; }
        public int ShortSideMedianVideoSending { get; set; }
        public string NetworkCongestion { get; set; }
        public DateTime MeetingStartDate { get; set; }
        public DateTime MeetingEndDate { get; set; }
        public DateTime MeetingEnteringDate { get; set; }
        public int TotalMeetingUserPartecipation { get; set; }
        public string CommonEuropeanTimeType { get; set; }

        #endregion
    }
}
