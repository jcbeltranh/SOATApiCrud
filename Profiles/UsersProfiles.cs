using AutoMapper;
using SOATApiReact.Data;
using SOATApiReact.DTOs;
using SOATApiReact.Model;

namespace SOATApiReact.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            CreateMap<User, UserReadDto>()
                .ForMember(
                    destinationMember => destinationMember.DocumentType,
                    operation => operation.MapFrom(
                        sourceMember => sourceMember.DocumentType.ToString()
                    )
                );
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}