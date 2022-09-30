using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models.UsuarioDestino;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Services;
using PlanNacionalNumeracion.Models.Usuario;

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
        public ActionResult<UsuarioDestino> GetUsuarioById(int id)
        {
            UsuarioDestinoService usuarioDestino = new UsuarioDestinoService();
            var respuesta = usuarioDestino.GetUsuarioDestino(id);
            if (respuesta == null)
                return NotFound("Usuario Destino Inexistente");
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public ActionResult<Response> PutUsuarioDestino(int id, [FromBody] UsuarioDestinoPost usuarioDestinoPost)
        {
            UsuarioDestinoService usuarioDestinoService = new UsuarioDestinoService();
            var respuesta = usuarioDestinoService.UpdateUsuarioDestino(id, usuarioDestinoPost);
            return Ok(respuesta);

        }

        [HttpDelete("{id}")]
        public ActionResult<Response> DeleteUsuarioDestino(int id)
        {
            UsuarioDestinoService usuarioDestinoService = new UsuarioDestinoService();
            var respuesta = usuarioDestinoService.DeleteUsuarioDestino(id);
            return Ok(respuesta);
        }
    }
}