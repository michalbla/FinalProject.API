using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI_FinalProject.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }

        public virtual RoleUser Role { get; set; }

        public virtual ICollection<UserDetails> Details { get; set; }
    }
}
