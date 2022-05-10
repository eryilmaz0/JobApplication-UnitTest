using PaymentProject.Service;

namespace PaymentProject.Banks;

public class ZiraatBankProxy : IBankProxy
{
    public ReceivePaymentResponse ReceivePayment(ReceivePaymentRequest request)
    {
        return new()
        {
            IsSuccess = true,
            ReceivedAmount = request.Amount,
            ResultMessage = "Ziraat Payment Received."
        };
    }
}