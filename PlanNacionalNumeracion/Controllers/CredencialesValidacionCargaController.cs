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
    [Route("api/credencialesValidacionCarga")]
    public class CredencialesValidacionCargaController: Controller
    {
        [HttpGet]
        public ActionResult<List<CredencialesValidacionCarga>> ObtenerTodosCredencialesValidacionCarga ()
        {
            try
            {
                CredencialesValidacionCargaService credencialesValidacionCargaService = new CredencialesValidacionCargaService();
                List<CredencialesValidacionCarga> response = credencialesValidacionCargaService.ObtenerTodosCredencialesValidacionCarga();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }
        [HttpPost]
        public ActionResult<Response> AddFileToTranscription(CredencialesValidacionCargaPost credencialesValidacionCargaPost)
        {
            try
            {
                CredencialesValidacionCargaService credencialesValidacionCargaService = new CredencialesValidacionCargaService();
                Response response = credencialesValidacionCargaService.AgregarCredencialesValidacionCarga(credencialesValidacionCargaPost);
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
        /*
        [HttpPost]
        public ActionResult<string> PostCredencialesValidacionCarga(string nombre)
        {
            return "prueba " + nombre;
        }
        */
        [HttpPut]
        public ActionResult<string> PutCredencialesValidacionCarga(CredencialesValidacionCargaPost nombre)
        {
            return "prueba " + nombre;
        }

        [HttpDelete]
        public ActionResult<string> DeleteCredencialesValidacionCarga(string nombre)
        {
            return "prueba " + nombre;
        }
    }
}
