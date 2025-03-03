namespace SUMAAmbulanciasAPI.Areas.Empleados.Models
{
    public class EmpleadoHorasPagadasModel
    {
        public int Id_HoraPagada { get; set; }
        public int Id_Empleado { get; set; }
        public double PagoTotal { get; set; }
        public int iHorasPagadas { get; set; }
        public string Feha_Registro { get; set; }
    }
}
