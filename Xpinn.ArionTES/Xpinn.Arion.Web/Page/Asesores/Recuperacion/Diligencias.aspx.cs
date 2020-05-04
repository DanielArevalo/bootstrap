using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using System.Web.Script.Services;

//using System.Threading;


public partial class Detalle : GlobalWeb
{

    ClienteService clienteServicio = new ClienteService();
    CreditosService creditoServicio = new CreditosService();
    DiligenciaService diligenciaServicio = new DiligenciaService();
    ProcesosCobroService procesosCobroServicio = new ProcesosCobroService();
    AtributoService atributoServicio = new AtributoService();

    Int64 codProceso, codUsuario, codAbogado, codMotivo,codciudadproceso;
      
    String IdCodeudor, TipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Direccion, Telefono,Barrio, Email;


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteServicio.CodigoProgramaRealRecuperacionesDetalle, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            Configuracion conf = new Configuracion();
             }
        catch 
        {
            VisualizarOpciones(clienteServicio.CodigoPrograma, "L");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    { 
        String radicado = "";
        try
        {
            if (!IsPostBack)
            {
               txtCodigoCliente.Text = Convert.ToString(Session["ESTADOCUENTA"]);
               Diligencias();
               if (Session[clienteServicio.CodigoPrograma + ".id"] != null && Session[creditoServicio.CodigoPrograma + ".id"] != null)
               {                             
                    if (radicado == null)
                    {
                        idObjeto = Session[clienteServicio.CodigoPrograma + ".id"].ToString();
                        Session.Remove(clienteServicio.CodigoPrograma + ".id");
                      
                        //* error cuando no hay datos en cobros_credito
                        Diligencias();
                    }
                    else 
                    {
                        Diligencias();
                    }
               }
            }          
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.GetType().Name, "Page_Load", ex);
        }
    }

   
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {

        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
        //String id = txtCodigoCliente.Text;
                

    }
     
    private void Diligencias()
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            List<Diligencia> lstConsultaDiligencias = new List<Diligencia>();
            Diligencia diligencia = new Diligencia();
            txtCodigoCliente.Text = Convert.ToString(Session["ESTADOCUENTA"]); 
            diligencia.codigo_deudor = Convert.ToInt64(txtCodigoCliente.Text);
            lstConsultaDiligencias = diligenciaServicio.ListarDiligenciaEstadocuenta(diligencia.codigo_deudor, (Usuario)Session["usuario"]);
            gvListaDiligencias.EmptyDataText = emptyQuery;
            gvListaDiligencias.DataSource = lstConsultaDiligencias;
            if (lstConsultaDiligencias.Count > 0)
            {
                gvListaDiligencias.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaDiligencias.Count.ToString();
                gvListaDiligencias.DataBind();
            }
            else
            {
                gvListaDiligencias.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(diligenciaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(diligenciaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
  
    

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

   



   

    protected void gvListaDiligencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
             
           gvListaDiligencias.PageIndex = e.NewPageIndex;
            Diligencias();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "gvListaDiligencias_PageIndexChanging", ex);
        }
        
      
    }
    [System.Web.Services.WebMethod]
    public static string GetNote()
    {
        return "- CodeFile";
    }

    protected void gvListaDiligencias_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 id = Convert.ToInt32(gvListaDiligencias.DataKeys[gvListaDiligencias.SelectedRow.RowIndex].Value.ToString());
        Session[diligenciaServicio.CodigoPrograma + ".id"] = id;

        String idCliente = txtCodigoCliente.Text;
        Session[clienteServicio.CodigoPrograma + ".id"] = idCliente;
        GridViewRow row = gvListaDiligencias.SelectedRow;
        int id2 = Convert.ToInt32(gvListaDiligencias.DataKeys[row.RowIndex].Value);
       
    }

    
    public String vacios(String texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            return " ";
        }
        else
        {
            return texto;
        }
    }

  

    protected void btnAgregar_Click(object sender, EventArgs e)
    {

    }

    

    protected void btnAgregar_Click1(object sender, EventArgs e)
    {

    }
   
    protected void btnCloseReg_Click(object sender, EventArgs e)
    {

    }




   
    protected void btnInfo1_Click(object sender, EventArgs e)
    {

    }
    protected void btnCloseRegConsulta_Click(object sender, EventArgs e)
    {




    }

    [WebMethodAttribute]
    [ScriptMethod]
    public static string GetContent(string contextKey)
    {

        return contextKey;

    }

    protected void gvListaDiligencias_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        Int64 id = Convert.ToInt32(gvListaDiligencias.DataKeys[gvListaDiligencias.SelectedRow.RowIndex].Value.ToString());
        Session[diligenciaServicio.CodigoPrograma + ".id"] = id;

        String idCliente = txtCodigoCliente.Text;
        Session[clienteServicio.CodigoPrograma + ".id"] = idCliente;
        GridViewRow row = gvListaDiligencias.SelectedRow;


        int id2 = Convert.ToInt32(gvListaDiligencias.DataKeys[row.RowIndex].Value);
      


    }


    protected void gvListaDiligencias_SelectedIndexChanging1(object sender, GridViewSelectEventArgs e)
    {
        //Int64 id = Convert.ToInt32(gvListaDiligencias.DataKeys[gvListaDiligencias.SelectedRow.RowIndex].Value.ToString());
        // Session[diligenciaServicio.CodigoPrograma + ".id"] = id;

        //String idCliente = txtCodigoCliente.Text;
        //  Session[clienteServicio.CodigoPrograma + ".id"] = idCliente;
        //GridViewRow row = gvListaDiligencias.SelectedRow;


        int id2 = Convert.ToInt32(gvListaDiligencias.DataKeys[e.NewSelectedIndex].Value);
        // int OrderId = Convert.ToInt32(gvOrders.DataKeys[e.NewSelectedIndex].Value);

     


    }
    protected void gvListaDiligencias_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnInfo2_DataBinding(object sender, EventArgs e)
    {

    }
    protected void btnCloseRegConsulta_Click1(object sender, EventArgs e)
    {
      
    }
    
    
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".docx":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":          
                return "application/ms-excel";
            case ".xlsx":
                return "application/ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }




    protected void btnActualizar_Click(object sender, EventArgs e)
    {

        Diligencias();

    }


   
 
 
}