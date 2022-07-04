using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BBSK_Psychologists.Tests;

public class ClientRegisterRequestTest
{

    [TestCaseSource(typeof(ClientRegisterRequestTestsSourceForRequired))]
    public void WhenEmailNamePasswordAreEmptyShouldThrowException(ClientRegisterRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults);
        Assert.IsFalse(isValid);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }
}
