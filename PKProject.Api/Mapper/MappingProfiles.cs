using AutoMapper;
using PKProject.Api.DTO;
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
            CreateMap<Comment, GetCommentDto>();
        }
    }
}
