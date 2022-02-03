using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Core.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

    }
}
