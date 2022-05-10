using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PaymentProject.BankFactory;
using PaymentProject.Banks;
using PaymentProject.Enum;
using PaymentProject.Models;
using PaymentProject.Service;

namespace PaymentProject.UnitTest;

public class PaymentServiceTests
{
    [Test]
    public void Test1()
    {
        var bankFactory = new Mock<IBankFactory>();
        var bankProxy = new Mock<IBankProxy>();

        User user = new()
        {
            Id = Guid.NewGuid(),
            Name = "Eren",
            Email = "eryilmaz0@hotmail.com",
            Password = "123456"
        };

        ReceivePaymentRequest receivePaymentRequest = new()
        {
            User = user,
            BankType = BankType.Akbank,
            Amount = 15000.00M
        };

        ReceivePaymentResponse receivePaymentResponse = new()
        {
            ReceivedAmount = 15000.00M,
            IsSuccess = true,
            ResultMessage = "Payment success."
        };

        bankProxy.Setup(x => x.ReceivePayment(receivePaymentRequest)).Returns(receivePaymentResponse);
        bankFactory.Setup(x => x.GetBank(It.IsAny<BankType>())).Returns(bankProxy.Object);
        
        //Act
        IPaymentService paymentService = new PaymentService(bankFactory.Object);
        var result = paymentService.ReceivePayment(receivePaymentRequest);
        
        //Assert
        
        bankFactory.Verify(x => x.GetBank(It.IsAny<BankType>()), Times.Once);
        bankProxy.Verify(x => x.ReceivePayment(receivePaymentRequest), Times.Once);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.ReceivedAmount.Should().Be(15000.00M);
        result.ResultMessage.Should().NotBeNullOrEmpty();
        result.ResultMessage.Should().Contain("success");
    }
}