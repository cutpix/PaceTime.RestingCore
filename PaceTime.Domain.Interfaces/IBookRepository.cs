using PaceTime.Domain.Models;
using System;
using System.Collections.Generic;

namespace PaceTime.Domain.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        void LoadRelatedEntities(Book currentEntity, string propertyName);
    }
}
