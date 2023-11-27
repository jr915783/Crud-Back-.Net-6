using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Numero minímo de caracteres são 4 !")]
        public string Password { get; set; }
    }
}
