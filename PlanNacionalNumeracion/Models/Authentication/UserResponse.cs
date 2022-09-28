using System;
namespace PlanNacionalNumeracion.Models.Authentication
{
	public record UserResponse
	{
        public bool Ok { get; set; }

        public string Message { get; set; }
        public TokenUsername User { get; set; }
    }
}

