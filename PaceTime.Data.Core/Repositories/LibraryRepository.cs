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
            this._context = context;
        }

        public Author GetAuthor(Guid id)
        {
            return _context.Authors.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors
                           .OrderBy(a => a.FirstName)
                           .ThenBy(a => a.LastName);
        }

        public IEnumerable<Book> GetBooks(Guid authorId)
        {
            return _context.Books
                           .Where(b => b.AuthorId == authorId)
                           .OrderBy(b => b.Title)
                           .ThenBy(b => b.Author.FirstName);
        }

        public bool IsAuthorExists(Guid id)
        {
            return _context.Authors.Any(a => a.Id == id);
        }
    }
}
