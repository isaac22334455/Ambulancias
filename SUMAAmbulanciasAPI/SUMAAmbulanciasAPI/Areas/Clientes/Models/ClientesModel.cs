namespace SUMAAmbulanciasAPI.Areas.Clientes.Models
{
    public class ClientesModel
    {
        public int Id_Cliente { get; set; }
        public int bPagado { get; set; }
        public int iMeses { get; set; }
        public double MCantidad { get; set; }
        public string Nombre_Cliente { get; set; }
        public string AppMat_Cliente { get; set; }
        public string AppPat_Cliente { get; set; }
        public string Tel_Cliente { get; set; }
        public string Correo_Cliente { get; set; }
        public int IdTipoMembrecia { get; set; }
        public string TiempoRestante { get; set; }
        public string TipoMembrecia { get; set; }
        public DateTime FechaInicio { get; set; }



    }
}
