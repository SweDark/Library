using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class LoanCurrentVm
    {
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public DateTime CurrentDate { get; set; }
    }
}
