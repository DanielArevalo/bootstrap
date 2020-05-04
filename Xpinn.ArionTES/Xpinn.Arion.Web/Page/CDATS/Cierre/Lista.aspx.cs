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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    AdministracionCDATService AdmService = new AdministracionCDATService();
    AperturaCDATService ApertuService = new AperturaCDATService();
    PoblarListas Poblar = new PoblarListas();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ApertuService.CodigoProgramaCierre, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarExportar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ApertuService.CodigoProgramaCierre, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                panelGrilla.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ApertuService.CodigoProgramaCierre, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    void cargarDropdown()
    {
        //txtFechaVencimientoInicial.Text = Convert.ToString(DateTime.Now);
        Cdat Data = new Cdat();

        List<Cdat> lstTipoLinea = new List<Cdat>();
        lstTipoLinea = ApertuService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }


        List<Cdat> lstAsesores = new List<Cdat>();
        lstAsesores = ApertuService.ListarUsuariosAsesores(Data, (Usuario)Session["usuario"]);
        if (lstAsesores.Count > 0)
        {
            ddlAsesor.DataSource = lstAsesores;
            ddlAsesor.DataTextField = "nombre";
            ddlAsesor.DataValueField = "codusuario";
            ddlAsesor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlAsesor.SelectedIndex = 0;
            ddlAsesor.DataBind();
        }

        Poblar.PoblarListaDesplegable("OFICINA", " COD_OFICINA,NOMBRE", " estado = 1", "1", ddlOficina, (Usuario)Session["usuario"]);
    }



    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && Session["DTCDAT"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.Columns[0].Visible = false;
                gvLista.DataSource = Session["DTCDAT"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=AdministracionCDATS.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
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
            BOexcepcion.Throw(ApertuService.CodigoProgramaCierre, "gvLista_PageIndexChanging", ex);
        }
    }



    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //RECUPERAR  PERMITE CIERRE ANTICIPADO
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(581, (Usuario)Session["usuario"]);
        DateTime fechavencimiento = Convert.ToDateTime(gvLista.Rows[e.NewEditIndex].Cells[5].Text);



        if (fechavencimiento < DateTime.Now)
        {
            String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
            int estado = Convert.ToInt32(gvLista.DataKeys[e.NewEditIndex].Values[1].ToString());
            if (estado == 2) //SI ES ACTIVO
            {
                Session[ApertuService.CodigoProgramaCierre + ".id"] = id;
                Session[ApertuService.CodigoProgramaCierre + ".ov"] = null;
                Navegar(Pagina.Nuevo);
            }
            else
            {
                VerError("No puede Ingresar al registro seleccionado. Verifique que el estado este activo");
                e.NewEditIndex = -1;
            }

        }


        if (pData.valor == "1" && fechavencimiento > DateTime.Now)
        {
            String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
            int estado = Convert.ToInt32(gvLista.DataKeys[e.NewEditIndex].Values[1].ToString());
            if (estado == 2) //SI ES ACTIVO
            {
                Session[ApertuService.CodigoProgramaCierre + ".id"] = id;
                Session[ApertuService.CodigoProgramaCierre + ".ov"] = null;
                Navegar(Pagina.Nuevo);
            }
            else
            {
                VerError("No puede Ingresar al registro seleccionado. Verifique que el estado este activo");
                e.NewEditIndex = -1;
            }

        }

        else
        {
            VerError("El Cdat no permite cierre anticipado,revisar parámetro general 581");
            e.NewEditIndex = -1;
        }


    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ApertuService.EliminarAperturaCdat(Convert.ToInt64(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ApertuService.CodigoProgramaCierre, "btnContinuarMen_Click", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            List<AdministracionCDAT> lstConsulta = new List<AdministracionCDAT>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime FechaApe, FechaVencimientoInicial, FechaVencimientoFinal;

            FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
            FechaVencimientoInicial = txtFechaVencimientoInicial.ToDateTime == null ? DateTime.MinValue : txtFechaVencimientoInicial.ToDateTime;
            FechaVencimientoFinal = txtFechaVencimientoFinal.ToDateTime == null ? DateTime.MinValue : txtFechaVencimientoFinal.ToDateTime;

            lstConsulta = AdmService.ListarCdatCancelacion(filtro, FechaApe, (Usuario)Session["usuario"], FechaVencimientoInicial, FechaVencimientoFinal);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTCDAT"] = lstConsulta;
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.DataSource = null;
                Session["DTCDAT"] = null;
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarExportar(true);
            }

            Session.Add(ApertuService.CodigoProgramaCierre + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ApertuService.CodigoProgramaCierre, "Actualizar", ex);
        }
    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        txtFechaVencimientoInicial.Text = "";
        txtFechaVencimientoFinal.Text = "";
        txtFecha.Text = "";
        txtNroCDAT.Text = "";
        ddlTipoLinea.SelectedIndex = 0;
        ddlAsesor.SelectedIndex = 0;
        ddlOficina.SelectedIndex = 0;
        LimpiarValoresConsulta(pConsulta, ApertuService.CodigoProgramaCierre);
    }

    private AdministracionCDAT ObtenerValores()
    {
        AdministracionCDAT vApertu = new AdministracionCDAT();
        if (txtNroCDAT.Text.Trim() != "")
            vApertu.numero_cdat = txtNroCDAT.Text;
        if (ddlTipoLinea.SelectedIndex != 0)
            vApertu.cod_lineacdat = ddlTipoLinea.SelectedValue;
        if (ddlAsesor.SelectedIndex != 0)
            vApertu.cod_asesor_com = Convert.ToInt32(ddlAsesor.SelectedValue);
        if (ddlOficina.SelectedIndex != 0)
            vApertu.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);

        //if (txtFecha.Text != "")
        //    vApertu.fecha_apertura = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;


        //if (txtFechaVencimiento.Text != "")
        //    vApertu.fecha_vencimiento = txtFechaVencimiento.ToDateTime == null ? DateTime.MinValue : txtFechaVencimiento.ToDateTime;



        return vApertu;
    }



    private string obtFiltro(AdministracionCDAT vApertu)
    {
        String filtro = String.Empty;
        //if (this.txtFecha.Text != "")
        //    filtro += " and c.fecha_apertura = '" + vApertu.fecha_apertura.ToShortDateString() + "'";

        //if (txtFechaVencimiento.Text != "")
        //    filtro += " and c.FECHA_VENCIMIENTO = '" + vApertu.fecha_vencimiento.ToShortDateString() + "'";

        if (txtNroCDAT.Text.Trim() != "")
            filtro += " and  C.numero_cdat = '" + vApertu.numero_cdat + "'";
        if (ddlTipoLinea.SelectedIndex != 0)
            filtro += " and  C.cod_lineacdat = '" + vApertu.cod_lineacdat + "'";
        if (ddlAsesor.SelectedIndex != 0)
            filtro += " and  C.cod_asesor_com = " + vApertu.cod_asesor_com;
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and C.cod_oficina = " + vApertu.cod_oficina;
        filtro += " and C.ESTADO = 2";
        return filtro;
    }



    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}