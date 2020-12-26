using CsvHelper.Configuration.Attributes;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.Utility;

namespace GoogleMeetLogsNavigator.TransferObject.ToITA
{
    /// <summary>
    /// Google Meet Log Transfer Object
    /// </summary>
    public class GoogleMeetLogTOITA : IGoogleMeetLogTO
    {
        #region Mandatory Data

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

        #endregion Mandatory Data

        #region Optional Data

        [Name(Constants.CSVHeader.ClientType)]
        public string ClientType { get; set; }
        [Name(Constants.CSVHeader.MeetingOwnerEmail), NullValues(Constants.ConstantsValue.EmptyString)]
        public string MeetingOwnerEmail { get; set; }
        [Name(Constants.CSVHeader.ProductType), NullValues(Constants.ConstantsValue.EmptyString)]
        public string ProductType { get; set; }
        [Name(Constants.CSVHeader.Duration)]
        public string Duration { get; set; }
        [Name(Constants.CSVHeader.CallEvaluationOn5), NullValues(Constants.ConstantsValue.EmptyString)]
        public string CallEvaluationOn5 { get; set; }
        [Name(Constants.CSVHeader.IPAddress), NullValues(Constants.ConstantsValue.EmptyString)]
        public string IPAddress { get; set; }
        [Name(Constants.CSVHeader.City), NullValues(Constants.ConstantsValue.EmptyString)]
        public string City { get; set; }
        [Name(Constants.CSVHeader.Nation), NullValues(Constants.ConstantsValue.EmptyString)]
        public string Nation { get; set; }
        [Name(Constants.CSVHeader.ActionCause), NullValues(Constants.ConstantsValue.EmptyString)]
        public string ActionCause { get; set; }
        [Name(Constants.CSVHeader.ActionDescription), NullValues(Constants.ConstantsValue.EmptyString)]
        public string ActionDescription { get; set; }
        [Name(Constants.CSVHeader.VisualizedDestinationNames), NullValues(Constants.ConstantsValue.EmptyString)]
        public string VisualizedDestinationNames { get; set; }
        [Name(Constants.CSVHeader.DestinationEmailsAddresses), NullValues(Constants.ConstantsValue.EmptyString)]
        public string DestinationEmailsAddresses { get; set; }
        [Name(Constants.CSVHeader.DestinationPhoneNumber), NullValues(Constants.ConstantsValue.EmptyString)]
        public string DestinationPhoneNumber { get; set; }
        [Name(Constants.CSVHeader.CalendarEventIdentifier), NullValues(Constants.ConstantsValue.EmptyString)]
        public string CalendarEventIdentifier { get; set; }
        [Name(Constants.CSVHeader.ConferenceID), NullValues(Constants.ConstantsValue.EmptyString)]
        public string ConferenceID { get; set; }
       

        [Name(Constants.CSVHeader.NETRoundTrip), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string NETRoundTrip { get; set; }
        [Name(Constants.CSVHeader.TransportProtocol), NullValues(Constants.ConstantsValue.EmptyString)]
        public string TransportProtocol { get; set; }
        [Name(Constants.CSVHeader.PredictedBandWidthLoading), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string PredictedBandWidthLoading { get; set; }
        [Name(Constants.CSVHeader.PredictedBandWidthUploading), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string PredictedBandWidthUploading { get; set; }

        [Name(Constants.CSVHeader.MaxReceptionAudioPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxReceptionAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageReceptionAudioPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageReceptionAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AudioReceptionDuration), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AudioReceptionDuration { get; set; }
        [Name(Constants.CSVHeader.BitRatioAudioSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioAudioSending { get; set; }
        [Name(Constants.CSVHeader.MaxSendingAudioPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxSendingAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageSendingAudioPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingAudioPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AudioSendingDuration), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AudioSendingDuration { get; set; }
        [Name(Constants.CSVHeader.AverageReceptionFlickering), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageReceptionFlickering { get; set; }
        [Name(Constants.CSVHeader.MaxReceptionFilckering), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxReceptionFilckering { get; set; }
        [Name(Constants.CSVHeader.AverageSendingFlickering), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingFlickering { get; set; }
        [Name(Constants.CSVHeader.BitRatioScreencastReception), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioScreencastReception { get; set; }
        [Name(Constants.CSVHeader.AverageScreecastReception), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageScreecastReception { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianScreencastReception), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianScreencastReception { get; set; }
        [Name(Constants.CSVHeader.MaxReceptionScreencastPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxReceptionScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageReceptionScreencastPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageReceptionScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.ScreencastReceptionDuration), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ScreencastReceptionDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianScreencastReception), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianScreencastReception { get; set; }
        [Name(Constants.CSVHeader.BitRatioScreencastSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioScreencastSending { get; set; }
        [Name(Constants.CSVHeader.AverageScreecastSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageScreecastSending { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianScreencastSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianScreencastSending { get; set; }
        [Name(Constants.CSVHeader.MaxSendingScreencastPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxSendingScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageSendingScreencastPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingScreencastPacketsLost { get; set; }
        [Name(Constants.CSVHeader.ScreencastSendingDuration), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ScreencastSendingDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianScreencastSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianScreencastSending { get; set; }
        [Name(Constants.CSVHeader.AverageVideoReception), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageVideoReception { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianVideoReception), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianVideoReception { get; set; }
        [Name(Constants.CSVHeader.MaxVideoReceptionPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxVideoReceptionPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageVideoReceptionPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageVideoReceptionPacketsLost { get; set; }
        [Name(Constants.CSVHeader.ReceptionVideoDuration), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ReceptionVideoDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianVideoReception), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianVideoReception { get; set; }
        [Name(Constants.CSVHeader.BitRatioVideoSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string BitRatioVideoSending { get; set; }
        [Name(Constants.CSVHeader.AverageVideoSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageVideoSending { get; set; }
        [Name(Constants.CSVHeader.LongSideMedianVideoSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string LongSideMedianVideoSending { get; set; }
        [Name(Constants.CSVHeader.MaxSendingVideoPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string MaxSendingVideoPacketsLost { get; set; }
        [Name(Constants.CSVHeader.AverageSendingVideoPacketsLost), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string AverageSendingVideoPacketsLost { get; set; }
        [Name(Constants.CSVHeader.VideoSendingDuration), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string VideoSendingDuration { get; set; }
        [Name(Constants.CSVHeader.ShortSideMedianVideoSending), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string ShortSideMedianVideoSending { get; set; }
        [Name(Constants.CSVHeader.NetworkCongestion), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string NetworkCongestion { get; set; }
        [Name(Constants.CSVHeader.MeetingStartDate), NullValues(Constants.ConstantsValue.MinValue)]
        public string MeetingStartDate { get; set; }
        [Name(Constants.CSVHeader.MeetingEndDate), NullValues(Constants.ConstantsValue.MinValue)]
        public string MeetingEndDate { get; set; }
        [Name(Constants.CSVHeader.MeetingEnteringDate), NullValues(Constants.ConstantsValue.MinValue)]
        public string MeetingEnteringDate { get; set; }
        [Name(Constants.CSVHeader.TotalMeetingUserPartecipation), NullValues(Constants.ConstantsValue.ZeroValue)]
        public string TotalMeetingUserPartecipation { get; set; }
        [Name(Constants.CSVHeader.CommonEuropeanTimeType), NullValues(Constants.ConstantsValue.EmptyString)]
        public string CommonEuropeanTimeType { get; set; }

        #endregion
    }
}
