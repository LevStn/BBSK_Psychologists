using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class ClientRegisterRequestPositiveTestsSourceForMinLengthPassword : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new ClientRegisterRequest
             {
                 Name = "Petro",
                 LastName ="Petrov",
                 Password="1232345678",
                 Email ="p@petrov.com",
                 PhoneNumber ="89119118696",
                 BirthDate = DateTime.Now,
             },
             ApiErrorMessage.NoErrorForTest
        };




    }
}

