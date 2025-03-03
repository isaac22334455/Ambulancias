namespace SUMAAmbulanciasAPI.Areas.Empleados.Models
{
    public class EmpleadoPagarModel
    {
        public List<int> Ids { get; set; }
        public int Id_Empleado { get; set; }
        public int iHorasPagadas { get; set; }
        public decimal TotalPagar { get; set; }
    }
}
