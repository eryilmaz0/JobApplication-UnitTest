using System;
using FluentAssertions;
using JobApplicationProject.Models;
using JobApplicationProject.Services;
using Moq;
using NUnit.Framework;

namespace JobApplicationProject.UnitTest;

public class ApplicationEvaluateTests
{
    [Test]
    public void ApplicationEvaluate_WithUnderAge_ShouldReturnAutoRejected()
    {
        //Arrange
        var identityValidator = new Mock<IIdentityValidator>();
        identityValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var evaluator = new ApplicationEvaluator(identityValidator.Object);
        var jobApplication = new JobApplication()
        {
            Applicant = new()
            {
                Age = 17,
                IdentityNumber = "123456789"
            },
            YearsOfExperience = 1,
            TechStackList = new()
        };
        
        //Action
        var evaluateResult = evaluator.Evaluate(jobApplication);
        
        //Assert
        // Assert.AreEqual(ApplicationResult.AutoRejected, evaluateResult);
        evaluateResult.Should().Be(ApplicationResult.AutoRejected);
    }
    
    
    [Test]
    public void ApplicationEvaluate_WithInvalidIdentityNumber_ShouldReturnTransferredToHR()
    {
        //Arrange
        var identityValidator = new Mock<IIdentityValidator>();
        identityValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
        var evaluator = new ApplicationEvaluator(identityValidator.Object);
        var jobApplication = new JobApplication()
        {
            Applicant = new()
            {
                Age = 21,
                IdentityNumber = "123456789"
            },
            YearsOfExperience = 1,
            TechStackList = new()
        };
        
        //Action
        var evaluateResult = evaluator.Evaluate(jobApplication);
        
        //Assert
        identityValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);
        // Assert.AreEqual(ApplicationResult.TransferredToHr, evaluateResult);
        evaluateResult.Should().Be(ApplicationResult.TransferredToHr);
    }
    
    
    [Test]
    public void ApplicationEvaluate_WithUnder25PercentTechStackSimilarity_ShouldReturnAutoRejected()
    {
        //Arrange
        var identityValidator = new Mock<IIdentityValidator>();
        identityValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var evaluator = new ApplicationEvaluator(identityValidator.Object);
        var jobApplication = new JobApplication()
        {
            Applicant = new()
            {
                Age = 24,
                IdentityNumber = "123456789"
            },
            YearsOfExperience = 3,
            TechStackList = new(){string.Empty}
        };
        
        //Action
        var evaluateResult = evaluator.Evaluate(jobApplication);
        
        //Assert
        // Assert.AreEqual(ApplicationResult.AutoRejected, evaluateResult);
        evaluateResult.Should().Be(ApplicationResult.AutoRejected);
    }
    
    
    [Test]
    public void ApplicationEvaluate_WithOver75PercentTechStackSimilarityAndYearsOfExperience_ShouldReturnAutoAccepted()
    {
        //Arrange
        var identityValidator = new Mock<IIdentityValidator>();
        identityValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var evaluator = new ApplicationEvaluator(identityValidator.Object);
        var jobApplication = new JobApplication()
        {
            Applicant = new()
            {
                Age = 35,
                IdentityNumber = "123456789"
            },
            YearsOfExperience = 15,
            TechStackList = new(){"C#", "JavaScript", "Full-Stack", "RabbitMQ", "Backend", "Others"}
        };
        
        //Action
        var evaluateResult = evaluator.Evaluate(jobApplication);
        
        //Assert
        // Assert.AreEqual(ApplicationResult.AutoAccepted, evaluateResult);
        evaluateResult.Should().Be(ApplicationResult.AutoAccepted);
    }


    [Test]
    public void ApplicationEvaluate_WithNullApplication_ShouldThrowArgumentNullException()
    {
        //Arrange
        var identityValidator = new Mock<IIdentityValidator>();
        identityValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var evaluator = new ApplicationEvaluator(identityValidator.Object);
        
        
        //Action
        Action evaluateResult = () => evaluator.Evaluate(null);
        
        
        //Assert
        evaluateResult.Should().Throw<ArgumentException>();
        identityValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
    }
}