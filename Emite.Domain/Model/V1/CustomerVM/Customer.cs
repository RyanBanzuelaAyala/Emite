namespace Emite.Domain.Model.V1.CustomerVM
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LastContactDate { get; set; }
    }

}
