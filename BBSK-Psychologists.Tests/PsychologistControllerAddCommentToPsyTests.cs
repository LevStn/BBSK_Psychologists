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
    public class PsychologistControllerAddCommentToPsyTests
{
        private PsychologistsController _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new PsychologistsController();
        }
        [Test]
        public void AddRequestForPsy()
        {
            // given

            var request = new CommentRequest
            {
              ClientId=1,
              PsychologistId= 1,
              Text = "kdffk",
              Rating= 1,
              Date= DateTime.Now
            };
            int psId = 2;
            // when
            var actual = _sut.AddCommentToPsyhologist(request, psId);

            // then
            var actualResult = actual.Result as CreatedResult;
            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

        }
    }
}

