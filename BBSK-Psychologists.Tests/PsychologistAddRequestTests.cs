using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void PsychologistAddRequestTest(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }
       
    }

    
}
