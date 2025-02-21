namespace DNSLab.Web.Enums
{
    public enum TransactionStatusEnum
    {
        AwaitingPayment = -1,
        InternalError = -2,
        PaidConfirmed = 1,
        PaidUnverified = 2,
        CanceledByUser = 3,
        TheCardNumberIsInvalid = 4,
        TheAccountBalanceIsInsufficient = 5,
        TheEnteredPasswordIsWrong = 6,
        TheNumberOfRequestsIsOverTheLimit = 7,
        TheNumberOfOnlinePaymentsPerDayIsMoreThanTheAllowedLimit = 8,
        TheAmountOfDailyInternetPaymentIsMoreThanTheAllowedLimit = 9,
        TheIssuerOfTheCardIsInvalid = 10,
        SwitchError = 11,
        TheCardIsNotAccessible = 12
    }
}
