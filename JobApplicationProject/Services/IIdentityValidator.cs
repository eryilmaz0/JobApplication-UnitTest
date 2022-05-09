namespace JobApplicationProject.Services;

public interface IIdentityValidator
{
    public bool IsValid(string identityNumber);
}