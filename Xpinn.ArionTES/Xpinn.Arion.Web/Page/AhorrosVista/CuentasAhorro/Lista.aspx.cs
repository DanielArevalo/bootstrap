using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Globalization;

partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    PoblarListas Poblar = new PoblarListas();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                PanelGrilla.Visible = false;
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
                //if (Session[AhorroVistaServicio.CodigoPrograma + ".consulta"] != null)
                //    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    void CargarDropDown()
    {
        Poblar.PoblarListaDesplegable("OFICINA", " COD_OFICINA,NOMBRE ", " ESTADO = 1 ", " 1 ", ddlOficina, (Usuario)Session["usuario"]);

        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlEstado.Items.Insert(1, new ListItem("Apertura", "0"));
        ddlEstado.Items.Insert(2, new ListItem("Activa", "1"));
        ddlEstado.Items.Insert(3, new ListItem("Inactiva", "2"));
        ddlEstado.Items.Insert(4, new ListItem("Cerrada", "3"));
        ddlEstado.Items.Insert(5, new ListItem("Bloqueada", "4"));
        ddlEstado.Items.Insert(6, new ListItem("Embargada", "5"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();


    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[AhorroVistaServicio.CodigoPrograma + ".id"] = null;
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ddllineaahorro.Indice = 0;
        txtFecApertura.Text = "";
        PanelGrilla.Visible = false;
        gvLista.DataSource = null;
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        if (gvLista.Rows[e.NewEditIndex].Cells[11].Text != "Inactivo")
        {
            Session[AhorroVistaServicio.CodigoPrograma + ".id"] = id;
            Navegar(Pagina.Nuevo);
        }
        else
        {
            VerError("No se puede editar la cuenta, se encuentra inactiva");
            e.Cancel = true;
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Convert.ToString(e.Keys[0]);
            if (id != null)
            {
                ctlMensaje.MostrarMensaje("Desea realizar la eliminación del registro seleccionado?");
                Session["ID"] = id;
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            AhorroVistaServicio.EliminarAhorroVista(Session["ID"].ToString(), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (!string.IsNullOrWhiteSpace(txtNumCuenta.Text))
            filtro += " AND A.NUMERO_CUENTA = '" + txtNumCuenta.Text.Trim() + "'";
        if (ddllineaahorro.Indice != 0)
            filtro += " AND A.COD_LINEA_AHORRO = '" + ddllineaahorro.Value.Trim() + "'";
        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
            filtro += " AND P.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        if (!string.IsNullOrWhiteSpace(txtNomTitular.Text))
            filtro += " AND P.NOMBRE = '%" + txtNomTitular.Text.Trim() + "%'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += " AND A.COD_OFICINA = " + ddlOficina.SelectedValue.Trim();
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '" + txtCodigoNomina.Text + "'";
        if (ddlEstado.SelectedIndex != 0)
            filtro += "AND a.ESTADO = " + ddlEstado.SelectedValue;

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " WHERE " + filtro;
        }

        return filtro;
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            DateTime pFechaAper;
            pFechaAper = txtFecApertura.ToDateTime == null ? DateTime.MinValue : txtFecApertura.ToDateTime;
            string pFiltro = obtFiltro();
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            lstConsulta = AhorroVistaServicio.ListarAhorroVista(pFiltro, pFechaAper, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                PanelGrilla.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                PanelGrilla.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AhorroVistaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AhorroVista.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}