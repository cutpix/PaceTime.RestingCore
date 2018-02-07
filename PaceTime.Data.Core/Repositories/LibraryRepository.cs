using Microsoft.EntityFrameworkCore;
using PaceTime.Domain.Interfaces;
using PaceTime.Domain.Models;
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

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors
                           .OrderBy(a => a.FirstName)
                           .ThenBy(a => a.LastName);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books
                           .OrderBy(b => b.Title)
                           .ThenBy(b => b.Author.FirstName);
        }
    }
}
