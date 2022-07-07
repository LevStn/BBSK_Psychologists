using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using System.Collections;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistAddRequestValidation_WhenValidModelPassed_NoErrorsReceived : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
            new AddPsychologistRequest
            {
                Name = "лял",
                LastName = "пвфа",
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
                Password = "12356764"
            },
            ApiErrorMessage.NoErrorForTest
            };
        }
    }
}