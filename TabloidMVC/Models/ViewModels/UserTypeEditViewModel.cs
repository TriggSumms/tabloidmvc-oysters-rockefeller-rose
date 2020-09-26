using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class UserTypeEditViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<UserType> UserTypeOptions{ get; set; }
    }
}
