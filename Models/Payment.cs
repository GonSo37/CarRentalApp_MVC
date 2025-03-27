namespace CarRentalApp_MVC.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int RentalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        public Rental? Rental { get; set; }
    }
}
