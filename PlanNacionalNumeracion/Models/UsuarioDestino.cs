using System;
namespace PlanNacionalNumeracion.Models.UsuarioDestino
{
    public class UsuarioDestino
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Psw { get; set; }
        public int IdPNNDestino { get; set; }
    }
}
