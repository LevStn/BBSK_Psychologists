using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class LoginRequestPositiveTests
{

    [TestCaseSource(typeof(LoginRequestPositiveTestsSourceToCheckEmailAndPassword))]
    public void WhenPassworEmailNotEmpty(LoginRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = "";
        Assert.AreEqual(errorMessage, actualMessage);
    }

    [TestCaseSource(typeof(LoginRequestPositiveTestsSourceToCheckEmailAndPassword))]
    public void WhenTheEmailContainsMoreEightSymbolAndSymbolRight(LoginRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = "";
        Assert.AreEqual(errorMessage, actualMessage);
    }

    

}
