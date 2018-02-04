using System;

namespace PaceTime.Domain.Models
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
