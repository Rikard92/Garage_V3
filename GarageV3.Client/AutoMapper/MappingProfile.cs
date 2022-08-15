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
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(dest => dest.VType, from => from.MapFrom(s => s.VehicleType.VType))
                .ForMember(dest => dest.CarModel, from => from.MapFrom(s => s.Model))
                .ForMember(dest => dest.OwnerId, from => from.MapFrom(s => s.OwnerId))
                .ForMember(dest => dest.CarModel, from => from.MapFrom(s => s.Model));


            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(dest => dest.VType, from => from.MapFrom(s => s.VehicleType.VType))
                .ForMember(dest => dest.CarModel, from => from.MapFrom(s => s.Model)).ReverseMap();


            CreateMap<Vehicle, ParkCarViewModel>()
                .ForMember(dest => dest.CarModel, from => from.MapFrom(m => m.Model));
            CreateMap<Vehicle, ParkCarViewModel>().ReverseMap();


            CreateMap<TicketViewModel, ReceitViewModel>();

            CreateMap<TicketViewModel, ReceitViewModel>();

            CreateMap<Owner, OwnerViewModel>();
            CreateMap<Owner, OwnerViewModel>().ReverseMap();

            CreateMap<VehicleType, VehicleTypeViewModel>();
            CreateMap<VehicleType, VehicleTypeViewModel>().ReverseMap();

        }
    }
}
