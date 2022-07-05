using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities;

public class Order
{
    public int Id { get; set; }
    public string? PsyhId { get; set; }

    public Client Client { get; set; }
}
