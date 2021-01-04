using System;

namespace GoogleMeetLogsNavigator.Model
{
    /// <summary>
    /// The Log Model Representation
    /// </summary>
    public class GoogleMeetLogModel
    {
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
        public string Country { get; set; }
        public string ActionReason { get; set; }
        public string ActionDescription { get; set; }
        public string TargetDisplayNames { get; set; }
        public string TargetEmail { get; set; }
        public string TargetPhoneNumber { get; set; }
        public string CalendarEventIdentifier { get; set; }
        public string ConferenceID { get; set; }
        public string NETRoundTrip { get; set; }
        public string TransportProtocol { get; set; }
        public int EstimatedUploadBandwidthInkbps { get; set; }
        public int EstimatedDownloadBandwidthInkbps { get; set; }
        public int AudioReceivePacketLossMax { get; set; }
        public double AudioReceivePacketLossMean { get; set; }
        public int AudioReceiveDuration { get; set; }
        public double BitRatioAudioSending { get; set; }
        public int AudioSendPacketLossMax { get; set; }
        public double AudioSendPacketLossMean { get; set; }
        public int AudioSendDuration { get; set; }
        public double NetworkRecvJitterMeaninms { get; set; }
        public int NetworkRecvJitterMaxinms { get; set; }
        public double NetworkSendJitterMeaninms { get; set; }
        public double BitRatioScreencastReception { get; set; }
        public double ScreencastReceiveFPSMean { get; set; }
        public int ScreencastReceiveLongSideMedian { get; set; }
        public int ScreencastReceivePacketLossMax { get; set; }
        public double ScreencastReceivePacketLossMean { get; set; }
        public int ScreencastReceiveDuration { get; set; }
        public int ScreencastReceiveShortSideMedian { get; set; }
        public double BitRatioScreencastSending { get; set; }
        public double ScreencastSendFPSMean { get; set; }
        public int ScreencastSendLongSideMedian { get; set; }
        public int ScreencastSendPacketLossMax { get; set; }
        public double ScreencastSendPacketLossMean { get; set; }
        public int ScreencastSendDuration { get; set; }
        public int ScreencastSendShortSideMedian { get; set; }
        public double VideoReceiveFPSMean { get; set; }
        public int VideoReceiveLongSideMedian { get; set; }
        public int MaxVideoReceptionPacketsLost { get; set; }
        public double VideoReceivePacketLossMax { get; set; }
        public int VideoReceiveDuration { get; set; }
        public int VideoReceiveShortSideMedian { get; set; }
        public double BitRatioVideoSending { get; set; }
        public double VideoSendFPSMean { get; set; }
        public int VideoSendLongSideMedian { get; set; }
        public int VideoSendPacketLossMax { get; set; }
        public double AverageSendingVideoPacketsLost { get; set; }
        public int VideoSendDuration { get; set; }
        public int VideoSendShortSideMedian { get; set; }
        public string NetworkCongestionRatio { get; set; }
        public DateTime MeetingStartDate { get; set; }
        public DateTime MeetingEndDate { get; set; }
        public DateTime MeetingEnteringDate { get; set; }
        public double TotalMeetingUserPartecipationInDecimal { get; set; }
        public int TotalMeetingUserPartecipationInSeconds { get; set; }
        public int TotalMeetingUserPartecipationInMinutes { get; set; }
        public int TotalMeetingUserPartecipationInHours { get; set; }
        public string TimeZone { get; set; }

    }
}
