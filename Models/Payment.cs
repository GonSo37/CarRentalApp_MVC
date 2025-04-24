namespace CarRentalApp_MVC.Models
{
    public class Payment
    {
        public enum PaymentMethod
        {
            Cash,
            DebitCard,
            CreditCard,
            PayPal,
            BankTransfer,
            ApplePay,
            GooglePay,
            Other
        }
        public int PaymentId { get; set; }
        public int RentalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PayMethod { get; set; }

        public Rental? Rental { get; set; }
    }
}
