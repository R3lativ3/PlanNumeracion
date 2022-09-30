using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using EncriptadorMA;
using PlanNacionalNumeracion.Common;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Usuario;
using PlanNacionalNumeracion.Models.UsuarioDestino;

public class UsuarioDestinoService
{
    public List<UsuarioDestino> ObtenerTodosUsuarioDestino()
    {
        string consulta = @"
            Select id, usuario, psw, id_PNN_destino as IdPNNDestino
            From PNN_usuario_destino WITH(NOLOCK)
        ";
        using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            var list = conn.Query<UsuarioDestino>(consulta).AsList();
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

    public Response AgregarUsuarioDestino(UsuarioDestinoPost usuarioDestinoPost)
    {
        try
        {
            string query = @"
                INSERT INTO PNN_usuario_destino (usuario, psw, id_PNN_destino)
                VALUES (@usuario, @psw, @id_PNN_destino)
            ";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var enc = new Encrypt();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@usuario", usuarioDestinoPost.Usuario);
                parameters.Add("@psw", enc.Encriptar(usuarioDestinoPost.Psw));
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

    public UsuarioDestino GetUsuarioDestino(int id)
    {
        try
        {
            string query = @"
                SELECT id, usuario, psw, id_PNN_destino as IdPNNDestino
                FROM PNN_usuario_destino WITH(NOLOCK)
                WHERE id = @id
            ";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var usuario = conn.QueryFirstOrDefault<UsuarioDestino>(query, new { id });
                return usuario;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Response UpdateUsuarioDestino(int id, UsuarioDestinoPost usuarioDestinoPost)
    {
        string update = @"
                UPDATE PNN_usuario_destino 
                SET usuario = @usuario, psw = @psw, id_PNN_destino = @id_PNN_destino
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
                var enc = new Encrypt();
                conn.Execute(update, new{
                    usuario = usuarioDestinoPost.Usuario,
                    psw = enc.Encriptar(usuarioDestinoPost.Psw),
                    id_PNN_destino = usuarioDestinoPost.IdPNNDestino,
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

    public Response DeleteUsuarioDestino(int id)
    {
        try
        {
            string query = @"DELETE FROM PNN_usuario_destino WHERE id = @id";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var delete = conn.Execute(query, new { id });
                return new Response { Status = 0, Message = "Usuario Destino Eliminado Correctamente, filas afectadas: "+delete };
            }
        }
        catch (Exception ex)
        {
            return new Response { Status = 1, Message = ex.Message };
        }
    }
}