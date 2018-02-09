using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaceTime.API.Models;
using PaceTime.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaceTime.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public BooksController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public IActionResult GetBooks(Guid authorId)
        {
            if (!_libraryRepository.IsAuthorExists(authorId))
                return NotFound();

            var booksFromRepo = _libraryRepository.GetBooks(authorId);

            var books = Mapper.Map<IEnumerable<BookDto>>(booksFromRepo);
            return Ok(books);
        }
    }
}
