using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Data;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Globalization;
using System.Threading.Tasks;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Page_Aportes_Personas_ImportClasificacion : GlobalWeb
{
    ExogenaReportService objReporteService = new ExogenaReportService();
    String operacion = "";
    List<ExogenaReport> _lstRegistroConcepto;
    int _contadorRegistro;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCargarPersonas_Click(object sender, EventArgs e)
    {
        VerError("");
        string error = "";
        try
        {
            if (!flpArchivo.HasFile)
            {
                VerError("Seleccione un archivo excel, verifique los datos.");
                return;
            }
            ExcelToGrid CargaEx = new ExcelToGrid();
            //CREACION DEL DATATABLE
            DataTable dt = new DataTable();

            String NombreArchivo = Path.GetFileName(this.flpArchivo.PostedFile.FileName);

            if (!validarExtension(flpArchivo))
            {
                VerError("Ingrese un Archivo valido (Excel).");
                return;
            }
            //GUARDANDO EL ARCHIVO EN EL SERVIDOR
            if (!Directory.Exists(Server.MapPath("Archivos\\")))
                Directory.CreateDirectory(Server.MapPath("Archivos\\"));

            flpArchivo.PostedFile.SaveAs(Server.MapPath("Archivos\\") + NombreArchivo);
            flpArchivo.Dispose();

            //LEENDO LOS DATOS EN EL DATATABLE
            try
            {
                dt = CargaEx.leerExcel(Server.MapPath("Archivos\\") + NombreArchivo, "Datos");

            }
            catch
            {
                VerError("No se pudo cargar el archivo o el archivo no contiene datos (Hoja: Datos)");
                return;
            }

            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }

            if (dt.Rows.Count <= 0)
            {
                error = "No se pudo cargar el archivo o el archivo no contiene datos (Hoja: Datos)";
                VerError(error);
                return;
            }
            else
            {
                List<InformacionAdicional> lstClientesImporte = new List<InformacionAdicional>();
                List<ErroresCarga> lstErrores = new List<ErroresCarga>();
                string pError = string.Empty;
                ObtenerClientes(ref lstClientesImporte, dt, lstErrores);

                Site toolBar = (Site)this.Master;
                toolBar.MostrarImportar(true);

                InformacionAdicionalServices objClienteService = new InformacionAdicionalServices();
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];


              
                    objClienteService.ActualizacionMasiva(lstClientesImporte, pUsuario, lstErrores);
                
               

                panCargueExitoso.Visible = true;
                gvCarguesExitosos.DataSource = lstClientesImporte;
                gvCarguesExitosos.DataBind();
                lblTotalCarguesExitoso.Visible = true;
                lblTotalCarguesExitoso.Text = "<br/> Registros cargados exitosamente  : " + lstClientesImporte.Count;

                if (lstErrores.Count > 0)
                {
                    panErrores.Visible = true;
                    gvErrores.DataSource = lstErrores;
                    gvErrores.DataBind();
                    cpeDemo1.CollapsedText = "(Click Aqui para ver " + lstErrores.Count() + " errores...)";
                    cpeDemo1.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                else
                {
                    panErrores.Visible = false;
                    lblMensajeExitoso.Visible = lstClientesImporte.Count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    bool validarExtension(FileUpload file)
    {
        bool rutaOk = false;
        string extension = Path.GetExtension(file.FileName).ToLower();

        rutaOk = extension == ".xls" || extension == ".xlsx" ? true : false;

        return rutaOk;
    }
    private void ObtenerClientes(ref List<InformacionAdicional> lstArchivo, DataTable dt, List<ErroresCarga> pErrores)
    {
        int NumFila = 1;

        //REALIZANDO CARGA DEL BALANCE INICIAL
        //RECORRIENDO LOS DATOS DEL EXCEL LEIDO
        InformacionAdicional objCliente;
        ErroresCarga objError = new ErroresCarga();

        foreach (DataRow rData in dt.Rows)
        {
            int NumCol = 0;
            try
            {
                NumFila++;
                objCliente = new InformacionAdicional();

                //ORDEN DE CARGA: Primer nombre, Segundo nombre, Número documento,  Telefono, Razón social, Sigla negocio,Dirección , Email,
                //Tipo identificación, Zona, Actividad
                for (NumCol = 0; NumCol <= 2; NumCol++)
                {
                    if (NumCol == 0)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.identificacion = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 1)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.valor = rData.ItemArray[NumCol].ToString().Trim();
                    }


                }
                lstArchivo.Add(objCliente);
            }
            catch (Exception ex)
            {
                objError = new ErroresCarga();
                objError.datos = NumFila.ToString();
                objError.numero_registro = NumFila.ToString();
                objError.error = "Se generó un error en la fila " + NumFila + " en la columna " + (NumCol + 1) + " - " + ex.Message;
                pErrores.Add(objError);
            }
        }
    }
}