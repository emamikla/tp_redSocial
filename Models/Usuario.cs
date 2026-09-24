using System.Text.RegularExpressions;

public class Usuario
{
    public string NombreUsuario { get; set; }
    public string Contraseña { get; set; } 
    public string Nombre { get; set; }
    public string Apellido { get; set; }

    public static bool ValidarDatosRegistro( string nombreUsuario, string contraseña, string nombre, string apellido)
    {
        bool esValido = true;

        if (string.IsNullOrWhiteSpace(nombre))
        {
            esValido = false;
        }
        else if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
        {
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(apellido))
        {
            esValido = false;
        }
        else if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
        {
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(nombreUsuario))
        {
            esValido = false;
        }
        else if (nombreUsuario.Length < 6)
        {
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(contraseña))
        {
            esValido = false;
        }
        else if (contraseña.Length <= 8)
        {
            esValido = false;
        }

        return esValido;
    }
}