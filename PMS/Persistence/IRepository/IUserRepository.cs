using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IUserRepository
    {
        ApplicationUser GetUserByEmail(string email);
    }
}
