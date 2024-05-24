using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.OleDb;

namespace NETCORE_ALUM.Models
{
    public class Alumno
    {

        public int IDAlum { get; set; }
        public string? WNom { get; set; }
        public string? WApe { get; set; }
        public int IDEst { get; set; }
        public List<SelectListItem>? Estados { get; set; }
        public IEnumerable<Alumno>? IEnumAlumnos { get; set; }

        public Alumno FNConsAlumno(int IDAlum)
        {
            Alumno alumno = new Alumno();
            alumno.Estados = FNLLenaCombo();
            if (IDAlum == 0)
            {
                alumno.IDAlum = 0; alumno.WNom = ""; alumno.WApe = "";
            }
            else
            {
                DataLayer DL = new DataLayer();
                DataSet rAlum = DL.FNFillTable("SP_CONS_ALUMNO", "@IDAlum", IDAlum.ToString());
                if (rAlum.Tables.Count>0) {
                    alumno.IDAlum = int.Parse(rAlum.Tables[0].Rows[0].ItemArray[0].ToString());
                    alumno.WNom = rAlum.Tables[0].Rows[0].ItemArray[1].ToString();
                    alumno.WApe = rAlum.Tables[0].Rows[0].ItemArray[2].ToString();
                    alumno.IDEst = int.Parse(rAlum.Tables[0].Rows[0].ItemArray[3].ToString());
                }
                DL.Dispose(); DL = null;
            }
            return alumno;
        }

        public void SPGuardaAlumno(string WNombre, string WApe, string IDAlum, string WIDEst)
        {
            DataLayer DL = new DataLayer();
            if (IDAlum == "0")
            {
                var PP = DL.SPExeCmd("SP_INS_ALUMNO", 1, "@Nombres|@Apellidos|@IDEst", WNombre, WApe, WIDEst);                
            }
            else
            {
                var PP = DL.SPExeCmd("SP_UPD_ALUMNO", 1, "@IDAlum|@Nombres|@APE|@IDEst", IDAlum, WNombre, WApe, WIDEst);
            }
            DL.Dispose(); DL = null;
        }

        public void SPBorrarAlumno(string IDAlum)
        {
            DataLayer DL = new DataLayer();
            var PP = DL.SPExeCmd("SP_DEL_ALUMNO", 1, "@ID", IDAlum);
            DL.Dispose(); DL = null;
        }

        public List<SelectListItem> FNLLenaCombo()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            DataLayer DL = new DataLayer();
            DataSet rEdo = DL.FNFillTable("SPEstados", "@ID", "1");
            for (int K = 0; K < rEdo.Tables[0].Rows.Count; K++)
            {
                items.Add(new SelectListItem
                {
                    Text = rEdo.Tables[0].Rows[K].ItemArray[1].ToString(),
                    Value = rEdo.Tables[0].Rows[K].ItemArray[0].ToString()
                });
            }
            DL.Dispose(); DL = null;
            return items;
        }

    }
}
