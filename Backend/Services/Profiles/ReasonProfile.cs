using AutoMapper;
using DAL.Dto.Response;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class ReasonProfile:Profile
    {
        public ReasonProfile()
        {
            CreateMap<Reason, ReasonResponse>()
            .ForMember(dest =>
            dest.description,
            opt => opt.MapFrom(src => src.Description))
            .ForMember(dest =>
            dest.name,
            opt => opt.MapFrom(src => src.appUser.Name))
            .ForMember(dest =>
            dest.surname,
            opt => opt.MapFrom(src => src.appUser.Surname));
        }
    }
}
