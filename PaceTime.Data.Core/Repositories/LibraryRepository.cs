using Microsoft.EntityFrameworkCore;
using PaceTime.Domain.Interfaces;
using PaceTime.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaceTime.Data.Core.Repositories
{
    public sealed class LibraryRepository : ILibraryRepository
    {
        private KnowledgeContext _context;

        public LibraryRepository(KnowledgeContext context)
        {
            _context = context;
        }

        public bool IsAuthorExists(Guid id)
        {
            return _context.Authors
                           .Any(a => a.Id == id);
        }

        public Author GetAuthor(Guid id)
        {
            return _context.Authors
                           .FirstOrDefault(x => x.Id == id);
        }

        public Book GetBookForAuthor(Guid authorId, Guid id)
        {
            return _context.Books
                           .Where(b => b.AuthorId == authorId && b.Id == id)
                           .FirstOrDefault();
        }
        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors
                           .OrderBy(a => a.FirstName)
                           .ThenBy(a => a.LastName);
        }

        public IEnumerable<Book> GetBooksForAuthor(Guid authorId)
        {
            return _context.Books
                           .Where(b => b.AuthorId == authorId)
                           .OrderBy(b => b.Title)
                           .ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void AddAuthor(Author author)
        {
            //author.Id = Guid.NewGuid();
            _context.Authors.Add(author);

            //if (author.Books.Any())
            //{
            //    foreach (var book in author.Books)
            //        book.Id = Guid.NewGuid();
            //}
        }
    }
}
