using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;


partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.DocumentosAnexosService DocumentosAnexosServicio = new Xpinn.FabricaCreditos.Services.DocumentosAnexosService();
    private Xpinn.FabricaCreditos.Services.TiposDocumentoService TiposDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DocumentosAnexosServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;            
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoAtras += btnAtras_Click;
            toolBar.eventoAdelante += btnAdelante_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";
            
            if (Session["TipoCredito"].ToString() == "C")
                btnAdelante.ImageUrl = "~/Images/btnPlanPagos.jpg";
            else
                btnAdelante.ImageUrl = "~/Images/btnViabilidadFinanciera.jpg";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, DocumentosAnexosServicio.CodigoPrograma);
                if (Session[DocumentosAnexosServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
                    ActualizarTiposDocumento();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void ActualizarTiposDocumento()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.TiposDocumento> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.TiposDocumento>();
            lstConsulta = TiposDocumentoServicio.ListarTiposDocumento(ObtenerValoresTiposDocumento(), (Usuario)Session["usuario"]);

            rblTipoDocumento.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                 
                ValidarPermisosGrilla(gvLista);

                rblTipoDocumento.DataTextField = "descripcion";
                rblTipoDocumento.DataValueField = "tipo_documento";
                rblTipoDocumento.DataSource = lstConsulta;
                rblTipoDocumento.DataBind();
            }
            else
            {
                lblTotalRegs.Visible = false;
            }

            Session.Add(TiposDocumentoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.TiposDocumento ObtenerValoresTiposDocumento()
    {
        Xpinn.FabricaCreditos.Entities.TiposDocumento vTiposDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
        vTiposDocumento.tipo = "A";
        return vTiposDocumento;
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, DocumentosAnexosServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, DocumentosAnexosServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, DocumentosAnexosServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[DocumentosAnexosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[DocumentosAnexosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            DocumentosAnexosServicio.EliminarDocumentosAnexos(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.DocumentosAnexos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.DocumentosAnexos>();
            try
            {
                lstConsulta = DocumentosAnexosServicio.ListarDocumentosAnexos(ObtenerValores(), 0, (Usuario)Session["usuario"]);
            }
            catch { }

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(DocumentosAnexosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma, "Actualizar Tipos de Documentos", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.DocumentosAnexos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.DocumentosAnexos vDocumentosAnexos = new Xpinn.FabricaCreditos.Entities.DocumentosAnexos();
        
        vDocumentosAnexos.numerosolicitud = Session["NumeroSolicitud"].ToString() != null ? Convert.ToInt64(Session["NumeroSolicitud"].ToString()) : 0;
       
        return vDocumentosAnexos;
    }

    protected void gvListaTiposDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           // gvListaTiposDocumento.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "gvListaTiposDocumento_PageIndexChanging", ex);
        }
    }

    protected void gvListaTiposDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFilaTiposDocumento(e, "btnBorrar0");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma + "L", "gvListaTiposDocumento_RowDataBound", ex);
        }
    }


    public void ConfirmarEliminarFilaTiposDocumento(GridViewRowEventArgs e, String pBoton)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                try
                {
                    ImageButton btnGrilla = (ImageButton)e.Row.FindControl(pBoton);
                    btnGrilla.Attributes.Add("onClick", "return confirm('Esta seguro que desea eliminar este registro?')");
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw("GlobalWeb", "ConfirmarEliminarFila", ex);
                }
                break;
        }
    }

    protected void gvListaTiposDocumento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            TiposDocumentoServicio.EliminarTiposDocumento(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "gvListaTiposDocumento_RowDeleting", ex);
        }
    }

    protected void gvListaTiposDocumento_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvListaTiposDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        //Guarda imagen

        try
        {
            Xpinn.FabricaCreditos.Entities.DocumentosAnexos vDocumentosAnexos = new Xpinn.FabricaCreditos.Entities.DocumentosAnexos();

            if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
            {                                
                byte[] myimage = new byte[FileUpload1.PostedFile.ContentLength];
                HttpPostedFile Image = FileUpload1.PostedFile;
                Image.InputStream.Read(myimage, 0, Convert.ToInt32(FileUpload1.PostedFile.ContentLength));
                
                vDocumentosAnexos.iddocumento = 0;
                if (Session["NumeroSolicitud"] != null)
                    vDocumentosAnexos.numerosolicitud = Session["NumeroSolicitud"].ToString() != null ? Convert.ToInt64(Session["NumeroSolicitud"].ToString()) : 0;
                else
                    vDocumentosAnexos.numerosolicitud = 0;
                vDocumentosAnexos.tipo_documento = Convert.ToInt64(rblTipoDocumento.SelectedValue);
                vDocumentosAnexos.cod_asesor = 0;
                vDocumentosAnexos.imagen = new byte[FileUpload1.PostedFile.ContentLength];
                vDocumentosAnexos.imagen = myimage; 
                vDocumentosAnexos.descripcion = myimage.Length.ToString(); 
                vDocumentosAnexos.fechaanexo = DateTime.Now;

                vDocumentosAnexos = DocumentosAnexosServicio.CrearDocumentosAnexos(vDocumentosAnexos, (Usuario)Session["usuario"]);
           }
            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }

        GuardarValoresConsulta(pConsulta, DocumentosAnexosServicio.CodigoPrograma);
        Actualizar();
    }
   

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx"); //Guarda la solicitud de consumo
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
        {
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx"); //Ir al Plan de Pagos
        }
        else
        { 
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/ViabilidadFinanciera/Nuevo.aspx"); 
        }
           
    }
}