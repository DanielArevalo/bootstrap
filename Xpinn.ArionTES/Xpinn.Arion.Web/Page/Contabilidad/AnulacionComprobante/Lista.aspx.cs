using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    Xpinn.Contabilidad.Services.AprobacionComprobantesService Aprobacionservice = new Xpinn.Contabilidad.Services.AprobacionComprobantesService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ComprobanteServicio.CodigoProgramaAnulacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAprobarComprobante.ActiveViewIndex = 0;
                CargarDDList();
                txtFechaIni.Text = System.DateTime.Now.ToString(gFormatoFecha);

                CargarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                Session["GENERAL"] = null;

                //CONSULTAR TABLA GENERAL
                Xpinn.Comun.Data.GeneralData ComunData = new Xpinn.Comun.Data.GeneralData();
                Xpinn.Comun.Entities.General General = new Xpinn.Comun.Entities.General();
                try
                {
                    General = ComunData.ConsultarGeneral(8, (Usuario)Session["usuario"]);
                }
                catch
                {
                    General.valor = "0";
                }
                if (General.valor == "1")
                {

                    Session["GENERAL"] = 1;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void CargarDDList()
    {
        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();



        Xpinn.Contabilidad.Entities.Comprobante TipoComprobantes = new Xpinn.Contabilidad.Entities.Comprobante();
        ddlMotivoAnulacion.DataSource = Aprobacionservice.ListarComprobanteTipoMotivoAnulacion(TipoComprobantes, (Usuario)Session["Usuario"]);
        ddlMotivoAnulacion.DataTextField = "DESCRIPCION";
        ddlMotivoAnulacion.DataValueField = "TIPO_MOTIVO";
        ddlMotivoAnulacion.DataBind();
        ddlMotivoAnulacion.Items.Insert(0, new ListItem("Motivo Anulación: ", "0"));
        ddlMotivoAnulacion.SelectedIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Int64 ventanActiva = mvAprobarComprobante.ActiveViewIndex;
        if (ventanActiva == 0)
        {
            try
            {
                GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                Actualizar();
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
            }
        }
        if (ventanActiva == 1)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarLimpiar(true);
            mvAprobarComprobante.ActiveViewIndex = 0;
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.Visible = false;
        txtNumComp.Text = "";
        ddlTipoComprobante.SelectedIndex = 0;
        txtFechaIni.Text = "";
        lblTotalRegs.Text = "";
        LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Contabilidad.Entities.Comprobante> lstConsulta = new List<Xpinn.Contabilidad.Entities.Comprobante>();
            string sFiltro = " ";

            // Verificar número de comprobante
            if (txtNumComp.Text.Trim() == "")
            {
                VerError("Debe ingresar el número de comprobante");
                return;
            }

            // Verificar que no este en operación
            Xpinn.Contabilidad.Entities.Comprobante anularcomp = new Xpinn.Contabilidad.Entities.Comprobante();
            anularcomp.cod_ope = ComprobanteServicio.ConsultarOperacion(Convert.ToInt64(txtNumComp.Text), Convert.ToInt64(ddlTipoComprobante.SelectedValue), (Usuario)Session["Usuario"]);
            if (anularcomp.cod_ope > 0)
            {
                VerError("No se puede anular este comprobante, tiene operaciones registradas ");
                return;
            }
            else
            {
                lstConsulta = Aprobacionservice.ListarComprobanteParaAprobar(ObtenerValores(), (Usuario)Session["usuario"], sFiltro);
                gvLista.DataSource = lstConsulta;
                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvLista.DataBind();
                    ValidarPermisosGrilla(gvLista);
                }
                else
                {
                    gvLista.Visible = false;
                    lblInfo.Visible = true;
                    lblTotalRegs.Visible = false;
                }
            }
            Session.Add(ComprobanteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.Comprobante ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Comprobante Comprobante = new Xpinn.Contabilidad.Entities.Comprobante();

        try
        {
            if (txtNumComp.Text.Trim() != "")
                Comprobante.num_comp = Convert.ToInt64(txtNumComp.Text.Trim());
            if (ddlTipoComprobante.SelectedValue != null && ddlTipoComprobante.SelectedIndex != 0)
                Comprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);
            if (Session["GENERAL"] != null && Session["GENERAL"].ToString() == "1")
                Comprobante.rptaLista = true;
            else
                Comprobante.rptaLista = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return Comprobante;
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Verificar que no este en operación
        Xpinn.Contabilidad.Entities.Comprobante anularcomp = new Xpinn.Contabilidad.Entities.Comprobante();
        anularcomp.cod_ope = ComprobanteServicio.ConsultarOperacion(Convert.ToInt64(gvLista.SelectedRow.Cells[1].Text), Convert.ToInt64(gvLista.SelectedRow.Cells[2].Text), (Usuario)Session["Usuario"]);
        if (anularcomp.cod_ope > 0)
        {
            VerError("No se puede anular este comprobante, tiene operaciones registradas ");
            return;
        }
        else
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarLimpiar(false);
            Session[ComprobanteServicio.CodigoProgramaAnulacion + ".seleccion"] = gvLista.SelectedIndex;
            String id = gvLista.SelectedRow.Cells[1].Text;
            String tipo_comp = gvLista.SelectedRow.Cells[2].Text;
            String identificacion = gvLista.SelectedRow.Cells[6].Text;
            Anular_comprobantes(id, tipo_comp, identificacion);
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
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (ddlMotivoAnulacion.SelectedIndex == 0)
        {
            VerError("Seleccione un motivo de anulación");
            return;
        }
    }

    protected void Anular_comprobantes(String ids, String tipocomps, String identificacion)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
        Xpinn.Contabilidad.Entities.DetalleComprobante vDetalleComprobante = new Xpinn.Contabilidad.Entities.DetalleComprobante();
        Xpinn.Contabilidad.Entities.Comprobante anularcomp = new Xpinn.Contabilidad.Entities.Comprobante();
        if (txtFechaIni.Text != "")
            anularcomp.fecha = Convert.ToDateTime(txtFechaIni.Text);
        anularcomp.idanulaicon = 0;
        if (tipocomps != "")
            anularcomp.tipo_comp = Convert.ToInt32(tipocomps);
        if (ddlMotivoAnulacion.SelectedIndex == 0)
        {
            VerError("Seleccione un motivo de anulación");
            return;
        }
        if (ddlMotivoAnulacion.SelectedIndex != 0)
            anularcomp.tipo_motivo = Convert.ToInt32(ddlMotivoAnulacion.SelectedValue);
        if (ids != "")
            anularcomp.num_comp = Convert.ToInt32(ids);

        anularcomp.cod_persona = usuap.codusuario;

        anularcomp.num_comp_anula = 0;
        anularcomp.tipo_comp_anula = 0;


        List<Xpinn.Contabilidad.Entities.Comprobante> LstDetalleComprobantes = new List<Xpinn.Contabilidad.Entities.Comprobante>();
        List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
   
        ///CREAR EL DETALLE Y EL ENCABEZADO CON LA ANULACION DEL COMPROBANTE
        ComprobanteServicio.crearanulacioncomprobante(LstDetalleComprobante, anularcomp, (Usuario)Session["Usuario"]);
        String num = ids;
        Session[ComprobanteServicio.CodigoProgramaAnulacion + ".num_comp"] = num;
        String sid = tipocomps;
        Session[ComprobanteServicio.CodigoProgramaAnulacion + ".tipo_comp"] = sid;
        String Sobservaciones = "COMPROBANTE_ANULADO Num:" + txtNumComp.Text + " "+ "Tipo:" + ddlTipoComprobante.SelectedValue;
        Session["Observaciones"] = Sobservaciones;
        Session["Comprobanteanulacion"] = num;
        Response.Redirect("~/Page/Contabilidad/Comprobante/Nuevo.aspx");
    }
    
   
    protected void ObtenerDatos(String pIdNComp, String pIdTComp)
    {
        try
        {

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoProgramaAnulacion, "ObtenerDatos", ex);
        }
    }

    protected void btnInfo_Click(object sender, ImageClickEventArgs e)
    {

    }

}