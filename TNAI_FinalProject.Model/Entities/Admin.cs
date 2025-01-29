using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI_FinalProject.Model.Entities
{
    public class Admin
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? PasswordHash { get; set; }   //zahashowane

        public virtual ICollection<User> Admin_Users { get; set; } = new List<User>();

        public virtual ICollection<UserDetails> Admin_Details { get; set; }


    }
}
