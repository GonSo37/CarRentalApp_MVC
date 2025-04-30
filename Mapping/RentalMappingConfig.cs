using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.ViewModels;
using Mapster;

namespace CarRentalApp_MVC.Mapping
{
    public class RentalMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Rental, RentalViewModel>()
                .Map(dst => dst.RentalId, src => src.RentalId)
                .Map(dst => dst.CarId, src => src.CarId)
                .Map(dst => dst.ClientId, src => src.ClientId)
                .Map(dst => dst.StartDate, src => src.StartDate)
                .Map(dst => dst.EndDate, src => src.EndDate)
                .Map(dst => dst.TotalCost, src => src.TotalCost)
                .Map(dst => dst.Car, src => src.Car)
                .Map(dst => dst.Client, src => src.Client);
        }
    }
}
