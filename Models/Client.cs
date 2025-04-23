namespace CarRentalApp_MVC.Models
{
    public class Client
    {
        public enum ClientStatus
        {
            Active,
            Inactive
        }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriversLicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ClientStatus Status { get; set; }

        public ICollection<Rental>? Rentals { get; set; }
    }
}
