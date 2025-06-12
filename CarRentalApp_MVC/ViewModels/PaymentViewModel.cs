using static CarRentalApp_MVC.Models.Payment;

namespace CarRentalApp_MVC.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }
        public int RentalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PayMethod { get; set; }
    }
}
