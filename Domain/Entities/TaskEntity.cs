using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class TaskEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Numero máximo de caracteres atingido !")]
        public string Title { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Numero máximo de caracteres atingido !")]
        public string Description { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }       
    }
}
