using Application.Dtos.Authentication;
using AutoMapper;
using DAL.Entities;

namespace Application.Utils
{
    /// <summary>
    /// AutoMapper profiles
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegister, User>();

            CreateMap<UserForLogin, User>();
        }
    }
}
