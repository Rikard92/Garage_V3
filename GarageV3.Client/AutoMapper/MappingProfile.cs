using AutoMapper;
using GarageV3.Core.Models;
using GarageV3.Core.ViewModels;

namespace GarageV3.AutoMapper
{


    public class MappingProfile : Profile
    {
        private readonly IMapper mapper;

        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleViewModel>().ForMember(
                dest => dest.VType, from => from.MapFrom(s => s.VehicleType.VType));


            CreateMap<Vehicle, ParkCarViewModel>();
            CreateMap<ParkCarViewModel, Vehicle>().ReverseMap();


            CreateMap<TicketViewModel, ReceitViewModel>();

            CreateMap<TicketViewModel, ReceitViewModel>();

            CreateMap<Owner, OwnerViewModel>();
            CreateMap<Owner, OwnerViewModel>().ReverseMap();

        }
    }
}
