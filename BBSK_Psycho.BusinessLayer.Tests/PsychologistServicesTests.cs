using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class PsychologistServicesTests
    {
        private PsychologistService _sut;
        private Mock<IPsychologistsRepository> _psychologistsRepositoryMock;
        //given
        //when
        //then
        [SetUp]
        public void Setup()
        {
            _psychologistsRepositoryMock = new Mock<IPsychologistsRepository>();
            _sut = new PsychologistService(_psychologistsRepositoryMock.Object);
        }

        [Test]
        public void AddCommentToPsychologist_ValidRequestPassed_ReturnComment()
        {
            //given
            var comment = new Comment();
            _psychologistsRepositoryMock.Setup(c => c.AddCommentToPsyhologist(It.IsAny<Comment>(), (It.IsAny <int>())))
                .Returns(comment);
            var expectedComment = comment;

            var commentActual = new Comment()
            {
                Text="very cool",
                Rating=5,
                Date=DateTime.Now,
                Psychologist = new Psychologist(),
                Client = new Client()
            };
            //when
            var actual = _sut.AddCommentToPsyhologist(commentActual, commentActual.Id);

            //then
            Assert.AreEqual(expectedComment.Id, actual.Id);
        }
       
        [Test]
        public void AddPsychologist_ValidRequestPassed_ReturnId()
        {
            //given
            _psychologistsRepositoryMock.Setup(c => c.AddPsychologist(It.IsAny<Psychologist>()))
                .Returns(1);
            var expectedId = 1;
            var psych = new Psychologist();
            //when
            var actual = _sut.AddPsychologist(psych);
            //then
            Assert.AreEqual(expectedId, actual);
            _psychologistsRepositoryMock.Verify(c => c.AddPsychologist(psych), Times.Once());
        }

        [Test]
        public void AddPsychologist_EmailIsNotUnique_ThrowUniquenessException()
        {
            //given
            var psychologists = new List<Psychologist>()
            {
                new Psychologist() { Email = "sobaka@.ru"},
                new Psychologist() { Email = "kryto@.ru"},
                new Psychologist() { Email = "xyu@.ru"}
            };
            _psychologistsRepositoryMock.Setup(c => c.GetAllPsychologists())
                .Returns(psychologists);
            var newPsych = new Psychologist()
            {
                Email = "xyu@.ru"
            };
            _psychologistsRepositoryMock.Setup(c => c.GetPsychologistByEmail("xyu@.ru")).Returns(psychologists[0]);
            //when
            //then
            Assert.Throws<Exceptions.UniquenessException>(()=> _sut.AddPsychologist(newPsych));
            _psychologistsRepositoryMock.Verify(c => c.AddPsychologist(It.IsAny<Psychologist>()), Times.Never());
        }
    }
}
