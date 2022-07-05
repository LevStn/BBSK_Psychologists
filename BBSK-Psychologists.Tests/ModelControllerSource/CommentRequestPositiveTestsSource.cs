using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class CommentRequestPositiveTestsSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new CommentRequest
            {
                ClientId = 1,
                PsychologistId = 2,
                Text = "",
                Rating = 5,
                Date = DateTime.Now

            },
            ApiErrorMessage.NoErrorForTest
        };

        yield return new object[]
        {
            new CommentRequest
            {
                ClientId = 1,
                PsychologistId = 2,
                Text = "",
                Rating = 1,
                Date = DateTime.Now

            },
            ApiErrorMessage.NoErrorForTest
        };

        yield return new object[]
        {
            new CommentRequest
            {
                ClientId = 1,
                PsychologistId = 2,
                Text = "",
                Rating = 3,
                Date = DateTime.Now

            },
            ApiErrorMessage.NoErrorForTest
        };

    }
}

