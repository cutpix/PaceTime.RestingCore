using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.Domain.Models
{
    public class Author : BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Genre { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
