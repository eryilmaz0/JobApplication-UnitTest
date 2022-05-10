using PaymentProject.Service;

namespace PaymentProject.Banks;

public class AkbankProxy : IBankProxy
{
    public ReceivePaymentResponse ReceivePayment(ReceivePaymentRequest request)
    {
        return new()
        {
            IsSuccess = true,
            ReceivedAmount = request.Amount,
            ResultMessage = "Akbank Payment Received."
        };
    }
}