using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog40.Repository;
using Blog40.Models;
using Blog40.ViewModels;

namespace Blog40.Controllers
{
    public class AuthorController : Controller
    {
        private AuthorRepository authorRepo = new AuthorRepository();

        [HttpGet]
        public ActionResult All()
        {
            AuthorDetailsViewModel viewModel = new AuthorDetailsViewModel();
            viewModel.Authors = this.authorRepo.GetAll().ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AuthorEditViewModel viewModel = new AuthorEditViewModel();
            viewModel.Author = this.authorRepo.Get(id);
            return View(viewModel);
        }

        [HttpPut]
        public ActionResult Edit(AuthorEditViewModel viewModel)
        {
            Author author = viewModel.Author;
            this.authorRepo.Update(author);
            return RedirectToAction("All");
        }

    }
}
