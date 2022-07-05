using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistUpdateRequestNegativeTests
{
        [TestCaseSource(typeof(PsychologistUpdateRequestNegativeTestSource))]
        public void PsychologistAddRequestTest(UpdatePsychologistRequest updatePsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(updatePsychologistRequest, new ValidationContext(updatePsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }
    }
}
