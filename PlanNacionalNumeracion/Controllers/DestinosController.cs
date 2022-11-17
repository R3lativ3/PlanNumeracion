using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Models.ModelsYat;
using PlanNacionalNumeracion.Models.UsuarioDestino;
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
       // [Authorize(Roles = "General")]
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
        [Authorize(Roles = "General")]
        public ActionResult<Response> AgregarDestino(DestinoPost destinoPost)
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


        [HttpPut("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> ActualizarDestino(int id, DestinoPost destinoPost)
        {
            try
            {
                DestinosService destinoService = new DestinosService();
                Response response = destinoService.ActualizarDestino(id,destinoPost);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> EliminarDestino(int id, DestinoPost destinoPost)
        {
            try
            {
                DestinosService destinoService = new DestinosService();
                Response response = destinoService.EliminarDestino(id);
                if (response.Status == 0)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<UsuarioDestino> GetDestinoById(int id)
        {
            try
            {
                DestinosService destinosService = new DestinosService();
                var respuesta = destinosService.GetDestino(id);
                if (respuesta == null)
                    return NotFound("Destino Inexistente");
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }
    }
}

