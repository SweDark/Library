using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class AuthorIndexVm
    {
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public string Ex { get; set; }
    }
}
