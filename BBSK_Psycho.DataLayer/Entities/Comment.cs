using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities;

public class Comment
{
    public int Id { get; set; }
    public int psychologistId { get; set; }

    public string Text { get; set; }

    public int Rating { get; set; } // ������ �� 1 �� 5 

    public DateTime Date { get; set; }
    public Client Client { get; set; }
}
