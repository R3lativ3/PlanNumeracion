using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Models.ModelsYat;
using PlanNacionalNumeracion.Services;
using System;
using System.Collections.Generic;
using System.Data;

namespace PlanNacionalNumeracion.Controllers
{
    [ApiController]
    [Route("api/credencialesValidacionCarga")]
    public class CredencialesValidacionCargaController: Controller
    {
        [HttpGet]
        [Authorize(Roles = "General")]
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
        [Authorize(Roles = "General")]
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

    }
}
