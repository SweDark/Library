using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IAuthorService
    {
        /// <summary>
        /// Gets all Authors
        /// </summary>
        /// <returns>List of authors</returns>
        IList<Author> GetAllAuthors();
        string GetAuthor(int Id);
        public Author GetAuthorObject(int id);
        public void AddAuthor(Author author);
        public void UpdateAuthor(Author author);
        public void UpdateAuthor(int id, Author author);
        public void DeleteAuthor(Author author, List <int> bookids, ICollection<Loan> loanedBooks);
    }
}
