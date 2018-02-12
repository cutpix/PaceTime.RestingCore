﻿using PaceTime.Domain.Models;
using System;
using System.Collections.Generic;

namespace PaceTime.Domain.Interfaces
{
    public interface ILibraryRepository
    {
        bool IsAuthorExists(Guid id);
        Author GetAuthor(Guid id);
        IEnumerable<Author> GetAuthors();
        Book GetBookForAuthor(Guid authorId, Guid id);
        IEnumerable<Book> GetBooksForAuthor(Guid authorId);
        void AddAuthor(Author author);
        bool Save();
    }
}
