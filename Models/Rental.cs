namespace CarRentalApp_MVC.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }

        public Car Сar { get; set; }
        public Client Сlient { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
