using CsvHelper.Configuration.Attributes;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.Utility;

namespace GoogleMeetLogsNavigator.TransferObject
{
    /// <summary>
    /// Google Meet Log Transfer Object for ITA Language
    /// </summary>
    public class GoogleMeetLogTO : IGoogleMeetLogTO
    {
        public string Date { get; set; }
       
        public string EventName { get; set; }
       
        public string EventDescription { get; set; }
      
        public string MeetingCode { get; set; }
      
        public string PartecipantIdentifier { get; set; }
       
        public string ExternalPartecipantIdentifier { get; set; }
      
        public string PartecipantName { get; set; }

      
        public string ClientType { get; set; }
       
        public string MeetingOwnerEmail { get; set; }
       
        public string ProductType { get; set; }
      
        public string Duration { get; set; }

        public string EffectiveMeetingDurationInHours { get; set; }
        
        public string EffectiveMeetingDurationInSeconds { get; set; }
   
        public string EffectiveMeetingDurationInMinutes { get; set; } 

        public string CallEvaluationOn5 { get; set; }
   
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
     
        public string EstimatedUploadBandwidthInkbps { get; set; }
     
        public string EstimatedDownloadBandwidthInkbps { get; set; }

      
        public string AudioReceivePacketLossMax { get; set; }
      
        public string AudioReceivePacketLossMean { get; set; }
     
        public string AudioReceiveDuration { get; set; }
     
        public string BitRatioAudioSending { get; set; }
   
        public string AudioSendPacketLossMax { get; set; }
      
        public string AudioSendPacketLossMean { get; set; }
    
        public string AudioSendDuration { get; set; }
   
        public string NetworkRecvJitterMeaninms { get; set; }
 
        public string NetworkRecvJitterMaxinms { get; set; }
      
        public string NetworkSendJitterMeaninms { get; set; }
        
        public string BitRatioScreencastReception { get; set; }
 
        public string ScreencastReceiveFPSMean { get; set; }
   
        public string ScreencastReceiveLongSideMedian { get; set; }
     
        public string ScreencastReceivePacketLossMax { get; set; }
     
        public string ScreencastReceivePacketLossMean { get; set; }
      
        public string ScreencastReceiveDuration { get; set; }
       
        public string ScreencastReceiveShortSideMedian { get; set; }
      
        public string BitRatioScreencastSending { get; set; }

        public string ScreencastSendFPSMean { get; set; }
       
        public string ScreencastSendLongSideMedian { get; set; }
      
        public string ScreencastSendPacketLossMax { get; set; }
      
        public string ScreencastSendPacketLossMean { get; set; }
       
        public string ScreencastSendDuration { get; set; }
      
        public string ScreencastSendShortSideMedian { get; set; }
      
        public string VideoReceiveFPSMean { get; set; }
    
        public string VideoReceiveLongSideMedian { get; set; }
   
        public string VideoReceivePacketLossMax { get; set; }
     
        public string VideoReceivePacketLossMean { get; set; }
       
        public string VideoReceiveDuration { get; set; }
     
        public string VideoReceiveShortSideMedian { get; set; }
      
        public string BitRatioVideoSending { get; set; }
      
        public string VideoSendFPSMean { get; set; }
      
        public string VideoSendLongSideMedian { get; set; }
       
        public string VideoSendPacketLossMax { get; set; }
       
        public string VideoSendPacketLossMean { get; set; }
    
        public string VideoSendDuration { get; set; }
       
        public string VideoSendShortSideMedian { get; set; }
       
        public string NetworkCongestionRatio { get; set; }
      
        public string MeetingStartDate { get; set; }

        public string EffectiveMeetingStartDate { get; set; }
        public string EffectiveMeetingEndDate { get; set; }

        public string MeetingEndDate { get; set; }
       
        public string MeetingEnteringDate { get; set; }

        public string TotalMeetingUserPartecipationInDecimal { get; set; }

        public string TotalMeetingUserPartecipationInSeconds { get; set; }

        public string TotalMeetingUserPartecipationInMinutes { get; set; }

        public string TotalMeetingUserPartecipationInHours { get; set; }

        public string TimeZone { get; set; }
    }
}
