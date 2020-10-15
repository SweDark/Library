using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Returned { get; set; }
        public int MemberId { get; set; }
        public Member member { get; set; }
        public int bookCopyId { get; set; }
        public BookCopy bookCopy { get; set; }
    }
}
