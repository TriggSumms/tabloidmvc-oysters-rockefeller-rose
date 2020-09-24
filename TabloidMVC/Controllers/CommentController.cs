using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
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

        public IActionResult Create(int id)
        {
            Comment comment = new Comment();
            comment.PostId = id;
            return View(comment);
        }

        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            try
            {
                comment.CreateDateTime = DateAndTime.Now;
                comment.UserProfileId = GetCurrentUserId();
                _commentRepo.Add(comment);

                return RedirectToAction("Index", new { id = comment.PostId });
            }
            catch
            {
                return View(comment);
            }
        }
        public ActionResult Edit(int id)
        {
            Comment comment = _commentRepo.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            try
            {
                _commentRepo.Update(comment);
                return RedirectToAction("Index", new { id = comment.PostId });
            }
            catch (Exception ex)
            {
                return View(comment);
            }
        }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}