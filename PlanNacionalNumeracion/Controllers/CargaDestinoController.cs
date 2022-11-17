using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Models.ModelsYat;
using PlanNacionalNumeracion.Services;
using System;
using System.Collections.Generic;

namespace PlanNacionalNumeracion.Controllers
{
    [ApiController]
    [Route("api/cargaDestino")]
    public class CargaDestinoController: Controller
    {
        [HttpGet]
        [Authorize(Roles = "General")]
        public ActionResult<List<CargaDestino>> ObtenerTodosCargaDestino()
        {
            try
            {
                CargaDestinoService cargaDestinoService = new CargaDestinoService();
                List<CargaDestino> response = cargaDestinoService.ObtenerTodosCargaDestino();
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
        [Authorize(Roles = "General")]
        public ActionResult<List<Response>> AddFileToTranscription([FromForm] UploadCargaDestino upload)
        {
            try
            {
                var cargaService = new CargaDestinoService();
                var response = cargaService.CargarArchivoDestino(upload.destinos, upload.archivo,"jz073s");

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
