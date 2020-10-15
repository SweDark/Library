using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class LoanDetailsVm
    {
        [Required]
        public int Id { get; set; }
        public Member member{ get; set; }
        public BookCopy bookCopy { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Returned { get; set; }
        public DateTime CurrentDate { get; set; }

    }
}
