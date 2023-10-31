

namespace DataAccessLayer.Enums
{
    public enum PaymentResponse
    {
        OK,
        INSUFFICIENT_BALANCE,
        EXPIRED_CARD,
        INCORRECT_VERIFICATION_CODE,
        TIMEOUT
    }
}
