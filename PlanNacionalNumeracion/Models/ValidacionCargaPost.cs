using System;
namespace PlanNacionalNumeracion.Models.ValidacionCarga
{
    public class ValidacionCargaPost
    {
        public DateTime FechaValidacion { get; set; }
        public string Estatus { get; set; }
        public string Comentario { get; set; }
        public int IdPNNCredencialesValidacionCarga { get; set; }
        public int IdPNNDestino { get; set; }
    }
}