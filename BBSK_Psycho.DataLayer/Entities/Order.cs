using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities;

public class Order
{
    public int Id { get; set; }

    public int PsychologistId { get; set; }
    public int ClientId { get; set; }
    public decimal Cost { get; set; }

    public int Duration { get; set; }

    public string Message { get; set; } 

    public DateTime SessionDate { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime PayDate { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public OrderStatus OrderPaymentStatus { get; set; }

    public Client Client { get; set; }

    public Psychologist Psychologist { get; set; }
}
