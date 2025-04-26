using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Interface.CRUD
{
    public interface IGetById<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
    }
}
