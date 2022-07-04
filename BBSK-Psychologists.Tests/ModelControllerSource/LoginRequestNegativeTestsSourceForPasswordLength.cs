using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class LoginRequestNegativeTestsSourceForPasswordLength : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new LoginRequest
             {
                Email = "ad@mail.ru",
                Password = "1234567"
             },
             ApiErrorMessage.PasswordLengthIsLessThanAllowed
        };

       


    }
}

