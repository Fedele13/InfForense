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
      
        public string CallEvaluationOn5 { get; set; }
     
        public string IPAddress { get; set; }
      
        public string City { get; set; }
     
        public string Nation { get; set; }
       
        public string ActionCause { get; set; }
      
        public string ActionDescription { get; set; }
       
        public string VisualizedDestinationNames { get; set; }
     
        public string DestinationEmailsAddresses { get; set; }
     
        public string DestinationPhoneNumber { get; set; }

        public string CalendarEventIdentifier { get; set; }
       
        public string ConferenceID { get; set; }
       
        public string NETRoundTrip { get; set; }
      
        public string TransportProtocol { get; set; }
     
        public string PredictedBandWidthLoading { get; set; }
     
        public string PredictedBandWidthUploading { get; set; }

      
        public string MaxReceptionAudioPacketsLost { get; set; }
      
        public string AverageReceptionAudioPacketsLost { get; set; }
     
        public string AudioReceptionDuration { get; set; }
     
        public string BitRatioAudioSending { get; set; }
   
        public string MaxSendingAudioPacketsLost { get; set; }
      
        public string AverageSendingAudioPacketsLost { get; set; }
    
        public string AudioSendingDuration { get; set; }
   
        public string AverageReceptionFlickering { get; set; }
 
        public string MaxReceptionFilckering { get; set; }
      
        public string AverageSendingFlickering { get; set; }
        
        public string BitRatioScreencastReception { get; set; }
 
        public string AverageScreecastReception { get; set; }
   
        public string LongSideMedianScreencastReception { get; set; }
     
        public string MaxReceptionScreencastPacketsLost { get; set; }
     
        public string AverageReceptionScreencastPacketsLost { get; set; }
      
        public string ScreencastReceptionDuration { get; set; }
       
        public string ShortSideMedianScreencastReception { get; set; }
      
        public string BitRatioScreencastSending { get; set; }

        public string AverageScreecastSending { get; set; }
       
        public string LongSideMedianScreencastSending { get; set; }
      
        public string MaxSendingScreencastPacketsLost { get; set; }
      
        public string AverageSendingScreencastPacketsLost { get; set; }
       
        public string ScreencastSendingDuration { get; set; }
      
        public string ShortSideMedianScreencastSending { get; set; }
      
        public string AverageVideoReception { get; set; }
    
        public string LongSideMedianVideoReception { get; set; }
   
        public string MaxVideoReceptionPacketsLost { get; set; }
     
        public string AverageVideoReceptionPacketsLost { get; set; }
       
        public string ReceptionVideoDuration { get; set; }
     
        public string ShortSideMedianVideoReception { get; set; }
      
        public string BitRatioVideoSending { get; set; }
      
        public string AverageVideoSending { get; set; }
      
        public string LongSideMedianVideoSending { get; set; }
       
        public string MaxSendingVideoPacketsLost { get; set; }
       
        public string AverageSendingVideoPacketsLost { get; set; }
    
        public string VideoSendingDuration { get; set; }
       
        public string ShortSideMedianVideoSending { get; set; }
       
        public string NetworkCongestion { get; set; }
      
        public string MeetingStartDate { get; set; }
      
        public string MeetingEndDate { get; set; }
       
        public string MeetingEnteringDate { get; set; }
     
        public string TotalMeetingUserPartecipation { get; set; }
       
        public string CommonEuropeanTimeType { get; set; }
    }
}
