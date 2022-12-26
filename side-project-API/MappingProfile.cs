using AutoMapper;
using side_project_API.Entities.DataTransferObjects;
using side_project_API.Entities.Models;

namespace side_project_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<UserForRegistrationDto, User>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
