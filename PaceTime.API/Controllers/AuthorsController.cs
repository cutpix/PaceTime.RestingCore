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
            var authorsFromRepo = _bookRepository.GetBooks().Select(x => x.Author).Distinct();

            var authorsDtoList = new List<AuthorDto>();

            foreach (var author in authorsFromRepo)
            {
                authorsDtoList.Add(new AuthorDto
                {
                    Id = author.Id,
                    FullName = $"{author.FirstName} {author.LastName}",
                    Genre = author.Genre,
                    Age = author.DateOfBirth.GetCurrentAge()
                });
            }

            return new JsonResult(authorsDtoList);
        }
    }
}
