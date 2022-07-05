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
    public class PsychologistControllerDeleteErrorCodeTests
{
        private PsychologistsController _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new PsychologistsController();
        }
        [Test]
        public void DeletePsychologist_NoContentResult()
        {
            // given

            //var psychologist = psychologistData;

            // when
            int id = 2;
            var actual = _sut.DeletePsychologist(id);

            // then
            var actualResult = actual as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        }
    }
}
