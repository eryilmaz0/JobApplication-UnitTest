using PaymentProject.Service;

namespace PaymentProject.Banks;

public class DenizbankProxy : IBankProxy
{
    public ReceivePaymentResponse ReceivePayment(ReceivePaymentRequest request)
    {
        return new()
        {
            IsSuccess = true,
            ReceivedAmount = request.Amount,
            ResultMessage = "Denizbank Payment Received."
        };
    }
}