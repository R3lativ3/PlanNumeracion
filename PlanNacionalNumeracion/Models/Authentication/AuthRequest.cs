using System;
using System.ComponentModel.DataAnnotations;

namespace PlanNacionalNumeracion.Models.Authentication
{
	public record AuthRequest
	{
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

