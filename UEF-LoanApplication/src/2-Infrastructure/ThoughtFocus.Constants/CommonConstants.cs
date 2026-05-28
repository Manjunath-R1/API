namespace ThoughtFocus.Constants
{
    public static class CommonConstants
    {
        #region Fields

        public const string UIExceptionPrefixText = "UIEx";
        public const string THRESHOLD_REQUEST_FLAG = "ThresholdRequestAmount";
        public static long ThresholdRequestAmount = 0;        
        //TO make how daye before need to enabled
        public const int NotificationEnabledDays = 1;
        public const int DisbursedEnabledDays = 0;
        public const string ProgressReportFLag = "Progress Report";
        public const string AccountDisbursedFLag = "Fund Disbursed";
        public const string FinalDisbursementFLag = "Final Disbursement";
        public static bool IsPaymentSchedule = false;

        public const string InvitedReportFlag = "INVITED";
        public const string ActiveAccountReportFlag = "ACTIVATEDACCOUNT";
        public const string SubmittedReportFlag = "SUBMITTED";
        public const string FundedReportFlag = "FUNDED";
        public const string FundReleasedReportFlag = "FUNDRELEASED";
        public const string StartedReportFlag = "STARTED";

        public const string FundAllocationFlag = "FUNDALLOCATION";
        public const string ApplicationStatsFlag = "APLLICATIONSTATS";
        public const string ProgramWiseFundAllocationFlag = "PROGRAMWISEFUNDALLOCATION";
        public const string ProgramWiseApplicationStatsFlag = "PROGRAMWISEAPLLICATIONSTATS";
        public const string AffiliateWiseFundUtilizedFlag = "AFFILIATEWISEFUNDUTILIZED";
        public const string AffiliateWiseApplicationStatsFlag = "AFFILIATEWISEAPPLICATIONSTATS";

        public const string UtilizedFlag = "UTILIZEDAMOUNT";
        public const string AvailableLimitFlag = "AVAILABLELIMIT";

        public const string NoActionFlag = "NOACTION";
        public const string StartedFlag = "STARTED";
        public const string InprogressFlag = "INPROGRESS";
        public const string FundedFlag = "FUNDED";
        #endregion Fields
    }
}