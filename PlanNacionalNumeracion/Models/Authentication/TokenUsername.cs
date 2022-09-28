using System;
namespace PlanNacionalNumeracion.Models.Authentication
{
	public record TokenUsername
	{
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}

