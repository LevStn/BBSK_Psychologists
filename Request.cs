namespace BBSK_Psycho
{
    public class Request
    {
        public int ClientId { get; set; }

        public int ServiceType { get; set; } //тип услуги (одиночная, семейная)

        public List<string> Problems { get; set; }
    }
}
