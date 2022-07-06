using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class ClientUpdateRequestNegativeTests
{

    [TestCaseSource(typeof(ClientUpdateRequestNegativeTestsSourceForRequired))]
    public void WhenNameAreEmptyShouldThrowException(ClientUpdateRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }

}
