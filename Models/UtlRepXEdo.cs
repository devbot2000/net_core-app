using AspNetCore.Reporting.ReportExecutionService;
using AspNetCore.Reporting;
using System.IO;
using System.Reflection;
using System.Data;
using System.Text;

namespace NETCORE_ALUM.Models
{
    public class UtlRepXEdo
    {
        public string? link { get; set; }
        public string FNRuta()
        {
            DataLayer DL = new DataLayer();
            string WRuta = MyServer._hostingEnvironment.ContentRootPath + "\\REPORT\\Report1.rdlc";
            string WIDC = DateTime.Now.Millisecond.ToString() + ".pdf";
            string WNomFile = "\\PDFFile\\Alum" + WIDC;
            string WDoc = MyServer._hostingEnvironment.WebRootPath + WNomFile;
            int extension = 1;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");
            LocalReport? localReport = new LocalReport(WRuta);
            DataTable? oTable = DL.FNFillTableRep("SP_REP_ALUMNOS", "@ID", "1");
            localReport.AddDataSource("DataSet1", oTable);
            byte[] Bytes = localReport.Execute(RenderType.Pdf, extension).MainStream;
            localReport = null;
            FileStream? fs = new FileStream(WDoc, FileMode.Create);
            fs.Write(Bytes, 0, Bytes.Length); fs.Close(); fs = null;
            MemoryStream ms = new MemoryStream(Bytes);
            BinaryWriter? BW = new BinaryWriter(ms);
            BW.Flush(); BW.Close(); BW.Dispose(); BW = null;
            oTable.Clear(); oTable.Dispose(); oTable = null;
            return "PDFFile/Alum" + WIDC;
        }

    }
}
