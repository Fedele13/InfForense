namespace GoogleMeetLogsNavigator.GoogleParser.GoogleEnum
{
    /// <summary>
    /// Treated CSV Hedaer
    /// </summary>
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
        EffectiveMeetingDurationInHours,
        EffectiveMeetingDurationInSeconds,
        EffectiveMeetingDurationInMinutes,
        ClientType,
        #endregion

        MeetingOwnerEmail,
        ProductType,
        CallEvaluationOn5,
        IPAddress,
        City,
        Country,
        ActionReason,
        ActionDescription,
        TargetDisplayNames,
        TargetEmail,
        TargetPhoneNumber,
        CalendarEventIdentifier,
        ConferenceID,
        
        NETRoundTrip,
        TransportProtocol,
        EstimatedUploadBandwidthInkbps,
        EstimatedDownloadBandwidthInkbps,
        
        AudioReceivePacketLossMax,
        AudioReceivePacketLossMean,
        AudioReceiveDuration,
        BitRatioAudioSending,
        AudioSendPacketLossMax,
        AudioSendPacketLossMean,
        AudioSendDuration,
        
        NetworkRecvJitterMeaninms,
        NetworkRecvJitterMaxinms,
        NetworkSendJitterMeaninms,
        
        BitRatioScreencastReception,
        ScreencastReceiveFPSMean,
        ScreencastReceiveLongSideMedian,
        ScreencastReceivePacketLossMax,
        ScreencastReceivePacketLossMean,
        ScreencastReceiveDuration,
        ScreencastReceiveShortSideMedian,
        BitRatioScreencastSending,
        ScreencastSendFPSMean,
        ScreencastSendLongSideMedian,
        ScreencastSendPacketLossMax,
        ScreencastSendPacketLossMean,
        ScreencastSendDuration,
        ScreencastSendShortSideMedian,
        
        VideoReceiveFPSMean,
        VideoReceiveLongSideMedian,
        VideoReceivePacketLossMax,
        VideoReceivePacketLossMean,
        VideoReceiveDuration,
        VideoReceiveShortSideMedian,
        BitRatioVideoSending,
        VideoSendFPSMean,
        VideoSendLongSideMedian,
        VideoSendPacketLossMax,
        VideoSendPacketLossMean,
        VideoSendDuration,
        VideoSendShortSideMedian,
        
        NetworkCongestionRatio,

        MeetingStartDate,
        EffectiveMeetingStartDate,
        MeetingEndDate,
        EffectiveMeetingEndDate,
        MeetingEnteringDate,
        TotalMeetingUserPartecipationInDecimal,
        TotalMeetingUserPartecipationInSeconds,
        TotalMeetingUserPartecipationInMinutes,
        TotalMeetingUserPartecipationInHours,
        TimeZone
    }
}
