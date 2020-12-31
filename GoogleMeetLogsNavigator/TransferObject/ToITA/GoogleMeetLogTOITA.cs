using CsvHelper.Configuration.Attributes;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.Utility;

namespace GoogleMeetLogsNavigator.TransferObject.ToITA
{
    /// <summary>
    /// Google Meet Log Transfer Object for ITA Language
    /// </summary>
    public class GoogleMeetLogTOITA : IGoogleMeetLogTO
    {
        [Name(Constants.CSVHeader.Date)]
        public string Date { get; set; }
        [Name(Constants.CSVHeader.EventName)]
        public string EventName { get; set; }
        [Name(Constants.CSVHeader.EventDescription)]
        public string EventDescription { get; set; }
        [Name(Constants.CSVHeader.MeetingCode)]
        public string MeetingCode { get; set; }
        [Name(Constants.CSVHeader.PartecipantIdentifier), NullValues(Constants.ConstantsValue.EmptyString)]
        public string PartecipantIdentifier { get; set; }
        [Name(Constants.CSVHeader.ExternalPartecipantIdentifier), NullValues(Constants.ConstantsValue.EmptyString)]
        public string ExternalPartecipantIdentifier { get; set; }
        [Name(Constants.CSVHeader.PartecipantName)]
        public string PartecipantName { get; set; }

        [Name(Constants.CSVHeader.ClientType)]
        public string ClientType { get; set; }
        [Name(Constants.CSVHeader.MeetingOwnerEmail), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string MeetingOwnerEmail { get; set; }
        [Name(Constants.CSVHeader.ProductType), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string ProductType { get; set; }
        [Name(Constants.CSVHeader.Duration)]
        public string Duration { get; set; }
        [Name(Constants.CSVHeader.CallEvaluationOn5), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string CallEvaluationOn5 { get; set; }
        [Name(Constants.CSVHeader.IPAddress), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string IPAddress { get; set; }
        [Name(Constants.CSVHeader.City), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string City { get; set; }
        [Name(Constants.CSVHeader.Nation), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string Nation { get; set; }
        [Name(Constants.CSVHeader.ActionCause), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string ActionCause { get; set; }
        [Name(Constants.CSVHeader.ActionDescription), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string ActionDescription { get; set; }
        [Name(Constants.CSVHeader.VisualizedDestinationNames), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string VisualizedDestinationNames { get; set; }
        [Name(Constants.CSVHeader.DestinationEmailsAddresses), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string DestinationEmailsAddresses { get; set; }
        [Name(Constants.CSVHeader.DestinationPhoneNumber), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string DestinationPhoneNumber { get; set; }
        [Name(Constants.CSVHeader.CalendarEventIdentifier), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string CalendarEventIdentifier { get; set; }
        [Name(Constants.CSVHeader.ConferenceID), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string ConferenceID { get; set; }
       

        [Name(Constants.CSVHeader.NETRoundTrip), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string NETRoundTrip { get; set; }
        [Name(Constants.CSVHeader.TransportProtocol), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string TransportProtocol { get; set; }
        [Name(Constants.CSVHeader.PredictedBandWidthLoading), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string PredictedBandWidthLoading { get; set; }
        [Name(Constants.CSVHeader.PredictedBandWidthUploading), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string PredictedBandWidthUploading { get; set; }

        [Name(Constants.CSVHeader.MaxReceptionAudioPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxReceptionAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageReceptionAudioPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageReceptionAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AudioReceptionDuration), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AudioReceptionDuration { get; set; }
        [Name(Constants.CSVHeader.BitRatioAudioSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioAudioSending { get; set; }
        [Name(Constants.CSVHeader.MaxSendingAudioPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxSendingAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageSendingAudioPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AudioSendingDuration), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AudioSendingDuration { get; set; }
        [Name(Constants.CSVHeader.AverageReceptionFlickering), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageReceptionFlickering { get; set; }
        [Name(Constants.CSVHeader.MaxReceptionFilckering), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxReceptionFilckering { get; set; }
        [Name(Constants.CSVHeader.AverageSendingFlickering), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingFlickering { get; set; }
        [Name(Constants.CSVHeader.BitRatioScreencastReception), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioScreencastReception { get; set; }
        [Name(Constants.CSVHeader.AverageScreecastReception), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageScreecastReception { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianScreencastReception), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianScreencastReception { get; set; }
        [Name(Constants.CSVHeader.MaxReceptionScreencastPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxReceptionScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageReceptionScreencastPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageReceptionScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.ScreencastReceptionDuration), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ScreencastReceptionDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianScreencastReception), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianScreencastReception { get; set; }
        [Name(Constants.CSVHeader.BitRatioScreencastSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioScreencastSending { get; set; }
        [Name(Constants.CSVHeader.AverageScreecastSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageScreecastSending { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianScreencastSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianScreencastSending { get; set; }
        [Name(Constants.CSVHeader.MaxSendingScreencastPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxSendingScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageSendingScreencastPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.ScreencastSendingDuration), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ScreencastSendingDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianScreencastSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianScreencastSending { get; set; }
        [Name(Constants.CSVHeader.AverageVideoReception), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageVideoReception { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianVideoReception), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianVideoReception { get; set; }
        [Name(Constants.CSVHeader.MaxVideoReceptionPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxVideoReceptionPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageVideoReceptionPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageVideoReceptionPacketsLost { get; set; }
        [Name(Constants.CSVHeader.ReceptionVideoDuration), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ReceptionVideoDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianVideoReception), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianVideoReception { get; set; }
        [Name(Constants.CSVHeader.BitRatioVideoSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioVideoSending { get; set; }
        [Name(Constants.CSVHeader.AverageVideoSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageVideoSending { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianVideoSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianVideoSending { get; set; }
        [Name(Constants.CSVHeader.MaxSendingVideoPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxSendingVideoPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageSendingVideoPacketsLost), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingVideoPacketsLost { get; set; }
        [Name(Constants.CSVHeader.VideoSendingDuration), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string VideoSendingDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianVideoSending), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianVideoSending { get; set; }
        [Name(Constants.CSVHeader.NetworkCongestion), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string NetworkCongestion { get; set; }
        [Name(Constants.CSVHeader.MeetingStartDate), Optional, NullValues(Constants.ConstantsValue.MinValue)]
        public string MeetingStartDate { get; set; }
        [Name(Constants.CSVHeader.MeetingEndDate), Optional, NullValues(Constants.ConstantsValue.MinValue)]
        public string MeetingEndDate { get; set; }
        [Name(Constants.CSVHeader.MeetingEnteringDate), Optional, NullValues(Constants.ConstantsValue.MinValue)]
        public string MeetingEnteringDate { get; set; }
        [Name(Constants.CSVHeader.TotalMeetingUserPartecipation), Optional, NullValues(Constants.ConstantsValue.ZeroValue)]
        public string TotalMeetingUserPartecipation { get; set; }
        [Name(Constants.CSVHeader.CommonEuropeanTimeType), Optional, NullValues(Constants.ConstantsValue.EmptyString)]
        public string CommonEuropeanTimeType { get; set; }
    }
}
