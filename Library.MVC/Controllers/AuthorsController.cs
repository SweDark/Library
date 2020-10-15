using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Domain;
using Library.Infrastructure.Persistence;
using Library.Application.Interfaces;
using Library.MVC.Models;

namespace Library.MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorservice;
        private readonly ILoanService loanservice;
        public AuthorsController(IAuthorService authorservice, ILoanService loanservice)
        {
            this.authorservice = authorservice;
            this.loanservice = loanservice;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string ex)
        {
            var vm = new AuthorIndexVm();

            if (!string.IsNullOrEmpty(ex))
            {
                vm.Ex = ex;
            }
            vm.Authors = authorservice.GetAllAuthors();
            return View(vm);
        }

        public IActionResult Create()
        {
            var vm = new AuthorCreateVm();
            return View(vm);
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                var newAuthor = new Author();
                newAuthor.Name = vm.Name;

                authorservice.AddAuthor(newAuthor);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }


        //// GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new AuthorDetailsVm();
                
                var author = authorservice.GetAuthorObject(id);
                vm.Name = author.Name;
                vm.Id = author.Id;
                vm.Books = author.Books;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
            
        }

        //// GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int id)/*int? id*/
        {
            if (ModelState.IsValid)
            {
                var vm = new AuthorEditVm();
                var author = authorservice.GetAuthorObject(id);
                vm.Name = author.Name;
                vm.Id = author.Id;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

        //// POST: Authors/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorEditVm vm)
        {
            if (ModelState.IsValid)
            {
                var editAuthor = new Author();
                editAuthor.Id = vm.Id;
                editAuthor.Name = vm.Name;
                authorservice.UpdateAuthor(editAuthor);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        //// GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new AuthorDeleteVm();
                var author = authorservice.GetAuthorObject(id);
                vm.Id = author.Id;
                vm.Name = author.Name;
                vm.Books = author.Books;
                vm.BookIds = vm.Books.Select(x => x.ID).ToList();
                
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }
        //// POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(AuthorDeleteVm vm)
        {
            if (ModelState.IsValid)
            {
                var deleteAuthor = new Author();
                deleteAuthor.Id = vm.Id;
                deleteAuthor.Name = vm.Name;
                

                try
                {
                    authorservice.DeleteAuthor(deleteAuthor, vm.BookIds,loanservice.GetAllCurrentLoans());
                    return RedirectToAction(nameof(Index));
                }
                catch (OperationCanceledException ex)
                {
                    var test = ex;
                    var message = ex.Message;

                    ViewBag.message = ("You probably tried to delete an author with a loaned book!");

                    return RedirectToAction(nameof(Index), new { ex = ViewBag.message });
                }
                //authorservice.DeleteAuthor(deleteAuthor);
            }
            return RedirectToAction("Error", "Home", "");

        }

    }
}
