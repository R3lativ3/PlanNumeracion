using System;

namespace PlanNacionalNumeracion.Models.ModelsYat
{
    public class CargaDestinoPost
    {
        public DateTime FechaCarga { get; set; }
        public string NombreArchivo { get; set; }
        public int IdPnnDestino { get; set; }
        public int IdPnnUsuario { get; set; }
    }
}
