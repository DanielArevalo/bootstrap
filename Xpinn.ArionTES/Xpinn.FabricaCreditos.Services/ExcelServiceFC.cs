using System;
using System.ServiceModel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using Xpinn.Util;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ExcelServiceFC
    {
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Atributo
        /// </summary>
        public ExcelServiceFC()
        {
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100141"; } }

        public void ExportarExcel(System.Data.DataTable dt)
        {
            try
            {
                string timeMark = DateTime.Now.ToString("yyyyMMdd HHmmss");
                string outputPath = "ExcelExportFastExport_" + timeMark + ".xls";
                // Create the Excel Application object
                //Word.Document document = Globals.ThisAddIn.Application.ActiveDocument;

                Application excelApp = new Application();
                //ApplicationClass excelApp = new ApplicationClass();

                // Create a new Excel Workbook
                Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);

                int sheetIndex = 0;

                //Copy each DataTable
                //foreach (System.Data.DataTable table in datos.Tables)
                //{

                //Copy the DataTable to an object array
                object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

                //Copy the column names to the first row of the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    rawData[0, col] = dt.Columns[col].ColumnName;
                }

                // Copy the values to the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        rawData[row + 1, col] = dt.Rows[row].ItemArray[col].ToString();
                    }
                }

                string az = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", nomulcol;
                if (dt.Columns.Count > 26)
                {
                    int primerletra = dt.Columns.Count / 26;
                    nomulcol = az.Substring(primerletra - 1, 1);
                    nomulcol += az.Substring((dt.Columns.Count - 1) - (primerletra * 26), 1);
                }
                else
                {
                    nomulcol = az.Substring(dt.Columns.Count - 1, 1);
                }
                // Create a new Sheet
                Worksheet excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, XlSheetType.xlWorksheet);

                //excelSheet.Name = table.TableName;

                // Fast data export to Excel
                string excelRange = string.Format("A4:{0}{1}", nomulcol, dt.Rows.Count + 4);
                excelApp.Visible = true;

                excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;

                // Mark the first row as BOLD
                ((Range)excelSheet.Rows[4, Type.Missing]).Font.Bold = true;

                excelSheet.get_Range(excelRange, Type.Missing).Cells.AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList3, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                excelApp.Columns.AutoFit();

                // Save and Close the Workbook
                excelWorkbook.SaveAs(outputPath, XlFileFormat.xlWorkbookNormal, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excelWorkbook.Close(true, Type.Missing, Type.Missing);
                excelWorkbook = null;

                // Release the Application object
                excelApp.Quit();
                excelApp = null;

                // Collect the unreferenced objects
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExcelService", "ExportarExcel", ex);
            }
        }
    }
}