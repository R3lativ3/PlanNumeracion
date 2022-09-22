using System;
namespace PlanNacionalNumeracion.Models
{
	public class Usuario
	{
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string attuid { get; set; }
        public string psw { get; set; }
    }
}

