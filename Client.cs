namespace BBSK_Psycho
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<Order> Orders { get; set; }
    }
}
