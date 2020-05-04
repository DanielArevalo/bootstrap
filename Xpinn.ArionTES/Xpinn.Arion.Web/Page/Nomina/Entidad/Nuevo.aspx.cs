using System;
using System.Web.UI;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using System.Web;
using System.Web.UI.WebControls;

using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

public partial class NuevoEn : GlobalWeb
{
    Nomina_EntidadService entservice = new Nomina_EntidadService();

    #region Eventos iniciales 

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[entservice.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(entservice.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(entservice.CodigoPrograma, "D");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_guardar_click;
            toolBar.eventoLimpiar += btnlimpiar_Click;
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(entservice.CodigoPrograma + ".id");
                Navegar("ListaEn.aspx");    
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entservice.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        VerError("");

        
        if (!IsPostBack)
        {
            InicializarPagina();
            if (Session[entservice.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[entservice.CodigoPrograma + ".id"].ToString();
                ObtenerDatos();
            }
            else
            {
                InicializarEditarRegistro();                
            }
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Ciudades, ddlciudad);
        LlenarListasDesplegables(TipoLista.Actividad2, ddlcodciiu);
    }

    void InicializarEditarRegistro()
    {
        
    }

    #endregion

    #region Obtener Datos
    protected void ObtenerDatos()
    {
        try {

            Nomina_EntidadService entidadservice = new Nomina_EntidadService();

            Nomina_Entidad entidad = entidadservice.ConsultarNomina_Entidad(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (entidad != null) {

                txtnombre.Text = entidad.nom_persona;

                if (entidad.consecutivo != Int64.MinValue)
                    txtconsecutivo.Text = HttpUtility.HtmlDecode(entidad.consecutivo.ToString().Trim());

                if (!string.IsNullOrEmpty(entidad.direccion))
                {
                    try
                    {
                        txtdir.Text = HttpUtility.HtmlDecode(entidad.direccion.ToString().Trim());
                        if (txtdir.Text == "")
                            txtdir.Text = HttpUtility.HtmlDecode(entidad.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("La dirección de correspondencia esta errada, no esta en el formato correcto");
                    }
                }

                if (entidad.ciudad != Int64.MinValue)
                {
                    try
                    {
                        ddlciudad.SelectedValue = HttpUtility.HtmlDecode(entidad.ciudad.ToString().Trim());
                    }
                    catch
                    {
                        ddlciudad.SelectedValue = ddlciudad.SelectedValue;
                    }
                }


                if (!string.IsNullOrEmpty(entidad.telefono))
                    txttel.Text = HttpUtility.HtmlDecode(entidad.telefono.ToString().Trim());

                if (!string.IsNullOrEmpty(entidad.nit))
                {
                    txtIdentificacion.Text = HttpUtility.HtmlDecode(entidad.nit.ToString().Trim());
                }

                if (!string.IsNullOrEmpty(entidad.clase)) 
                {
                    try
                    {
                        ddlclase.SelectedValue = HttpUtility.HtmlDecode(entidad.clase.ToString().Trim());
                    } catch
                    {
                        ddlclase.SelectedValue = ddlclase.SelectedValue;
                    }
                }

                if (!string.IsNullOrEmpty(entidad.responsable))
                    txtrespon.Text = HttpUtility.HtmlDecode(entidad.responsable.ToString().Trim());

                if (!string.IsNullOrEmpty(entidad.codigociiu))
                    ddlcodciiu.Text = HttpUtility.HtmlDecode(entidad.codigociiu.ToString().Trim());

                if (!string.IsNullOrEmpty(entidad.codigopila))
                    txtcodpila.Text = HttpUtility.HtmlDecode(entidad.codigopila.ToString().Trim()).ToUpper();

                if (!string.IsNullOrEmpty(entidad.email))
                    txtmail.Text = HttpUtility.HtmlDecode(entidad.email.ToString().Trim());

                if (!string.IsNullOrEmpty(entidad.cod_cuenta))
                    txtCodCuenta.Text = HttpUtility.HtmlDecode(entidad.cod_cuenta.ToString().Trim());
                if (!string.IsNullOrEmpty(entidad.cod_cuenta))
                    txtCodCuenta_TextChanged(txtCodCuenta, null);


                if (!string.IsNullOrEmpty(entidad.cod_cuenta_contra))
                    txtCodCuentaNomina.Text = HttpUtility.HtmlDecode(entidad.cod_cuenta_contra.ToString().Trim());

                if (!string.IsNullOrEmpty(entidad.cod_cuenta_contra))
                    txtCodCuentaNomina_TextChanged(txtCodCuentaNomina, null);


            }
            else
                VerError("error De datos");
        } catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    #endregion

    #region Eventos Intermedios

    void btn_guardar_click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarCampos())
        {
            GuardarDatos();
        }
    }

    #endregion

    #region Metodos de Ayuda

    Nomina_Entidad ConsultarNomina(Int64 idEntidad)
        {
        try
        {
            if (string.IsNullOrWhiteSpace(idEntidad.ToString())) return null;
            Nomina_Entidad Nomina = entservice.ConsultarNomina_Entidad(Convert.ToInt64(idEntidad), Usuario);
            return Nomina;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }

    void llenarNomina(Nomina_Entidad pnomina)
    {

        ddlcodciiu.Text = Convert.ToInt64(pnomina.codigociiu).ToString();
        txtcodpila.Text = Convert.ToInt64(pnomina.codigopila).ToString();
        txtdir.Text = pnomina.direccion;
        txtmail.Text = pnomina.email;
        txtIdentificacion.Text = Convert.ToInt64(pnomina.nit).ToString();
        txtnombre.Text = pnomina.nom_persona.ToString();
        txtrespon.Text = pnomina.responsable;
        txttel.Text = Convert.ToInt64(pnomina.telefono).ToString();
        ddlciudad.SelectedValue = Convert.ToInt64(pnomina.ciudad).ToString();
        ddlclase.SelectedValue = Convert.ToInt64(pnomina.clase).ToString();

    }

    void GuardarDatos() {

        try
        {
            Xpinn.Nomina.Services.Nomina_EntidadService entidadservice = new Xpinn.Nomina.Services.Nomina_EntidadService();
            Xpinn.Nomina.Entities.Nomina_Entidad entitidadenti = new Xpinn.Nomina.Entities.Nomina_Entidad();

            if (idObjeto != "")
            {
                entitidadenti = entidadservice.ConsultarNomina_Entidad(Convert.ToInt64(idObjeto), Usuario);
            }
 
            if (txtnombre.Text != "")
                entitidadenti.nom_persona = (txtnombre.Text != "") ? Convert.ToString(txtnombre.Text.Trim()).ToUpper() : String.Empty;
            if (txttel.Text != "")
                entitidadenti.telefono = (txttel.Text != "") ? Convert.ToString(txttel.Text.Trim()) : String.Empty;
            if (txtrespon.Text != "")
                entitidadenti.responsable = (txtrespon.Text != "") ? Convert.ToString(txtrespon.Text.Trim()) : String.Empty;
            if (txtIdentificacion.Text != "")
                entitidadenti.nit = (txtIdentificacion.Text != "") ? Convert.ToString(txtIdentificacion.Text.Trim()) : String.Empty;
            if (txtmail.Text != "")
                entitidadenti.email = (txtmail.Text != "") ? Convert.ToString(txtmail.Text.Trim()) : String.Empty;
            if (txtdir.Text != "")
                entitidadenti.direccion = (txtdir.Text != "") ? Convert.ToString(txtdir.Text.Trim ()).ToUpper() : String.Empty;
            if (txtcodpila.Text != "")
                entitidadenti.codigopila = (txtcodpila.Text != "") ? Convert.ToString(txtcodpila.Text.Trim()).ToUpper() : String.Empty;
            if (ddlcodciiu.Text != "")
                entitidadenti.codigociiu = (ddlcodciiu.Text != "") ? Convert.ToString(ddlcodciiu.Text.Trim()) : String.Empty;
            if (ddlciudad.Text != "")
                entitidadenti.ciudad = Convert.ToInt64(ddlciudad.SelectedValue);
            if(ddlclase.Text != "")
                entitidadenti.clase = (ddlclase.Text != "") ? Convert.ToString(ddlclase.Text.Trim()) : String.Empty;

            if (txtCodCuenta.Text != "")
                entitidadenti.cod_cuenta = (txtCodCuenta.Text != "") ? Convert.ToString(txtCodCuenta.Text.Trim()) : String.Empty;



            if (txtCodCuentaNomina.Text != "")
                entitidadenti.cod_cuenta_contra = (txtCodCuentaNomina.Text != "") ? Convert.ToString(txtCodCuentaNomina.Text.Trim()) : String.Empty;






            if (idObjeto == "")
            {
                Nomina_Entidad pentidad = entidadservice.CrearNomina_Entidad(entitidadenti, Usuario);
                entitidadenti.consecutivo = pentidad.consecutivo;
            }
            else
            {
                Nomina_Entidad pentidad = entidadservice.ModificarNomina_Entidad(entitidadenti, Usuario);
            }

            if (entitidadenti.consecutivo != 0)
            {                
                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);

                Session.Remove(entidadservice.CodigoPrograma + ".id");
                mvPrincipal.SetActiveView(viewGuardado);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        
        }
    }
    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void btnListadoPlanNomina_Click(object sender, EventArgs e)
    {
        ctlCuentasNomina.Motrar(true, "txtCodCuentaNomina", "txtNombreCuentaNomina");
    }
    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtCodCuenta.Text))
        {
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas PlanCuentas = new PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, Usuario);

            // Mostrar el nombre de la cuenta           
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }


    protected void txtCodCuentaNomina_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtCodCuentaNomina.Text))
        {
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas PlanCuentas = new PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaNomina.Text, Usuario);

            // Mostrar el nombre de la cuenta           
            if (txtNombreCuentaNomina != null)
                txtNombreCuentaNomina.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNombreCuentaNomina.Text = "";
        }
    }
    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid || 
            string.IsNullOrWhiteSpace(txtnombre.Text) ||
            string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
            string.IsNullOrWhiteSpace(ddlclase.Text) ||
            string.IsNullOrWhiteSpace(txtmail.Text) ||
            string.IsNullOrWhiteSpace(txtrespon.Text) ||
            string.IsNullOrWhiteSpace(txtcodpila.Text))
        {
            VerError("Faltan Campos por validar");
            return false;
        }
        return true;
    }

    Nomina_Entidad ObtenerEntidadGuardar()
    {
        Nomina_Entidad nomina = new Nomina_Entidad();
        if (!string.IsNullOrWhiteSpace(idObjeto))
        {
            nomina.consecutivo = ConsultarNomina(Convert.ToInt64(idObjeto)).consecutivo;
        }

        nomina.nom_persona = txtnombre.Text;
        nomina.responsable = txtrespon.Text;
        nomina.telefono = txttel.Text;
        nomina.email = txtmail.Text;
        nomina.direccion = txtdir.Text;
        nomina.ciudad = Convert.ToInt64(ddlciudad.Text);
        nomina.clase = ddlclase.SelectedValue;
        nomina.codigociiu = ddlciudad.Text;
        nomina.codigopila = txtcodpila.Text;
        nomina.nit = txtIdentificacion.Text;

        return nomina;
    }

    #endregion

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        txtnombre.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        ddlclase.Text = string.Empty;
        txtmail.Text = string.Empty;
        txtrespon.Text = string.Empty;
        txtcodpila.Text = string.Empty;
        ddlcodciiu.Text = string.Empty;
        txttel.Text = string.Empty;
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {

        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        try
        {
            VerError("");
            vPersona1.seleccionar = "Identificacion";
            vPersona1.soloPersona = 1;
            vPersona1.identificacion = txtIdentificacion.Text;
            vPersona1.telefono = txttel.Text;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1.nombre != null)
            {
                VerError("ERROR: La Identificación ingresada no existe,grabarlo primero como tercero");           
                txtdir.Enabled = false;
                txtnombre.Enabled = false;
                txttel.Enabled = false;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);



            }
            else
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    if (vPersona1.tipo_persona == "N")
                    {
                        txtnombre.Text = vPersona1.apellidos;
                    }
                if (vPersona1.tipo_persona == "J")
                {
                    txtnombre.Text = vPersona1.razon_social;
                }

             
                txtdir.Text = vPersona1.direccion;
                txttel.Text=  vPersona1.telefono;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}