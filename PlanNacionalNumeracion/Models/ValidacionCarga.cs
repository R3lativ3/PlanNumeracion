using System;
namespace PlanNacionalNumeracion.Models.ValidacionCarga
{
    public class ValidacionCarga
    {
        public int Id { get; set; }
        public DateTime FechaValidacion { get; set; }
        public string Estatus { get; set; }
        public string Comentario { get; set; }
        public int IdPNNCredencialesValidacionCarga { get; set; }
        public int IdPNNDestino { get; set; }
    }
}