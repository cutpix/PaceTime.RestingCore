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

        public void AddBookForAuthor(Guid authorId, Book bookEntity)
        {
            var author = GetAuthor(authorId);
            if (author != null)
                author.Books.Add(bookEntity);
        }

        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            return _context.Authors
                           .Where(x => authorIds.Contains(x.Id))
                           .OrderBy(x => x.FirstName)
                           .OrderBy(x => x.LastName)
                           .ToList();
        }
    }
}
