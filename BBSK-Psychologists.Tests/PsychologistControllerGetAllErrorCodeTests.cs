using BBSK_Psycho.Controllers;
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
    public class PsychologistControllerGetAllErrorCodeTests
{

        private PsychologistsController _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new PsychologistsController();
        }
        [Test]
        public void GetAllPsychologist_ObjectResultPassed()
        {
            // when

            var actual = _sut.GetAllPsychologists();

            // then
            var actualResult = actual.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        }

    }
}
