using System;
using System.ComponentModel.DataAnnotations;

namespace PlanNacionalNumeracion.Models.Destino
{
	public class DestinoPost
	{
		public string Hostname { get; set; }
        public string Ruta { get; set; }
        public string Ip { get; set; }
        public string Puerto { get; set; }
        public DateTime FechaValidarBd { get; set; }
        public string Status { get; set; }
        public string Protocolo { get; set; }
        public string Crom { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Psw { get; set; }
    }
}

