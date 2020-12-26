﻿namespace GoogleMeetLogsNavigator.GoogleParser.GoogleEnum
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
        Duration,

        #endregion

        ClientType,
        MeetingOwnerEmail,
        ProductType,
        CallEvaluationOn5,
        IPAddress,
        City,
        Nation,
        ActionCause,
        ActionDescription,
        VisualizedDestinationNames,
        DestinationEmailsAddresses,
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
        BitRatioAudioSending,
        MaxSendingAudioPacketsLost,
        AverageSendingAudioPacketsLost,
        AudioSendingDuration,
        
        AverageReceptionFlickering,
        MaxReceptionFilckering,
        AverageSendingFlickering,
        
        BitRatioScreencastReception,
        AverageScreecastReception,
        LongSideMedianScreencastReception,
        MaxReceptionScreencastPacketsLost,
        AverageReceptionScreencastPacketsLost,
        ScreencastReceptionDuration,
        ShortSideMedianScreencastReception,
        BitRatioScreencastSending,
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
        BitRatioVideoSending,
        AverageVideoSending,
        LongSideMedianVideoSending,
        MaxSendingVideoPacketsLost,
        AverageSendingVideoPacketsLost,
        VideoSendingDuration,
        ShortSideMedianVideoSending,
        
        NetworkCongestion,

        MeetingStartDate,
        MeetingEndDate,
        MeetingEnteringDate,
        TotalMeetingUserPartecipation,
        CommonEuropeanTimeType

    }
}
