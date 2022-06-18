using System.Collections.Generic;

namespace RazorLesson.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<UserRoles> UserRoles { get; set; }
    }
}
