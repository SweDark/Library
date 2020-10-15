using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        ICollection<Loan> GetAllLoans();
        ICollection<Loan> GetAllCurrentLoans();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of the loan you wish to view</param>
        public Loan GetLoan(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loan">loan that you wish to add to the list</param>
        public void AddLoan(Loan loan);

        public ICollection<Loan> GetLoansByMember(int memberid);
        public ICollection<Loan> GetLoansByBookId(int bookid);
        void UpdateLoan(Loan loan);

        void UpdateLoan(int id, Loan loan);
    }
}
