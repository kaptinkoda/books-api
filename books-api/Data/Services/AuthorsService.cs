using books_api.Data.Models;
using books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public Author AddAuthor(AuthorVM author)
        {
            var _author = new Author
            {
                FullName = author.FullName
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();

            return _author;
        }

        public List<Author> GetAuthors() => _context.Authors.ToList();
        public Author GetAuthor(int authorId) => _context.Authors.FirstOrDefault(x => x.Id == authorId);

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(x => x.Id == authorId).Select(n => new AuthorWithBooksVM
            {
                FullName = n.FullName,
                BookTitles = n.Books_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }

    }
}
