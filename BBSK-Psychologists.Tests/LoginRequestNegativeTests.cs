using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class LoginRequestNegativeTests
{

    [TestCaseSource(typeof(LoginRequestNegativeTestsSourceForRequired))]
    public void WhenPassworEmailAreEmptyShouldThrowException(LoginRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults,true);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }

    [TestCaseSource(typeof(LoginRequestNegativeTestsSourceForEmailSymbol))]
    public void WhenTheEmailContainsInvalidCharactersThrowException(LoginRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }

    [TestCaseSource(typeof(LoginRequestNegativeTestsSourceForEmailSymbol))]
    public void WhenPasswordLessThanEightThrowException(LoginRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }

}
