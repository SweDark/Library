using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookCreateVm
    {
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Please enter valid ISBN Number (13 integers)")]
        [MinLength(13, ErrorMessage = "Please enter valid ISBN Number (13 integers)")]
        [MaxLength(13)]
        public string ISBN { get; set; }
        [Display(Name = "Title")]
        [MaxLength(30)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "String requires characters, it can't be empty.")]
        public string Title { get; set; }
        [Display(Name = "Author")]
        public SelectList AuthorList { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public string Description { get; set; }
    }
}
