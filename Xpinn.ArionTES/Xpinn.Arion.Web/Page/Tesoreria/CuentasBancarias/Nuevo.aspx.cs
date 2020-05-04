using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

using Xpinn.Contabilidad.Services;

public partial class Nuevo : GlobalWeb
{
    CuentasBancariasServices CuentaService = new CuentasBancariasServices();
    PoblarListas PoblarDrop = new PoblarListas();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[CuentaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CuentaService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CuentaService.CodigoPrograma, "N");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                cargarDropdown();
                txtCodigo.Enabled = false;
                rblEstado.SelectedValue = "1";
                if (Session[CuentaService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CuentaService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CuentaService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = "Autogenerado";
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.GetType().Name + "L", "Page_Load", ex);
        }
    }


    void cargarDropdown()
    {
        List<CuentasBancarias> lstConsulta = new List<CuentasBancarias>();
        lstConsulta = CuentaService.ListarALLBancos((Usuario)Session["usuario"]);

        if (lstConsulta.Count > 0)
        {
            ddlBanco.DataSource = lstConsulta;
            ddlBanco.DataTextField = "nombrebanco";
            ddlBanco.DataValueField = "cod_banco";
            ddlBanco.AppendDataBoundItems = true;
            ddlBanco.Items.Insert(0, new ListItem("Selecione un item", "0"));
            ddlBanco.SelectedIndex = 0;
            ddlBanco.DataBind();
        }
       

        ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCuenta.Items.Insert(1, new ListItem("Ahorros", "1"));
        ddlTipoCuenta.Items.Insert(2, new ListItem("Corriente", "2"));
        ddlTipoCuenta.SelectedIndex = 0;
        ddlTipoCuenta.DataBind();

        PoblarDrop.PoblarListaDesplegable("OFICINA", "cod_oficina, nombre", "estado = 1", "2", ddlOficina, (Usuario)Session["usuario"]);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            CuentasBancarias vCuentas = new CuentasBancarias();
            
            vCuentas = CuentaService.ConsultarCuentasBancarias(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCuentas.idctabancaria != 0)
                txtCodigo.Text = vCuentas.idctabancaria.ToString();
            if (vCuentas.cod_banco != 0)
                ddlBanco.SelectedValue = vCuentas.cod_banco.ToString();
            if (vCuentas.num_cuenta != "")
                txtnumCuenta.Text = vCuentas.num_cuenta;
            if (vCuentas.tipo_cuenta != 0)
                ddlTipoCuenta.SelectedValue = vCuentas.tipo_cuenta.ToString();
            if (vCuentas.cod_cuenta != "")
                txtCodCuenta.Text = vCuentas.cod_cuenta;
            txtCodCuenta_TextChanged(txtCodCuenta, null);
            if (vCuentas.cod_oficina != 0)
                ddlOficina.SelectedValue = vCuentas.cod_oficina.ToString();

            if (vCuentas.estado != "")
                rblEstado.SelectedValue = vCuentas.estado;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    public Boolean ValidarDatos()
    {
        if (ddlBanco.SelectedValue == "0")
        {
            VerError("Seleccione el banco de la Cuenta");
            return false;
        }

        if (ddlTipoCuenta.SelectedIndex == 0) 
        {
            VerError("Seleccione el Tipo de Cuenta");
            return false;
        }
        if (txtCodCuenta.Text == "")
        {
            VerError("Ingrese o Seleccone una Cuenta Contable");
            return false;
        }
        if (txtnumCuenta.Text == "")
        {
            VerError("Ingrese el Número de cuenta");
            return false;
        }
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "Modificar" : "Grabar";
            ctlMensaje.MostrarMensaje("Esta seguro de " + msj + " los datos ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario User = (Usuario)Session["usuario"];

            CuentasBancarias eCuenta = new CuentasBancarias();
            if (idObjeto != "")
                eCuenta.idctabancaria = Convert.ToInt32(txtCodigo.Text);
            else
                eCuenta.idctabancaria = 0;
            eCuenta.cod_banco = Convert.ToInt32(ddlBanco.SelectedValue);
            eCuenta.num_cuenta = txtnumCuenta.Text;
            eCuenta.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            eCuenta.cod_cuenta = txtCodCuenta.Text;
            eCuenta.estado = rblEstado.SelectedValue;
            eCuenta.cod_oficina = ddlOficina.SelectedIndex != 0 ? Convert.ToInt64(ddlOficina.SelectedValue) : 0; 

            if (idObjeto != "")
            {
                //MODIFICAR
                CuentaService.CrearCuentasContables(eCuenta, (Usuario)Session["usuario"], 2);
            }
            else
            {
                //CREAR             
                CuentaService.CrearCuentasContables(eCuenta, (Usuario)Session["usuario"], 1);
            }

            mvAplicar.ActiveViewIndex = 1;
            string msj = idObjeto != "" ? "modificaron" : "grabaron";
            lblmsj.Text = "Se " + msj + " los datos correctamente"; 
            //Navegar("~/Page/RecaudosMasivos/Empresas/Lista.aspx");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }
    

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
            //int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }
}
