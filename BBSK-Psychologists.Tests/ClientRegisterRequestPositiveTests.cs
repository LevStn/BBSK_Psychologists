using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class ClientRegisterRequestPositiveTests
{

    [TestCaseSource(typeof(ClientRegisterRequestPositiveTestsSourceForMinLengthPassword))]
    public void NumberOfCharactersInThePasswordIsGreaterThanOrEqualToEight(ClientRegisterRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = "";
        Assert.AreEqual(errorMessage, actualMessage);
    }

    [TestCaseSource(typeof(ClientRegisterRequestPositiveTestsSourceForRequired))]
    public void NamePasswordEmailNotEmpty(ClientRegisterRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = "";
        Assert.AreEqual(errorMessage, actualMessage);
    }


}
