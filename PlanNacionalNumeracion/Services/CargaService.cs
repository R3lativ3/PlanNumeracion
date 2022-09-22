using Microsoft.AspNetCore.Http;
using PlanNacionalNumeracion.Models;
using System;
using Renci.SshNet;
using System.IO;

namespace PlanNacionalNumeracion.Services
{
    public class CargaService
    {
        public Response CargarArchivoDestino(int idDestino, IFormFile archivo)
        {
            try
            {
                using (ScpClient client = new ScpClient("10.103.18.20", "fq4815", "Unidos_45_Yf"))
                {
                    client.Connect();

                    client.Upload(archivo.OpenReadStream(), "/home/fq4815/listas/"+archivo.FileName);
                    return new Response() { Status = 1, Message = $"Carga de archivo: {archivo.FileName} realizada exitosamente"};
                }
            }
            catch (Exception ex)
            {
                return new Response() { Status = 1, Message = $"{ex.Message}" };
            }
        }
    }
}
