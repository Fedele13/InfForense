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

        public string Date { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string MeetingCode { get; set; }
        public string PartecipantIdentifier { get; set; }
        public string ExternalPartecipantIdentifier { get; set; }
        public string ClientType { get; set; }
        public string MeetingOwnerEmail { get; set; }
    
        public string ProductType { get; set; }
    
        public string Duration { get; set; }
    
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
    
        public string PredictedBandWidthLoading { get; set; }
    
        public string PredictedBandWidthUploading { get; set; }

    
        public string MaxReceptionAudioPacketsLost { get; set; }
    
        public string AverageReceptionAudioPacketsLost { get; set; }
    
        public string AudioReceptionDuration { get; set; }
    
        public string BitRationAudioSending { get; set; }
    
        public string MaxSendingAudioPacketsLost { get; set; }
        
        public string AverageSendingAudioPacketsLost { get; set; }
        
        public string AudioSendingDuration { get; set; }
        
        public string AverageReceptionFlickering { get; set; }
        
        public string MaxReceptionFilckering { get; set; }
        
        public string AverageSendingFlickering { get; set; }
        
        public string BitRationScreencastReception { get; set; }
        
        public string AverageScreecastReception { get; set; }
        
        public string LongSideMedianScreencastReception { get; set; }
        
        public string MaxReceptionScreencastPacketsLost { get; set; }
        
        public string AverageReceptionScreencastPacketsLost { get; set; }
        
        public string ScreencastReceptionDuration { get; set; }
        
        public string ShortSideMedianScreencastReception { get; set; }
        
        public string BitRationScreencastSending { get; set; }
        
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

        public string BitRationVideoSending { get; set; }

        public string AverageVideoSending { get; set; }

        public string LongSideMedianVideoSending { get; set; }

        public string MaxSendingVideoPacketsLost { get; set; }

        public string AverageSendingVideoPacketsLost { get; set; }

        public string VideoSenfingDuration { get; set; }

        public string ShortSideMedianVideoSending { get; set; }

        public string NetworkCongestion { get; set; }

        //TO ADD IN specular of this object model
        //Meeting Start Data (La data più piccola - la durata del log di riferimento)
        //MeetingEnteringData (la data del log di abbandono - la durata)
        //Total meeting partecipation duration (somma delle durate di quel pèartecipante)
        /*
         Se l'id del partecipante non è presente riferirsi a nome del partecipante che ce l'ho sempre a 
         disposizione mentre  l'indirizzo IP in anonimo non l'avrò mai
         */

        #endregion
    }
}
