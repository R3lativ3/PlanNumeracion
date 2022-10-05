using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models.UsuarioDestino;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Services;
using PlanNacionalNumeracion.Models.Usuario;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/usuariosDestinos")]
    [ApiController]
    public class UsuarioDestinoController : Controller
    {
        public UsuarioDestinoController()
        {
        }

        [HttpGet]
        //[Authorize(Roles = "General")]
        public ActionResult<List<UsuarioDestino>> ObtenerTodosUsuarioDestino()
        {
            try
            {
                UsuarioDestinoService usuarioDestinoService = new UsuarioDestinoService();
                List<UsuarioDestino> response = usuarioDestinoService.ObtenerTodosUsuarioDestino();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "General")]
        public ActionResult<Response> AddUsuarioDestino(UsuarioDestinoPost usuarioDestinoPost)
        {
            try
            {
                UsuarioDestinoService usuarioDestinoService = new UsuarioDestinoService();
                var respuesta = usuarioDestinoService.AgregarUsuarioDestino(usuarioDestinoPost);
                return respuesta;
            }
            catch(Exception ex)
            {
                return new Response() { Status = 1, Message = ex.Message };
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<UsuarioDestino> GetUsuarioById(int id)
        {
            try
            {
                UsuarioDestinoService usuarioDestino = new UsuarioDestinoService();
                var respuesta = usuarioDestino.GetUsuarioDestino(id);
                if (respuesta == null)
                    return NotFound("Usuario Destino Inexistente");
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpGet("destino/{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<UsuarioDestino> GetUsuarioByIdDestino(int id)
        {
            try
            {
                UsuarioDestinoService usuarioDestino = new UsuarioDestinoService();
                var respuesta = usuarioDestino.ObtenerUsuarioDestinoPorIdDestino(id);
                if (respuesta == null)
                    return NotFound("Usuario Destino Inexistente");
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> PutUsuarioDestino(int id, [FromBody] UsuarioDestinoPost usuarioDestinoPost)
        {
            try
            {
                UsuarioDestinoService usuarioDestinoService = new UsuarioDestinoService();
                var respuesta = usuarioDestinoService.UpdateUsuarioDestino(id, usuarioDestinoPost);
                return Ok(respuesta);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> DeleteUsuarioDestino(int id)
        {
            try
            {
                UsuarioDestinoService usuarioDestinoService = new UsuarioDestinoService();
                var respuesta = usuarioDestinoService.DeleteUsuarioDestino(id);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }
    }
}