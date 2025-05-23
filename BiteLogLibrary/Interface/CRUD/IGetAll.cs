﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Interface.CRUD
{
    public interface IGetAll<T> where T : class
    {
          Task<List<T>> GetAllAsync();
    }
}
