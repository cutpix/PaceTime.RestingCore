using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.Domain.Models
{
    public class Review : BaseEntity<Guid>
    {
        public string Description { get; set; }
        public int Rating { get; set; }
        public Guid BookId { get; set; }
    }
}
