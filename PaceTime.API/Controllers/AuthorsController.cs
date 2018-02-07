using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaceTime.API.Helpers;
using PaceTime.API.Models;
using PaceTime.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaceTime.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public AuthorsController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = UglyCodeForGettingAuthors();

            var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);

            return new JsonResult(authors);
        }

        private IEnumerable<Domain.Models.Author> UglyCodeForGettingAuthors()
        {
            var booksFromRepo = _bookRepository.GetBooks();

            foreach (var book in booksFromRepo)
                _bookRepository.LoadRelatedEntities(book, nameof(book.Author));

            var authors = booksFromRepo.Select(x => x.Author).Distinct();
            return authors;
        }
    }
}
