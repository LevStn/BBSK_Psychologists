using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class CommentRequestNegativeTests
{

    [TestCaseSource(typeof(CommentRequestNegativeTestsSourceRangeForRating))]
    public void WhenMeaningRatingNotIncludedInTheRangeFromOneToFiveThrowException(CommentRequest client, string errorMessage)
    {
        var validationsResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }

}
