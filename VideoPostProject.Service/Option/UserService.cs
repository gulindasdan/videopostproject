using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Model.Entities;
using VideoPostProject.Service.Base;

namespace VideoPostProject.Service.Option
{
    public class UserService : BaseService<User>
    {

        public bool CheckLogin(string email, string password)
        {
            return Any(x=>x.EmailAddress== email && x.Password==password);
        }

        public User FindByUsername(string username)
        {
            return GetByDefault(x => x.Username == username);
        }
        public User FindByEmail(string email)
        {
            return GetByDefault(x => x.EmailAddress == email);
        }

    }
}
