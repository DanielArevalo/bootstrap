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

using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

partial class Nuevo : GlobalWeb
{
    private ParametroCtasObligacionService ParametroService = new ParametroCtasObligacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ParametroService.CodigoPrograma, "E");
            else
                VisualizarOpciones(ParametroService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvParametros.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarListas();
                if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ParametroService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ParametroService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {                    
                    txtCodigo.Text = "Autogenerado";
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "Page_Load", ex);
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


    private void CargarListas()
    {
        try
        {
            PoblarLista("OBLINEAOBLIGACION", ddlLineObligacion);
            PoblarLista("ESTRUCTURA_DETALLE", ddlEstructura);
            PoblarLista("obcomponente", ddlComponente);

            ddlTipoMov.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoMov.Items.Insert(1, new ListItem("Débito", "1"));
            ddlTipoMov.Items.Insert(2, new ListItem("Crédito", "2"));
            ddlTipoMov.SelectedIndex = 0;
            ddlTipoMov.DataBind();

            PoblarLista("bancos", ddlEntidad);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Par_Cue_Obligacion pConsulta = new Par_Cue_Obligacion();
            pConsulta = ParametroService.ConsultarParametroCtasOBLI(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (pConsulta.idparametro != 0)
                txtCodigo.Text = pConsulta.idparametro.ToString();
            if (pConsulta.codlineaobligacion != 0)
                ddlLineObligacion.SelectedValue = pConsulta.codlineaobligacion.ToString();
            if (pConsulta.cod_est_det != 0)
                ddlEstructura.SelectedValue = pConsulta.cod_est_det.ToString();
            if (pConsulta.cod_cuenta != "" && pConsulta.cod_cuenta != null)
                txtCodCuenta.Text = pConsulta.cod_cuenta;
            
            txtCodCuenta_TextChanged(txtCodCuenta, null);

            if (pConsulta.tipo_mov != 0)
                ddlTipoMov.SelectedValue = pConsulta.tipo_mov.ToString();
            if (pConsulta.codcomponente != 0)
                ddlComponente.SelectedValue = pConsulta.codcomponente.ToString();
            if (pConsulta.codentidad != 0)
                ddlEntidad.SelectedValue = pConsulta.codentidad.ToString();

            Session["tipo_cuenta"] = pConsulta.tipo_cuenta;
            Session["tipo"] = pConsulta.tipo;
        }
        catch (Exception ex)
        {            
            VerError(ex.Message);
        }
    }


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);

            // Mostrar el nombre de la cuenta           
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }


    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }
    

    Boolean validarDatos()
    {
        VerError("");
        if (ddlLineObligacion.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Aporte");
            return false;
        }
        if (ddlTipoMov.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Movimiento");
            return false;
        }
        if (txtCodCuenta.Text == "")
        {
            VerError("Seleccione una Cuenta Contable");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (validarDatos())
            {
                Par_Cue_Obligacion vData = new Par_Cue_Obligacion();

                if (idObjeto != "")
                {
                    vData.idparametro = Convert.ToInt32(txtCodigo.Text.Trim());
                    vData.tipo_cuenta = Convert.ToInt32(Session["tipo_cuenta"].ToString());
                    vData.tipo = Convert.ToInt32(Session["tipo"].ToString());
                }                
                else
                {
                    vData.idparametro = 0;
                    vData.tipo_cuenta = 1;
                    vData.tipo = 1;
                }
                vData.codlineaobligacion = Convert.ToInt32(ddlLineObligacion.SelectedValue);
                if (ddlEstructura.SelectedIndex != 0)
                    vData.cod_est_det = Convert.ToInt32(ddlEstructura.SelectedValue);
                else
                    vData.cod_est_det = 0;
                // CODIGO CUENTA LOCAL
                if (txtCodCuenta.Text != "" && txtNomCuenta.Text != "")
                    vData.cod_cuenta = txtCodCuenta.Text;
                else
                    vData.cod_cuenta = null;
                // TIPO DE TRANSACCION
                vData.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);

                if (ddlComponente.SelectedIndex != 0)
                    vData.codcomponente = Convert.ToInt32(ddlComponente.SelectedValue);
                else
                    vData.codcomponente = 0;

                if (ddlEntidad.SelectedIndex != 0)
                    vData.codentidad = Convert.ToInt32(ddlEntidad.SelectedValue);
                else
                    vData.codentidad = 0;

                if (idObjeto != "")
                {
                    ParametroService.CrearParamCtasObligacion(vData, (Usuario)Session["usuario"],2); // MODIFICAR
                }
                else
                {
                    ParametroService.CrearParamCtasObligacion(vData,(Usuario)Session["usuario"],1);//CREAR                    
                }

                string msj = idObjeto != "" ? "Modifico" : "Grabo";
                lblmsj.Text = "Se " + msj + " Correctamente los Datos";
                mvParametros.ActiveViewIndex = 1;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }
    

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnFin_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
}