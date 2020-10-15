using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Domain;
using Library.MVC.Models;
using Library.Application.Interfaces;

namespace Library.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookservice;
        private readonly IAuthorService authorService;
        private readonly IBookCopyService copyservice;
        private readonly ILoanService loanservice;
        public BooksController(IBookService bookservice, IAuthorService authorService, IBookCopyService copyservice, ILoanService loanservice)
        {
            this.bookservice = bookservice;
            this.authorService = authorService;
            this.copyservice = copyservice;
            this.loanservice = loanservice;
            
        }

        //GET: Books
        public async Task<IActionResult> Index(string ex, string searchString)
        {
            var vm = new BookIndexVm();
            if (!string.IsNullOrEmpty(ex))
            {
                vm.Ex = ex;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                try
                {
                    vm.Books = bookservice.GetBooksBySearch(searchString);
                    return View(vm);
                }
                catch (KeyNotFoundException Ex)
                {
                    var message = Ex.Message;
                    ViewBag.message = (message + "\nNo titles contains the search string '" + searchString + "'");
                    vm.Ex = ViewBag.message;
                    vm.Books = bookservice.GetAllBooks();
                    return View(vm);
                }
                
            } else
            {
                vm.Books = bookservice.GetAllBooks();
            }
            
            return View(vm);
        }

        public async Task<IActionResult> Available()
        {
            var vm = new BookAvailableVm();
            vm.Books = bookservice.GetAllAvailableBooks(loanservice.GetAllCurrentLoans());

            return View(vm);
        }

        //// GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new BookDetailsVm();
                var book = bookservice.GetBook(id);
                vm.Title = book.Title;
                vm.Description = book.Description;
                vm.ISBN = book.ISBN;
                vm.AuthorId = book.AuthorID;
                vm.Author = authorService.GetAuthor(book.AuthorID);
                vm.Id = book.ID;
                vm.Copies = book.Copies;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var vm = new BookCreateVm();
            vm.AuthorList = new SelectList(authorService.GetAllAuthors(), "Id", "Name");
            return View(vm);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Skapa ny bok
                var newBook = new BookDetails();
                newBook.AuthorID = vm.AuthorId;
                newBook.Description = vm.Description;
                newBook.ISBN = vm.ISBN;
                newBook.Title = vm.Title;

                bookservice.AddBook(newBook);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        //// GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)/*int? id*/
        {
            if (ModelState.IsValid)
            {
                var vm = new BookEditVm();
                var book = bookservice.GetBook(id);
                vm.Title = book.Title;
                vm.Description = book.Description;
                vm.ISBN = book.ISBN;
                vm.AuthorId = book.AuthorID;
                vm.ID = book.ID;
                vm.AuthorList = new SelectList(authorService.GetAllAuthors(), "Id", "Name");
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookEditVm vm)
        {
            if (ModelState.IsValid)
            {
                var editBook = new BookDetails();
                editBook.AuthorID = vm.AuthorId;
                editBook.Description = vm.Description;
                editBook.ISBN = vm.ISBN;
                editBook.Title = vm.Title;
                editBook.ID = vm.ID;
                bookservice.UpdateBookDetails(editBook);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        //// GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new BookDeleteVm();
                var book = bookservice.GetBook(id);
                vm.Title = book.Title;
                vm.Description = book.Description;
                vm.ISBN = book.ISBN;
                vm.AuthorId = book.AuthorID;
                vm.ID = book.ID;
                vm.Author = authorService.GetAuthor(book.AuthorID);
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }
        //// POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(BookDeleteVm vm)
        {
            if (ModelState.IsValid)
            {
                var deleteBook = new BookDetails();
                deleteBook.AuthorID = vm.AuthorId;
                deleteBook.Description = vm.Description;
                deleteBook.ISBN = vm.ISBN;
                deleteBook.Title = vm.Title;
                deleteBook.ID = vm.ID;
                try
                {
                    bookservice.DeleteBookDetails(deleteBook, loanservice.GetLoansByBookId(vm.ID));
                    return RedirectToAction(nameof(Index));
                }
                catch (OperationCanceledException ex)
                {
                    var test = ex;
                    var message = ex.Message;

                    ViewBag.message = ("You tried to delete a book that has books loaned out.");
                    return RedirectToAction(nameof(Index), new { ex = ViewBag.message });
                }
                
                
            }
            return RedirectToAction("Error", "Home", "");
            
        }
        public async Task<IActionResult> AddCopy(int id)/*int? id*/
        {
            if (ModelState.IsValid)
            {
                var vm = new BookCopyAddVm();
                var book = bookservice.GetBook(id);
                vm.Title = book.Title;
                vm.Description = book.Title;
                vm.ISBN = book.ISBN;
                vm.AuthorId = book.AuthorID;
                vm.Id = book.ID;
                vm.CurrentCopies = book.Copies.Count;
                vm.Author = authorService.GetAuthor(book.AuthorID);
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

            //// POST: Books/Edit/5
            //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddCopy(BookCopyAddVm vm)
            {
                if (ModelState.IsValid)
                {
                    var BookCopy = new BookDetails();
                    BookCopy.AuthorID = vm.AuthorId;
                    BookCopy.Description = vm.Description;
                    BookCopy.ISBN = vm.ISBN;
                    BookCopy.Title = vm.Title;
                    BookCopy.ID = vm.Id;
                    copyservice.AddCopy(vm.Amount, BookCopy);
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("Error", "Home", "");
            }

        //// GET: Books/Delete/5/Copies
        public async Task<IActionResult> RemoveCopy(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new BookCopyRemoveVm();
                var book = bookservice.GetBook(id);
                vm.Title = book.Title;
                vm.Description = book.Description;
                vm.ISBN = book.ISBN;
                vm.AuthorId = book.AuthorID;
                vm.Id = book.ID;
                vm.CurrentCopies = book.Copies.Count;
                vm.Author = authorService.GetAuthor(book.AuthorID);

                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }
        //// POST: Books/Delete/5/Copies
        [HttpPost, ActionName("RemoveCopy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCopiesConfirmed(BookCopyRemoveVm vm)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    copyservice.RemoveCopy(vm.Amount, vm.Id, loanservice.GetLoansByBookId(vm.Id));
                    return RedirectToAction(nameof(Index));
                }
                catch(InvalidOperationException ex)
                {
                    var test = ex;
                    var message = ex.Message;
                    // todo visa felmeddelandet
                    //return View("Error", new HandleErrorInfo(ex, "test1", "Test2"));
                    ViewBag.message = (ex.Message + ", You probably tried to delete a loaned book!");

                    //return RedirectToAction("Error", "home", message);
                    //return View();
                    
                    return RedirectToAction(nameof(Index), new { ex = ViewBag.message });
                    
                }

            }
            return RedirectToAction("Error", "Home", "");

        }
        //private bool BookDetailsExists(int id)
        //{
        //    return _context.BookDetails.Any(e => e.ID == id);
        //}
    }
 
}
