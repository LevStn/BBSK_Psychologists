using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class ClientUpdateRequestPositiveTestsSourceForRequired : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new ClientUpdateRequest
             {
                 Name = "Petro",
                 LastName ="",
                 BirthDate = DateTime.Now,
             },
             ApiErrorMessage.NoErrorForTest
        };


    }
}

