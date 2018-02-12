using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.API.Models
{
    public class AuthorForCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Genre { get; set; }

        public ICollection<BookForCreationDto> Books { get; set; }
        = new List<BookForCreationDto>();
    }
}
