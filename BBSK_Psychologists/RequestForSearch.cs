using System.Collections.Generic;

namespace BBSK_Psycho
{
    public class RequestForSearch
    {
        public int ClientId { get; set; }

        public int ServiceType { get; set; } //тип услуги (одиночная, семейная)

        public List<string> Problems { get; set; }
    }
}
