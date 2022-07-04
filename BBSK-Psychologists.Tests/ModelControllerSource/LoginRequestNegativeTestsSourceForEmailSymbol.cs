using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class LoginRequestNegativeTestsSourceForEmailSymbol : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new LoginRequest
             {
                Email = "admail.ru",
                Password = "1234546adadas"
             },
             ApiErrorMessage.InvalidCharacterInEmail
        };

       


    }
}

