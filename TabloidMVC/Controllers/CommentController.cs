using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPostRepository _postRepo;
        private readonly ICategoryRepository _categoryRepo;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _commentRepo = commentRepository;
            _postRepo = postRepository;
            _categoryRepo = categoryRepository;
        }

        public ActionResult Index(int id)
        {
            Post post = _postRepo.GetPublishedPostById(id);
            List<Comment> comments = _commentRepo.GetAllPostComments(id);
            CommentViewModel vm = new CommentViewModel()
            {
                Comments = comments,
                Post = post
            };
            return View(vm);
        }
        /*
        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _commentRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
        */
    }
}