
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class LoanCreateVm
    {

        public SelectList MemberList { get; set; }
        [Required]
        public int MemberId { get; set; }
        public SelectList AvailableBooksList { get; set; }
        [Required]
        public int BookCopyId { get; set; }
        public DateTime LoanDate { get; set; }


    }
}
