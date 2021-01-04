namespace GoogleMeetLogsNavigator.Utility
{
    /// <summary>
    /// Constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Google Meet Csv File HEADER ITA
        /// </summary>
        public class CSVHeaderITA
        {
            #region Mandatory Data

            public const string Date = "Data";
            public const string EventName = "Nome evento";
            public const string EventDescription = "Descrizione evento";
            public const string Duration = "Durata";
            public const string EffectiveMeetingDurationInSeconds = "Durata effettiva del meeting in secondi";
            public const string EffectiveMeetingDurationInMinutes = "Durata effettiva del meeting in minuti";
            public const string EffectiveMeetingDurationInHours = "Durata effettiva del meeting in ore";
            public const string MeetingCode = "Codice riunione";
            public const string PartecipantIdentifier = "Identificatore partecipante";
            public const string PartecipantName = "Nome partecipante";
            public const string ExternalPartecipantIdentifier = "Partecipante esterno all'organizzazione";
            public const string ClientType = "Tipo di client";

            #endregion Mandatory Data

            #region Optional Data


            public const string MeetingOwnerEmail = "Email organizzatore";
            public const string ProductType = "Tipo di prodotto";
            public const string CallEvaluationOn5 = "Valutazione chiamata su 5";
            public const string IPAddress = "Indirizzo IP";
            public const string City = "CittÃ ";
            public const string City2 = "Città";
            public const string Country = "Paese";
            public const string ActionReason = "Motivo azione";
            public const string ActionDescription = "Descrizione azione";
            public const string TargetDisplayNames = "Nomi visualizzati di destinazione";
            public const string TargetEmail = "Email di destinazione";
            public const string TargetPhoneNumber = "Numero telefono di destinazione";
            public const string CalendarEventIdentifier = "ID evento Calendar";
            public const string ConferenceID = "ID conferenza";

            public const string NETRoundTrip = "Media tempo di round trip rete in ms";
            public const string TransportProtocol = "Protocollo di trasporto";
            public const string EstimatedUploadBandwidthInkbps = "Larghezza di banda per il caricamento in kbps prevista";
            public const string EstimatedDownloadBandwidthInkbps = "Larghezza di banda per il download in kbps prevista";
                   
            public const string AudioReceivePacketLossMax = "Perdita max pacchetti ricezione audio";
            public const string AudioReceivePacketLossMean = "Perdita media pacchetti ricezione audio";
            public const string AudioReceiveDuration = "Durata ricezione audio";
            public const string BitRatioAudioSending = "Media velocitÃ  in bit invio audio in kbps";
            public const string BitRatioAudioSending2 = "Media velocità in bit invio audio in kbps";
            public const string AudioSendPacketLossMax = "Perdita max pacchetti invio audio";
            public const string AudioSendPacketLossMean = "Perdita media pacchetti invio audio";
            public const string AudioSendDuration = "Durata invio audio";

            public const string NetworkRecvJitterMeaninms = "Media tremolio ricezione rete in ms";
            public const string NetworkRecvJitterMaxinms = "Massimo tremolio ricezione rete in ms";
            public const string NetworkSendJitterMeaninms = "Media tremolio invio rete in ms";

            public const string BitRatioScreencastReception = "Media velocitÃ  in bit ricezione screencast in kbps";
            public const string BitRatioScreencastReception2 = "Media velocità in bit ricezione screencast in kbps";
            public const string ScreencastReceiveFPSMean = "Media f/s ricezione screencast";
            public const string ScreencastReceiveLongSideMedian = "Mediana lato lungo ricezione screencast";
            public const string ScreencastReceivePacketLossMax = "Perdita max pacchetti ricezione screencast";
            public const string ScreencastReceivePacketLossMean = "Perdita media pacchetti ricezione screencast";
            public const string ScreencastReceiveDuration = "Durata ricezione screencast";
            public const string ScreencastReceiveShortSideMedian = "Mediana lato corto ricezione screencast";
            public const string BitRatioScreencastSending = "Media velocitÃ  in bit invio screencast in kbps";
            public const string BitRatioScreencastSending2 = "Media velocità in bit invio screencast in kbps";
            public const string ScreencastSendFPSMean = "Media f/s invio screencast";
            public const string ScreencastSendLongSideMedian = "Mediana lato lungo invio screencast";
            public const string ScreencastSendPacketLossMax = "Perdita max pacchetti invio screencast";
            public const string ScreencastSendPacketLossMean = "Perdita media pacchetti invio screencast";
            public const string ScreencastSendDuration = "Durata invio screencast";
            public const string ScreencastSendShortSideMedian = "Mediana lato corto invio screencast";
                   
            public const string VideoReceiveFPSMean = "Media f/s ricezione video";
            public const string VideoReceiveLongSideMedian = "Mediana lato lungo ricezione video";
            public const string VideoReceivePacketLossMax = "Perdita max pacchetti ricezione video";
            public const string VideoReceivePacketLossMean = "Perdita media pacchetti ricezione video";

            public const string VideoReceiveDuration = "Durata ricezione video";
            public const string VideoReceiveShortSideMedian = "Mediana lato corto ricezione video";
            public const string BitRatioVideoSending = "Media velocitÃ  in bit invio video in kbps";
            public const string BitRatioVideoSending2 = "Media velocità in bit invio video in kbps";
            public const string VideoSendFPSMean = "Media f/s invio video";
            public const string VideoSendLongSideMedian = "Mediana lato lungo invio video";
            public const string VideoSendPacketLossMax = "Perdita max pacchetti invio video";
            public const string VideoSendPacketLossMean = "Perdita media pacchetti invio video";
            public const string VideoSendDuration = "Durata invio video";
            public const string VideoSendShortSideMedian = "Mediana lato corto invio video";
                    
            public const string NetworkCongestionRatio = "Rapporto congestione della rete";

            public const string MeetingStartDate = "Data inizio meeting";
            public const string EffectiveMeetingStartDate = "Data inizio meeting effettiva";
            public const string MeetingEndDate = "Data fine meeting";
            public const string EffectiveMeetingEndDate = "Data fine meeting effettiva";
            public const string MeetingEnteringDate = "Data di entrata nel meeting";
            public const string TotalMeetingUserPartecipationInDecimal = "Totale tempo partecipazione";
            public const string TotalMeetingUserPartecipationInSeconds = "Totale tempo partecipazione in secondi";
            public const string TotalMeetingUserPartecipationInMinutes = "Totale tempo partecipazione in minuti";
            public const string TotalMeetingUserPartecipationInHours = "Totale tempo partecipazione in ore";
            public const string TimeZone = "Tipo Fuso Orario";

            #endregion
        }


        /// <summary>
        /// Google Meet Csv File HEADER EN
        /// </summary>
        public class CSVHeaderEN
        {
            #region Mandatory Data

            public const string Date = "Date";
            public const string EventName = "Event Name";
            public const string EventDescription = "Event Description";
            public const string MeetingCode = "Meeting Code";
            public const string Duration = "Duration";
            public const string EffectiveMeetingDurationInSeconds = "Effective meeting duration in seconds";
            public const string EffectiveMeetingDurationInMinutes = "Effective meeting duration in minutes";
            public const string EffectiveMeetingDurationInHours = "Effective meeting duration in hours";
            public const string PartecipantIdentifier = "Participant Identifier";
            public const string PartecipantName = "Participant Name";
            public const string ExternalPartecipantIdentifier = "Participant Outside Organisation";
            public const string ClientType = "Client Type";

            #endregion Mandatory Data

            #region Optional Data

            public const string MeetingOwnerEmail = "Organizer Email";
            public const string ProductType = "Product Type,Duration";
            public const string CallEvaluationOn5 = "Call Rating out of 5";
            public const string IPAddress = "IP Address";
            public const string City = "City";
            public const string Country = "Country";
            public const string ActionReason = "Action Reason";
            public const string ActionDescription = "Action Description";
            public const string TargetDisplayNames = "Target Display Names";
            public const string TargetEmail = "Target Email";
            public const string TargetPhoneNumber = "Target Phone Number";
            public const string CalendarEventIdentifier = "Calendar Event Id";
            public const string ConferenceID = "Conference ID";

            public const string NETRoundTrip = "Network Round Trip Time Mean In ms";
            public const string TransportProtocol = "Transport Protocol";
            public const string EstimatedUploadBandwidthInkbps = "Estimated Upload Bandwidth in kbps";
            public const string EstimatedDownloadBandwidthInkbps = "Estimated Download Bandwidth in kbps";

            public const string AudioReceivePacketLossMax = "Audio Receive Packet Loss Max";
            public const string AudioReceivePacketLossMean = "Audio Receive Packet Loss Mean";
            public const string AudioReceiveDuration = "Audio Receive Duration";
            public const string BitRatioAudioSending = "Audio Send Bitrate Mean in kbps";
            public const string BitRatioAudioSending2 = "Media velocità in bit invio audio in kbps";
            public const string AudioSendPacketLossMax = "Audio Send Packet Loss Max";
            public const string AudioSendPacketLossMean = "Audio Send Packet Loss Mean";
            public const string AudioSendDuration = "Audio Send Duration";

            public const string NetworkRecvJitterMeaninms = "Network Recv Jitter Mean in ms";
            public const string NetworkRecvJitterMaxinms = "Network Recv Jitter Max in ms";
            public const string NetworkSendJitterMeaninms = "Network Send Jitter Mean in ms";

            public const string BitRatioScreencastReception = "Screencast Receive Bitrate Mean in kbps";
            public const string ScreencastReceiveFPSMean = "Screencast Receive FPS Mean";
            public const string ScreencastReceiveLongSideMedian = "Screencast Receive Long Side Median";
            public const string ScreencastReceivePacketLossMax = "Screencast Receive Packet Loss Max";
            public const string ScreencastReceivePacketLossMean = "Screencast Receive Packet Loss Mean";
            public const string ScreencastReceiveDuration = "Screencast Receive Duration";
            public const string ScreencastReceiveShortSideMedian = "Screencast Receive Short Side Median";
            public const string BitRatioScreencastSending = "Screencast Send Bitrate Mean in kbps";
            public const string ScreencastSendFPSMean = "Screencast Send FPS Mean";
            public const string ScreencastSendLongSideMedian = "Screencast Send Long Side Median";
            public const string ScreencastSendPacketLossMax = "Screencast Send Packet Loss Max";
            public const string ScreencastSendPacketLossMean = "Screencast Send Packet Loss Mean";
            public const string ScreencastSendDuration = "Screencast Send Duration";
            public const string ScreencastSendShortSideMedian = "Screencast Send Short Side Median";

            public const string VideoReceiveFPSMean = "Video Receive FPS Mean";
            public const string VideoReceiveLongSideMedian = "Video Receive Long Side Median";
            public const string VideoReceivePacketLossMax = "Video Receive Packet Loss Max";
            public const string VideoReceivePacketLossMean = "Video Receive Packet Loss Mean";
            public const string VideoReceiveDuration = "Video Receive Duration";
            public const string VideoReceiveShortSideMedian = "Video Receive Short Side Median";
            public const string BitRatioVideoSending = "Video Send Bitrate Mean in kbps";
            public const string VideoSendFPSMean = "Video Send FPS Mean";
            public const string VideoSendLongSideMedian = "Video Send Long Side Median";
            public const string VideoSendPacketLossMax = "Video Send Packet Loss Max";
            public const string VideoSendPacketLossMean = "Video Send Packet Loss Mean";
            public const string VideoSendDuration = "Video Send Duration";
            public const string VideoSendShortSideMedian = "Video Send Short Side Median";

            public const string NetworkCongestionRatio = "Network Congestion Ratio";

            public const string MeetingStartDate = "Start Meeting Date";
            public const string EffectiveMeetingStartDate = "Data inizio meeting effettiva";
            public const string MeetingEndDate = "End Meeting Date";
            public const string EffectiveMeetingEndDate = "Data fine meeting effettiva";
            public const string MeetingEnteringDate = "Entering Meeting Date";
            public const string TotalMeetingUserPartecipationInDecimal = "Total User Partecipation";
            public const string TotalMeetingUserPartecipationInSeconds = "Total User Partecipation in seconds";
            public const string TotalMeetingUserPartecipationInMinutes = "Total User Partecipation in minutes";
            public const string TotalMeetingUserPartecipationInHours = "TTotal User Partecipation in hours";
            public const string TimeZone = "Time Zone";

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
            public const string CallExitITA = "L'endpoint ha abbandonato la chiamata";

            /// <summary>
            /// 
            /// </summary>
            public const string CallExitEN = "Endpoint left";
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

            /// 
            /// </summary>
            public const string GMT1 = "GMT+1";

            /// <summary>
            /// 
            /// </summary>
            public const string GMT2 = "GMT+2";

            /// <summary>
            /// 
            /// </summary>
            public const string EmptyString = "";
        }

        /// <summary>
        /// Supported Langauges
        /// </summary>
        public static class Langauges
        {
            /// <summary>
            /// Language ITA
            /// </summary>
            public const string ITA = "it";


            /// <summary>
            /// Language EN
            /// </summary>
            public const string EN = "en";
        }
    }
}
