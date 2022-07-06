using BBSK_Psycho.Controllers;
using BBSK_Psycho.Models;
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
    public class PsychologistControllerAddRequestErrorCodeTests
{


        private PsychologistsController _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new PsychologistsController();
        }
        [Test]
        public void AddRequestForPsy_ValidRequestPassed_CreatedResultReceived()
        {
            // given
            int psId = 2;
            var request = new RequestPsyhologistSearch
            {
                Name =  "kd",
                PhoneNumber = "976",
                Description= "sgj",
                PsychologistGender= BBSK_Psycho.Enums.Gender.Male,
                CostMin=2999,
                CostMax=5000,
                Date= DateTime.Now,
                Time= BBSK_Psycho.Enums.TimeOfDay.Evening,
                ClientId=2

            };

            // when
            var actual = _sut.AddRequestPsyhologistSearch(request);

            // then
            var actualResult = actual.Result as CreatedResult;
            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

        }
    }
}
