using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Interface.CRUD
{
    public interface IDelete<T> where T : class
    {
        Task<T> DeleteAsync(int id);
    }
}
