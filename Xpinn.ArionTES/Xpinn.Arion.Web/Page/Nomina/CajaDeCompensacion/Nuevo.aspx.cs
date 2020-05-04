using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
public partial class Nuevo : GlobalWeb
{
    CajaDeCompensacionService service = new CajaDeCompensacionService();
    string caja;

    Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();


    #region Eventos iniciales

    protected void Page_Load(object sender, EventArgs e)
    {
        VerError("");

        if (!IsPostBack)
        {
            
            if (Session[service.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[service.CodigoPrograma + ".id"].ToString();
                ObtenerDatos();
            }
            else
            {
                InicializarEditarRegistro();
            }
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[service.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(service.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(service.CodigoPrograma, "D");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_guardar_click;
            toolBar.eventoLimpiar += btnlimpiar_Click;
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(service.CodigoPrograma + ".id");
                Navegar("Lista.aspx");
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(service.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    void InicializarEditarRegistro()
    {
        txtconsecutivo.ReadOnly = true;
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

    #region obtener datos  
    protected void ObtenerDatos()
    {
        try
        {
            Xpinn.Nomina.Services.CajaDeCompensacionService service = new Xpinn.Nomina.Services.CajaDeCompensacionService();
            Xpinn.Nomina.Entities.CajaDeCompensacion entities = new Xpinn.Nomina.Entities.CajaDeCompensacion();

            entities.consecutivo = Convert.ToInt64(idObjeto);

            entities = service.ConsultarDIRCAJADECOMPENSACION(entities.consecutivo, (Usuario)Session["usuario"]);

            if (entities != null) { 

                if(entities.consecutivo != Int64.MinValue)
                txtconsecutivo.Text = HttpUtility.HtmlDecode(entities.consecutivo.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.nombre))
                    txtnombre.Text = HttpUtility.HtmlDecode(entities.nombre.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.nit))
                    txtidentificacion.Text = HttpUtility.HtmlDecode(entities.nit.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.telefono))
                    txttelefono.Text = HttpUtility.HtmlDecode(entities.telefono.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.codigopila))
                    txtcodpila.Text = HttpUtility.HtmlDecode(entities.codigopila.ToString().Trim()).ToUpper();


                if (!string.IsNullOrEmpty(entities.cod_cuenta))
                    txtCodCuenta.Text = HttpUtility.HtmlDecode(entities.cod_cuenta.ToString().Trim());
                if (!string.IsNullOrEmpty(entities.cod_cuenta))
                    txtCodCuenta_TextChanged(txtCodCuenta, null);


                if (!string.IsNullOrEmpty(entities.cod_cuenta_contra))
                    txtCodCuentaNomina.Text = HttpUtility.HtmlDecode(entities.cod_cuenta_contra.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.cod_cuenta_contra))
                    txtCodCuentaNomina_TextChanged(txtCodCuentaNomina, null);


                if (!string.IsNullOrEmpty(entities.direccion))
                {
                    try
                    {
                        txtdir.Text = HttpUtility.HtmlDecode(entities.direccion.ToString().Trim());
                        if (txtdir.Text == "")
                            txtdir.Text = HttpUtility.HtmlDecode(entities.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("La dirección de correspondencia esta errada, no esta en el formato correcto");
                    }
                }
            } else
             VerError("error De datos");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    #endregion

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

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        txtnombre.Text = string.Empty;
        txttelefono.Text = string.Empty;
        txtidentificacion.Text = string.Empty;
        txtconsecutivo.Text = string.Empty;
        txtcodpila.Text = string.Empty;
     }

    #endregion

    #region Metodos de Ayuda

    CajaDeCompensacion ConsultarCaja(Int64 idCaja)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(idCaja.ToString())) return null;
            CajaDeCompensacion entiti = service.ConsultarDIRCAJADECOMPENSACION(Convert.ToInt64(idCaja), Usuario);
            return entiti;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }

    void Llenarcaja (CajaDeCompensacion pcaja)
    {
        txtconsecutivo.Text = Convert.ToInt64(pcaja.consecutivo).ToString();
        txtnombre.Text = pcaja.nombre;
        txtidentificacion.Text = Convert.ToInt64(pcaja.nit).ToString();
        txtdir.Text = pcaja.direccion;
        txttelefono.Text = Convert.ToInt64(pcaja.telefono).ToString();
        txtcodpila.Text = Convert.ToInt64(pcaja.codigopila).ToString();

    }

    void GuardarDatos()
    {

        try
        {
            Xpinn.Nomina.Services.CajaDeCompensacionService service = new Xpinn.Nomina.Services.CajaDeCompensacionService();
            Xpinn.Nomina.Entities.CajaDeCompensacion entities = new Xpinn.Nomina.Entities.CajaDeCompensacion();
            
            if (idObjeto != "")
            {
                entities = service.ConsultarDIRCAJADECOMPENSACION(Convert.ToInt64(idObjeto), Usuario);
            }

            if (txtnombre.Text != "")
                entities.nombre = (txtnombre.Text != "") ? Convert.ToString(txtnombre.Text.Trim()) : String.Empty;

            if (txttelefono.Text != "")
                entities.telefono = (txttelefono.Text != "") ? Convert.ToString(txttelefono.Text.Trim()) : String.Empty;

            if (txtdir.Text != "")
                entities.direccion = (txtdir.Text != "") ? Convert.ToString(txtdir.Text.Trim()) : String.Empty;

            if (txtidentificacion.Text != "")
                entities.nit = (txtidentificacion.Text != "") ? Convert.ToString(txtidentificacion.Text.Trim()) : String.Empty;

            if (txtcodpila.Text != "")
                entities.codigopila = (txtcodpila.Text != "") ? Convert.ToString(txtcodpila.Text.Trim()).ToUpper() : String.Empty;

            if (txtCodCuenta.Text != "")
                entities.cod_cuenta = (txtCodCuenta.Text != "") ? Convert.ToString(txtCodCuenta.Text.Trim()) : String.Empty;



            if (txtCodCuentaNomina.Text != "")
                entities.cod_cuenta_contra = (txtCodCuentaNomina.Text != "") ? Convert.ToString(txtCodCuentaNomina.Text.Trim()) : String.Empty;


            if (idObjeto == "")
            {
                CajaDeCompensacion pnomina = service.CrearDIRCAJADECOMPENSACION(entities, Usuario);
                entities.consecutivo = pnomina.consecutivo; 
            }
            else
            {
                CajaDeCompensacion pnomina = service.ModificarDIRCAJADECOMPENSACION(entities, Usuario);
            }

            if (entities.consecutivo != 0)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);

                Session.Remove(service.CodigoPrograma + ".id");
                mvPrincipal.SetActiveView(viewGuardado);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    bool ValidarCampos()
    {
        Page.Validate();
        if(!Page.IsValid || 
            string.IsNullOrWhiteSpace(txtcodpila.Text) ||
            string.IsNullOrWhiteSpace(txtidentificacion.Text) ||
            string.IsNullOrWhiteSpace(txtnombre.Text) ||
            string.IsNullOrWhiteSpace(txttelefono.Text) ||
            string.IsNullOrWhiteSpace(txtdir.Text))
        {
            VerError("Faltan Campos por validar");
        }
        return true;



    }
     
    CajaDeCompensacion ObtenerDatosGuardar()
    {
        CajaDeCompensacion pnomina = new CajaDeCompensacion();
        if (!string.IsNullOrWhiteSpace(idObjeto))
        {
            pnomina.consecutivo = ConsultarCaja(Convert.ToInt64(idObjeto)).consecutivo;
        }
    
        pnomina.codigopila = txtcodpila.Text;
        pnomina.direccion = txtdir.Text;
        pnomina.nit = txtcodpila.Text;
        pnomina.nombre = txtnombre.Text;
        pnomina.telefono = txttelefono.Text;

        return pnomina;

    }


    #endregion

    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            vPersona1.seleccionar = "Identificacion";
            vPersona1.soloPersona = 1;
            vPersona1.identificacion = txtidentificacion.Text;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1.nombre!=null)
            {
                     VerError("ERROR: La Identificación ingresada no existe,grabarlo primero como tercero");
                     txttelefono.Enabled = false;
                     txtdir.Enabled = false;
                     txtnombre.Enabled = false;
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

                txttelefono.Text = vPersona1.telefono;
                txtdir.Text = vPersona1.direccion;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

}
