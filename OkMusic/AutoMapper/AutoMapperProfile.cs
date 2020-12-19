using System;
using AutoMapper;
using OkMusic.Domain;

namespace OkMusic.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Music, JukeBoxMusic>()
                .ForMember(x => x.PlayId, opt => opt.MapFrom(x => Guid.NewGuid()));
        }
    }
}