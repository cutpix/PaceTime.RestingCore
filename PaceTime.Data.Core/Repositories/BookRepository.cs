using Microsoft.EntityFrameworkCore;
using PaceTime.Domain.Interfaces;
using PaceTime.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PaceTime.Data.Core.Repositories
{
    public sealed class BookRepository : IBookRepository
    {
        private KnowledgeContext _context;

        public BookRepository(KnowledgeContext context)
        {
            this._context = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return this._context.Books
                                .OrderBy(b => b.Title)
                                .ThenBy(b => b.Author.FirstName);
        }

        public void LoadRelatedEntities(Book currentEntity, string propertyName)
        {
            _context.Entry(currentEntity).Reference(propertyName).Load();
        }
    }
}
