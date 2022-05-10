using PaymentProject.Enum;
using PaymentProject.Models;

namespace PaymentProject.Service;

public class ReceivePaymentRequest
{
    public User User { get; set; }
    public decimal Amount { get; set; }
    public BankType BankType { get; set; }
}