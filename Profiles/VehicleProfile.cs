using AutoMapper;
using SOATApiReact.DTOs;
using SOATApiReact.Model;

namespace SOATApiReact.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleReadDto>();
            CreateMap<VehicleCreateDto, Vehicle>();
            CreateMap<VehicleUpdateDto, Vehicle>();
            CreateMap<Vehicle, VehicleUpdateDto>();
        }
    }
}