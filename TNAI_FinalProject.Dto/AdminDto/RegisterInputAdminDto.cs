using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI_FinalProject.Dto.AdminDto
{
    public class RegisterInputAdminDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
