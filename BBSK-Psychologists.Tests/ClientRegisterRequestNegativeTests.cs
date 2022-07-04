using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class ClientRegisterRequestNegativeTests
{

    [TestCaseSource(typeof(ClientRegisterRequestNegativeTestsSourceForRequired))]
    public void WhenEmailNamePasswordAreEmptyShouldThrowException(ClientRegisterRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        //Assert.IsFalse(isValid);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }

    [TestCaseSource(typeof(ClientRegisterRequestNegativeTestsSourceForMinLengthPassword))]
    public void WhenPasswordLengthLessEightCharactersThrowException(ClientRegisterRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        //Assert.IsFalse(isValid);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }


}
