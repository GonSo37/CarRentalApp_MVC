using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.ViewModels;
using Mapster;

namespace CarRentalApp_MVC.Mapping
{
    public class CarMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Car, CarViewModel>()
          .Map(dest => dest.CarId, src => src.CarId)
          .Map(dest => dest.Brand, src => src.Brand)
          .Map(dest => dest.CarModel, src => src.CarModel)
          .Map(dest => dest.YearOfProduction, src => src.YearOfProduction)
          .Map(dest => dest.RegistrationNumber, src => src.RegistrationNumber)
          .Map(dest => dest.Status, src => src.Status)
          .Map(dest => dest.PricePerDay, src => src.PricePerDay)
          .Map(dest => dest.EngineCapacity, src => src.EngineCapacity)
          .Map(dest => dest.EnginePower, src => src.EnginePower);

        }
    }
}
