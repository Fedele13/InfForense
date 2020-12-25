namespace GoogleMeetLogsNavigator.GoogleParser
{
    public enum CSVHeaderEnum : int
    {
        #region MANDATORY DATA
        
        Date,
        EventName,
        EventDescription,
        MeetingCode,
        PartecipantIdentifier,
        ExternalPartecipantIdentifier,
        PartecipantName,

        #endregion

        ClientType,
        MeetingOwnerEmail,
        ProductType,
        Duration,
        CallEvaluationOn5,
        IPAddress,
        City,
        Nation,
        ActionCause,
        ActionDescription,
        VisualizedDestinationName,
        DetinationEmailsAddresses,
        DestinationPhoneNumber,
        CalendarEventIdentifier,
        ConferenceID,
        
        NETRoundTrip,
        TransportProtocol,
        PredictedBandWidthLoading,
        PredictedBandWidthUploading,
        
        MaxReceptionAudioPacketsLost,
        AverageReceptionAudioPacketsLost,
        AudioReceptionDuration,
        BitRationAudioSending,
        MaxSendingAudioPacketsLost,
        AverageSendingAudioPacketsLost,
        AudioSendingDuration,
        
        AverageReceptionFlickering,
        MaxReceptionFilckering,
        AverageSendingFlickering,
        
        BitRationScreencastReception,
        AverageScreecastReception,
        LongSideMedianScreencastReception,
        MaxReceptionScreencastPacketsLost,
        AverageReceptionScreencastPacketsLost,
        ScreencastReceptionDuration,
        ShortSideMedianScreencastReception,
        BitRationScreencastSending,
        AverageScreecastSending,
        LongSideMedianScreencastSending,
        MaxSendingScreencastPacketsLost,
        AverageSendingScreencastPacketsLost,
        ScreencastSendingDuration,
        ShortSideMedianScreencastSending,
        
        AverageVideoReception,
        LongSideMedianVideoReception,
        MaxVideoReceptionPacketsLost,
        AverageVideoReceptionPacketsLost,
        ReceptionVideoDuration,
        ShortSideMedianVideoReception,
        BitRationVideoSending,
        AverageVideoSending,
        LongSideMedianVideoSending,
        MaxSendingVideoPacketsLost,
        AverageSendingVideoPacketsLost,
        VideoSenfingDuration,
        ShortSideMedianVideoSending,
        
        NetworkCongestion
    }
}
