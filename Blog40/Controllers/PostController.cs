using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog40.Models;
using Blog40.Repository;
using Blog40.ViewModels;
using AutoMapper;

namespace Blog40.Controllers
{
    public class PostController : Controller
    {
        private PostRepository postRepo = new PostRepository();

        [HttpGet]
        public ActionResult Index()
        {
            AuthorRepository authorRepo = new AuthorRepository();
            CategoryRepository catRepo = new CategoryRepository();
            PostEditViewModel viewModel = new PostEditViewModel();
            viewModel.Authors = authorRepo.GetAll().ToList();
            viewModel.Categories = catRepo.GetAll().ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PostEditViewModel viewModel)
        {
            Post post = viewModel.Post;
            postRepo.Add(post);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult All()
        {
            PostDetailsViewModel postDetails = new PostDetailsViewModel();
            PostRepository repo = new PostRepository();
            postDetails.Posts = repo.GetAll().ToList();
            return View(postDetails);
        }
            
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PostEditViewModel editPost = new PostEditViewModel();
            editPost.Post = this.postRepo.Get(id);
            CategoryRepository catRepo = new CategoryRepository();
            editPost.Categories = catRepo.GetAll().ToList();
            AuthorRepository authorRepo = new AuthorRepository();
            editPost.Authors = authorRepo.GetAll().ToList();
            return View(editPost);
        }

        [HttpPut]
        public ActionResult Edit(PostEditViewModel viewModel)
        {
            Post model = new Post();
            model.Author.AuthorId = viewModel.Post.Author.AuthorId;
            model.Category.CategoryId = viewModel.Post.Category.CategoryId;
            model.Title = viewModel.Post.Title;
            model.Content = viewModel.Post.Content;
            model.PostId = viewModel.Post.PostId;
            int rowsAffected = this.postRepo.Update(model);
            return RedirectToAction("All");
        }
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            int recordsAffected = this.postRepo.Delete(Id);
            return RedirectToAction("All");
        }

        [HttpDelete]
        public ActionResult UndeleteAll()
        {
            this.postRepo.UndeleteAll();
            return RedirectToAction("All");
        }
    }
}
