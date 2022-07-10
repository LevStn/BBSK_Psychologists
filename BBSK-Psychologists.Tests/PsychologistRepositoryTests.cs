using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistRepositoryTests
{
        private readonly DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

        public PsychologistRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }
        [Test]
        public void DeletePsychologist_WhenCorrectIdPassed_ThenSoftDeleteApplied()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psychologist = new Psychologist
            {
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                Gender = Gender.Male,
                Phone = "85884859",
                Educations = new List<Education> { new Education { Id=1, EducationData= "2020-12-12", IsDeleted=false }},
                CheckStatus = CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<Problem> { new Problem { Id=1, ProblemName="ds", IsDeleted=false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Id=1, Method="therapy lal", IsDeleted=false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "123"
            };

            context.Psychologists.Add(psychologist);
            context.SaveChanges();

            //when

            sut.DeletePsychologist(psychologist.Id);

            //then
            Assert.True(psychologist.IsDeleted);
        }

        [TestCaseSource(typeof(PsychologistRepositoryTestsSource))]
        public void GetPsychologist_WhenCorrectIdPassed_ThenSoftDeleteApplied ()
        {

        }
}
}
