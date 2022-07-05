using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class LoginRequestPositiveTestsSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new LoginRequest
             {
                Email = "ad@mail.ru",
                Password = "123456789"
             },
           
        };
    }
}

