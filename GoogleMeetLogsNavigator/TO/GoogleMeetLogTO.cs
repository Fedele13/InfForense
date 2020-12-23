using CsvHelper.Configuration.Attributes;

namespace GoogleMeetLogsNavigator.TO
{
    /// <summary>
    /// Google Meet Log Transfer Object
    /// </summary>
    public class GoogleMeetLogTO
    {
        #region Mandatory Data

        [Index(0)]
        public string Date { get; set; }
        [Index(1)]
        public string EventName { get; set; }
        [Index(2)]
        public string EventDescription { get; set; }
        [Index(3)]
        public string MeetingCode { get; set; }
        [Index(4),NullValues("-")]
        public string PartecipantIdentifier { get; set; }
        [Index(5), NullValues("-")]
        public string ExternalPartecipantIdentifier { get; set; }
        [Index(6)]
        public string ClientType { get; set; }
        [Index(7), NullValues("-")]
        public string MeetingOwnerEmail { get; set; }
        [Index(8), NullValues("-")]
        public string ProductType { get; set; }
        [Index(9)]
        public string Duration { get; set; }
        [Index(10), NullValues("-")]
        public string CallEvaluationOn5 { get; set; }
        [Index(11)]
        public string PartecipantName { get; set; }
        [Index(12), NullValues("-")]
        public string IPAddress { get; set; }
        [Index(13), NullValues("-")]
        public string City { get; set; }
        [Index(14), NullValues("-")]
        public string Nation { get; set; }
        [Index(59), NullValues("-")]
        public string ActionCause { get; set; }
        [Index(60), NullValues("-")]
        public string ActionDescription { get; set; }
        [Index(61), NullValues("-")]
        public string VisualizedDestinationName { get; set; }
        [Index(62), NullValues("-")]
        public string DetinationEmailsAddresses { get; set; }
        [Index(63), NullValues("-")]
        public string DestinationPhoneNumber { get; set; }
        [Index(26), NullValues("-")]
        public string CalendarEventIdentifier { get; set; }
        [Index(27), NullValues("-")]
        public string ConferenceID { get; set; }

        #endregion Mandatory Data

        #region Optional Data

        [Index(15), NullValues("0")]
        public string NETRoundTrip { get; set; }
        [Index(16), NullValues("-")]
        public string TransportProtocol { get; set; }
        [Index(17), NullValues("0")]
        public string PredictedBandWidthLoading { get; set; }
        [Index(18), NullValues("0")]
        public string PredictedBandWidthUploading { get; set; }

        [Index(19), NullValues("0")]
        public string MaxReceptionAudioPacketsLost { get; set; }
        [Index(20), NullValues("0")]
        public string AverageReceptionAudioPacketsLost { get; set; }
        [Index(21), NullValues("0")]
        public string AudioReceptionDuration { get; set; }
        [Index(22), NullValues("0")]
        public string BitRationAudioSending { get; set; }
        [Index(23), NullValues("0")]
        public string MaxSendingAudioPacketsLost { get; set; }
        [Index(24), NullValues("0")]
        public string AverageSendingAudioPacketsLost { get; set; }
        [Index(25), NullValues("0")]
        public string AudioSendingDuration { get; set; }
        [Index(28), NullValues("0")]
        public string AverageReceptionFlickering { get; set; }
        [Index(29), NullValues("0")]
        public string MaxReceptionFilckering { get; set; }
        [Index(30), NullValues("0")]
        public string AverageSendingFlickering { get; set; }
        [Index(31), NullValues("0")]
        public string BitRationScreencastReception { get; set; }
        [Index(32), NullValues("0")]
        public string AverageScreecastReception { get; set; }
        [Index(33), NullValues("0")]
        public string LongSideMedianScreencastReception { get; set; }
        [Index(34), NullValues("0")]
        public string MaxReceptionScreencastPacketsLost { get; set; }
        [Index(35), NullValues("0")]
        public string AverageReceptionScreencastPacketsLost { get; set; }
        [Index(36), NullValues("0")]
        public string ScreencastReceptionDuration { get; set; }
        [Index(37), NullValues("0")]
        public string ShortSideMedianScreencastReception { get; set; }
        [Index(38), NullValues("0")]
        public string BitRationScreencastSending { get; set; }
        [Index(39), NullValues("0")]
        public string AverageScreecastSending { get; set; }
        [Index(40), NullValues("0")]
        public string LongSideMedianScreencastSending { get; set; }
        [Index(41), NullValues("0")]
        public string MaxSendingScreencastPacketsLost { get; set; }
        [Index(42), NullValues("0")]
        public string AverageSendingScreencastPacketsLost { get; set; }
        [Index(43), NullValues("0")]
        public string ScreencastSendingDuration { get; set; }
        [Index(44), NullValues("0")]
        public string ShortSideMedianScreencastSending { get; set; }
        [Index(45), NullValues("0")]
        public string AverageVideoReception { get; set; }
        [Index(46), NullValues("0")]
        public string LongSideMedianVideoReception { get; set; }
        [Index(47), NullValues("0")]
        public string MaxVideoReceptionPacketsLost { get; set; }
        [Index(48), NullValues("0")]
        public string AverageVideoReceptionPacketsLost { get; set; }
        [Index(49), NullValues("0")]
        public string ReceptionVideoDuration { get; set; }
        [Index(50), NullValues("0")]
        public string ShortSideMedianVideoReception { get; set; }
        [Index(52), NullValues("0")]
        public string BitRationVideoSending { get; set; }
        [Index(53), NullValues("0")]
        public string AverageVideoSending { get; set; }
        [Index(54), NullValues("0")]
        public string LongSideMedianVideoSending { get; set; }
        [Index(55), NullValues("0")]
        public string MaxSendingVideoPacketsLost { get; set; }
        [Index(56), NullValues("0")]
        public string AverageSendingVideoPacketsLost { get; set; }
        [Index(57), NullValues("0")]
        public string VideoSenfingDuration { get; set; }
        [Index(58), NullValues("0")]
        public string ShortSideMedianVideoSending { get; set; }
        [Index(51), NullValues("0")]
        public string NetworkCongestion { get; set; }

        #endregion
    }
}
