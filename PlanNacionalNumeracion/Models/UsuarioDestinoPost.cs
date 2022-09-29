using System;
using System.ComponentModel.DataAnnotations;

namespace PlanNacionalNumeracion.Models.UsuarioDestino
{
    public class UsuarioDestinoPost
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Psw { get; set; }
        [Required]
        public int IdPNNDestino { get; set; }
    }
}
