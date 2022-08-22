using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests
{
    public class FilterOptionRequestValidationsTests
    {
        [TestCaseSource(typeof(FilterOptionRequestPositiveTestSource))]
        public void FilterOptionRequest_SendingCorrectData_GetAnEmptyStringError(FilterOptionRequest filterOptionRequest)
        {
            //given
            var validationsResults = new List<ValidationResult>();

            //when
            var isValid = Validator.TryValidateObject(filterOptionRequest, new ValidationContext(filterOptionRequest), validationsResults, true);

            //then
            Assert.True(isValid);
        }

        [TestCaseSource(typeof(FilterOptionRequestNegativeTestSource))]
        public void FilterOptionRequest_SendingIncorrectData_GetErrorMessage(FilterOptionRequest filterOptionRequest, string errorMessage)
        {
            //given
            var validationsResults = new List<ValidationResult>();

            //when
            var isValid = Validator.TryValidateObject(filterOptionRequest, new ValidationContext(filterOptionRequest), validationsResults, true);

            //then
            var actualMessage = validationsResults[0].ErrorMessage;
            Assert.AreEqual(errorMessage, actualMessage);
        }
    }
}
