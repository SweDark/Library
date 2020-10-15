using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class AuthorCreateVm
    {

        [Required]
        public string Name { get; set; }
        
    }
}
