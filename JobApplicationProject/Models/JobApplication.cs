﻿namespace JobApplicationProject.Models;

public class JobApplication
{
    public Applicant Applicant { get; set; }
    public int YearsOfExperience { get; set; }
    public List<string> TechStackList { get; set; }
}