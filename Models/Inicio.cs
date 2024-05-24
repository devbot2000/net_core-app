using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NETCORE_ALUM.Models
{
    public class Inicio
    {
        public List<Alumno> GetAlumnos()
        {
            List<Alumno> oAlumno = new List<Alumno>();
            DataLayer? DL = new DataLayer();            
            DataTable? oTT = DL.FNFillTable(1,"SPVER_ALUMNOS", "@ID", "1");
            foreach (DataRow row in oTT.Rows)                       
                oAlumno.Add(new Alumno 
                { 
                    IDAlum = Convert.ToInt16(row["IDAlum"].ToString()), 
                    WNom = row["Nombres"].ToString(), 
                    WApe = (string)row["Apellidos"] 
                });
            oTT.Dispose(); oTT = null;
            DL.Dispose(); DL = null;            
            return oAlumno;
        }
    }
}
