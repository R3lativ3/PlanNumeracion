using System.Collections;
using System.Collections.Generic;
using PlanNacionalNumeracion.Common;
using System.Data;
using PlanNacionalNumeracion.Models.ValidacionCarga;
using System.Data.SqlClient;
using Dapper;
using System;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Usuario;

public class ValidacionCargaService
{
    public List<ValidacionCarga> ObtenerTodos()
    {
        string consulta = @"
            SELECT id, fecha_validacion FechaValidacion, estatus, comentario, id_PNN_credenciales_validacion_carga as IdPNNCredencialesValidacionCarga, id_PNN_Destino as IdPNNDestino
            FROM PNN_validacion_carga";
        using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<ValidacionCarga> list = new List<ValidacionCarga>(); //opcion #1
            //var list = new List<ValidacionCarga>();             // opcion #2
            list = conn.Query<ValidacionCarga>(consulta).AsList();
            return list;
        }
    }

    public Response AgregarValidacionCargar(ValidacionCargaPost validacionCargaPost)
    {
        try
        {
            string query = @"
                            INSERT INTO PNN_validacion_carga (fecha_validacion, estatus, comentario, id_PNN_credenciales_validacion_carga, id_PNN_destino)
                            VALUES (@fecha_validacion, @estatus, @comentario, @id_PNN_credenciales_validacion_carga, @id_PNN_destino)";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DynamicParameters paramateres = new DynamicParameters();
                paramateres.Add("@fecha_validacion", validacionCargaPost.FechaValidacion);
                paramateres.Add("@estatus", validacionCargaPost.Estatus);
                paramateres.Add("@comentario", validacionCargaPost.Comentario);
                paramateres.Add("@id_PNN_credenciales_validacion_carga", validacionCargaPost.IdPNNCredencialesValidacionCarga);
                paramateres.Add("@id_PNN_destino", validacionCargaPost.IdPNNDestino);
                conn.Execute(query, paramateres);
            }

            return new Response() { Status = 0, Message = "Se ha creado el registro exitosamente" };
        }

        catch (Exception ex)
        {
            return new Response() { Status = 1, Message = ex.Message };
        }
    }

    public ValidacionCarga GetValidacionCarga(int id)
    {
        try
        {
            string query = @"
            SELECT id, fecha_validacion FechaValidacion, estatus, comentario, id_PNN_credenciales_validacion_carga as IdPNNCredencialesValidacionCarga, id_PNN_Destino as IdPNNDestino
            FROM PNN_validacion_carga
            WHERE id = @id" ;
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var validacionCarga = conn.QueryFirstOrDefault<ValidacionCarga>(query, new { id = id });
                return validacionCarga;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public Response UpdateValidacionCarga(int id, ValidacionCargaPost validacionCargaPost)
    {
        string update = @"
                UPDATE PNN_validacion_carga 
                SET fecha_validacion = @fecha_validacion, estatus = @estatus, comentario = @comentario,  id_PNN_credenciales_validacion_carga = @id_PNN_credenciales_validacion_carga, id_PNN_destino = @id_PNN_destino
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
                var updated = conn.Execute(update, new
                {
                    fecha_validacion = validacionCargaPost.FechaValidacion,
                    estatus = validacionCargaPost.Estatus,
                    comentario = validacionCargaPost.Comentario,
                    id_PNN_credenciales_validacion_carga = validacionCargaPost.IdPNNCredencialesValidacionCarga,
                    id_PNN_destino = validacionCargaPost.IdPNNDestino,
                    id
                });
                return new Response { Status = 0, Message = "Actualizado correctamente" };
            }
        }
        catch (Exception ex)
        {
            return new Response { Status = 1, Message = ex.Message };
        }
    }

    public Response DeleteValidacionCarga(int id)
    {

        try
        {
            string query = @"DELETE FROM PNN_validacion_carga WHERE id = @id";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                var delete = conn.Execute(query, new { id });
                return new Response { Status = 0, Message = "Usuario Eliminado Correctamente" };
            }
        }
        catch (Exception ex)
        {
            return new Response { Status = 1, Message = ex.Message };
        }
    }

}
