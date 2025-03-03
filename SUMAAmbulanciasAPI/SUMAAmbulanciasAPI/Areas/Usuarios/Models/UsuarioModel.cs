namespace SUMAAmbulanciasAPI.Areas.Usuarios.Models
{
    public class UsuarioModel
    {
        public UsuarioModel() { }

        public UsuarioModel(int idUsuario, string nombreUsuario, int idRolUsuario, string passUsuario)
        {
            Id_Usuario = idUsuario;
            Nombre_Usuario = nombreUsuario;
            IdRol_Usuario = idRolUsuario;
            Pass_Usuario = passUsuario;
        }

        public int Id_Usuario { get; set; }       
        public string Nombre_Usuario { get; set; } 
        public int IdRol_Usuario { get; set; }    
        public string Pass_Usuario { get; set; }
        public string dtFechaRegistro { get; set; }

    }
}
