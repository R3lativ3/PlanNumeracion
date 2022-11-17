using System;

namespace PlanNacionalNumeracion.Models.ModelsYat
{
    public class CargaDestinoPost
    {
        public string Name { get; set; }
        public string NumeroRegistro { get; set; }
        public string PesoArchivo { get; set; }
        public DateTime FechaCarga { get; set; }
        public string FormatoArchivo { get; set; }
        public int IdPnnDestino { get; set; }
        public int IdPnnUsuario { get; set; }
    }
}
