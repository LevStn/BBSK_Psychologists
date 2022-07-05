using BBSK_Psycho.Controllers;
using BBSK_Psycho.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistControllerPutErrorCodeTests
{
        private UpdatePsychologistRequest psychologistData = new UpdatePsychologistRequest
        {
            Name = "лял",
            Surname = "пвфа",
            Patronymic = "ПВАПВА",
            gender = BBSK_Psycho.Enums.Gender.Male,
            Phone = "85884859",
            Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
            checkStatus = BBSK_Psycho.Enums.CheckStatus.Completed,
            Email = "ros@fja.com",
            PasportData = "23146456",
            Price = 2000,
            Problems = new List<string> { "dfsdf", "dasd" },
            TherapyMethods = new List<string> { "dasda", "asd" },
            WorkExperience = 10,
            BirthDate = DateTime.Parse("1210 - 12 - 12"),
            Password = "1235345"
        };

        private PsychologistsController _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new PsychologistsController();
        }
        [Test]
        public void UpdatePsychologist_NoContentResult()
        {
            // given

            var psychologist = psychologistData;

            // when
            int id = 2;
            var actual = _sut.UpdatePsychologist(psychologist, id);

            // then
            var actualResult = actual as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        }
        //[Test]
        //public void UpdatePsychologist_NoContentResultNotFound()
        //{
        //    // given

        //    var psychologist = psychologistData;

        //    // when
        //    int id = -1;
        //    var actual = _sut.UpdatePsychologist(psychologist, id);

        //    // then
        //    var actualResult = actual as NoContentResult;
        //    Assert.AreEqual(StatusCodes.Status404NotFound, actualResult.StatusCode);

        //}
    }
}
