using PaceTime.Domain.Models;
using System;
using System.Collections.Generic;

namespace PaceTime.Domain.Interfaces
{
    public interface ILibraryRepository
    {
        IEnumerable<Author> GetAuthors();
        IEnumerable<Book> GetBooks();
        Author GetAuthor(Guid id);
        bool IsAuthorExists(Guid id);
    }
}
