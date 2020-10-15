using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class MemberDetailsVm
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public DateTime CurrentDate { get; set; }
    }
}
