namespace SUMAAmbulanciasAPI.Areas.Empleados.Models
{
    public class EmpleadosModel
    {
        public EmpleadosModel() { }

        public EmpleadosModel(int id_Empleado, string appPat_Empleado, string appMat_Empleado, string nombre_Empleado, string tel_Empleado, string Correo_Empleado, double costHor_Empleado)
        {
            Id_Empleado = id_Empleado;
            AppPat_Empleado = appPat_Empleado;
            AppMat_Empleado = appMat_Empleado;
            Nombre_Empleado = nombre_Empleado;
            Tel_Empleado = tel_Empleado;
            correo_Empleado = Correo_Empleado;
            CostHor_Empleado = costHor_Empleado;
        }


        public int Id_Empleado { get; set; }
        public string AppPat_Empleado { get; set; }
        public string AppMat_Empleado { get; set; }
        public string Nombre_Empleado { get; set; }
        public string Tel_Empleado { get; set; }
        public string correo_Empleado { get; set; }
        public double CostHor_Empleado { get; set; }

    }
}
