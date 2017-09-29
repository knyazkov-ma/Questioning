using System.Collections.Generic;

namespace Questioning.Entity
{
    public class Profile: BaseEntity
    {
        public IEnumerable<ProfileItem> Items { get; set; }
    }
}
