using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Microsoft.Reporting.WebForms;
using Xpinn.Ahorros.Entities;
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Data;
using Xpinn.Comun.Entities;

public partial class Nuevo : GlobalWeb
{

    PoblarListas poblar = new PoblarListas();
    Xpinn.Contabilidad.Services.Par_Cue_OtrosService servicioParCueAuxilios = new Xpinn.Contabilidad.Services.Par_Cue_OtrosService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[servicioParCueAuxilios.codigoProgramaParametro + ".id"] != null)
                VisualizarOpciones(servicioParCueAuxilios.codigoProgramaParametro, "E");
            else
                VisualizarOpciones(servicioParCueAuxilios.codigoProgramaParametro, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarGuardar(false);            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioParCueAuxilios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarddl();
                if (Session[servicioParCueAuxilios.codigoProgramaParametro + ".id"] != null)
                {
                    idObjeto = Session[servicioParCueAuxilios.codigoProgramaParametro + ".id"].ToString();
                    Session.Remove(servicioParCueAuxilios.codigoProgramaParametro + ".id");
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    ObtenerDatos(Convert.ToInt64(idObjeto));
                }
                else
                {
                    txtCodigo.Text = servicioParCueAuxilios.ObtenerSiguienteCodigo_ParCue_LinAHO((Usuario)Session["usuario"]).ToString();
                    Site toolBar = (Site)this.Master;
                    toolBar.eventoGuardar += btnGuardar_Click;
                    toolBar.MostrarGuardar(true);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioParCueAuxilios.GetType().Name + "L", "Page_Load", ex);
        }

    }
    // carga ddl
    protected void cargarddl()
    {
        ddlTipoAhorro.Items.Insert(0, new ListItem("Seleccione Un Item ", "0"));
        ddlTipoAhorro.Items.Insert(1, new ListItem("Ahorro A la Vista ", "3"));
        ddlTipoAhorro.Items.Insert(2, new ListItem("Ahorro Programado ", "9"));
        ddlTipoAhorro.Items.Insert(3, new ListItem("CDAT", "5"));

        ddlTipoMov.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoMov.Items.Insert(1, new ListItem("Débito", "1"));
        ddlTipoMov.Items.Insert(2, new ListItem("Crédito", "2"));
        ddlTipoMov.SelectedIndex = 0;
        ddlTipoMov.DataBind();
    }

    // redirecciona ala pagina principal para cancelar acciones 
    public void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public void limpiarcampos()
    {
        ctlListarCodigoAhorro.LimpiarControl();
        ddlTipoAhorro.ClearSelection();
        ctlListarCodigoTransaccion.LimpiarControl();
        txtCodigo.Text = "";
        txtCodCuenta.Text = string.Empty;

    }

    protected void ObtenerDatos(Int64 idObjeto)
    {
        Par_Cue_LinAho entidad = servicioParCueAuxilios.getParametroByIdServices((Usuario)Session["usuario"], idObjeto);
        // seleccion en index indicado de los ddl
        txtCodigo.Text = idObjeto.ToString();
        ddlTipoAhorro.SelectedValue = entidad.TipoAhorro;
        ddlTipoAhorro_SelectedIndexChanged(ddlTipoAhorro, null);
        if (!string.IsNullOrWhiteSpace(entidad.LineaAhorro))
            ctlListarCodigoAhorro.SelectedValue(entidad.LineaAhorro);
        if (entidad.tipo_mov != 0)
            ddlTipoMov.SelectedValue = entidad.tipo_mov.ToString();
        if (!string.IsNullOrWhiteSpace(entidad.tipoTrasaccion))
            ctlListarCodigoTransaccion.SelectedValue(entidad.tipoTrasaccion);
        if (entidad.CodigoCuenta != null)
        {
            txtCodCuenta.Text = entidad.CodigoCuenta;
            txtCodCuenta_TextChanged(txtCodCuenta, null);
        }
        if (entidad.cod_cuenta_niif != null)
        {
            txtCodCuentaNIF.Text = entidad.cod_cuenta_niif;
            txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);
        }
    }

    // evento del control
    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    // para guardar parametros nuevo
    protected Par_Cue_LinAho getDatosEntidadParametros()
    {
        Par_Cue_LinAho entidadcargar = new Par_Cue_LinAho();

        if (idObjeto != "")
            entidadcargar.Codigo = Convert.ToInt64(idObjeto);
        else
            entidadcargar.Codigo = 0;
        entidadcargar.TipoAhorro = ddlTipoAhorro.SelectedValue;
        entidadcargar.LineaAhorro = ctlListarCodigoAhorro.Codigo;
        if (ddlTipoMov.SelectedValue != null)
            if (ddlTipoMov.SelectedValue != "")
                entidadcargar.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
        if (!string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo))
            entidadcargar.tipoTrasaccion = ctlListarCodigoTransaccion.Codigo;
        entidadcargar.CodigoCuenta = txtCodCuenta.Text;
        if (txtCodCuentaNIF.Text.Trim() != "")
            entidadcargar.cod_cuenta_niif = txtCodCuentaNIF.Text;

        return entidadcargar;
    }

    // nuevo
    public void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Validar())
        {
            if (idObjeto != "")
            {
                //MODIFICAR
                servicioParCueAuxilios.updateParametroServices((Usuario)Session["usuario"], getDatosEntidadParametros());                
            }
            else
            {
                //CREAR
                servicioParCueAuxilios.insertarParametroServices((Usuario)Session["usuario"], getDatosEntidadParametros());
            }
            limpiarcampos();
            Navegar(Pagina.Lista);
        }
    }



    private bool Validar()
    {
        if (txtCodigo.Text.Trim() == "")
        {
            VerError("Ingrese el código de la parametrización");
            return false;
        }


        if (ddlTipoAhorro.SelectedItem != null)
        {
            if (ddlTipoAhorro.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de ahorro");
                ddlTipoAhorro.Focus();
                return false;
            }
        }


        if (string.IsNullOrWhiteSpace(ctlListarCodigoAhorro.Codigo) && ctlListarCodigoAhorro.Codigo == "0")
        {
            VerError("Seleccione la Linea de ahorro");
            return false;
        }


        if (string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo) && ctlListarCodigoTransaccion.Codigo == "0")
        {
            VerError("Seleccione el tipo de transaccion");
            return false;
        }
    

        if (txtCodCuenta.Text.Trim() == "")
        {
            VerError("Ingrese o seleccione la cuenta contable local");
            txtCodCuenta.Focus();
            return false;
        }

        return true;
    }

    // carga las lista Segun el filtro del tipo de cuenta
    protected void ddlTipoAhorro_SelectedIndexChanged(object sender, EventArgs e)
    {
        String tabla = "";
        ctlListarCodigoAhorro.LimpiarControl();
        ctlListarCodigoTransaccion.LimpiarControl();

        if (ddlTipoAhorro.SelectedIndex != 0)
        {
            String codigo = ddlTipoAhorro.SelectedValue;

            switch (codigo)
            {
                case "3":
                    tabla = "lineaahorro";
                    break;
                case "9":
                    tabla = "lineaprogramado";
                    break;
                case "5":
                    tabla = "lineacdat";
                    break;
            }

            List<ListaDesplegable> plista = new List<ListaDesplegable>();
            Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

            plista = pservicio.ListarListaDesplegable(new ListaDesplegable(), tabla, "", "", "", (Usuario)Session["usuario"]);

            if (plista.Count > 0)
            {
                ctlListarCodigoAhorro.TextField = "descripcion";
                ctlListarCodigoAhorro.ValueField = "idconsecutivo";
                ctlListarCodigoAhorro.BindearControl(plista);
            }

            ParametroCtasOtrosData objeparam = new ParametroCtasOtrosData();
            List<Par_Cue_LinAho> lista = new ParametroCtasOtrosData().llenarLista((Usuario)Session["usuario"], ddlTipoAhorro.SelectedValue);

            if (lista.Count > 0)
            {
                ctlListarCodigoTransaccion.ValueField = "tipoTrasaccion";
                ctlListarCodigoTransaccion.TextField = "NombreCuenta";
                ctlListarCodigoTransaccion.BindearControl(lista);
            }

            if (plista.Count > 0 || lista.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }
        }
        else
        {
            List<ListaDesplegable> plista = new List<ListaDesplegable>();
            ctlListarCodigoAhorro.TextField = "descripcion";
            ctlListarCodigoAhorro.ValueField = "idconsecutivo";

            ctlListarCodigoAhorro.BindearControl(plista);

            List<Par_Cue_LinAho> lista = new List<Par_Cue_LinAho>();
            ctlListarCodigoTransaccion.ValueField = "tipoTrasaccion";
            ctlListarCodigoTransaccion.TextField = "NombreCuenta";

            ctlListarCodigoTransaccion.BindearControl(lista);

            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            PlanCuentas PlanCuentas = new PlanCuentas();
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


    protected void txtCodCuentaNIF_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuentaNIF.Text != "")
        {
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIF.Text, (Usuario)Session["usuario"]);

            // Mostrar el nombre de la cuenta
            if (txtNomCuentaNif != null)
                txtNomCuentaNif.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuentaNif.Text = "";
            txtCodCuentaNIF.Text = "";
        }
    }


    protected void btnListadoPlanNIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true, "txtCodCuentaNIF", "txtNomCuentaNif");
    }

}

