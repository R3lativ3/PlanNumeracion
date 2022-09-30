using System;
using Microsoft.AspNetCore.Http;

namespace PlanNacionalNumeracion.Models.ModelsYat
{
    public class CargaDestino
    {
        public int Id { get; set; }
        public DateTime FechaCarga { get; set; }
        public string NombreArchivo { get; set; }
        public string nombreDestino { get; set; }
        public string pathDestino { get; set; }
        public string ipDestino { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string attuid { get; set; }
    }

    public class UploadCargaDestino
    {
        public int[] destinos { get; set; }
        public IFormFile archivo { get; set; }
    }
}
