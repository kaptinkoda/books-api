using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigation properties
        public List<Book_Author> Books_Authors { get; set; }
    }
}
