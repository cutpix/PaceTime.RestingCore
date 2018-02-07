using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaceTime.API.Models;
using PaceTime.Domain.Interfaces;
using System;
using System.Collections.Generic;


namespace PaceTime.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository _bookRepository;

        public AuthorsController(ILibraryRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _bookRepository.GetAuthors();

            var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(Guid id)
        {
            var authorFromRepo = _bookRepository.GetAuthor(id);

            if (authorFromRepo == null)
                return NotFound();

            var author = Mapper.Map<AuthorDto>(authorFromRepo);
            return Ok(author);
        }
    }
}
