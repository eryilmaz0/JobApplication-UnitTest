using PaymentProject.Service;

namespace PaymentProject.Banks;

public class HalkbankProxy : IBankProxy
{
    public ReceivePaymentResponse ReceivePayment(ReceivePaymentRequest request)
    {
        return new()
        {
            IsSuccess = true,
            ReceivedAmount = request.Amount,
            ResultMessage = "Halkbank Payment Received."
        };
    }
}