using System;
namespace PlanNacionalNumeracion.Models.Usuario
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Attuid { get; set; }
        public DateTime LastLog { get; set; }
        public string BlockByInact { get; set; }    
        public string Status { get; set; }
        public string Psw { get; set; }
    }
}