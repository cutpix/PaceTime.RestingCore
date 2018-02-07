using Microsoft.AspNetCore.Mvc;
using PaceTime.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaceTime.API.Controllers
{
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly ILibraryRepository _booksRepository;

        public BooksController(ILibraryRepository booksRepository)
        {
            this._booksRepository = booksRepository;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var booksFromRepo = _booksRepository.GetBooks();

            return new JsonResult(booksFromRepo);
        }
    }
}
