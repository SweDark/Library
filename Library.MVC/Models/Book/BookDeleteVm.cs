using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookDeleteVm
    {
        [Required]
        public int ID { get; set; }
        public string ISBN { get; set; }
        [Display(Name = "Title")]
        [MaxLength(15)]
        public string Title { get; set; }
        [Display(Name = "Author")]
        public string  Author { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
    }
}
