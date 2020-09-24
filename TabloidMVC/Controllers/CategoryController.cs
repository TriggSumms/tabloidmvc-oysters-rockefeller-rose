using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System.Collections.Generic;
using System;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepo;

        public CategoryController( ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }
        // GET: CategoryContoller
        public ActionResult Index()
        {
            List<Category> category = _categoryRepo.GetAll();
            return View(category);
        }



        // GET: CategoryContoller/Details/5
        public ActionResult Details(int id)
        {
            Category category = _categoryRepo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //GET: CategoryContoller/Create

        public ActionResult Create()
        {
            

            CategoryFormViewModel vm = new CategoryFormViewModel()
            {
                Category = new Category(),
                
            };

            return View(vm);
        }
        //CHP 5 END

        // POST: CategoryContoller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRepo.AddCategory(category);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }
        // GET: CategoryContoller/Delete/5
        public ActionResult Delete(int id)
        {
           Category category = _categoryRepo.GetCategoryById(id);
            return View(category);
        }

        // POST: CategoryContoller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
             _categoryRepo.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepo.UpdateCategory(category);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }


    }
}
