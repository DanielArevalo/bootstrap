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

using Xpinn.ConciliacionBancaria.Services;
using Xpinn.ConciliacionBancaria.Entities;

partial class Lista : GlobalWeb
{
    private ExtractoBancarioServices ExtractoServicio = new ExtractoBancarioServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ExtractoServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                CargarValoresConsulta(pConsulta, ExtractoServicio.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

    private void CargarDropDown()
    {
        Xpinn.Caja.Services.BancosService BOBancos = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos pEntidad = new Xpinn.Caja.Entities.Bancos();
        List<Xpinn.Caja.Entities.Bancos> lstBancos = new List<Xpinn.Caja.Entities.Bancos>();
        lstBancos = BOBancos.ListarBancosegre((Usuario)Session["usuario"]);

        ddlBanco.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        if (lstBancos.Count > 0)
        {
            ddlBanco.DataSource = lstBancos;
            ddlBanco.DataTextField = "nombrebanco";
            ddlBanco.DataValueField = "cod_banco";
            ddlBanco.AppendDataBoundItems = true;
            ddlBanco.DataBind();
            ddlBanco.SelectedIndex = 0;
        }
        //PoblarLista("bancos", ddlBanco);
        ddlEstado.Items.Insert(0, new ListItem("Pendiente", "0"));
        ddlEstado.Items.Insert(1, new ListItem("Conciliado", "1"));
        ddlEstado.Items.Insert(2, new ListItem("Anulado", "2"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();

        Xpinn.Tesoreria.Services.CuentasBancariasServices vData = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        List<Xpinn.Tesoreria.Entities.CuentasBancarias> lstConsulta = new List<Xpinn.Tesoreria.Entities.CuentasBancarias>();
        lstConsulta = vData.ListarCuentasBancarias1((Usuario)Session["usuario"]);
        if (lstConsulta.Count > 0)
        {
            ddlCuenta.DataSource = lstConsulta;
            ddlCuenta.DataTextField = "num_cuenta";
            ddlCuenta.DataValueField = "num_cuenta";
            ddlCuenta.AppendDataBoundItems = true;
            ddlCuenta.Items.Insert(0, new ListItem("Seleccione un item","0"));
            ddlCuenta.SelectedIndex = 0;
            ddlCuenta.DataBind();
        }
    }

    private void Actualizar()
    {
        try
        {
            List<ExtractoBancario> lstConsulta = new List<ExtractoBancario>();
            string filtro = obtFiltro();
            lstConsulta = ExtractoServicio.ListarExtractoBancario(filtro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfo.Visible = false;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                lblInfo.Visible = true;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ExtractoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        string filtro = "";

        if (txtCodigo.Text.Trim() != "")
            filtro += " and e.idextracto = " + txtCodigo.Text.Trim();
        if (ddlBanco.SelectedIndex != 0)
            filtro += " and e.cod_banco = " + ddlBanco.SelectedValue;
        if (ddlCuenta.SelectedIndex != 0)
            filtro += " and e.numero_cuenta = '" + ddlCuenta.SelectedValue + "'";
        if (txtPeriodo.Text.Trim() != "")
            filtro += " and e.periodo = '" + txtPeriodo.Text.Trim() +"'";

        filtro += " and e.estado = " + ddlEstado.SelectedValue;

        return filtro;
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ExtractoServicio.CodigoPrograma);
    }

       
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[ExtractoServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea realizar la eliminación del registro seleccionado?");
        }
        catch (Exception ex)
        {
            VerError("gvLista_RowDeleting "+ex.Message);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ID"].ToString() != "")
                ExtractoServicio.EliminarExtractoBancario(Convert.ToInt64(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
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
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }   


}