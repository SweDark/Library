using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class MemberDeleteVm
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
