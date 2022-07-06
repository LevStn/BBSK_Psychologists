using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class LoginRequestNegativeTestsSourceForRequired : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new LoginRequest
             {
                Email = "ad@mail.ru",
                Password = ""
             },
             ApiErrorMessage.PasswordIsRequired
        };

        yield return new object[]
        {
             new LoginRequest
             {
                Email = "",
                Password = "12sf345878"
             },
             ApiErrorMessage.EmailIsRequire
        };


    }
}

