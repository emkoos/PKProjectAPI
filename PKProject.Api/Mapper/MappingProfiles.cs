using AutoMapper;
using PKProject.Api.DTO;
using PKProject.Api.DTO.Users;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BoardType, GetBoardTypeDto>();
            CreateMap<Comment, GetCommentDto>()
                .ForMember(dest => dest.Date, opt => opt.ConvertUsing(new DateFormatter(), src => src.Date)); ;
            CreateMap<Status, GetStatusDto>();
            CreateMap<Card, GetCardDto>()
                .ForMember(dest => dest.DeadlineDate, opt => opt.ConvertUsing(new DateFormatter(), src => src.DeadlineDate));
            CreateMap<Column, GetColumnDto>();
            CreateMap<Board, GetBoardDto>();
            CreateMap<Team, GetTeamDto>();
            CreateMap<User, GetUserDto>();
        }
    }

    public class DateFormatter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime source, ResolutionContext context)
               => source.ToString();
    }
}
