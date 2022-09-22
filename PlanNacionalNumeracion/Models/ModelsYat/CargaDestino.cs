using System;

namespace PlanNacionalNumeracion.Models.ModelsYat
{
    public class CargaDestino
    {
        public int Id { get; set; }
        public DateTime FechaCarga { get; set; }
        public string NombreArchivo { get; set; }
        public int IdPnnDestino { get; set; }
        public int IdPnnUsuario { get; set; }

    }
}
