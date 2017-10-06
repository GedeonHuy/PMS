﻿using PMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
