using PaceTime.Domain.Interfaces;
using PaceTime.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.Data.Core.Repositories
{
    public sealed class BookRepository : IBookRepository
    {
        public IEnumerable<Book> GetBooks()
        {
            throw new NotImplementedException();
        }
    }
}
