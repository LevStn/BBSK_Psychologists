using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPasswordLengthIsLess()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var item = new AddPsychologistRequest()
            {
                Name = "Маша",
                Surname = "Иванова",
                Patronymic = "Ляляка",
                gender = Enums.Gender.Male,
                Phone = "85884859",
                Education = new List<string> { "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education", "sg osgj sopj r" },
                checkStatus = Enums.CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "123"
            };
            var validationResultList = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(item, new ValidationContext(item), validationResultList, true);

            Assert.IsFalse(isValid);

            var message = validationResultList[0].ErrorMessage;

            Assert.AreEqual(ApiErrorMessage.PasswordLengthIsLessThanAllowed, message);
        }
    }
}