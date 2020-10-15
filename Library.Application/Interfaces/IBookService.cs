using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Adds the book to DB
        /// </summary>
        /// <param name="book"></param>
        void AddBook(BookDetails book);

        /// <summary>
        /// Updates a book
        /// </summary>
        /// <param name="book"></param>
        void UpdateBookDetails(BookDetails book);

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="id">Id of book to update</param>
        /// <param name="book">New values of book (Id is ignored)</param>
        void UpdateBookDetails(int id, BookDetails book);
        /// <summary>
        /// Gets all books from the database
        /// </summary>
        /// <returns>list of books</returns>
        ICollection<BookDetails> GetAllBooks();
        BookDetails GetBook(int? id);

        void DeleteBookDetails(BookDetails book, ICollection<Loan> loanedBooks);
        ICollection<BookDetails> GetAllAvailableBooks(ICollection<Loan> loanedBooks);

        ICollection<BookDetails> GetBooksBySearch(string searchString);
    }
}
