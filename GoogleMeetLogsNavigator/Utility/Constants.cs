namespace GoogleMeetLogsNavigator.Utility
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

            public const string Date = "Data";
            public const string EventName = "Nome evento";
            public const string EventDescription = "Descrizione evento";
            public const string MeetingCode = "Codice riunione";
            public const string PartecipantIdentifier = "Identificatore partecipante";
            public const string PartecipantName = "Nome partecipante";

            #endregion Mandatory Data

            #region Optional Data
            
            public const string ExternalPartecipantIdentifier = "Partecipante esterno all'organizzazione";
            public const string ClientType = "Tipo di client";
            public const string MeetingOwnerEmail = "Email organizzatore";
            public const string ProductType = "Tipo di prodotto";
            public const string Duration = "Durata";
            public const string CallEvaluationOn5 = "Valutazione chiamata su 5";
            public const string IPAddress = "Indirizzo IP";
            public const string City = "Città";
            public const string Nation = "Paese";
            public const string ActionCause = "Motivo azione";
            public const string ActionDescription = "Descrizione azione";
            public const string VisualizedDestinationNames = "Nomi visualizzati di destinazione";
            public const string DestinationEmailsAddresses = "Email di destinazione";
            public const string DestinationPhoneNumber = "Numero telefono di destinazione";
            public const string CalendarEventIdentifier = "ID evento Calendar";
            public const string ConferenceID = "ID conferenza";

            public const string NETRoundTrip = "Media tempo di round trip rete in ms";
            public const string TransportProtocol = "Protocollo di trasporto";
            public const string PredictedBandWidthLoading = "Larghezza di banda per il caricamento in kbps prevista";
            public const string PredictedBandWidthUploading = "Larghezza di banda per il download in kbps prevista";
                   
            public const string MaxReceptionAudioPacketsLost = "Perdita max pacchetti ricezione audio";
            public const string AverageReceptionAudioPacketsLost = "Perdita media pacchetti ricezione audio";
            public const string AudioReceptionDuration = "Durata ricezione audio";
            public const string BitRatioAudioSending = "Media velocità in bit invio audio in kbps";
            public const string MaxSendingAudioPacketsLost = "Perdita max pacchetti invio audio";
            public const string AverageSendingAudioPacketsLost = "Perdita media pacchetti invio audio";
            public const string AudioSendingDuration = "Durata invio audio";

            public const string AverageReceptionFlickering = "Media tremolio ricezione rete in ms";
            public const string MaxReceptionFilckering = "Massimo tremolio ricezione rete in ms";
            public const string AverageSendingFlickering = "Media tremolio invio rete in ms";

            public const string BitRatioScreencastReception = "Media velocità in bit ricezione screencast in kbps";
            public const string AverageScreecastReception = "Media f/s ricezione screencast";
            public const string LongSideMedianScreencastReception = "Mediana lato lungo ricezione screencast";
            public const string MaxReceptionScreencastPacketsLost = "Perdita max pacchetti ricezione screencast";
            public const string AverageReceptionScreencastPacketsLost = "Perdita media pacchetti ricezione screencast";
            public const string ScreencastReceptionDuration = "Durata ricezione screencast";
            public const string ShortSideMedianScreencastReception = "Mediana lato corto ricezione screencast";
            public const string BitRatioScreencastSending = "Media velocità in bit invio screencast in kbps";
            public const string AverageScreecastSending = "Media f/s invio screencast";
            public const string LongSideMedianScreencastSending = "Mediana lato lungo invio screencast";
            public const string MaxSendingScreencastPacketsLost = "Perdita max pacchetti invio screencast";
            public const string AverageSendingScreencastPacketsLost = "Perdita media pacchetti invio screencast";
            public const string ScreencastSendingDuration = "Durata invio screencast";
            public const string ShortSideMedianScreencastSending = "Mediana lato corto invio screencast";
                   
            public const string AverageVideoReception = "Media f/s ricezione video";
            public const string LongSideMedianVideoReception = "Mediana lato lungo ricezione video";
            public const string MaxVideoReceptionPacketsLost = "Perdita max pacchetti ricezione video";
            public const string AverageVideoReceptionPacketsLost = "Perdita media pacchetti ricezione video";
            public const string ReceptionVideoDuration = "Durata ricezione video";
            public const string ShortSideMedianVideoReception = "Mediana lato corto ricezione video";
            public const string BitRatioVideoSending = "Media velocità in bit invio video in kbps";
            public const string AverageVideoSending = "Media f/s invio video";
            public const string LongSideMedianVideoSending = "Mediana lato lungo invio video";
            public const string MaxSendingVideoPacketsLost = "Perdita max pacchetti invio video";
            public const string AverageSendingVideoPacketsLost = "Perdita media pacchetti invio video";
            public const string VideoSendingDuration = "Durata invio video";
            public const string ShortSideMedianVideoSending = "Mediana lato corto invio video";
                    
            public const string NetworkCongestion = "Rapporto congestione della rete";

            public const string MeetingStartDate = "Data inizio meeting";
            public const string MeetingEndDate = "Data fine meeting";
            public const string MeetingEnteringDate = "Data di entrata nel meeting";
            public const string TotalMeetingUserPartecipation = "Totale tempo partecipazione in secondi";
            public const string CommonEuropeanTimeType = "Tipo Fuso Orario";

            #endregion
        }

        /// <summary>
        /// Google Meet events to analyze
        /// </summary>
        public static class EventsToConsider
        {
            /// <summary>
            /// 
            /// </summary>
            public const string CallExit = "L'endpoint ha abbandonato la chiamata";
        }

        /// <summary>
        /// 
        /// </summary>
        public static class ConstantsValue
        {
            /// <summary>
            /// 
            /// </summary>
            public const string MinValue = "1 gen 1900, 00:00:00";

            /// <summary>
            /// 
            /// </summary>
            public const string ZeroValue = "0";

            /// <summary>
            /// 
            /// </summary>
            public const string CEST = "CEST";

            /// <summary>
            /// 
            /// </summary>
            public const string CET = "CET";

            /// <summary>
            /// 
            /// </summary>
            public const string EmptyString = "";
        }
    }
}
