using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.Domain.Models
{
    public class Book : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
