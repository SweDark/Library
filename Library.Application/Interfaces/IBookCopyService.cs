using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IBookCopyService
    {
        /// <summary>
        /// Adds a copy for a book to the DB
        /// </summary>
        /// <param name="book">New values of book (Id is ignored)</param>
        void AddCopy(int amount, BookDetails book);
        /// <summary>
        /// Remove a copy
        /// </summary>
        /// <param name="id">Id of book to update</param>
        /// <param name="book">New values of book (Id is ignored)</param>
        void RemoveCopy(int amount, int id, ICollection<Loan> loanedBooks);

        ICollection<BookCopy> GetAllAvailableBooks(ICollection<Loan> loanedBooks);


    }
}
