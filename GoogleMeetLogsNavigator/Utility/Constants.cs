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

            public string Date = "Data";
            public string EventName = "Nome evento";
            public string EventDescription = "Descrizione evento";
            public string MeetingCode = "Codice riunione";
            public string PartecipantIdentifier = "Identificatore partecipante";
            public string ExternalPartecipantIdentifier = "Partecipante esterno all'organizzazione";
            public string ClientType = "Tipo di client";
            public string MeetingOwnerEmail = "Email organizzatore";
            public string ProductType = "Tipo di prodotto";
            public string Duration = "Durata";
            public string CallEvaluationOn5 = "Valutazione chiamata su 5";
            public string PartecipantName = "Nome partecipante";
            public string IPAddress = "Indirizzo IP";
            public string City = "CittÃ";
            public string Nation = "Paese";
            public string ActionCause = "Motivo azione";
            public string ActionDescription = "Descrizione azione";
            public string VisualizedDestinationName = "Nomi visualizzati di destinazione";
            public string DetinationEmailsAddresses = "Email di destinazione";
            public string DestinationPhoneNumber = "Numero telefono di destinazione";
            public string CalendarEventIdentifier = "ID evento Calendar";
            public string ConferenceID = "ID conferenza";

            #endregion Mandatory Data

            #region Optional Data

            public string NETRoundTrip = "Media tempo di round trip rete in ms";
            public string TransportProtocol = "Protocollo di trasporto";
            public string PredictedBandWidthLoading = "Larghezza di banda per il caricamento in kbps prevista";
            public string PredictedBandWidthUploading = "Larghezza di banda per il download in kbps prevista";

            public string MaxReceptionAudioPacketsLost = "max pacchetti ricezione audio";
            public string AverageReceptionAudioPacketsLost = "Perdita media pacchetti ricezione audio";
            public string AudioReceptionDuration = "Durata ricezione audio";
            public string BitRationAudioSending = "Media velocitÃ  in bit invio audio in kbps";
            public string MaxSendingAudioPacketsLost = "Perdita max pacchetti invio audio";
            public string AverageSendingAudioPacketsLost = "Perdita media pacchetti invio audio";
            public string AudioSendingDuration = "Durata invio audio";

            public string AverageReceptionFlickering = "Media tremolio ricezione rete in ms";
            public string MaxReceptionFilckering = "Massimo tremolio ricezione rete in ms";
            public string AverageSendingFlickering = "Media tremolio invio rete in ms";

            public string BitRationScreencastReception = "Media velocitÃ  in bit ricezione screencast in kbps";
            public string AverageScreecastReception = "Media f/s ricezione screencast";
            public string LongSideMedianScreencastReception = "Mediana lato lungo ricezione screencast";
            public string MaxReceptionScreencastPacketsLost = "Perdita max pacchetti ricezione screencast";
            public string AverageReceptionScreencastPacketsLost = "Perdita media pacchetti ricezione screencast";
            public string ScreencastReceptionDuration = "Durata ricezione screencast";
            public string ShortSideMedianScreencastReception = "Mediana lato corto ricezione screencast";
            public string BitRationScreencastSending = "Media velocitÃ  in bit invio screencast in kbps";
            public string AverageScreecastSending = "Media f/s invio screencast";
            public string LongSideMedianScreencastSending = "Mediana lato lungo invio screencast";
            public string MaxSendingScreencastPacketsLost = "Perdita max pacchetti invio screencast";
            public string AverageSendingScreencastPacketsLost = "Perdita media pacchetti invio screencast";
            public string ScreencastSendingDuration = "Durata invio screencast";
            public string ShortSideMedianScreencastSending = "Mediana lato corto invio screencast";

            public string AverageVideoReception = "Media f/s ricezione video";
            public string LongSideMedianVideoReception = "Mediana lato lungo ricezione video";
            public string MaxVideoReceptionPacketsLost = "Perdita max pacchetti ricezione video";
            public string AverageVideoReceptionPacketsLost = "Perdita media pacchetti ricezione video";
            public string ReceptionVideoDuration = "Durata ricezione video";
            public string ShortSideMedianVideoReception = "Mediana lato corto ricezione video";
            public string BitRationVideoSending = "Media velocitÃ  in bit invio video in kbps";
            public string AverageVideoSending = "Media f/s invio video";
            public string LongSideMedianVideoSending = "Mediana lato lungo invio video";
            public string MaxSendingVideoPacketsLost = "Perdita max pacchetti invio video";
            public string AverageSendingVideoPacketsLost = "Perdita media pacchetti invio video";
            public string VideoSenfingDuration = "Durata invio video";
            public string ShortSideMedianVideoSending = "Mediana lato corto invio video";

            public string NetworkCongestion = "Rapporto congestione della rete";

            public string MeetingStartData = "Data inizio riunione";
            public string MeetingEndData = "Data fine riunione";
            public string MeetingEnteringData = "Data ingresso riunione";
            public string TotalMeetingUserPartecipation = "Durata totale di partecipazione alla riunione";

            #endregion

        }
    }
}
