using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.InventariosServices InventariosServicio = new Xpinn.Tesoreria.Services.InventariosServices();
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();

    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(InventariosServicio.CodigoProgramaMC, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoProgramaMC, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoProgramaMC, "Page_Load", ex);
        }
    }

    protected void ObtenerDatos()
    {
        List<Xpinn.Tesoreria.Entities.ivimpuesto> lstConsulta = new List<Xpinn.Tesoreria.Entities.ivimpuesto>();
        Xpinn.Tesoreria.Entities.ivimpuesto _impuesto = new ivimpuesto();
        lstConsulta = InventariosServicio.ListarImpuestos(_impuesto, "", (Usuario)Session["usuario"]);
        if (lstConsulta.Count > 0)
        {
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["Detalle"] = lstConsulta;
        }

        List<Xpinn.Tesoreria.Entities.ivconceptos> lstConceptos = new List<Xpinn.Tesoreria.Entities.ivconceptos>();
        Xpinn.Tesoreria.Entities.ivconceptos _conceptos = new ivconceptos();
        lstConceptos = InventariosServicio.ListarConceptos(_conceptos, (Usuario)Session["usuario"]);
        if (lstConceptos.Count > 0)
        {
            gvImpuestos.DataSource = lstConceptos;
            gvImpuestos.DataBind();
            Session["DetalleConceptos"] = lstConceptos;
        }
    }

    #endregion
    
    
    #region Eventos Botonera


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            List<ivimpuesto> lstDetalle = new List<ivimpuesto>();
            lstDetalle = ObtenerListaGridView();

            // Si llega nulo es porque hubo algun error
            if (lstDetalle != null)
            {
                if (lstDetalle.Count > 0)
                {
                    foreach (ivimpuesto item in lstDetalle)
                    {
                        ParCueIvimpuesto _parametro = new ParCueIvimpuesto();
                        _parametro.idparametro = Convert.ToInt64(item.idparametro);
                        _parametro.id_impuesto = item.id_impuesto;
                        _parametro.cod_cuenta = item.cod_cuenta;
                        _parametro.cod_cuenta_niif = item.cod_cuenta_niif;
                        _parametro.tipo = 1;
                        ParCueIvimpuesto result = InventariosServicio.ModificarParCueIvimpuesto(_parametro, (Usuario)Session["usuario"]);
                    }
                }
                else
                {
                    VerError("Algo salió mal, intenta nuevamente más tarde!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoProgramaMC, "btnGuardar_Click", ex);
        }

        try
        {
            List<ivconceptos> lstDetalle = new List<ivconceptos>();
            lstDetalle = ObtenerListaGridViewCon();

            // Si llega nulo es porque hubo algun error
            if (lstDetalle != null)
            {
                if (lstDetalle.Count > 0)
                {
                    foreach (ivconceptos item in lstDetalle)
                    {
                        ParCueIvimpuesto _parametro = new ParCueIvimpuesto();
                        _parametro.idparametro = 0;
                        _parametro.id_impuesto = Convert.ToInt32(item.id_concepto);
                        _parametro.cod_cuenta = item.cod_cuenta;
                        _parametro.cod_cuenta_niif = item.cod_cuenta_niif;
                        _parametro.tipo = 0;
                        ParCueIvimpuesto result = InventariosServicio.ModificarParCueIvimpuesto(_parametro, (Usuario)Session["usuario"]);
                    }                    
                }
                else
                {
                    VerError("Algo salió mal, intenta nuevamente más tarde!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoProgramaMC, "btnGuardar_Click", ex);
        }
        ObtenerDatos();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    #endregion


    #region Eventos GridView

    protected void gvImpuestos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBoxGrid txtCodCuentaCon = (TextBoxGrid)e.Row.FindControl("txtCodCuentaCon");
            if (txtCodCuentaCon != null)
            {
                // Determinar los datos de la cuenta contable
                Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
                Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
                PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaCon.Text, (Usuario)Session["usuario"]);
                int rowIndex = Convert.ToInt32(txtCodCuentaCon.CommandArgument);
                // Mostrar el nombre de la cuenta
                TextBoxGrid ddlNomCuentaCon = (TextBoxGrid)e.Row.FindControl("ddlNomCuentaCon");
                if (ddlNomCuentaCon != null)
                    ddlNomCuentaCon.Text = PlanCuentas.nombre;
            }
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBoxGrid txtCodCuenta = (TextBoxGrid)e.Row.FindControl("txtCodCuenta");
            if (txtCodCuenta != null)
            {
                // Determinar los datos de la cuenta contable
                Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
                Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
                PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
                int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);
                // Mostrar el nombre de la cuenta
                TextBoxGrid ddlNomCuenta = (TextBoxGrid)e.Row.FindControl("ddlNomCuenta");
                if (ddlNomCuenta != null)
                    ddlNomCuenta.Text = PlanCuentas.nombre;
            }
        }
    }


    #endregion


    #region Metodos De Ayuda

    protected List<ivimpuesto> ObtenerListaGridView()
    {
        List<ivimpuesto> lista = new List<ivimpuesto>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            ivimpuesto mImpuesto = new ivimpuesto();

            Label lblparametro = (Label)rfila.FindControl("lblparametro");
            if (lblparametro != null)
                mImpuesto.idparametro = ConvertirStringToIntN(lblparametro.Text);

            Label lblIdimpuesto = (Label)rfila.FindControl("lblIdimpuesto");
            if (lblIdimpuesto != null)
                mImpuesto.id_impuesto = ConvertirStringToInt(lblIdimpuesto.Text);

            Label lblImpuesto = (Label)rfila.FindControl("lblImpuesto");
            if (lblImpuesto != null)
                mImpuesto.descripcion = lblImpuesto.Text;

            TextBoxGrid txtCodCuenta = (TextBoxGrid)rfila.FindControl("txtCodCuenta");
            if (txtCodCuenta != null)
                mImpuesto.cod_cuenta = txtCodCuenta.Text; 

            TextBoxGrid txtCodCuenta_niif = (TextBoxGrid)rfila.FindControl("txtCodCuenta_niif");
            if (txtCodCuenta_niif != null)
                mImpuesto.cod_cuenta_niif = txtCodCuenta_niif.Text;

            lista.Add(mImpuesto);
        }

        return lista;
    }

    protected List<ivconceptos> ObtenerListaGridViewCon()
    {
        List<ivconceptos> lista = new List<ivconceptos>();

        foreach (GridViewRow rfila in gvImpuestos.Rows)
        {
            ivconceptos mImpuesto = new ivconceptos();

            Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
            if (lblCodigo != null)
                mImpuesto.id_concepto = ConvertirStringToIntN(lblCodigo.Text);

            Label lblNombre = (Label)rfila.FindControl("lblNombre");
            if (lblNombre != null)
                mImpuesto.nombre = lblNombre.Text;

            Label lblBaseMinima = (Label)rfila.FindControl("lblBaseMinima");
            if (lblBaseMinima != null)
                mImpuesto.base_minima = ConvertirStringToDecimal(lblBaseMinima.Text);

            Label lblPorcentaje = (Label)rfila.FindControl("lblPorcentaje");
            if (lblPorcentaje != null)
                mImpuesto.porcentaje_calculo = ConvertirStringToDecimal(lblPorcentaje.Text);

            TextBoxGrid txtCodCuentaCon = (TextBoxGrid)rfila.FindControl("txtCodCuentaCon");
            if (txtCodCuentaCon != null)
                mImpuesto.cod_cuenta = txtCodCuentaCon.Text;

            lista.Add(mImpuesto);
        }

        return lista;
    }

    #endregion


    #region eventos còdigos de cuenta

    protected void txtCodCuentaCon_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuentaCon = null;
        GridViewRow rFila = null;
        try
        {
            txtCodCuentaCon = (TextBoxGrid)sender;
        }
        catch
        {
            rFila = ((GridViewRow)((GridView)sender).NamingContainer.NamingContainer);
            txtCodCuentaCon = (TextBoxGrid)gvImpuestos.Rows[rFila.RowIndex].FindControl("txtCodCuentaCon");
        }

        if (txtCodCuentaCon != null)
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaCon.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtCodCuentaCon.CommandArgument);

            // Mostrar el nombre de la cuenta
            TextBoxGrid ddlNomCuentaCon = (TextBoxGrid)gvImpuestos.Rows[rowIndex].FindControl("ddlNomCuentaCon");
            if (ddlNomCuentaCon != null)
                ddlNomCuentaCon.Text = PlanCuentas.nombre;
        }
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuenta = null;
        GridViewRow rFila = null;
        try
        {
            txtCodCuenta = (TextBoxGrid)sender;
        }
        catch
        {
            rFila = ((GridViewRow)((GridView)sender).NamingContainer.NamingContainer);
            txtCodCuenta = (TextBoxGrid)gvLista.Rows[rFila.RowIndex].FindControl("txtCodCuenta");
        }

        if (txtCodCuenta != null)
        {
            
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            // Mostrar el nombre de la cuenta
            TextBoxGrid ddlNomCuenta = (TextBoxGrid)gvLista.Rows[rowIndex].FindControl("ddlNomCuenta");
            if (ddlNomCuenta != null)
                ddlNomCuenta.Text = PlanCuentas.nombre;              

        }
    }

    protected void txtCodCuenta_niif_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuenta_niif = null;
        GridViewRow rFila = null;
        try
        {
            txtCodCuenta_niif = (TextBoxGrid)sender;
        }
        catch
        {
            rFila = ((GridViewRow)((GridView)sender).NamingContainer.NamingContainer);
            txtCodCuenta_niif = (TextBoxGrid)gvLista.Rows[rFila.RowIndex].FindControl("txtCodCuenta");
        }

        if (txtCodCuenta_niif != null)
        {

            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta_niif.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtCodCuenta_niif.CommandArgument);

            // Mostrar el nombre de la cuenta
            TextBoxGrid ddlNomCuenta_niif = (TextBoxGrid)gvLista.Rows[rowIndex].FindControl("ddlNomCuenta_niif");
            if (ddlNomCuenta_niif != null)
                ddlNomCuenta_niif.Text = PlanCuentas.nombre;

        }
    }

    protected void btnListadoPlanCon_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlanCon = (ButtonGrid)sender;
        if (btnListadoPlanCon != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlanCon.CommandArgument);
            ctlPlanCuentas ctlListadoPlanCon = (ctlPlanCuentas)gvImpuestos.Rows[rowIndex].FindControl("ctlListadoPlanCon");
            TextBoxGrid txtCodCuentaCon = (TextBoxGrid)gvImpuestos.Rows[rowIndex].FindControl("txtCodCuentaCon");
            TextBoxGrid ddlNomCuentaCon = (TextBoxGrid)gvImpuestos.Rows[rowIndex].FindControl("ddlNomCuentaCon");
            if (ctlListadoPlanCon != null)
                ctlListadoPlanCon.Motrar(true, "txtCodCuentaCon", "ddlNomCuentaCon");
            txtCodCuentaCon_TextChanged(txtCodCuentaCon, null);
        }

        return;
    }


    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan.CommandArgument);
            ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)gvLista.Rows[rowIndex].FindControl("ctlListadoPlan");
            TextBoxGrid txtCodCuenta = (TextBoxGrid)gvLista.Rows[rowIndex].FindControl("txtCodCuenta");
            TextBoxGrid ddlNomCuenta = (TextBoxGrid)gvLista.Rows[rowIndex].FindControl("ddlNomCuenta");
            ctlListadoPlan.Motrar(true, "txtCodCuenta", "ddlNomCuenta");
            txtCodCuenta_TextChanged(txtCodCuenta, null);
        }

        return;
    }

    protected void btnListadoPlan_niif_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan_niif = (ButtonGrid)sender;
        if (btnListadoPlan_niif != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan_niif.CommandArgument);
            ctlPlanCuentasNif ctlListadoPlan = (ctlPlanCuentasNif)gvLista.Rows[rowIndex].FindControl("ctlListadoPlan_niif");
            TextBoxGrid txtCodCuenta_niif = (TextBoxGrid)gvLista.Rows[rowIndex].FindControl("txtCodCuenta_niif");
            TextBoxGrid txtNomCuentaNif = (TextBoxGrid)gvLista.Rows[rowIndex].FindControl("ddlNomCuenta_niif");
            ctlListadoPlan.Motrar(true, "txtCodCuenta_niif", "ddlNomCuenta_niif");
            txtCodCuenta_niif_TextChanged(txtCodCuenta_niif, null);
        }

        return;
    }
    #endregion


}
