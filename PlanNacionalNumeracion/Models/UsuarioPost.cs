using System;
namespace PlanNacionalNumeracion.Models.Usuario
{
    public class UsuarioPost
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Attuid { get; set; }
        public string Psw { get; set; }
    }
}