using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;

public class ExpExcelConfecoop
{
    public System.Data.DataTable EjecutarExportacion(ref Int32 fila, StreamReader pArchivo, Usuario pUsuario)
    {
        string readLine;
        StreamReader strReader = pArchivo;
        // Leer línea de títulos
        readLine = strReader.ReadLine();
        string[] arrayTitulos = readLine.Split('|');
        System.Data.DataTable table = new System.Data.DataTable();
        for (int i = 0; i < arrayTitulos.Length; i++)
        {
            table.Columns.Add(arrayTitulos[i].ToString());
        }
        Int32 C = 0;
        //RECORRIENDO LOS DATOS 
        while (strReader.Peek() >= 0)
        {
            C++;
            readLine = strReader.ReadLine();
            string[] arrayline = readLine.Split('|');

            int cont = 0;
            System.Data.DataRow dr = table.NewRow();
            //RECORRIENDO CADA COLUMNA
            foreach (string item in arrayline)
            {
                try
                {
                    if (cont <= arrayline.Length - 1)
                        dr[cont] = " " + item.Trim();
                }
                catch { }
                cont++;
            }
            fila = C;
            table.Rows.Add(dr);
        }

        return table;
    }

}
