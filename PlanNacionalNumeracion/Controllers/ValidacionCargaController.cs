using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.ValidacionCarga;
using System.Collections.Generic;
using System;
using PlanNacionalNumeracion.Models.UsuarioDestino;
using PlanNacionalNumeracion.Models.Usuario;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/validacionCarga")]
    [ApiController]
    public class ValidacionCargaController: Controller
    {

        public ValidacionCargaController()
        {
        }

        [HttpGet]
        [Authorize(Roles = "General")]
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
        [Authorize(Roles = "General")]
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
        [Authorize(Roles = "General")]
        public ActionResult<ValidacionCarga> GetValidacionCargaById(int id)
        {
            try
            {
                ValidacionCargaService validacionCargaService = new ValidacionCargaService();
                var respuesta = validacionCargaService.GetValidacionCarga(id);
                if (respuesta == null)
                    return NotFound("Validaci�n Carga Inexistente");
                return Ok(respuesta);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> PutValidacionCarga(int id, [FromBody] ValidacionCargaPost validacionCargaPost)
        {
            try
            {
                ValidacionCargaService validacionCargaService = new ValidacionCargaService();
                var respuesta = validacionCargaService.UpdateValidacionCarga(id, validacionCargaPost);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> DeleteValidacionCarga(int id)
        {
            try
            {
                ValidacionCargaService validacionCargaService = new ValidacionCargaService();
                var respuesta = validacionCargaService.DeleteValidacionCarga(id);
                return Ok(respuesta);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }
    }
    
}