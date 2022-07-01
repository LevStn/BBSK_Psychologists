using BBSK_Psycho.Enums;

namespace BBSK_Psycho.Models;

public class OrderStatusPatchRequest
{
    public OrderStatus OrderStatus { get; set; }

    public OrderStatus OrderPaymentStatus { get; set; }
}
