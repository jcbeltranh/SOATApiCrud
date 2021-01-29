using AutoMapper;
using SOATApiReact.DTOs;
using SOATApiReact.Model;

namespace SOATApiReact.Profiles
{
    public class SOATProfile : Profile
    {
        public SOATProfile()
        {
            CreateMap<SOAT, SOATReadDto>();
            CreateMap<SOATCreateDto, SOAT>()
                .ForMember(
                    destination => destination.Owner,
                    options => options.MapFrom(
                        source => source
                    )
                ).ForMember(
                    destination => destination.Vehicle,
                    options => options.MapFrom(
                        source => source
                    )
                ).ForMember(
                    destination => destination.VehiclePlate,
                    options => options.MapFrom(
                        source => source.Vehicle
                    )
                ).ForMember(
                    destination => destination.OwnerDocument,
                    options => options.MapFrom(
                        source => source.Owner
                    )
                );
            CreateMap<SOATCreateDto, User>()
                .ForMember(
                    destination => destination.Document,
                    options => options.MapFrom(
                        source => source.Owner
                    )
                );
            CreateMap<SOATCreateDto, Vehicle>()
                .ForMember(
                    destination => destination.Plate,
                    options => options.MapFrom(
                        source => source.Vehicle
                    )
                );
            CreateMap<SOATUpdateDto, SOAT>()
                .ForMember(
                    destination => destination.Owner,
                    options => options.MapFrom(
                        source => source
                    )
                ).ForMember(
                    destination => destination.Vehicle,
                    options => options.MapFrom(
                        source => source
                    )
                );
            CreateMap<SOATUpdateDto, User>()
                .ForMember(
                destination => destination.Document,
                options => options.MapFrom(
                    source => source.Owner
                )
            );
            CreateMap<SOATUpdateDto, Vehicle>()
                .ForMember(
                destination => destination.Plate,
                options => options.MapFrom(
                    source => source.Vehicle
                )
            );
            CreateMap<SOAT, SOATUpdateDto>()
                .ForMember(
                    destination => destination.Owner,
                    options => options.MapFrom(
                        source => source.Owner.Document
                    )
                ).ForMember(
                    destination => destination.Vehicle,
                    options => options.MapFrom(
                        source => source.Vehicle.Plate
                    )
                );
        }
    }
}