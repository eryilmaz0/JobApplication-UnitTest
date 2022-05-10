using PaymentProject.Banks;
using PaymentProject.Enum;

namespace PaymentProject.BankFactory;

public class BankFactory : IBankFactory
{
    public IBankProxy GetBank(BankType bankType)
    {
        return bankType switch
        {
            BankType.Akbank => new AkbankProxy(),
            BankType.Denizbank => new DenizbankProxy(),
            BankType.Halkbank => new HalkbankProxy(),
            BankType.Ziraat => new ZiraatBankProxy()
        };
    }
}