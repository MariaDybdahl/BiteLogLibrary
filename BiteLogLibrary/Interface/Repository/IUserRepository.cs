using BiteLogLibrary.Interface.CRUD;
using BiteLogLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Interface.Repository
{
    public interface IUserRepository : IAdd<User>, IUpdate<User>, IDelete<User>, IGetAll<User>, IGetById<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
    }
}
