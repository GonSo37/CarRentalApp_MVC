using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.ViewModels;
using Mapster;

namespace CarRentalApp_MVC.Mapping
{
    public class ClientMappingConfig : IRegister
    { 
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Client, ClientViewModel>()
                .Map(dest => dest.ClientId, src => src.ClientId)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.DriversLicenseNumber, src => src.DriversLicenseNumber)
                .Map(dst => dst.PhoneNumber, src => src.PhoneNumber)
                .Map(dst => dst.Email, src => src.Email)
                .Map(dst => dst.Status, src => src.Status);

        }
    }
}
