using System;
namespace PlanNacionalNumeracion.Models.Authentication
{
	public class UserLogin
	{
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Attuid { get; set; }
        public string Password { get; set; }
    }
}

