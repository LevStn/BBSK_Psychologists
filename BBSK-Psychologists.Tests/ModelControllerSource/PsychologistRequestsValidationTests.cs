using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests.ModelControllerSource
{
    public class PsychologistRequestsValidationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(typeof(PsychologistAddRequestValidation_WhenInvalidModel_ErrorsReceived))]
        public void PsychologistAddRequestTest(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            //given
            var validationResultList = new List<ValidationResult>();
            //when
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            //then
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistUpdateRequestValidation_WhenInvalidModel_ErrorsReceived))]
        public void PsychologistAddRequestTest(UpdatePsychologistRequest updatePsychologistRequest, string errorMessage)
        {
            //given
            var validationResultList = new List<ValidationResult>();
            //when
            var isValid = Validator.TryValidateObject(updatePsychologistRequest, new ValidationContext(updatePsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            //then
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistAddRequestValidation_WhenValidModelPassed_NoErrorsReceived))]
        public void PsychologistAddRequestPositiveTest(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            //given
            var validationResultList = new List<ValidationResult>();
            //when
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = "";
            //then
            Assert.AreEqual(errorMessage, message);

        }

    }

    
}
