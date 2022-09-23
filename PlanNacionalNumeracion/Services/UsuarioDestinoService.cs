using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PlanNacionalNumeracion.Common;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.UsuarioDestino;

public class UsuarioDestinoService
{
    public List<UsuarioDestino> ObtenerTodosUsuarioDestino()
    {
        string consulta = @"
                            Select id, usuario, psw, id_PNN_destino IdPNNDestino
                            From PNN_usuario_destino WITH(NOLOCK)";
        using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<UsuarioDestino> list = new List<UsuarioDestino>(); //opcion #1
            //var list = new List<UsuarioDestino>();             // opcion #2
            list = conn.Query<UsuarioDestino>(consulta).AsList();
            return list;
        }
    }


    public UsuarioDestino ObtenerUsuarioDestinoPorIdDestino(int id)
    {
        string consulta = @"
            SELECT id, usuario, psw, id_PNN_destino
            FROM PNN_usuario_destino WITH(NOLOCK)
            WHERE id_PNN_destino = @id
        ";
        using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            var resp = conn.QueryFirstOrDefault<UsuarioDestino>(consulta, new { id });
            return resp;
        }
    }

    public Response AgregarUsuarioDestino (UsuarioDestinoPost usuarioDestinoPost)
    {
        try
        {
            string query = @"
                            INSERT INTO PNN_usuario_destino (usuario, psw, id_PNN_destino)
                            VALUES (@usuario, @psw, @id_PNN_destino)";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@usuario", usuarioDestinoPost.Usuario);
                parameters.Add("@psw", usuarioDestinoPost.Psw);
                parameters.Add("@id_PNN_destino", usuarioDestinoPost.IdPNNDestino);
                conn.Execute(query, parameters);
                return new Response() { Status = 0, Message = "Se ha creado el registro exitosamente" };
            }
        }
        catch (Exception ex)
        {
            return new Response() { Status = 1, Message = ex.Message };
        }
    }
}