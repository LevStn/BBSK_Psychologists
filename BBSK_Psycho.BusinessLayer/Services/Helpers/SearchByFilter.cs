using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.BusinessLayer.Services.Helpers;

public class SearchByFilter : ISearchByFilter
{
    public async Task<List<Psychologist>> GetPsychologistsByParametrs(Price price, List<int> problems, Gender? gender, List<Psychologist> psychologists)
    {
        var psychologistsSampling = await GetPsychologistsByProblem(psychologists, problems);
       
        switch (price)
        {
            case Price.Ascending:
                psychologistsSampling.Sort((x, y) => x.Price.CompareTo(y.Price));
                break;

            case Price.Descending:
                psychologistsSampling = psychologistsSampling.OrderByDescending(p => p.Price).ToList();
                break;
        }
        var correctGradation = psychologistsSampling.GroupBy(i => i)
            .OrderByDescending(group => group.Count())
            .Select(c => c.Key).ToList();

        if (gender is not null)
        {
            correctGradation = correctGradation.Where(p => p.Gender == gender).ToList();
        }
        return correctGradation;
    }

    public async Task<List<Psychologist>> GetPsychologistsByProblem(List<Psychologist> psychologists, List<int> problems)
    {
        var result = new List<Psychologist>();

        foreach(var psychologist in psychologists)
        {
            var psychologistProblems = psychologist.Problems;

            foreach(var psychologistProblem in psychologistProblems)
            {
                if (psychologists.Any(p => problems.Contains(psychologistProblem.Id)))
                {
                    result.Add(psychologist);
                }
            }
        }
        return result;
    }
}
