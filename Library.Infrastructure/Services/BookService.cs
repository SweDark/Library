using Library.Application.Interfaces;
using Library.Domain;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext context;

        public BookService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddBook(BookDetails book)
        {
            context.Add(book);
            context.SaveChanges();
        }

        public ICollection<BookDetails> GetAllBooks()
        {
            // Here we are using .Include() to eager load the author, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.BookDetails.Include(x => x.Author).Include(y => y.Copies).OrderBy(x => x.Title).ToList();
        }
        public ICollection<BookDetails> GetAllAvailableBooks(ICollection<Loan> loanedBooks)
        {
            var books = GetAllBooks();

            // prestanda problem?
            foreach(var loanedbook in loanedBooks)
            {
                foreach (var book in books)
                {
                    if(loanedbook.bookCopy.Details.ID == book.ID)
                    {
                        book.Copies.Remove(loanedbook.bookCopy);
                    }
                }
            }
            return books.Where(x => x.Copies.Count != 0).OrderBy(x => x.Title).ToList();
        }

        public BookDetails GetBook(int? id)
        {
            BookDetails book = new BookDetails();
            book = context.BookDetails.Include(y => y.Copies).SingleOrDefault(x => x.ID == id); 
            return book;
        }

        public void UpdateBookDetails(BookDetails book)
        {
            context.Update(book);
            context.SaveChanges();
        }

        public void UpdateBookDetails(int id, BookDetails book)
        {
            context.Update(book);
            context.SaveChanges();
        }
        
        public void DeleteBookDetails(BookDetails book, ICollection<Loan> loanedBooks)
        {
            if(loanedBooks.Count == 0)
            {
                context.Remove(book);
                context.SaveChanges();
            }
            else
            {
                throw new OperationCanceledException();
            }
            
        }

        public ICollection<BookDetails> GetBooksBySearch(string searchString)
        {
            var search = context.BookDetails.Where(s => s.Title.Contains(searchString)).Include(x => x.Copies).Include(x => x.Author).OrderBy(x => x.Title).ToList();
            if(search.Count > 0)
            {
                return search;
            } else
            {
                throw new KeyNotFoundException();
            }
            
        }
    }
}
