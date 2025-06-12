using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.ViewModels
{
    public class RentalViewModel
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? TotalCost { get; set; }
        public ClientViewModel? Client { get; set; }
        public CarViewModel? Car { get; set; }
    }
}
