using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models.Usuario;
using PlanNacionalNumeracion.Models;
using System.Threading.Tasks;
using PlanNacionalNumeracion.Models.ModelsYat;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/usuarios")]
    [ApiController]

    public class UsuarioController : Controller
    {
        public UsuarioController() {
        }

        [HttpGet]
        //[Authorize(Roles = "General")]
        public ActionResult<List<Usuario>> ObtenerTodosUsuario()
        {
            try
            {
                UsuarioService usuarioService = new UsuarioService();
                List<Usuario> response = usuarioService.ObtenerTodos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            UsuarioService usuario = new UsuarioService();
            var respuesta = usuario.GetUsuario(id);
            if (respuesta == null)
                return NotFound("Usuario Inexistente");
            return Ok(respuesta);
        }

        [HttpPost]
        
        public ActionResult<Response> CrearUsuario(UsuarioPost usuarioPost)
        {
            try
            {
                UsuarioService usuarioService = new UsuarioService();
                var respuesta = usuarioService.AgregarUsuario(usuarioPost);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return new Response() { Status = 1, Message = ex.Message };
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> PutUsuario(int id, [FromBody] UsuarioPost usuarioPost)
        {
            try
            {
                UsuarioService usuario = new UsuarioService();
                var respuesta = usuario.UpdateUsuario(id, usuarioPost);
                return Ok(respuesta);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "General")]
        public ActionResult<Response> DeleteUsuario(int id)
        {
            try
            {
                UsuarioService usuarioService = new UsuarioService();
                var respuesta = usuarioService.DeleteUsuario(id);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }
    }
}
    

    

