using TabloidMVC.Models;
using System.Collections.Generic;

namespace TabloidMVC.Repositories
{
    public interface IUserTypeRepository
    {

        List<UserType> GetAllUserTypes();
    }
}