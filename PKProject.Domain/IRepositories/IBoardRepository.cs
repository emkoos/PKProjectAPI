﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface IBoardRepository
    {
        Task<bool> BoardExist(Guid id);
    }
}
