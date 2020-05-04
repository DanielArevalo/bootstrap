using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Xpinn.Util
{
    public static class ExportarExcel
    {
        public static void ExportCsv<T>(List<T> genericList, string fileName)
        {
            var sb = new StringBuilder();
            var basePath = AppDomain.CurrentDomain.BaseDirectory + "Archivos\\";
            var finalPath = Path.Combine(basePath, fileName + ".csv");
            var header = "";
            var info = typeof(T).GetProperties();
            DirectoryInfo di = new DirectoryInfo(basePath);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            if (!File.Exists(finalPath))
            {
                var file = File.Create(finalPath);
                file.Close();
                foreach (var prop in typeof(T).GetProperties())
                {
                    header += prop.Name + "; ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
            foreach (var obj in genericList)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + "; ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
        }

        public static void ExportToExcelXls<T>(List<T> genericList, string NombreArchivo, int Sheetmas = 0)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            try
            {
                //Parametros y direcion del archivo
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook currentWorkbook = excelApp.Workbooks.Add(Type.Missing);
                Worksheet currentWorksheet = (Worksheet)currentWorkbook.ActiveSheet;
                var basePath = AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Cosechas\\";
                DirectoryInfo di = new DirectoryInfo(basePath);
                var info = typeof(T).GetProperties();
                currentWorksheet.Columns[5].ColumnWidth = 20;
                currentWorksheet.Name = "Cosechas";
                //Si no existe el path lo creo 
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                //Si existen archivos con ese nombre entonces los borro
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                if (genericList.Count > 0)
                {
                    //Creo El Cuerpo Del archivo
                    CrearcuerpoArchivo(genericList, currentWorksheet, info);
                    //Alinio todos los Datos a la izquierda
                    Range fullTextRange = currentWorksheet.get_Range("1:1048576", Type.Missing);
                    fullTextRange.WrapText = true;
                    fullTextRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                }
                else
                {
                    string timeStamp = DateTime.Now.ToString("s");
                    timeStamp = timeStamp.Replace(':', '-');
                    timeStamp = timeStamp.Replace("T", "__");
                    currentWorksheet.Cells[1, 1] = timeStamp;
                    currentWorksheet.Cells[1, 2] = "No error occured";
                }
                currentWorkbook.SaveAs(di + NombreArchivo, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
                currentWorkbook.Saved = true;
                currentWorkbook.Close();

                if (Sheetmas == 0)
                    DescargarArchivo(basePath, NombreArchivo, NombreArchivo);
            }
            catch //(Exception ex)
            {
                //ignore
            }
        }
        public static void DescargarArchivo(string di, string NombreArchivo, string fullFileName)
        {
            HttpContext.Current.Response.ContentType = "application/xls";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename= " + NombreArchivo + ".xlsx");
            HttpContext.Current.Response.AppendHeader("Content-Length", Encoding.UTF8.ToString());
            HttpContext.Current.Response.WriteFile(di + fullFileName + ".xlsx");

        }
        public static void AgregarNuevoSheet<T>(List<T> lstListaNueva, string nombreArchivo)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                excelApp.DisplayAlerts = false;
                var basePaths = AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Cosechas\\";
                var basePath = AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Cosechas\\" + nombreArchivo;
                Workbook xlWorkBook = excelApp.Workbooks.Open(basePath, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Sheets worksheets = xlWorkBook.Worksheets;

                var xlNewSheet = (Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = "Texto2";

                AgregarNuevoSheet(lstListaNueva, nombreArchivo);

                xlWorkBook.Save();
                xlWorkBook.Close();
                DescargarArchivo(basePaths, nombreArchivo, basePath);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static void CrearcuerpoArchivo<T>(List<T> genericList, Worksheet currentWorksheet, PropertyInfo[] info)
        {
            int i = 1;
            foreach (var prop in typeof(T).GetProperties())
            {
                var header = prop.Name;
                currentWorksheet.Cells[2, i] = header;

                Range cell = currentWorksheet.Cells[2, i];
                Borders border = cell.Borders;
                border[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                border[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                border[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                border[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                currentWorksheet.Columns[i].ColumnWidth = 20;
                ++i;
            }

            Range headerColumnRange = currentWorksheet.get_Range("2:2", Type.Missing);
            headerColumnRange.Font.Bold = true;
            headerColumnRange.Font.Color = 0xFF0000;
            int rowIndex = 0, cellIndex = 0;

            foreach (var obj in genericList)
            {
                foreach (var prop in info)
                {
                    currentWorksheet.Cells[rowIndex + 3, cellIndex + 1] = prop.GetValue(obj, null);
                    Range cell = currentWorksheet.Cells[2, i];
                    Borders border = cell.Borders;
                    border[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                    border[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                    border[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                    border[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                    currentWorksheet.Columns[i].ColumnWidth = 20;
                    cellIndex++;
                }
                rowIndex++; cellIndex = 0;
            }

        }
    }
}
