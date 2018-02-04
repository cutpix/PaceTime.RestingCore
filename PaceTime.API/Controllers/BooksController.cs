using Microsoft.AspNetCore.Mvc;
using PaceTime.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaceTime.API.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _booksRepository;

        public BooksController(IBookRepository booksRepository)
        {
            this._booksRepository = booksRepository;
        }

        public IActionResult GetBooks()
        {
            var booksFromRepo = _booksRepository.GetBooks();

            return new JsonResult(booksFromRepo);
        }
    }
}
