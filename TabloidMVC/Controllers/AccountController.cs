using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;


namespace TabloidMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public AccountController(
            IUserProfileRepository userProfileRepository,
            IUserTypeRepository userTypeRepository)
        {
            _userProfileRepository = userProfileRepository;
            _userTypeRepository = userTypeRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            var userProfile = _userProfileRepository.GetByEmail(credentials.Email);

            if (userProfile == null)
            {
                ModelState.AddModelError("Email", "Invalid email");
                return View();
            }
            else if (userProfile.IsActive == false)
            {
                ModelState.AddModelError("Email", "User is Inactive");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userProfile.Id.ToString()),
                new Claim(ClaimTypes.Email, userProfile.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        /// <summary>
        /// THIS BEGINS THE USER PROFILE CONTROLLER VIEWS
        /// </summary>
        /// <returns></returns>
        /// 

        public IActionResult Index()
        {
            List<UserProfile> userProfiles = _userProfileRepository.GetAllUserProfiles();
            List<UserProfile> activeUsers = new List<UserProfile>();
            
                foreach(var user in userProfiles)
                {
                    if (user.IsActive == true)
                    {
                    activeUsers.Add(user);
                    }
                };

            return View(activeUsers);
        }

        public IActionResult Inactive()
        {
            List<UserProfile> userProfiles = _userProfileRepository.GetAllUserProfiles();
            List<UserProfile> inactiveUsers = new List<UserProfile>();

            foreach (var user in userProfiles)
            {
                if (user.IsActive == false)
                {
                    inactiveUsers.Add(user);
                }
            };

            return View(inactiveUsers);
        }



        public ActionResult Details(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(id);
            if (userProfile == null)
            {

                
                if (userProfile == null)
                {
                    return NotFound();
                }
            }
            return View(userProfile);
        }






        public ActionResult Edit(int id)
        {
            List<UserType> userTypes = _userTypeRepository.GetAllUserTypes();

            
            UserProfile userProfile = _userProfileRepository.GetUserProfileById(id);


            UserTypeEditViewModel vm = new UserTypeEditViewModel()
            {
                UserProfile = userProfile,
                
                UserTypeOptions = userTypes

            };
            if (vm.UserProfile == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfile userProfile)
        {
            try
            {
               
                _userProfileRepository.UpdateUserProfile(userProfile);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(userProfile);
            }
        }
        public ActionResult Deactivate(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetUserProfileById(id);
            userProfile.IsActive = false;

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(UserProfile userProfile)
        {
            try
            {
                
                _userProfileRepository.UpdateUserProfile(userProfile);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(userProfile);
            }
        }
        public ActionResult Reactivate(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetUserProfileById(id);
            userProfile.IsActive = true;

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(UserProfile userProfile)
        {
            try
            {

                _userProfileRepository.UpdateUserProfile(userProfile);

                return RedirectToAction("Inactive");
            }
            catch (Exception)
            {
                return View(userProfile);
            }
        }

    }
}

