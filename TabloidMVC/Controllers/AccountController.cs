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

        public AccountController(IUserProfileRepository userProfileRepository, IUserTypeRepository userTypeRepository)
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
        public ActionResult Index()
        {
            var userProfiles = _userProfileRepository.GetAllUsers();
            return View(userProfiles);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(id);
            if (userProfile == null)
            {
                
               // userProfile = _postRepository.GetUserPostById(id, userId);
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

            UserTypeEditViewModel vm = new UserTypeEditViewModel()
            {
                UserProfile = _userProfileRepository.GetUserProfileById(id),
                UserTypes = userTypes
            };

            //if (owner == null)
            //{
            //    return NotFound();
            //}

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
        //// GET: UserProfileController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: UserProfileController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: UserProfileController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: UserProfileController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
    }
