using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookDetailsVm
    {
        [Required]
        public int Id { get; set; }
        
        public string ISBN { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Author")]
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }

        public ICollection<BookCopy> Copies { get; set; }
        //public int Copies { get; set; }
    }
}
