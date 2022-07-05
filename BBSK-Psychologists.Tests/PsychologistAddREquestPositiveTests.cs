using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistAddREquestPositiveTests
{
    [TestCaseSource(typeof(PsychologistAddRequestPositiveTestSource))]
    public void PsychologistAddRequestPositiveTest(AddPsychologistRequest addPsychologistRequest, string errorMessage)
    {
        var validationResultList = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
        var message = "";
        Assert.AreEqual(errorMessage, message);

    }
}
}
