using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using FluentAssertions;
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
            _psychologistsRepositoryMock.Setup(c => c.AddCommentToPsyhologist(It.IsAny<Comment>(), (It.IsAny<int>())))
                .Returns(comment);
            var expectedComment = comment;

            var commentActual = new Comment()
            {
                Text = "very cool",
                Rating = 5,
                Date = DateTime.Now,
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
            var psychologist = new Psychologist() { Email = "lala@.ru" };
            var newPsych = new Psychologist()
            {
                Email = "lala@.ru"
            };
            _psychologistsRepositoryMock.Setup(c => c.GetPsychologistByEmail("lala@.ru")).Returns(psychologist);
            //when
            //then
            Assert.Throws<Exceptions.UniquenessException>(() => _sut.AddPsychologist(newPsych));
            _psychologistsRepositoryMock.Verify(c => c.AddPsychologist(It.IsAny<Psychologist>()), Times.Never());
        }

        [Test]
        public void DeletePsychologist_ValidRequestPassed_DeletePsychologist()
        {
            //given
            var psychologist = new Psychologist();
            _sut.AddPsychologist(psychologist);
            _psychologistsRepositoryMock.Setup(c => c.DeletePsychologist(psychologist.Id));
            //when
            var actual = _sut.GetPsychologist(psychologist.Id);
            //then
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void DeletePsychologist_PsychologistNotFound_ThrowException()
        {
            //given
            var fakeId = 2;
            _psychologistsRepositoryMock.Setup(o => o.DeletePsychologist(fakeId));
            //when
            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.DeletePsychologist(fakeId));
            _psychologistsRepositoryMock.Verify(c => c.DeletePsychologist(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GetAllPsychologists_ValidRequestPassed_PsychologistsReceived()
        {
            //given
            var psychologists = new List<Psychologist>()
            {
                new Psychologist()
                {
                    Name = "Robert",
                    Comments = new List<Comment>() { new Comment { Text="Nice"} }
                },
                new Psychologist()
                {
                    Name = "Tanya",
                    IsDeleted = true
                }
            };
            _psychologistsRepositoryMock.Setup(o => o.GetAllPsychologists()).Returns(psychologists);
            //when
            var actual = _sut.GetAllPsychologists();
            //then
            Assert.NotNull(actual);
            Assert.True(actual.GetType() == typeof(List<Psychologist>));
            Assert.NotNull(actual[0].Comments);
            Assert.AreEqual(actual[0].Problems, null);
            Assert.True(actual[1].IsDeleted == true);
            _psychologistsRepositoryMock.Verify(c => c.GetAllPsychologists(), Times.Once);
        }

        [Test]
        public void GetCommentsByPsychologistId_ValidRequestPassed()
        {
            //given
            
            var psychologist = new Psychologist()
            {
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Rating=5,
                        Date= DateTime.Now,
                        Text = "Nice"
                    },
                    new Comment
                    {
                        Rating=5,
                        Text = "Bad",
                        Date= DateTime.Now,
                        IsDeleted = true
                    },
                    new Comment
                    {
                        Rating=5,
                        Date= DateTime.Now,
                        Text = "Horrible",
                    }
                }
            };
            _psychologistsRepositoryMock.Setup(o => o.GetPsychologist(psychologist.Id)).Returns(psychologist);
            _sut.AddPsychologist(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).Returns(psychologist.Comments);
            var expected = psychologist.Comments;
            //when
            var actual = _sut.GetCommentsByPsychologistId(psychologist.Id);

            //then
            expected.Should().BeEquivalentTo(actual);
            Assert.True(actual[1].IsDeleted==true);
            _psychologistsRepositoryMock.Verify(c => c.GetCommentsByPsychologistId(psychologist.Id), Times.Once);
        }

        [Test]
        public void GetCommentsByPsychologistId_PsychologistIsNull_Exception()
        {
            //given
            var psychologist = new Psychologist()
            {
                IsDeleted = true
            };
            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).Returns(psychologist.Comments);
            //when

            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetCommentsByPsychologistId(psychologist.Id));
        }

        [Test]
        public void GetOrdersByPsychologistId_ValidRequestPassed_ReturnOrders()
        {
            //given
            var psychologist = new Psychologist()
            {
                Orders = new List<Order>
                {
                    new Order
                    { 
                        Client = new Client(),
                        Cost=1000
                    },
                    new Order
                    {
                        Client = new Client(),
                        Cost=500
                    }
                }
            };
            _psychologistsRepositoryMock.Setup(o => o.GetPsychologist(psychologist.Id)).Returns(psychologist);
            _sut.AddPsychologist(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).Returns(psychologist.Orders);
            var expected = psychologist.Orders;

            //when
            var actual = _sut.GetOrdersByPsychologistId(psychologist.Id);

            //then
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetOrdersByPsychologistId_PsychologistIsNull_Exception()
        {
            //given
            var psychologist = new Psychologist()
            {
                IsDeleted = true
            };
            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).Returns(psychologist.Orders);
            //when

            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetOrdersByPsychologistId(psychologist.Id));
        }

        [Test]
        public void UpdatePsychologist_ValidRequestPassed_UpdatePsycho()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 1,
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                Gender = Gender.Male,
                Phone = "85884859",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "12334534"
            };
            var newPsychologist = new Psychologist()
            {
                Price = 1000
            };
            _psychologistsRepositoryMock.Setup(o=> o.GetPsychologist(psychologist.Id)).Returns(psychologist);
            _psychologistsRepositoryMock.Setup(o=> o.UpdatePsychologist(newPsychologist, psychologist.Id));

            //when
            _sut.UpdatePsychologist(newPsychologist, psychologist.Id);
            
            //then
            var actual = _sut.GetPsychologist(psychologist.Id);

            Assert.True(actual.Price == 1000);
            _psychologistsRepositoryMock.Verify(c => c.GetPsychologist(It.IsAny<int>()), Times.Exactly(2));
            _psychologistsRepositoryMock.Verify(o => o.UpdatePsychologist(It.IsAny<Psychologist>(), It.IsAny<int>()), Times.Once);
        }
    }
}
