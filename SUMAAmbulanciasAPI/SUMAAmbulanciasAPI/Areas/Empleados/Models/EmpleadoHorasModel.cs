namespace SUMAAmbulanciasAPI.Areas.Empleados.Models
{
    public class EmpleadoHorasModel
    {

        public EmpleadoHorasModel() { }

        public EmpleadoHorasModel(int id_Horas,int id_Empleado,int IHorasTrabajadas, string Dt_Fecha, double saldoTotal)
        {
            Id_Horas = id_Horas;
            Id_Empleado = id_Empleado;
            dt_Fecha = Dt_Fecha;
            iHorasTrabajadas = IHorasTrabajadas;
            SaldoTotal = saldoTotal;
        }

        public int Id_Horas { get; set; }
        public int Id_Empleado { get; set; }
        public int iHorasTrabajadas { get; set; }
        public double SaldoTotal { get; set; }
        public string dt_Fecha { get; set; }

    }
}
