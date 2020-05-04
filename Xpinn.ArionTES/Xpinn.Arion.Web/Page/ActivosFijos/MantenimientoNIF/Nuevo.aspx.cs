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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.ActivosFijoservices ActivosFijoservicio = new Xpinn.ActivosFijos.Services.ActivosFijoservices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActivosFijoservicio.CodigoProgramaMantenimientoNif, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarConsultar(false);           
            toolBar.eventoRegresar += btnRegresar_Click; 
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaMantenimientoNif, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!IsPostBack)
            {
                txtfecha.Text = DateTime.Today.ToShortDateString();
                txtfecha.Enabled=false;
                CargarListas();
               
                CargarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaMantenimientoNif);
                
                MultiView1.ActiveViewIndex = 0;

                if (Session[ActivosFijoservicio.CodigoProgramaMantenimientoNif + ".id"] != null)
                {
                    idObjeto = Session[ActivosFijoservicio.CodigoProgramaMantenimientoNif + ".id"].ToString();
                    Session.Remove(ActivosFijoservicio.CodigoProgramaMantenimientoNif + ".id");
                    ObtenerDatos(idObjeto);
                    bloquear_Datos();
                    btnConsultaPersonas.Enabled = false;
                }
                else
                {
                    txtCodigo.Text = ActivosFijoservicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                }                
              
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaMantenimientoNif, "Page_Load", ex);
        }
    }

    void bloquear_Datos()
    {
        txtCodigo.Enabled = false;
        txtnombre.Enabled = false;
        txtNomEncargado.Enabled = false;
        ddlUbicacion.Enabled = false;
        ddlCentroCosto.Enabled = false;
        ddlOficina.Enabled = false;
    }


    protected Boolean ValidaDatos()
    {
        if (ddlClasificacion.SelectedValue == "0")
        {
            VerError("Seleccione una Clasificación");
            return false;
        }
        if (ddlClasificacion.SelectedValue == "")
        {
            VerError("Seleccione una Clasificación");
            return false;
        }
        if (ddlTipoActivo.SelectedValue == "0")
        {
            VerError("Seleccione un Tipo de Archivo");
            return false;
        }
        if (ddlTipoActivo.SelectedValue == "")
        {
            VerError("Seleccione un Tipo de Archivo");
            return false;
        }
        if (txtValorResidual.Text == "0" && txtVidaUtil.Text == "0" && txtDeterioro.Text == "0" && txtRecupDeterioro.Text =="0")
        {
            VerError("Debe realizar algún Cambio");
            return false;
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidaDatos())
        {
            ctlMensaje.MostrarMensaje("Desea guardar los datos del activo fijo?");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {

            Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();

            if (idObjeto != "")
                vActivoFijo = ActivosFijoservicio.ConsultarActivoFijo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (lblConsecutivo.Text != "")
                vActivoFijo.consecutivo = Convert.ToInt64(lblConsecutivo.Text);
            else
                vActivoFijo.consecutivo = 0;

            if (ddlClasificacion.SelectedValue != "")
                vActivoFijo.codclasificacion_nif = Convert.ToInt32(ddlClasificacion.SelectedValue);
            
            if (ddlTipoActivo.SelectedValue != "")
                vActivoFijo.tipo_activo_nif = Convert.ToInt32(ddlTipoActivo.SelectedValue);

            if (txtValorResidual.Text != "")
                vActivoFijo.valor_residual_nif = Convert.ToDecimal(txtValorResidual.Text);

            if (txtVidaUtil.Text != "")
                vActivoFijo.vida_util_nif = Convert.ToDecimal(txtVidaUtil.Text);

            if (txtDeterioro.Text != "")
                vActivoFijo.vrdeterioro_nif = Convert.ToDecimal(txtDeterioro.Text);

            if (txtRecupDeterioro.Text != "")
                vActivoFijo.vrrecdeterioro_nif = Convert.ToDecimal(txtRecupDeterioro.Text);
            
            vActivoFijo.observaciones = txtObservacion.Text;
            
            vActivoFijo.fecultmod = DateTime.Now;


            if (idObjeto != "")
            {
                vActivoFijo.consecutivo = Convert.ToInt64(idObjeto);
                ActivosFijoservicio.ModificarMantenimientoNif(vActivoFijo, (Usuario)Session["usuario"]);
            }

            Session[ActivosFijoservicio.CodigoProgramaMantenimientoNif + ".id"] = idObjeto;
            Site toolBar = (Site)this.Master;
            //toolBar.eventoRegresar += btnRegresar_Click; 
            toolBar.MostrarGuardar(false);
            VerError("");
            MultiView1.ActiveViewIndex = 1;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaBaja, "btnGuardar_Click", ex);
        }
    }

    

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {      
        Navegar(Pagina.Lista);
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
            vActivoFijo = ActivosFijoservicio.ConsultarActivoFijo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            lblConsecutivo.Text = HttpUtility.HtmlDecode(vActivoFijo.consecutivo.ToString().Trim());
            txtCodigo.Text = HttpUtility.HtmlDecode(vActivoFijo.cod_act.ToString().Trim());

            if (!string.IsNullOrEmpty(vActivoFijo.nombre))
                txtnombre.Text = HttpUtility.HtmlDecode(vActivoFijo.nombre.ToString());

            //modificar

            if (!string.IsNullOrEmpty(vActivoFijo.nom_encargado.ToString()))
                txtNomEncargado.Text = HttpUtility.HtmlDecode(vActivoFijo.nom_encargado.ToString().Trim());
            
            //
            if (!string.IsNullOrEmpty(vActivoFijo.cod_ubica.ToString()))
                ddlUbicacion.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_ubica.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.cod_costo.ToString()))
                ddlCentroCosto.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_costo.ToString().Trim());            
            if (!string.IsNullOrEmpty(vActivoFijo.cod_oficina.ToString()))
                ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_oficina.ToString().Trim());


            if (!string.IsNullOrEmpty(vActivoFijo.codclasificacion_nif.ToString()))
                ddlClasificacion.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.codclasificacion_nif.ToString().Trim());

            if (!string.IsNullOrEmpty(vActivoFijo.tipo_activo_nif.ToString()))
                ddlTipoActivo.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.tipo_activo_nif.ToString().Trim());
            
            if (!string.IsNullOrEmpty(vActivoFijo.observaciones))
                txtObservacion.Text = HttpUtility.HtmlDecode(vActivoFijo.observaciones);

            if (vActivoFijo.valor_residual_nif.ToString() != "")
                txtValorResidual.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_residual_nif.ToString().Trim());

            if (!string.IsNullOrEmpty(vActivoFijo.vida_util_nif.ToString()))
                txtVidaUtil.Text = HttpUtility.HtmlDecode(vActivoFijo.vida_util_nif.ToString().Trim());
            
            if (!string.IsNullOrEmpty(vActivoFijo.vrdeterioro_nif.ToString()))
                txtDeterioro.Text = HttpUtility.HtmlDecode(vActivoFijo.vrdeterioro_nif.ToString().Trim());
            
            if (!string.IsNullOrEmpty(vActivoFijo.vrrecdeterioro_nif.ToString()))
                txtRecupDeterioro.Text = HttpUtility.HtmlDecode(vActivoFijo.vrrecdeterioro_nif.ToString().Trim());
            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaMantenimientoNif, "ObtenerDatos", ex);
        }
    }

    
    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficina.DataTextField = "nombre";
        ddlOficina.DataValueField = "cod_oficina";
        ddlOficina.DataSource = oficinaServicio.ListarOficina(oficina, pUsuario);
        ddlOficina.DataBind();

        PoblarLista("Ubicacion_Activo", ddlUbicacion);
        PoblarLista("centro_costo", ddlCentroCosto);
        PoblarLista("clasificacion_activo_nif", ddlClasificacion);
        PoblarLista("tipo_activo_nif", ddlTipoActivo);

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


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodEncargado", "txtIdenEncargado", "txtNomEncargado");
    }
}