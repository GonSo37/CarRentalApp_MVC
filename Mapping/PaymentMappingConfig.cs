using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.ViewModels;
using Mapster;

namespace CarRentalApp_MVC.Mapping
{
    public class PaymentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Payment, PaymentViewModel>()
                .Map(dst => dst.PaymentId, src => src.PaymentId)
                .Map(dst => dst.RentalId, src => src.RentalId)
                .Map(dst => dst.Amount, src => src.Amount)
                .Map(dst => dst.PaymentDate, src => src.PaymentDate)
                .Map(dst => dst.PayMethod, src => src.PayMethod);

        }
    }
}
