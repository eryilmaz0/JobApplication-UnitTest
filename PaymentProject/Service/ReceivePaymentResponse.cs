namespace PaymentProject.Service;

public class ReceivePaymentResponse
{
    public decimal ReceivedAmount { get; set; }
    public string ResultMessage { get; set; }
    public bool IsSuccess { get; set; }
}