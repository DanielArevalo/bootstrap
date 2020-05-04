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
using Xpinn.Integracion.Entities;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Integracion.Services;

partial class Lista : GlobalWeb
{
    PagoInternoService _pagoServices = new PagoInternoService();
    IntegracionService _integraService = new IntegracionService();
    EjecutivoService serviceEjecutivo = new EjecutivoService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_pagoServices.CodigoProgramaConsultaTransaccionesWeb, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagoServices.CodigoProgramaConsultaTransaccionesWeb, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                cargarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagoServices.CodigoProgramaConsultaTransaccionesWeb, "Page_Load", ex);
        }
    }

    private void cargarCombos()
    {
        List<Ejecutivo> lstZonas = new List<Ejecutivo>();
        EjecutivoService serviceEjecutivo = new EjecutivoService();

        UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        atrusuarios.codusuario = _usuario.codusuario;
        atrusuarios.tipoatribucion = 1;
        atrusuarios.activo = 1;
        List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, _usuario);
        if (atrusuarios != null && atrusuario.Count > 0)
        {
            //Lista de zonas en total
            lstZonas = serviceEjecutivo.ListarZonasDeEjecutivo(0, (Usuario)Session["usuario"]);
            List<Ejecutivo> LstEjecutivos = serviceEjecutivo.ListarEjecutivo((Usuario)Session["usuario"]);
            pnlAse.Visible = true;
            if (LstEjecutivos != null && LstEjecutivos.Count > 0)
            {
                ddlAsesores.DataSource = LstEjecutivos;
                ddlAsesores.DataValueField = "Codigo";
                ddlAsesores.DataTextField = "PrimerNombre";
                ddlAsesores.DataBind();
                ddlAsesores.Items.Insert(0, new ListItem("Seleccione", ""));
                ddlAsesores.SelectedValue = "";
            }
        }
        else
        {
            //Lista de zonas por ejecutivo
            lstZonas = serviceEjecutivo.ListarZonasDeEjecutivo(_usuario.codusuario, (Usuario)Session["usuario"]);
        }
        if (lstZonas.Count > 0)
        {
            ddlZona.DataSource = lstZonas;
            ddlZona.DataValueField = "icodciudad";
            ddlZona.DataTextField = "nomciudad";
            ddlZona.DataBind();
        }
        ddlZona.Items.Insert(0, new ListItem("Seleccione", ""));
        ddlZona.SelectedValue = "";
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Actualizar();
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarData();
    }


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
    }


    protected void LimpiarData()
    {
        VerError("");

        txtIdentificacion.Text = string.Empty;
        txtNombres.Text = string.Empty;
        ddlOrigen.Text = "2";
        ddlTipoSol.SelectedValue = "0";
        txtFechaInicial.Text = "";
        txtFechaFin.Text = "";
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        gvLista.DataSource = null;
        gvLista.DataBind();
    }
    
    void Actualizar()
    {
        try
        {            
            string filtro = obtFiltro();
           
            List<Xpinn.Integracion.Entities.PagoInterno> lstPagos = _pagoServices.listarPagosInternos(filtro, _usuario);
            gvLista.PageSize = 15;
            gvLista.DataSource = lstPagos;
            Site toolBar = (Site)Master;
            Session[Usuario.codusuario + "DTSolicitud"] = lstPagos;
            if (lstPagos.Count > 0)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstPagos.Count.ToString();
            }
            else
            {
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al consutlar los datos, " + ex.Message);
        }
    }                            

    string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        string filtro = string.Empty;

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and p.identificacion = '" + txtIdentificacion.Text + "' ";
        if (txtNombres.Text.Trim() != "")
            filtro += " and upper(p.PRIMER_NOMBRE||' '||p.SEGUNDO_NOMBRE||' '||p.PRIMER_APELLIDO||' '||p.SEGUNDO_APELLIDO) like upper('%"+txtNombres.Text+"%')";        
        if (ddlTipoSol.SelectedValue != "0")       
            filtro += " and B.Destino_tipo = " + ddlTipoSol.SelectedValue;
        if (ddlOrigen.SelectedValue != "2")
            filtro += " and B.Origen_tipo = " + ddlTipoSol.SelectedValue;
        if (!string.IsNullOrEmpty(txtFechaInicial.Text))
        {
            DateTime ini = Convert.ToDateTime(txtFechaInicial.Text);
            filtro += " and B.fecha > '"+ini.ToString("dd/MM/yy") + "'";
        }
        if (!string.IsNullOrEmpty(txtFechaFin.Text))
        {
            DateTime ini = Convert.ToDateTime(txtFechaFin.Text);
            filtro += " and B.fecha < '" + ini.ToString("dd/MM/yy") +"'";
        }
        if (ddlZona.SelectedValue != "")
        {
            if (ddlZona.SelectedValue == "0")
                filtro += " and p.cod_zona is null";
            else
                filtro += " and p.cod_zona = " + ddlZona.SelectedValue + " ";
        }

        UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        atrusuarios.codusuario = _usuario.codusuario;
        atrusuarios.tipoatribucion = 1;
        atrusuarios.activo = 1;
        List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, _usuario);
        if (atrusuarios != null && atrusuario.Count > 0)
        {
            if (ddlAsesores.SelectedValue != "")
            {
                if (chkSinAsesor.Checked)
                    filtro += " and (p.Cod_Asesor = " + ddlAsesores.SelectedValue + " or p.Cod_Asesor is null or p.Cod_Asesor = '' or p.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
                else
                    filtro += " and p.Cod_Asesor = " + ddlAsesores.SelectedValue + " ";
            }
        }
        else if (chkSinAsesor.Checked)
            filtro += " and (p.Cod_Asesor = " + _usuario.codusuario + " or p.Cod_Asesor is null or p.Cod_Asesor = '' or p.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
        else
            filtro += " and p.Cod_Asesor = " + _usuario.codusuario + " ";

        return filtro;
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
            VerError("Error al cambiar de pagina en la tabla, " + ex.Message);
        }
    }       

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session[Usuario.codusuario + "DTSolicitud"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ListaPersonas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }
}