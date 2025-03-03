namespace SUMAAmbulanciasAPI.Areas.Clientes.Models
{
    public class RenovarMembreciaModel
    {
        public int Id_Entrada { get; set; }
        public int Id_Cliente { get; set; }
        public int IdTipoMembrecia { get; set; }
        public DateTime FechaInicio { get; set; }
        public double MCantidad { get; set; }
        public int iMeses { get; set; }
    }
}
