using System;
using Dapper;
using PlanNacionalNumeracion.Common;
using PlanNacionalNumeracion.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PlanNacionalNumeracion.Models.Destino;

namespace PlanNacionalNumeracion.Services
{
	public class DestinosService
	{
		public DestinosService()
		{
		}

        public List<Destino> ObtenerTodosDestinos()
        {
            try
            {
                string query = @"
                    SELECT id, nombre, ruta, ip, puerto
                    FROM PNN_destino WITH(NOLOCK)
                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var respuesta = conn.Query<Destino>(query);
                    return respuesta.AsList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Destino ObtenerDestino(int id)
        {
            try
            {
                string query = @"
                    SELECT id, nombre, ruta, ip, puerto
                    FROM PNN_destino WITH(NOLOCK)
                    WHERE id = @id
                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var respuesta = conn.QueryFirstOrDefault<Destino>(query, new { id });
                    return respuesta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Response AgregarDestino(DestinoPost destinoPost)
        {
            try
            {
                string insert = @"
                    INSERT INTO PNN_destino (nombre, ruta, ip, puerto)  OUTPUT INSERTED.*
                    VALUES (@nombre, @ruta, @ip, @puerto);
                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)              
                    {
                        conn.Open();
                    }
                    
                    int id = conn.ExecuteScalar<int>(insert, new {
                        nombre = destinoPost.Nombre,
                        ruta = destinoPost.Ruta,
                        ip = destinoPost.Ip,
                        puerto = destinoPost.Puerto
                    });
                    Response resp = new Response();
                    resp.Status = 0;
                    resp.Message = $"Creado satisfactoriamente ID: {id}";
                    return resp;
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }

        public Response ActualizarDestino(int id, DestinoPost destinoPost)
        {
            string update = @"
                UPDATE PNN_destino  
                SET nombre = @nombre, ruta = @ruta, ip = @ip, puerto = @puerto
                WHERE id = @id
            ";
            try
            {
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var updated = conn.Execute(update, new { nombre = destinoPost.Nombre, ruta = destinoPost.Ruta, ip = destinoPost.Ip, puerto = destinoPost.Puerto });
                    return new Response { Status = 0, Message = $"{updated} Registros Actualizados correctamente" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }

        public Response EliminarDestino(int id)
        {
            string update = @"
                DELETE FROM PNN_carga_destion where id_PNN_destino = @id;
                DELETE FROM PNN_credenciales_validacion_carga where id_PNN_destino = @id;
                DELETE FROM PNN_validacion_carga where id_PNN_destino = @id;
                DELETE FROM PNN_usuario_destino where id_PNN_destino = @id;
                DELETE FROM PNN_destino where id = @id;
            ";
            try
            {
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    var deleted = conn.Execute(update, new { id });
                    return new Response { Status = 0, Message = $"{deleted} Registros eliminados"};
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }
    }
}

