using System;
using Microsoft.AspNetCore.Http;

namespace PlanNacionalNumeracion.Models.ModelsYat
{
    public class CargaDestino
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumeroRegistro { get; set; }
        public string PesoArchivo { get; set; }
        public DateTime FechaCarga { get; set; }
        public string FormatoArchivo { get; set; }
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
