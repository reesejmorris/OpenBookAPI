using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Models
{
    public class Author
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Image { get; set; }
    }
}
