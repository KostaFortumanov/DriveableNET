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
    public class AllPostsProfile : Profile
    {
        public AllPostsProfile()
        {
            CreateMap<Post, PostResponse>()
                .ForMember(dest =>
                dest.title,
                opt => opt.MapFrom(src => src.Title))
                .ForMember(dest =>
                dest.price,
                opt => opt.MapFrom(src => src.Price))
                .ForMember(dest =>
                dest.horsepower,
                opt => opt.MapFrom(src => src.Horsepower))
                .ForMember(dest =>
                dest.isNew,
                opt => opt.MapFrom(src => src.IsNew))
                .ForMember(dest =>
                dest.date,
                opt => opt.MapFrom(src => src.Date))
                .ForMember(dest =>
                dest.color,
                opt => opt.MapFrom(src => src.Mileage))
                .ForMember(dest =>
                dest.id,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest =>
                dest.description,
                opt => opt.MapFrom(src => src.Description))
                .ForMember(dest =>
                dest.manufacturingYear,
                opt => opt.MapFrom(src => src.ManufacturingYear))
                .ForMember(dest =>
                dest.mileage,
                opt => opt.MapFrom(src => src.Mileage))
                .ForMember(dest =>
                dest.name,
                opt => opt.MapFrom(src => src.Creator.Name))
                .ForMember(dest =>
                dest.phoneNumber,
                opt => opt.MapFrom(src => src.Creator.PhoneNumber))
                .ForMember(dest =>
                dest.surname,
                opt => opt.MapFrom(src => src.Creator.Surname))
                .ForMember(dest =>
                dest.car,
                opt => opt.MapFrom(src => src.car));
        }
    }
}
