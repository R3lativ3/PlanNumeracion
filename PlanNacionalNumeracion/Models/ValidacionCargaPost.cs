using System;
using System.ComponentModel.DataAnnotations;

namespace PlanNacionalNumeracion.Models.ValidacionCarga
{
    public class ValidacionCargaPost
    {
        public DateTime FechaValidacion { get; set; }
        [Required]
        public string Estatus { get; set; }
        public string Comentario { get; set; }
        [Required]
        public int IdPNNCredencialesValidacionCarga { get; set; }
        [Required]
        public int IdPNNDestino { get; set; }
    }
}