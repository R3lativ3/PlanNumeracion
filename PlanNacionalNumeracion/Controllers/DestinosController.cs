using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
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

        [HttpPost]
        public ActionResult<Response> AddFileToTranscription([FromForm]int id, IFormFile file)
        {
            try
            {
                var cargaService = new CargaService();
                var response = cargaService.CargarArchivoDestino(id, file);
                if(response.Status == 0)
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

