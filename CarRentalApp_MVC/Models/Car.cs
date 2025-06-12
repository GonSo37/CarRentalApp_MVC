namespace CarRentalApp_MVC.Models
{
    public class Car
    {
        public enum CarStatus
        {
            Available,
            Unavailable,
            Rented, 
            InRepair
        }
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string CarModel { get; set; }
        public int YearOfProduction { get; set; }
        public string RegistrationNumber { get; set; }
        public CarStatus Status { get; set; }
        public decimal PricePerDay { get; set; }
        public double EngineCapacity { get; set; }
        public int EnginePower { get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}
