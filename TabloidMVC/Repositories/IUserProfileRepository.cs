﻿using TabloidMVC.Models;
using System.Collections.Generic;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        List<UserProfile> GetAllUserProfiles();

        UserProfile GetUserProfileById(int id);

        void UpdateUserProfile(UserProfile userProfile);

    }
}