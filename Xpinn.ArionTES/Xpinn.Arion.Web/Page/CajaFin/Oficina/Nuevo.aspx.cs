using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[oficinaService.CodigoOficina + ".id"] != null)
                VisualizarOpciones(oficinaService.CodigoPrograma,"E");
            else
                VisualizarOpciones(oficinaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
             
                //se inicializa el combo de ciudades, centro de costos
                LlenarComboOficinas(ddlOficinaSuper);

                CargarDropDown();
                LlenarComboCiudades(ddlCiudades);
                //se inicializa el combo de ciudades, centro de costos
                LlenarComboCentroCosto(ddlCentrosCosto);
                //LlenarComboEncargados(ddlEncargados);

                if (Session[oficinaService.CodigoOficina + ".id"] != null)
                {
                    idObjeto = Session[oficinaService.CodigoOficina + ".id"].ToString();
                    Session.Remove(oficinaService.CodigoOficina + ".id");
                    ObtenerDatos(idObjeto);
					txtCodigo.Enabled = false;
                }  
                else
                    ObtenerDatos("");
                rblIndicadorCorres_SelectedIndexChanged(rblIndicadorCorres, null);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "Page_Load", ex);
        }
    }
    
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
            if (idObjeto != "")
                oficina = oficinaService.ConsultarOficina(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            
            //se atrapan los datos del formulario
            oficina.cod_oficina = txtCodigo.Text.Trim();
            oficina.nombre = txtOficina.Text.Trim();
            oficina.cod_ciudad = Convert.ToInt64(ddlCiudades.SelectedValue);
            oficina.direccion = txtDirCorrespondencia.Text;
            oficina.telefono = txtTelefono.Text.Trim();
            
            //Modificado para hacer uso del control Personas
            //if (ddlEncargados.SelectedValue != null)
            //    if (ddlEncargados.SelectedValue != "")
            //        oficina.cod_persona = ConvertirStringToInt(ddlEncargados.SelectedValue);
            oficina.cod_persona = Convert.ToInt64(txtCodEncargado.Text.Trim());                

            // por defecto se coloca la oficina Inactiva para 
            // que pueda quedar el rastro en ProcesoOficina
            oficina.fecha_creacion = Convert.ToDateTime(txtFechaCreacion.Text);
            oficina.centro_costo = Convert.ToInt64(ddlCentrosCosto.SelectedValue);
            oficina.estado = Convert.ToInt64(ddlestado.SelectedValue);
            oficina.cod_cuenta = txtCodCuenta.Text;
            oficina.sede_propia = Convert.ToInt32(rblSedePropia.SelectedValue);
            oficina.indicador_corresponsal = Convert.ToInt32(rblIndicadorCorres.SelectedValue);
            oficina.tipo_negocio = rblIndicadorCorres.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlTipoNegocio.SelectedValue);
            
            Usuario usuario1=(Usuario)Session["usuario"];
            oficina.cod_empresa = usuario1.idEmpresa;
            oficina.cod_super= Convert.ToInt64(ddlOficinaSuper.SelectedValue);  

            if (idObjeto != "")
            {
                oficina.cod_oficina = idObjeto;
                oficinaService.ModificarOficina(oficina, (Usuario)Session["usuario"]);
            }
            else 
            {
                oficina = oficinaService.CrearOficina(oficina, (Usuario)Session["usuario"]);
                idObjeto = oficina.cod_oficina.ToString();
            }

            Session[oficinaService.CodigoOficina + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {

        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[oficinaService.CodigoOficina+ ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
            if (idObjeto != "")
                oficina = oficinaService.ConsultarOficina(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);//, (TOSesion)Session["user"]);

            if (oficina.fecha_creacion.ToShortDateString() != "01/01/0001")
                txtFechaCreacion.Text = oficina.fecha_creacion.ToShortDateString();
            else
                txtFechaCreacion.Text = DateTime.Now.ToShortDateString();


            if (idObjeto != "")
            {
                if (!string.IsNullOrEmpty(oficina.cod_oficina.ToString()))
                     txtCodigo.Text  = oficina.cod_oficina.ToString();
                if (!string.IsNullOrEmpty(oficina.nombre))
                    txtOficina.Text = oficina.nombre.Trim().ToString();
                if (!string.IsNullOrEmpty(oficina.direccion.Trim().ToString()))
                    txtDirCorrespondencia.Text = oficina.direccion.ToString();
                if (!string.IsNullOrEmpty(oficina.telefono.Trim().ToString()))
                    txtTelefono.Text = oficina.telefono.Trim().ToString();
                if (!string.IsNullOrEmpty(oficina.cod_ciudad.ToString()))
                    ddlCiudades.SelectedValue = oficina.cod_ciudad.ToString();
                if (!string.IsNullOrEmpty(oficina.cod_persona.ToString()))
                {    //ddlEncargados.SelectedValue = oficina.cod_persona.ToString();
                    txtCodEncargado.Text = oficina.cod_persona.ToString();
                    Xpinn.Caja.Services.PersonaService personaServicio = new Xpinn.Caja.Services.PersonaService();
                    Xpinn.Caja.Entities.Persona vPersona = new Xpinn.Caja.Entities.Persona();
                    vPersona.cod_persona = Convert.ToInt64(oficina.cod_persona.ToString()); 
                    vPersona = personaServicio.ConsultarPersonaXCodigo(vPersona, (Usuario)Session["usuario"]);
                    txtNombreEncargado.Text = vPersona.nom_persona;
                }
                if (!string.IsNullOrEmpty(oficina.centro_costo.ToString()))
                    ddlCentrosCosto.SelectedValue = oficina.centro_costo.ToString();
                if (oficina.cod_cuenta != null)
                    txtCodCuenta.Text = oficina.cod_cuenta;
                txtCodCuenta_TextChanged(txtCodCuenta, null);
                rblSedePropia.SelectedValue = oficina.sede_propia.ToString();
                rblIndicadorCorres.SelectedValue = oficina.indicador_corresponsal.ToString();
                if (oficina.tipo_negocio != null)
                    ddlTipoNegocio.SelectedValue = oficina.tipo_negocio.ToString();

                if (!string.IsNullOrEmpty(oficina.cod_super.ToString()))
                    ddlOficinaSuper.SelectedValue = oficina.cod_super.ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void CargarDropDown()
    {
        ddlTipoNegocio.Items.Insert(0, new ListItem("Droguería","1"));
        ddlTipoNegocio.Items.Insert(1, new ListItem("Supermercado", "2"));
        ddlTipoNegocio.Items.Insert(2, new ListItem("Tienda", "3"));
        ddlTipoNegocio.Items.Insert(3, new ListItem("Oficina Postal", "4"));
        ddlTipoNegocio.Items.Insert(4, new ListItem("Centro de telecomunicaciones", "5"));
        ddlTipoNegocio.Items.Insert(5, new ListItem("Otra cooperativa", "6"));
        ddlTipoNegocio.Items.Insert(6, new ListItem("Otro", "9"));
        ddlTipoNegocio.SelectedIndex = 0;
    }

    protected void LlenarComboCiudades(DropDownList ddlCiudades)
    {
        Xpinn.Caja.Services.CiudadService ciudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudades.DataSource = ciudadService.ListarCiudad(ciudad, (Usuario)Session["usuario"]);
        ddlCiudades.DataTextField = "nom_ciudad";
        ddlCiudades.DataValueField = "cod_ciudad";
        ddlCiudades.DataBind();
    }
    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {

        Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficinaSuper.DataSource = oficinaService.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ddlOficinaSuper.DataTextField = "nombre";
        ddlOficinaSuper.DataValueField = "cod_oficina";
        ddlOficinaSuper.DataBind();
    }
    protected void LlenarComboCentroCosto(DropDownList ddlCentrosCosto)
    {

        Xpinn.Caja.Services.CentroCostoService centroCostoService = new Xpinn.Caja.Services.CentroCostoService();
        Xpinn.Caja.Entities.CentroCosto centroCosto = new Xpinn.Caja.Entities.CentroCosto ();
        ddlCentrosCosto.DataSource = centroCostoService.ListarCentroCosto(centroCosto, (Usuario)Session["usuario"]);
        ddlCentrosCosto.DataTextField = "nom_centro";
        ddlCentrosCosto.DataValueField = "centro_costo";
        ddlCentrosCosto.DataBind();

    }

    //protected void LlenarComboEncargados(DropDownList ddlEncargados)
    //{
    //    Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
    //    Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
    //    ddlEncargados.DataSource = personaService.ListarPersona(persona, (Usuario)Session["usuario"]);
    //    ddlEncargados.DataTextField = "nom_persona";
    //    ddlEncargados.DataValueField = "cod_persona";
    //    ddlEncargados.DataBind();
    //}


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuenta != null)
            txtNomCuenta.Text = PlanCuentas.nombre;
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void rblIndicadorCorres_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool rpta = false;
        rpta = rblIndicadorCorres.SelectedIndex == 0 ? false : true;

        lblTipoNegocio.Visible = rpta;
        ddlTipoNegocio.Visible = rpta;
    }

    //Agregado para hacer uso del control de personas ya que el ddlEncargado se demoraba en cargar
    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodEncargado", "", "txtNombreEncargado");
    }
}