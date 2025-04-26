using BiteLogLibrary.DTO;
using BiteLogLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Interface.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterRequest registerRequest);
    }
}
