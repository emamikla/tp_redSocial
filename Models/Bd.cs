namespace tp_redSocial.Models;
using Microsoft.Data.SqlClient;
using Dapper;

public class Bd
{
    private string _connectionString = @"Server=localhost;Database=redSocial; Integrated Security=True; TrustServerCertificate=True;";

    public void AgregarUsuario(Usuario usuario)
    {
        string nombre = usuario.Nombre;
        string nombreUsuario = usuario.NombreUsuario;
        string contraseña = usuario.Contraseña;
        string apellido = usuario.Apellido;
        string tipoUsuario = usuario.TipoUsuario;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Usuarios (nombre, nombreUsuario, contraseña, apellido, tipoUsuario)
                             VALUES (@nombre, @nombreUsuario, @contraseña, @apellido, @tipoUsuario)";
            connection.Execute(query, new
            {
                nombre = nombre,
                nombreUsuario = nombreUsuario,
                contraseña = contraseña,
                apellido = apellido,
                tipoUsuario = tipoUsuario
            });
        }
    }

    public bool FijarseSiExisteUsuario(string nombreUsuario)
    {
        bool existe = false;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE nombreUsuario = @nombreUsuario";

            //esto que sigue me a a devolver un numero que es la cantidad de usuarios con ese nombre de usuario que es la pk
            
            int count = connection.QueryFirstOrDefault<int>(query, new { nombreUsuario = nombreUsuario });
            
            if (count > 0)
            {
                existe = true;
            }
        }
        return existe;
    }

    public Usuario ObtenerUsuario(string nombreUsuario, string contraseña)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE nombreUsuario = @nombreUsuario AND contraseña = @contraseña";
            //esto me va a devolver un objeto de tipo usuario que es el que tiene ese nombre de usuario y esa contraseña tipo para poder llevarlo a la vista y mostrarlo en la pagina de inicio
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { nombreUsuario, contraseña }); 
        }
        return usuario;
    }
}

