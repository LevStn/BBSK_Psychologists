using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using System.Collections;

namespace BBSK_Psychologists.Tests.ModelControllerSource
{
    public class FilterOptionRequestNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = (Price)(-1),
                    Problems = new List<int> { 1, 2 },
                    Gender = Gender.Male,
                },
                ApiErrorMessage.RangeIsError
            };
            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = (Price)(0),
                    Problems = new List<int> { 1, 2 },
                    Gender = Gender.Male,
                },
                ApiErrorMessage.RangeIsError
            };

            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = (Price)(20),
                    Problems = new List<int> { 1, 2 },
                    Gender = Gender.Male,
                },
                ApiErrorMessage.RangeIsError
            };
            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = Price.Descending,
                    Problems = new List<int> { 1, 2 },
                    Gender = (Gender)(-1),
                },
                ApiErrorMessage.RangeIsError
            };
            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = Price.Descending,
                    Problems = new List<int> { 1, 2 },
                    Gender = (Gender)(0),
                },
                ApiErrorMessage.RangeIsError
            };
            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = Price.Descending,
                    Problems = new List<int> { 1, 2 },
                    Gender = (Gender)(20),
                },
                ApiErrorMessage.RangeIsError
            };
            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = Price.Descending,
                    Problems = null,
                    Gender = Gender.Male
                },
                ApiErrorMessage.ProblemsIsRequired
            };
        }
    }    
}