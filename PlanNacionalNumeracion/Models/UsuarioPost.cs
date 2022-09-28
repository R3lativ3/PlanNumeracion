using System;
using System.ComponentModel.DataAnnotations;

namespace PlanNacionalNumeracion.Models.Usuario
{
    public class UsuarioPost
    {
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [Required]
        public string Attuid { get; set; }
        [Required]
        public string Psw { get; set; }
    }
}