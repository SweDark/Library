using Library.Application.Interfaces;
using Library.Domain;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class BookCopyService : IBookCopyService
    {        
        private readonly ApplicationDbContext context;

        public BookCopyService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddCopy(int amount, BookDetails book)
        {
            for(int i = 0; i < amount; i++)
            {
                BookCopy newBookCopy = new BookCopy();
                newBookCopy.DetailsId = book.ID;
                context.Copies.Add(newBookCopy);
                context.SaveChanges();
            }
            
        }

        public ICollection<BookCopy> GetAllBookCopies()
        {

            return context.Copies.Include(x => x.Details).OrderBy(x => x.Details.Title).ToList();
        }

        public ICollection<BookCopy> GetAllAvailableBooks(ICollection<Loan> loanedBooks)
        {
            //hämta lista av aktuella lån
            var allCopies = GetAllBookCopies();
            //hämta där id inte finns med i aktuella lån
            foreach (var loanedBook in loanedBooks)
            {
                allCopies.Remove(loanedBook.bookCopy);
            }
            return allCopies.OrderBy(x => x.Details.Title).ToList();
        }

        //Se över om vi tar bort för många kopior eller ej
        public void RemoveCopy(int amount, int id, ICollection<Loan> loanedBooks)
        {
            for(int i = 0; i < amount; i++)
            {
                BookCopy removeCopy = GetAvailableCopy(id, loanedBooks);
                //context.BookDetails.Find(id).Copies.Remove(removeCopy);
                    context.Copies.Remove(removeCopy);
                    context.SaveChanges();
            }
        }
        private BookCopy GetAvailableCopy(int id, ICollection<Loan> loanedBooks)
        {
            //todo: lägg till logik för att kolla att boken inte är utlånad
            var bookcopies = context.Copies.Where(x => x.DetailsId == id).ToList();
            foreach (var loanedBook in loanedBooks)
            {
                var copy = context.Copies.Where(x => x.Id == loanedBook.bookCopyId).Where(x => x.DetailsId == id).ToList().Last();
                bookcopies.Remove(copy);
                
            }
            return bookcopies.ToList().Last();
                //context.Copies.Where(x => x.DetailsId == id).Where.ToList().Last();
        }




    }

}
