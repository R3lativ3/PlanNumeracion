using Dapper;
using Microsoft.Data.SqlClient;
using PlanNacionalNumeracion.Common;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Models.ModelsYat;
using System;
using System.Collections.Generic;
using System.Data;

namespace PlanNacionalNumeracion.Services
{
    public class CredencialesValidacionCargaService
    {
        public List<CredencialesValidacionCarga> ObtenerTodosCredencialesValidacionCarga()
        {
            try
            {
                string consulta = @"SELECT id, hostname,puerto,sid,tabla,id_PNN_destino idPnnDestino
                                   FROM PNN_credenciales_validacion_carga WITH(NOLOCK)";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var list = conn.Query<CredencialesValidacionCarga>(consulta);
                    return list.AsList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Response AgregarCredencialesValidacionCarga(CredencialesValidacionCargaPost credencialesValidacionCargaPost)
        {
            try
            {
                string insert = @"
                    INSERT INTO PNN_credenciales_validacion_carga (hostname, puerto, sid, tabla, id_PNN_destino)  
                    VALUES (@hostname, @puerto, @sid, @tabla, @id_PNN_destino);

                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString)) // utilizar la cadena de conexion que establecimos en el archivo Startup.cs
                {
                    if (conn.State == ConnectionState.Closed)                           // validar si la conexion esta cerrada, si lo esta, abrir la conexion
                    {
                        conn.Open();
                    }

                    int id = conn.ExecuteScalar<int>(insert, new
                    {
                        hostname = credencialesValidacionCargaPost.Hostname,
                        puerto = credencialesValidacionCargaPost.Puerto,
                        sid = credencialesValidacionCargaPost.Sid,
                        tabla = credencialesValidacionCargaPost.Tabla,
                        id_PNN_destino = credencialesValidacionCargaPost.IdPnnDestino

                    });
                    Response resp = new Response();
                    resp.Status = 0;
                    resp.Message = $"se a creado el registro exitosamente";
                    return resp;
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }

    }
   
}
