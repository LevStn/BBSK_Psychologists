using BBSK_Psycho.Enums;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Responses;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models;

public class OrderCreateRequest
{
    [Required(ErrorMessage = ApiErrorMessage.ClientIdIsRequired)]
    public int ClientId { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.CostIsRequired)]
    public decimal Cost { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.DurationIsRequired)]
    public int Duration { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.MessageIsRequired)]
    public string Message { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.SessionDateIsRequired)]
    [DataType(DataType.Date, ErrorMessage = ApiErrorMessage.InvalidDate)]
    public DateTime SessionDate { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.SessionDateIsRequired)]
    [DataType(DataType.Date, ErrorMessage = ApiErrorMessage.InvalidDate)]
    public DateTime OrderDate { get; set; }

    [DataType(DataType.Date, ErrorMessage = ApiErrorMessage.InvalidDate)]
    public DateTime? PayDate { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.OrderPaymentStatusIsRequired)]
    public OrderPaymentSatus OrderPaymentStatus { get; set; }
}




