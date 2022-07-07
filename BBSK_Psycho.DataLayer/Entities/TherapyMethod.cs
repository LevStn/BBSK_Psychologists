using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities;

public class TherapyMethod
{
    public int Id { get; set; }
    public string Method { get; set; }
    public bool IsDelete { get; set; }


    public List<Psychologist> Psychologists { get; set; }

}
