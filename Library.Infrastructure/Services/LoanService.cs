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
    public class LoanService : ILoanService
    {

        private readonly ApplicationDbContext context;

        public LoanService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddLoan(Loan loan)
        {
            context.Add(loan);
            context.SaveChanges();
        }

        public ICollection<Loan> GetAllLoans()
        {
            return context.Loans.Include(x => x.member).Include(x => x.bookCopy).Include(x => x.bookCopy.Details).OrderByDescending(x => x.LoanDate).ToList();
        }

        public ICollection<Loan> GetAllCurrentLoans()
        {
            return context.Loans.Include(x=> x.member).Include(x => x.bookCopy).Include(x => x.bookCopy.Details).Where(x => x.Returned == false).OrderByDescending(x => x.LoanDate).ToList();
        }
        public Loan GetLoan(int id)
        {
            Loan loan = new Loan();
            loan = context.Loans.Include(y => y.bookCopy.Details).Include(z => z.bookCopy.Details.Author).Include(x => x.member).SingleOrDefault(x => x.Id == id);
                //context.BookDetails.Include(y => y.Copies).SingleOrDefault(x => x.ID == id);
            return loan;
        }

        public ICollection<Loan> GetLoansByMember(int memberid)
        {
            return context.Loans.Include(x => x.bookCopy.Details).Include(x => x.bookCopy.Details.Author).Where(x => x.MemberId == memberid).OrderBy(x => x.LoanDate).ToList();
        }

        public ICollection<Loan> GetLoansByBookId(int bookid)
        {
            return context.Loans.Include(x => x.bookCopy.Details).Where(x => x.bookCopy.DetailsId == bookid).OrderBy(x => x.LoanDate).ToList();
        }

        public void UpdateLoan(Loan loan)
        {
            context.Update(loan);
            context.SaveChanges();
        }

        public void UpdateLoan(int id, Loan loan)
        {
            context.Update(loan);
            context.SaveChanges();
        }
    }
}
