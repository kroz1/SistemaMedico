using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SistemaMedico.cModels
{
    public class cConexion
    {
        public string sConn;
        public SqlDataAdapter oDataAdapter;
        public SqlConnection oConn;
        public string sTipo = "SQLServer";

        public cConexion()
        {
            sConn = System.Configuration.ConfigurationManager.ConnectionStrings["citas_medicasEntities1"].ConnectionString;
            
        }

        public cConexion(string pTipo, string pServer, string pDatabase, string pUsername, string pPassword)
        {
            sTipo = pTipo;
            sConn = @"server=" + pServer + ";database=" + pDatabase + ";uid=" + pUsername + ";pwd=" + pPassword + ";";
        }

        public cConexion(string pConn)
        {
            this.sConn = pConn;
        }

        public DataTable EjecutarConsulta(string pQuery, bool mostrar_mensaje = true)
        {
            try
            {
                DataTable oDT = new DataTable();
                //Ejecutar para SQL Server
                oDT = new DataTable();
                oConn = new SqlConnection(sConn);
                oDataAdapter = new SqlDataAdapter();
                SqlCommand oSqlCommand = new SqlCommand(pQuery, oConn);
                oSqlCommand.CommandTimeout = 10000000;
                oDataAdapter.SelectCommand = oSqlCommand;
                oDataAdapter.Fill(oDT);
                return oDT;
            }
            catch (Exception error)
            {
                string mensaje = "";
                mensaje = error.Message.ToString();
                if (error.InnerException != null)
                {
                    mensaje += Environment.NewLine + error.InnerException.ToString() + Environment.NewLine
                        + pQuery + Environment.NewLine
                        + "cCONEXION.cs 117";
                }
                throw new Exception(mensaje);
            }
        }
    }
}