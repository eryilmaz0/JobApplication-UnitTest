using PaymentProject.Service;

namespace PaymentProject.Banks;

public interface IBankProxy
{
    public ReceivePaymentResponse ReceivePayment(ReceivePaymentRequest request);
}