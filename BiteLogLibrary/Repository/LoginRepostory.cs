using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Repository
{
    public class LoginRepostory
    {

        private readonly UserRepository _userRepository;

        public LoginRepostory(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }




    }
}
