using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using System.Linq;

partial class Lista : GlobalWeb
{
    CuentaService CuentaService = new CuentaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("220508", "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("220508", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                // Cargar listas desplegables
                cargarDropdown();
               // Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("220508", "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            if (mvPrincipal.ActiveViewIndex == 1)
            {
                mvPrincipal.ActiveViewIndex = 0;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
            }
            Actualizar();
        }
    }


    protected void cargarDropdown()
    {
        TarjetaService tarjetaServicio = new TarjetaService();
        ddloficina.DataSource = tarjetaServicio.ListarOficina(new Tarjeta(), (Usuario)Session["Usuario"]);
        ddloficina.DataTextField = "oficina";
        ddloficina.DataValueField = "cod_oficina";
        ddloficina.DataBind();
        ddloficina.Items.Insert(0, new ListItem("Selecione un item", "0"));
        ddloficina.SelectedIndex = 0;

        ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCuenta.Items.Insert(1, new ListItem("Ahorros", "A"));
        ddlTipoCuenta.Items.Insert(2, new ListItem("Credito Rotativo", "C"));
        ddlTipoCuenta.SelectedIndex = 0;
        ddlTipoCuenta.DataBind();
    }

    protected void Check_Clicked(object sender, EventArgs e)
    {
        CheckBox chkHeader = sender as CheckBox;

        if (chkHeader.Checked == true)
        {
            if (gvLista.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvLista.Rows)
                {
                    CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                    CheckBoxgv.Checked = true;

                }

            }
        }
        else
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                CheckBoxgv.Checked = false;

            }
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
            BOexcepcion.Throw("220508", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Cuenta> lstConsulta = new List<Cuenta>();
            List<Tarjeta> tajeta_verifi = new List<Tarjeta>();
            CuentaService cuentaservice = new CuentaService();

            lstConsulta = CuentaService.ListarCuentaAsignacion(ObtenerValores(), (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                if (ddlEstadoCuenta.SelectedValue != "")
                {
                    lstConsulta = lstConsulta.Where(x => x.estado == ddlEstadoCuenta.SelectedValue).ToList();
                }

            }

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session["DTCUENTAS"] = lstConsulta;
            Session.Add("220508" + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("220508", "Actualizar", ex);
        }
    }

    private Cuenta ObtenerValores()
    {
        Cuenta entitytarjeta = new Cuenta();

        if (!string.IsNullOrEmpty(ddloficina.SelectedValue))
            entitytarjeta.cod_oficina = Convert.ToInt32(ddloficina.SelectedValue);

        if (!string.IsNullOrEmpty(ddlTipoCuenta.SelectedValue))
            entitytarjeta.tipocuenta = ddlTipoCuenta.SelectedValue;

        if (!string.IsNullOrEmpty(txtNumIdent.Text.Trim()))
            entitytarjeta.identificacion = Convert.ToString(txtNumIdent.Text.Trim());

        if (!string.IsNullOrEmpty(txtNumCuenta.Text.Trim()))
            entitytarjeta.numero_cuenta = Convert.ToString(txtNumCuenta.Text.Trim());

        return entitytarjeta;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            ctlMensaje.MostrarMensaje("Desea agregar estas cuentas para ser reportadas en Enpacto?");
        }
        catch
        {
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        List<Cuenta> lstcuentas = new List<Cuenta>();

        if (gvLista.Rows.Count > 0)
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                if (CheckBoxgv != null)
                {
                    if (CheckBoxgv.Checked == true)
                    {
                        Int64 Consecutivo;
                        Cuenta Entidad = new Cuenta();
                        Entidad.cod_persona = Convert.ToInt64(row.Cells[1].Text.ToString().Trim());
                        Entidad.tipocuenta = row.Cells[7].Text.ToString().Trim();
                        Entidad.numero_cuenta = row.Cells[8].Text.ToString().Trim();

                        Consecutivo = CuentaService.AsignarCuenta(Entidad, (Usuario)Session["usuario"]);
                    }
                }
            }
        }

        mvPrincipal.ActiveViewIndex = 1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
    }
}