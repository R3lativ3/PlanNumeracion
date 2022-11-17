using System;
namespace PlanNacionalNumeracion.Models.Destino
{
    public class Destino
    {
        public int Id { get; set; }
        public string Hostname { get; set; }
        public string Ruta { get; set; }
        public string Ip { get; set; }
        public string Puerto { get; set; }
        public DateTime FechaValidarBd { get; set; }
        public string Status { get; set; }
        public string Protocolo { get; set; }
        public string Crom { get; set; }
        public string Usuario { get; set; }
        public string Psw { get; set; }
    }
}

