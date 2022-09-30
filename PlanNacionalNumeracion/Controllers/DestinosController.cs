using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Models.ModelsYat;
using PlanNacionalNumeracion.Services;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/destinos")]
    [ApiController]
    public class DestinosController : Controller
	{
		public DestinosController()
		{
		}

		[HttpGet]
        public ActionResult<List<Destino>> ObtenerTodosDestinos()
        {
            try
            {
                DestinosService destinoService = new DestinosService();
                List<Destino> response = destinoService.ObtenerTodosDestinos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para recibir los archivos que se van a enviar a los servidores
        /// </summary>
        /// <param name="destinos">Arreglo que contiene todos los id's de destino (para en base a ese id ir a buscar credenciales, etc)</param>
        /// <param name="archivo">Archivo que se va a depositar en los destinos</param>
        /// <returns>confirmacion si se realizo o no, ademas envia un email con el resultado</returns>
        [HttpPost("send-file")]
        public ActionResult<List<Response>> AddFileToTranscription([FromForm] UploadCargaDestino upload)
        {
            try
            {
                var cargaService = new CargaDestinoService();
                var response = cargaService.CargarArchivoDestino(upload.destinos, upload.archivo);



                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //agregar destinos a la  base de datos.
        [HttpPost("destino-Endpoint")]
        public ActionResult<Response> AddFileToTranscription(DestinoPost destinoPost)
        {
            try
            {
                DestinosService destinoService = new DestinosService();
                Response response = destinoService.AgregarDestino(destinoPost);
                if (response.Status == 0)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

