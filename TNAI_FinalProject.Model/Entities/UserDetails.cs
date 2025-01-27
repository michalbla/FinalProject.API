using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI_FinalProject.Model.Entities
{
    public class UserDetails
    {
        public int Id { get; set; }
        public bool HasChilldren { get; set; }
        public int ChilldrenCount { get; set; }
        public bool IsHandicaped {  get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public double Payment { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public ICollection<Admin> Admins { get; set;}
    }
}
