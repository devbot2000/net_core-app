using Microsoft.AspNetCore.Hosting.Server;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;

namespace NETCORE_ALUM.Models
{
    public class DataLayer
    {
        bool disposed = false; public string WMsg = ".";
        public int SSAccDB = 1; // 0 SQL SERVER - 1 Access
        public object? RD;
        private string? DevRootDB()
        {
            string? WSDB = MyServer._configuration.GetConnectionString("AlumnSQL");
            string WProv = MyServer._configuration.GetConnectionString("AlumnProv");
            string WProp = MyServer._configuration.GetConnectionString("AlumnDBProp");
            var WwwRoot = MyServer._hostingEnvironment.WebRootPath; 
            var WPath = MyServer._hostingEnvironment.ContentRootPath;
            string WDB = WProv + WPath + WProp;
            return SSAccDB == 0 ? WSDB : WDB;
        }   
        public int SPExeCmd(string WSP, int nData, string WPar, params string[]? oVal) 
        {
            if (SSAccDB == 0)
            {
                FNRdr(WSP, nData, WPar, oVal);
            }
            else {
                 FNRdr(1, WSP, nData, WPar, oVal);
            }
            return 1;
        }
        private SqlCommand FNCMDExeSP(string WSP, int nData, string WPar, params string[]? oVal)
        {
            char CLim = '|'; string[]? oPars = WPar.Split(CLim); int K = 0;
            string? WDB = DevRootDB();
            SqlConnection DB = new SqlConnection(WDB); DB.Open(); SqlCommand cmd = DB.CreateCommand(); cmd.CommandText = WSP;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var val in oVal)
            {
                cmd.Parameters.AddWithValue(oPars[K], oVal[K]); K++;
            }
            Array.Clear(oPars, 0, oPars.Length); oPars = null; Array.Clear(oVal, 0, oVal.Length); oVal = null;
            return cmd;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        private OleDbCommand FNCMDExeSP(int EsAccess, string WSP, int nData, string WPar, params string[]? oVal)
        {
            char CLim = '|'; string[]? oPars = WPar.Split(CLim); int K = 0;
            string? WPath = DevRootDB();
            OleDbConnection DB = new OleDbConnection(WPath); DB.Open(); OleDbCommand cmd = DB.CreateCommand(); cmd.CommandText = WSP;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var val in oVal)
            {
                cmd.Parameters.AddWithValue(oPars[K], oVal[K]); K++;
            }
            Array.Clear(oPars, 0, oPars.Length); oPars = null; Array.Clear(oVal, 0, oVal.Length); oVal = null;
            return cmd;
        }
        private SqlDataReader? FNRdr(string WSP, int nData, string WPar, params string[]? oVal)
        {
            SqlCommand cmd = FNCMDExeSP(WSP, nData, WPar, oVal);
            if (nData == 1)
            {
                cmd.ExecuteNonQuery(); return null;
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        private OleDbDataReader? FNRdr(int EsAccess, string WSP, int nData, string WPar, params string[]? oVal)
        {
            OleDbCommand cmd = FNCMDExeSP(1, WSP, nData, WPar, oVal);
            if (nData == 1)
            {
                cmd.ExecuteNonQuery(); return null;
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public DataTable FNFillTableRep(string WSP, string WPar, params string[] oVal)
        {
            DataTable ds = new DataTable();
            if (SSAccDB == 0)
            {
                SqlDataAdapter? da = FNLLenaTabla(WSP, WPar, oVal);
                da.Fill(ds); da.Dispose(); da = null;
            }
            else
            {
                OleDbDataAdapter? da = FNLLenaTabla(1, WSP, WPar, oVal);
                da.Fill(ds); da.Update(ds); da.Dispose(); da = null;
            }
            return ds;
        }
        public DataSet FNFillTable(string WSP, string WPar, params string[] oVal) {
            DataSet ds = new DataSet();
            if (SSAccDB == 0)
            {
                SqlDataAdapter? da = FNLLenaTabla(WSP, WPar, oVal);
                da.Fill(ds); da.Dispose(); da = null;
            }
            else
            {
                OleDbDataAdapter? da = FNLLenaTabla(1, WSP, WPar, oVal);
                da.Fill(ds); da.Dispose(); da = null;
            }            
            return ds;
        }
        public DataTable FNFillTable(int EsAccess ,string WSP, string WPar, params string[] oVal)
        {
            DataTable ds = new DataTable();
            if (SSAccDB == 0)
            {
                SqlDataAdapter? da = FNLLenaTabla(WSP, WPar, oVal);
                da.Fill(ds); da.Dispose(); da = null;
            }
            else
            {
                Thread.Sleep(1000);
                OleDbDataAdapter? da = FNLLenaTabla(1, WSP, WPar, oVal);
                da.Fill(ds); da.Update(ds); da.Dispose(); da = null;
            }
            return ds;
        }
        private SqlDataAdapter FNLLenaTabla(string WSP, string WPar, params string[] oVal)
        {
            SqlCommand cmd = FNCMDExeSP(WSP, 1, WPar, oVal);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd; 
            return da;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        private OleDbDataAdapter FNLLenaTabla(int EsAccess, string WSP, string WPar, params string[] oVal)
        {
            OleDbCommand cmd = FNCMDExeSP(1, WSP, 1, WPar, oVal);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            return da;
        }
        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this); GC.Collect(); GC.WaitForPendingFinalizers();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)// Free any other managed objects here.
            {
            }// Free any unmanaged objects here.
            disposed = true;  WMsg = ""; SSAccDB = -1;
            if (RD != null)  RD=null;
        }
        ~DataLayer()
        {
            Dispose(false);
        }
    }
}