using AutoMapper;
using CollegePick10V2.Entities;
using CollegePick10V2.Entities.Models;
using CollegePick10V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.Favorite, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.UnderDog, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.FavoriteAbbreviation, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.UnderdogAbbreviation, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.FavortieRank, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.UnderdogRank, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.FavoriteCssClass, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.UnderdogCssClass, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.OverCssClass, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.UnderCssClass, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.SpreadPushCssClass, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.OverUnderPushCssClass, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.SpreadPickType, opt => opt.Ignore());
            CreateMap<CurrentWeek, ScheduleModel>()
                .ForMember(dest => dest.OverUnderPickType, opt => opt.Ignore());
            CreateMap<CurrentWeek, GameArchive>()
                .ForMember(dest => dest.WeekNumber, opt => opt.Ignore());
            CreateMap<Pick, PickArchive>()
                .ForMember(dest => dest.WeekNumber, opt => opt.Ignore());
        }
    }
}
