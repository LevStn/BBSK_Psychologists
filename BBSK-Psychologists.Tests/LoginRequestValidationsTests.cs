
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class LoginRequestValidationsTests
{

    [TestCaseSource(typeof(LoginRequestNegativeTestsSource))]
    public void WhenPassworEmailAreEmptyShouldThrowException(LoginRequest request, string errorMessage)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationsResults, true);

        //then
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }


    [TestCaseSource(typeof(LoginRequestPositiveTestsSource))]
    public void ClientUpdateRequest_SendingCorrectData_GetAnEmptyStringError(LoginRequest request)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationsResults, true);

        //then
        Assert.True(isValid);
    }

}
