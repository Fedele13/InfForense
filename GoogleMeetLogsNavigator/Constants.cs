using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator
{
    /// <summary>
    /// Constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Google Meet Csv File HEADER
        /// </summary>
        public class CSVHeader
        {
            #region Mandatory Data

            public static readonly string Date = "Data";
            public static readonly string EventName = "Nome evento";
            public static readonly string EventDescription = "Descrizione evento";
            public static readonly string MeetingCode = "Codice riunione";
            public static readonly string PartecipantIdentifier = "Identificatore partecipante";
            public static readonly string ExternalPartecipantIdentifier = "Partecipante esterno all'organizzazione";
            public static readonly string ClientType = "Tipo di client";
            public static readonly string MeetingOwnerEmail = "Email organizzatore";
            public static readonly string ProductType = "Tipo di prodotto";
            public static readonly string Duration = "Durata";
            public static readonly string CallEvaluationOn5 = "Valutazione chiamata su 5";
            public static readonly string PartecipantName = "Nome partecipante";
            public static readonly string IPAddress = "Indirizzo IP";
            public static readonly string City = "CittÃ";
            public static readonly string Nation = "Paese";
            public static readonly string ActionCause = "Motivo azione";
            public static readonly string ActionDescription = "Descrizione azione";
            public static readonly string VisualizedDestinationName = "Nomi visualizzati di destinazione";
            public static readonly string DetinationEmailsAddresses = "Email di destinazione";
            public static readonly string DestinationPhoneNumber = "Numero telefono di destinazione";
            public static readonly string CalendarEventIdentifier = "ID evento Calendar";
            public static readonly string ConferenceID = "ID conferenza";

            #endregion Mandatory Data

            #region Optional Data

            public static readonly string NETRoundTrip = "Media tempo di round trip rete in ms";
            public static readonly string TransportProtocol = "Protocollo di trasporto";
            public static readonly string PredictedBandWidthLoading = "Larghezza di banda per il caricamento in kbps prevista";
            public static readonly string PredictedBandWidthUploading = "Larghezza di banda per il download in kbps prevista";
                   
            public static readonly string MaxReceptionAudioPacketsLost = "max pacchetti ricezione audio";
            public static readonly string AverageReceptionAudioPacketsLost = "Perdita media pacchetti ricezione audio";
            public static readonly string AudioReceptionDuration = "Durata ricezione audio";
            public static readonly string BitRationAudioSending = "Media velocitÃ  in bit invio audio in kbps";
            public static readonly string MaxSendingAudioPacketsLost = "Perdita max pacchetti invio audio";
            public static readonly string AverageSendingAudioPacketsLost = "Perdita media pacchetti invio audio";
            public static readonly string AudioSendingDuration = "Durata invio audio";

            public static readonly string AverageReceptionFlickering = "Media tremolio ricezione rete in ms";
            public static readonly string MaxReceptionFilckering = "Massimo tremolio ricezione rete in ms";
            public static readonly string AverageSendingFlickering = "Media tremolio invio rete in ms";

            public static readonly string BitRationScreencastReception = "Media velocitÃ  in bit ricezione screencast in kbps";
            public static readonly string AverageScreecastReception = "Media f/s ricezione screencast";
            public static readonly string LongSideMedianScreencastReception = "Mediana lato lungo ricezione screencast";
            public static readonly string MaxReceptionScreencastPacketsLost = "Perdita max pacchetti ricezione screencast";
            public static readonly string AverageReceptionScreencastPacketsLost = "Perdita media pacchetti ricezione screencast";
            public static readonly string ScreencastReceptionDuration = "Durata ricezione screencast";
            public static readonly string ShortSideMedianScreencastReception = "Mediana lato corto ricezione screencast";
            public static readonly string BitRationScreencastSending = "Media velocitÃ  in bit invio screencast in kbps";
            public static readonly string AverageScreecastSending = "Media f/s invio screencast";
            public static readonly string LongSideMedianScreencastSending = "Mediana lato lungo invio screencast";
            public static readonly string MaxSendingScreencastPacketsLost = "Perdita max pacchetti invio screencast";
            public static readonly string AverageSendingScreencastPacketsLost = "Perdita media pacchetti invio screencast";
            public static readonly string ScreencastSendingDuration = "Durata invio screencast";
            public static readonly string ShortSideMedianScreencastSending = "Mediana lato corto invio screencast";
                   
            public static readonly string AverageVideoReception = "Media f/s ricezione video";
            public static readonly string LongSideMedianVideoReception = "Mediana lato lungo ricezione video";
            public static readonly string MaxVideoReceptionPacketsLost = "Perdita max pacchetti ricezione video";
            public static readonly string AverageVideoReceptionPacketsLost = "Perdita media pacchetti ricezione video";
            public static readonly string ReceptionVideoDuration = "Durata ricezione video";
            public static readonly string ShortSideMedianVideoReception = "Mediana lato corto ricezione video";
            public static readonly string BitRationVideoSending = "Media velocitÃ  in bit invio video in kbps";
            public static readonly string AverageVideoSending = "Media f/s invio video";
            public static readonly string LongSideMedianVideoSending = "Mediana lato lungo invio video";
            public static readonly string MaxSendingVideoPacketsLost = "Perdita max pacchetti invio video";
            public static readonly string AverageSendingVideoPacketsLost = "Perdita media pacchetti invio video";
            public static readonly string VideoSenfingDuration = "Durata invio video";
            public static readonly string ShortSideMedianVideoSending = "Mediana lato corto invio video";
                    
            public static readonly string NetworkCongestion = "Rapporto congestione della rete";

            #endregion

        }

        public class EventsToConsider
        {
            public static readonly string CallExit = "L'endpoint ha abbandonato la chiamata";
        }
    }
}
