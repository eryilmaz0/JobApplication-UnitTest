using System.Collections.ObjectModel;
using JobApplicationProject.Services;

namespace JobApplicationProject.Models;

public class ApplicationEvaluator
{
    private readonly int limitAge = 18;
    private readonly int autoAcceptYearsOfExperience = 15;
    private readonly ReadOnlyCollection<string> techStackList = new List<string>(){"C#", "JavaScript", "Full-Stack", "Microservice"}.AsReadOnly();
    private readonly IIdentityValidator _identityValidator;

    public ApplicationEvaluator(IIdentityValidator identityValidator)
    {
        _identityValidator = identityValidator;
    }

    public ApplicationResult Evaluate(JobApplication application)
    {
        if (application is null)
            throw new ArgumentNullException("Application is null");
        
        if (application.Applicant.Age < limitAge)
            return ApplicationResult.AutoRejected;

        bool validateIdentity = this._identityValidator.IsValid(application.Applicant.IdentityNumber);

        if (!validateIdentity)
            return ApplicationResult.TransferredToHr;

        var tectStackRate = CalculateTechStackSimilarityRate(application.TechStackList);

        if (tectStackRate < 25)
            return ApplicationResult.AutoRejected;

        if (tectStackRate >= 75 && application.YearsOfExperience >= autoAcceptYearsOfExperience)
            return ApplicationResult.AutoAccepted;
        
        return ApplicationResult.TransferredToHr;
    }


    private int CalculateTechStackSimilarityRate(List<string> applicantTechStacks)
    {
        var matchedStacksCount = applicantTechStacks
            .Where(stack => this.techStackList.Contains(stack, StringComparer.OrdinalIgnoreCase))
            .Count();
        
        var result = ((double)matchedStacksCount / this.techStackList.Count()) * 100;
        return (int)result;
    }
}


public enum ApplicationResult
{
    AutoRejected = 1,
    TransferredToHr,
    TransferredToLead,
    TransferredToCto,
    AutoAccepted
}