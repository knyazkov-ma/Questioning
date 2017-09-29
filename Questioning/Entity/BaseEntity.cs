using System;

namespace Questioning.Entity
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<String>
    {
        
    }
}
