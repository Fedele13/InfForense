using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.TransferObject.Interface
{
    /// <summary>
    /// The Google Meet Logel interface
    /// </summary>
    public interface IGoogleMeetLogTO
    {
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
        string ActionReason { get; set; }
        string ActionDescription { get; set; }
        string TargetDisplayNames { get; set; }
        string TargetEmail { get; set; }
        string TargetPhoneNumber { get; set; }
        string CalendarEventIdentifier { get; set; }
        string ConferenceID { get; set; }

        string NETRoundTrip { get; set; }
        string TransportProtocol { get; set; }
        string EstimatedUploadBandwidthInkbps { get; set; }
        string EstimatedDownloadBandwidthInkbps { get; set; }
        string AudioReceivePacketLossMax { get; set; }
        string AudioReceivePacketLossMean { get; set; }
        string AudioReceiveDuration { get; set; }
        string BitRatioAudioSending { get; set; }
        string AudioSendPacketLossMax { get; set; }
        string AudioSendPacketLossMean { get; set; }
        string AudioSendDuration { get; set; }
        string NetworkRecvJitterMeaninms { get; set; }
        string NetworkRecvJitterMaxinms { get; set; }
        string NetworkSendJitterMeaninms { get; set; }
        string BitRatioScreencastReception { get; set; }
        string ScreencastReceiveFPSMean { get; set; }
        string ScreencastReceiveLongSideMedian { get; set; }
        string ScreencastReceivePacketLossMax { get; set; }
        string ScreencastReceivePacketLossMean { get; set; }
        string ScreencastReceiveDuration { get; set; }
        string ScreencastReceiveShortSideMedian { get; set; }
        string BitRatioScreencastSending { get; set; }
        string AverageScreecastSending { get; set; }
        string ScreencastSendLongSideMedian { get; set; }
        string ScreencastSendPacketLossMax { get; set; }
        string ScreencastSendPacketLossMean { get; set; }
        string ScreencastSendDuration { get; set; }
        string ScreencastSendShortSideMedian { get; set; }
        string VideoReceiveFPSMean { get; set; }
        string VideoReceiveLongSideMedian { get; set; }
        string VideoReceivePacketLossMax { get; set; }
        string VideoReceivePacketLossMean { get; set; }
        string VideoReceiveDuration { get; set; }
        string VideoReceiveShortSideMedian { get; set; }
        string BitRatioVideoSending { get; set; }
        string VideoSendFPSMean { get; set; }
        string VideoSendLongSideMedian { get; set; }
        string VideoSendPacketLossMax { get; set; }
        string VideoSendPacketLossMean { get; set; }
        string VideoSendDuration { get; set; }
        string VideoSendShortSideMedian { get; set; }
        string NetworkCongestionRatio { get; set; }
        string MeetingStartDate { get; set; }
        string EffectiveMeetingStartDate { get; set; }
        string MeetingEndDate { get; set; }
        string MeetingEnteringDate { get; set; }
        public string TotalMeetingUserPartecipationInDecimal { get; set; }
        public string TotalMeetingUserPartecipationInSeconds { get; set; }
        public string TotalMeetingUserPartecipationInMinutes { get; set; }
        public string TotalMeetingUserPartecipationInHours { get; set; }
        string TimeZone { get; set; }
    }
}
