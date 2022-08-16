using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models.Requests;

public class FilterOptionRequest
{
    public Price Price{ get; set; }

    public List<int> Problems { get; set; }

    public Gender? Gender { get; set; }
}
