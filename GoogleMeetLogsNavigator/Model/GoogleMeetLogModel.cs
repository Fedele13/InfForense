using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GoogleMeetLogsNavigator.Model
{
   
    class GoogleMeetLogModel
    {
        #region Mandatory Data

        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string MeetingCode { get; set; }
        public string PartecipantIdentifier { get; set; }
        public string ExternalPartecipantIdentifier { get; set; }
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
        
        public string DetinationEmailsAddresses { get; set; }
        
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
        public int AverageReceptionAudioPacketsLost { get; set; }
        public int AudioReceptionDuration { get; set; }
        public int BitRationAudioSending { get; set; }
        public int MaxSendingAudioPacketsLost { get; set; }
        public int AverageSendingAudioPacketsLost { get; set; }
        public int AudioSendingDuration { get; set; }
        public int AverageReceptionFlickering { get; set; }
        public int MaxReceptionFilckering { get; set; }
        public int AverageSendingFlickering { get; set; }
        public int BitRationScreencastReception { get; set; }
        public int AverageScreecastReception { get; set; }
        public int LongSideMedianScreencastReception { get; set; }
        public int MaxReceptionScreencastPacketsLost { get; set; }
        public int AverageReceptionScreencastPacketsLost { get; set; }
        public int ScreencastReceptionDuration { get; set; }
        public int ShortSideMedianScreencastReception { get; set; }
        public int BitRationScreencastSending { get; set; }
        public int AverageScreecastSending { get; set; }
        public int LongSideMedianScreencastSending { get; set; }
        public int MaxSendingScreencastPacketsLost { get; set; }
        public int AverageSendingScreencastPacketsLost { get; set; }
        public int ScreencastSendingDuration { get; set; }
        public int ShortSideMedianScreencastSending { get; set; }
        public int AverageVideoReception { get; set; }
        public int LongSideMedianVideoReception { get; set; }
        public int MaxVideoReceptionPacketsLost { get; set; }
        public int AverageVideoReceptionPacketsLost { get; set; }
        public int ReceptionVideoDuration { get; set; }
        public int ShortSideMedianVideoReception { get; set; }
        public int BitRationVideoSending { get; set; }
        public int AverageVideoSending { get; set; }
        public int LongSideMedianVideoSending { get; set; }
        public int MaxSendingVideoPacketsLost { get; set; }
        public int AverageSendingVideoPacketsLost { get; set; }
        public int VideoSenfingDuration { get; set; }
        public int ShortSideMedianVideoSending { get; set; }
        public string NetworkCongestion { get; set; }

        //TO ADD IN specular of this object model
        //Meeting Start Data (La data più piccola - la durata del log di riferimento)
        //MeetingEnteringData (la data del log di abbandono - la durata)
        //Total meeting partecipation duration (somma delle durate di quel pèartecipante)
        /*
         Se l'id del partecipante non è presente riferirsi a nome del partecipante che ce l'ho sempre a 
         disposizione mentre  l'indirizzo IP in anonimo non l'avrò mai
         */
        public DateTime MeetingStartData { get; set; }
        public DateTime MeetingEndData { get; set; }
        public DateTime MeetingEnteringData { get; set; }
        public int TotalMeetingUserPartecipation { get; set; }

        #endregion
    }
}
