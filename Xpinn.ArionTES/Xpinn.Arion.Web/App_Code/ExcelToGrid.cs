using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;

public class ExcelToGrid
{
    public DataTable leerExcel(string rutaExcel, string Hoja)
    {        
        //Creamos la cadena de conexión con el fichero excel
        OleDbConnectionStringBuilder cb = new OleDbConnectionStringBuilder();
        cb.DataSource = rutaExcel; 
        if (Path.GetExtension(rutaExcel).ToUpper() == ".XLS")
        {
            cb.Provider = "Microsoft.Jet.OLEDB.4.0";
            cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
        }
        else if (Path.GetExtension(rutaExcel).ToUpper() == ".XLSX")
        {
            cb.Provider = "Microsoft.ACE.OLEDB.12.0";
            cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
        }
        DataTable dt = new DataTable(Hoja);
        using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
        {
            conn.Open();
            using (OleDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + Hoja + "$]";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }
            conn.Close();
        }
        return dt;
    }    

}

