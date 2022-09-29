using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.ValidacionCarga;
using System.Collections.Generic;
using System;
using PlanNacionalNumeracion.Models.UsuarioDestino;

namespace PlanNacionalNumeracion.Controllers
{


    [Route("api/validacionCarga")]
    [ApiController]
    public class ValidacionCargaController: Controller
    {

        public ValidacionCargaController()
        {
        }

        [HttpGet()]
        public ActionResult<List<ValidacionCarga>> ObtenerTodosValidacionCarga()
        {
            try
            {
                ValidacionCargaService validacionCargaService = new ValidacionCargaService();
                List<ValidacionCarga> response = validacionCargaService.ObtenerTodos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Response> AgregarValidacionCarga(ValidacionCargaPost validacionCargaPost)
        {
            try
            {
                ValidacionCargaService validacionCargaService = new ValidacionCargaService();
                var respuesta = validacionCargaService.AgregarValidacionCargar(validacionCargaPost);
                return respuesta;
            }
            catch(Exception ex)
            {
                return new Response() { Status = 1, Message = ex.Message };
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ValidacionCarga> GetValidacionCargaById(int id)
        {
            ValidacionCargaService validacionCargaService = new ValidacionCargaService();
            var respuesta = validacionCargaService.GetValidacionCarga(id);
            if (respuesta == null)
                return NotFound("Validaci�n Carga Inexistente");
            return Ok(respuesta);
        }
    }
    
}