using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.BusinessLayer.Services.Helpers;

public class SearchByFilter : ISearchByFilter
{
    public async Task<List<Psychologist>> GetPsychologistsByParametrs(Price price, List<int> problems, Gender? gender, List<Psychologist> psychologists)
    {
        var problemSampling = (from psychologist in psychologists
                               let psychologistProblems = psychologist.Problems
                               from problem in psychologistProblems
                               where psychologists.Any(a => problems.Contains(problem.Id))
                               select psychologist).ToList();
        switch (price)
        {
            case Price.Ascending:
                problemSampling.Sort((x, y) => x.Price.CompareTo(y.Price));
                break;

            case Price.Descending:
                problemSampling = problemSampling.OrderByDescending(p => p.Price).ToList();
                break;
        }

        var correctGradation = problemSampling.GroupBy(i => i)
            .OrderByDescending(group => group.Count())
            .Select(c => c.Key).ToList();

        if (gender is not null)
        {
            correctGradation = correctGradation.Where(p => p.Gender == gender).ToList();
        }

        return correctGradation;
    }
}
