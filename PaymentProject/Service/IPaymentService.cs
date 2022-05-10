namespace PaymentProject.Service;

public interface IPaymentService
{
    public ReceivePaymentResponse ReceivePayment(ReceivePaymentRequest request);
}