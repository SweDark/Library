using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class AuthorDeleteVm
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<BookDetails> Books { get; set; }
        public List<int> BookIds { get; set; }
    }
}
