using BiteLogLibrary.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Repository
{
    public class LoginRepostory
    {

        private readonly IUserRepository _userRepository;

        public LoginRepostory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }




    }
}
