using PaymentProject.BankFactory;
using PaymentProject.Banks;

namespace PaymentProject.Service;

public class PaymentService : IPaymentService
{
    private readonly IBankFactory _bankFactory;

    public PaymentService(IBankFactory bankFactory)
    {
        _bankFactory = bankFactory;
    }

    public ReceivePaymentResponse ReceivePayment(ReceivePaymentRequest request)
    {
        IBankProxy bankProxy = _bankFactory.GetBank(request.BankType);
        var paymentResult = bankProxy.ReceivePayment(request);
        return paymentResult;
    }
}