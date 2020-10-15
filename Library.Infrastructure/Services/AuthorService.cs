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
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext context;

        public AuthorService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IList<Author> GetAllAuthors()
        {
            // Here we are NOT using .Include() so the authors books will NOT be loaded, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Authors.OrderBy(x => x.Name).ToList();
        }

        public string GetAuthor(int Id)
        {
            return context.Authors.Find(Id).Name;
        }

        public Author GetAuthorObject(int id)
        {
            return context.Authors.Include(y => y.Books).SingleOrDefault(x => x.Id == id);
        }

        public void AddAuthor(Author author)
        {
            context.Add(author);
            context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            context.Update(author);
            context.SaveChanges();
        }

        public void UpdateAuthor(int id, Author author)
        {
            context.Update(author);
            context.SaveChanges();
        }

        public void DeleteAuthor(Author author, List <int> bookids, ICollection<Loan> loanedBooks)
        {
            foreach(var id in bookids)
            {
                foreach(var loanedbook in loanedBooks)
                {
                    if(id == loanedbook.bookCopy.DetailsId)
                    {
                        throw new OperationCanceledException();
                    }
                }

            }
            context.Remove(author);
            context.SaveChanges();
        }

    }
}
