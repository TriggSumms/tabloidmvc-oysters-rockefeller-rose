using TabloidMVC.Models;
using System.Collections.Generic;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        List<UserProfile> GetAllUsers();

        UserProfile GetUserProfileById(int id);

        void UpdateUserProfile(UserProfile userProfile);

    }
}