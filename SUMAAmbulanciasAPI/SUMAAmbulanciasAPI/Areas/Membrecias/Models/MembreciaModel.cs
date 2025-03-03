namespace SUMAAmbulanciasAPI.Areas.Membrecias.Models
{
    public class MembreciaModel
    {
        public int Id_Membrecia { get; set; }
        public int IFrecMen_Membrecia { get; set; }
        public int Id_TipoCliente { get; set; }
        public string vcTipoCliente { get; set; }
        public string Dt_FechaRegistro { get; set; }
        public double MCantidadCobrar { get; set; }
    }
}
