
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class MemberCreateVm
    {
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter a valid Name!")]
        public string Name { get; set; }
        [MaxLength(30)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter a valid Email!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        

    }
}
