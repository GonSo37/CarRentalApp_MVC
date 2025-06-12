using static CarRentalApp_MVC.Models.Client;

namespace CarRentalApp_MVC.ViewModels
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriversLicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ClientStatus Status { get; set; }
    }
}
