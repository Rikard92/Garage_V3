using AutoMapper;
using GarageV3.Client.Controllers;
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
                .ForMember(dest => dest.OwnerId, from => from.MapFrom(s => s.OwnerId)).ReverseMap();


            CreateMap<ParkCarViewModel, Vehicle>()
                .ForMember(dest => dest.Model, from => from.MapFrom(m => m.CarModel)).ReverseMap();

            //CreateMap<Vehicle, ParkCarViewModel>().ReverseMap();


            CreateMap<TicketViewModel, ReceitViewModel>();

            CreateMap<TicketViewModel, ReceitViewModel>();

            CreateMap<Owner, OwnerViewModel>();
            CreateMap<Owner, OwnerViewModel>().ReverseMap();

            CreateMap<VehicleType, VehicleTypeViewModel>();
            CreateMap<VehicleType, VehicleTypeViewModel>().ReverseMap();


            //Memberships

            CreateMap<Membership, MemberShipsViewModel>()
                .ForMember(dest => dest.FirstName, from => from.MapFrom(m => m.Owner.FirstName))
                .ForMember(dest => dest.LastName, from => from.MapFrom(m => m.Owner.LastName))
                .ForMember(dest => dest.BirthDate, from => from.MapFrom(m => m.Owner.PersonNumber));

            CreateMap<Membership, MemberShipsViewModel>()
                .ForMember(dest => dest.FirstName, from => from.MapFrom(m => m.Owner.FirstName))
                .ForMember(dest => dest.LastName, from => from.MapFrom(m => m.Owner.LastName)).ReverseMap();
        }
    }
}
