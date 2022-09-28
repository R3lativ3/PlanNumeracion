using System;
using Microsoft.AspNetCore.Http;

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

    public class UploadCargaDestino
    {
        public int[] destinos { get; set; }
        public IFormFile archivo { get; set; }
    }
}
