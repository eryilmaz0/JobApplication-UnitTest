using PaymentProject.Banks;
using PaymentProject.Enum;

namespace PaymentProject.BankFactory;

public interface IBankFactory
{
    public IBankProxy GetBank(BankType bankType);
}