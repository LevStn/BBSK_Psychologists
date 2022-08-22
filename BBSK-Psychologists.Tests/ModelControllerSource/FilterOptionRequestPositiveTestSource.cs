using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Models.Requests;
using System.Collections;

namespace BBSK_Psychologists.Tests.ModelControllerSource
{
    public class FilterOptionRequestPositiveTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new FilterOptionRequest
                {
                    Price = Price.Ascending,
                    Problems = new List<int> { 1, 2 },
                    Gender = Gender.Male,
                }
            };
        }
    }    
}
