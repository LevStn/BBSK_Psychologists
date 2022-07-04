using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class CommentRequestNegativeTestsSourceRangeForRating : IEnumerable
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
                Rating = 0,
                Date = DateTime.Now

            },
            ApiErrorMessage.RatingIsRequired
        };

        yield return new object[]
        {
            new CommentRequest
            {
                ClientId = 1,
                PsychologistId = 2,
                Text = "",
                Rating = 6,
                Date = DateTime.Now

            },
            ApiErrorMessage.RatingIsRequired
        };

        yield return new object[]
        {
            new CommentRequest
            {
                ClientId = 1,
                PsychologistId = 2,
                Text = "",
                Rating = -2,
                Date = DateTime.Now

            },
            ApiErrorMessage.RatingIsRequired
        };
    }
}

