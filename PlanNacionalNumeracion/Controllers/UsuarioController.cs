using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models.Usuario;
using PlanNacionalNumeracion.Models;
using System.Threading.Tasks;
using PlanNacionalNumeracion.Models.ModelsYat;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/usuarios")]
    [ApiController]

    public class UsuarioController : Controller
    {
        public UsuarioController() {
        }

        [HttpGet()]
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

        [HttpPost()]
        public ActionResult<Response> PostPrueba(UsuarioPost usuarioPost)
        {
            try
            {
                UsuarioService usuarioService = new UsuarioService();
                var respuesta = usuarioService.AgregarUsuario(usuarioPost);
                return respuesta;
            }
            catch (Exception ex)
            {
                return new Response() { Status = 1, Message = ex.Message };
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            UsuarioService usuario = new UsuarioService();
            var respuesta = usuario.GetUsuario(id);
            if (respuesta == null)
                return NotFound("Usuario Inexistente");
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public ActionResult<Response> PutUsuario(int id, [FromBody] UsuarioPost usuarioPost)
        {
            UsuarioService usuario = new UsuarioService();
            var respuesta = usuario.UpdateUsuario(id, usuarioPost);
            return Ok(respuesta);

        }

        [HttpDelete("{id}")]
        public ActionResult<Response> DeleteUsuario(int id)
        {
            UsuarioService usuarioService = new UsuarioService();
            var respuesta = usuarioService.DeleteUsuario(id);
            return Ok(respuesta);
        }
    }
}
    

    

