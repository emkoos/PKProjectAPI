﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO
{
    public class GetCommentsDto
    {
        public IEnumerable<GetCommentDto> Comments { get; set; }
    }
}
