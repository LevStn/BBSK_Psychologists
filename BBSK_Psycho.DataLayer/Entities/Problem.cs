using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities;

public class Problem
{
    public int Id { get; set; }
    public string ProblemName { get; set; }

    public List<Psychologist> Psychologists { get; set; }
}
