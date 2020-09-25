using TabloidMVC.Models;
using System.Collections.Generic;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
<<<<<<< HEAD
        List<UserProfile> GetAllUserProfiles();

        UserProfile GetUserProfileById(int id);

        void UpdateUserProfile(UserProfile userProfile);

=======
        UserProfile GetByCommentUserId(int id);
>>>>>>> f042b6374a002dfe2a4f8847fb7b4fb2784cfa8b
    }
}