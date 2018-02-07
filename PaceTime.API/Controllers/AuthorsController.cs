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

            return new JsonResult(authors);
        }
    }
}
