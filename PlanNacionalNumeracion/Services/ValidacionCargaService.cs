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
            Select id, fecha_validacion FechaValidacion, estatus, comentario, id_PNN_credenciales_validacion_carga IdCredencialesValidacionCarga, id_PNN_Destino IdDestino
            From PNN_validacion_carga";
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
}
