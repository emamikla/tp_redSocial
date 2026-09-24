namespace tp_redSocial.Models;
using Microsoft.Data.SqlClient;
using Dapper;

public class Bd
{
    private string _connectionString = @"Server=localhost;Database=redSocial; Integrated Security=True; TrustServerCertificate=True;";

    public static void AgregarUsuario(Usuario usuario)
    {
        string nombre = usuario.Nombre;
        string nombreUsuario = usuario.NombreUsuario;
        string contraseña = usuario.Contraseña;
        string apellido = usuario.Apellido;
        string tipoUsuario = usuario.TipoUsuario;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombre, Apellido)
                             VALUES (@nombreUsuario, @contraseña, @nombre, @apellido)";
            connection.Execute(query, new
            {
                NombreUsuario = nombreUsuario,
                Contraseña = contraseña,
                Nombre = nombre,
                Apellido = apellido,
            });
        }
    }

    public static bool FijarseSiExisteUsuario(string nombreUsuario)
    {
        bool existe = false;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @nombreUsuario";

            //esto que sigue me a a devolver un numero que es la cantidad de usuarios con ese nombre de usuario que es la pk
            
            int count = connection.QueryFirstOrDefault<int>(query, new { NombreUsuario = nombreUsuario });
            
            if (count > 0)
            {
                existe = true;
            }
        }
        return existe;
    }

    public static Usuario ObtenerUsuario(string nombreUsuario, string contraseña)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña";
            //esto me va a devolver un objeto de tipo usuario que es el que tiene ese nombre de usuario y esa contraseña tipo para poder llevarlo a la vista y mostrarlo en la pagina de inicio
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { nombreUsuario, contraseña }); 
        }
        return usuario;
    }
}

