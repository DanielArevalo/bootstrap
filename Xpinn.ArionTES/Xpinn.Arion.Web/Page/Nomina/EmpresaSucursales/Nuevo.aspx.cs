using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    Empresa_SucursalService service = new Empresa_SucursalService();
    string caja;
    #region Eventos iniciales

    protected void Page_Load(object sender, EventArgs e)
    {
        VerError("");

        if (!IsPostBack)
        {
            InicializarPagina();
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

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Ciudades, ddciudad);
    }

    void InicializarEditarRegistro()
    {
        
    }

    #region obtener datos  
    protected void ObtenerDatos()
    {
        try
        {
            Xpinn.Nomina.Services.Empresa_SucursalService service = new Xpinn.Nomina.Services.Empresa_SucursalService();
            Xpinn.Nomina.Entities.Empresa_Sucursal entities = new Xpinn.Nomina.Entities.Empresa_Sucursal();

            entities.consecutivo = Convert.ToInt64(idObjeto);

            entities = service.ConsultarEmpresa_Sucursal(entities.consecutivo, (Usuario)Session["usuario"]);

            if (entities != null)
            {

                if (entities.consecutivo != Int64.MinValue)
                    txtcodigo.Text = HttpUtility.HtmlDecode(entities.consecutivo.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.descripcion))
                    txtnombre.Text = HttpUtility.HtmlDecode(entities.descripcion.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.Email))
                    txtemail.Text = HttpUtility.HtmlDecode(entities.Email.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.telefono))
                    txttelefono.Text = HttpUtility.HtmlDecode(entities.telefono.ToString().Trim());

                if (entities.ciudad != Int64.MinValue)
                    ddciudad.SelectedValue = HttpUtility.HtmlDecode(entities.ciudad.ToString().Trim());

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
            }
            else
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
        txtdir.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtnombre.Text = string.Empty;
    }

    #endregion

    #region Metodos de Ayuda

    Empresa_Sucursal ConsultarCaja(Int64 idCaja)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(idCaja.ToString())) return null;
            Empresa_Sucursal entiti = service.ConsultarEmpresa_Sucursal(Convert.ToInt64(idCaja), Usuario);
            return entiti;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }

    void Llenarcaja(Empresa_Sucursal pcaja)
    {
        txtcodigo.Text = Convert.ToInt64(pcaja.consecutivo).ToString();
        txtnombre.Text = pcaja.descripcion;
        txtemail.Text = pcaja.Email;
        txtdir.Text = pcaja.direccion;
        txttelefono.Text = pcaja.telefono;
        ddciudad.Text = Convert.ToInt64(pcaja.ciudad).ToString();

    }

    void GuardarDatos()
    {

        try
        {
            Xpinn.Nomina.Services.Empresa_SucursalService service = new Xpinn.Nomina.Services.Empresa_SucursalService();
            Xpinn.Nomina.Entities.Empresa_Sucursal entities = new Xpinn.Nomina.Entities.Empresa_Sucursal();

            if (idObjeto != "")
            {
                entities = service.ConsultarEmpresa_Sucursal(Convert.ToInt64(idObjeto), Usuario);
            }

            if (txtnombre.Text != "")
                entities.descripcion = (txtnombre.Text != "") ? Convert.ToString(txtnombre.Text.Trim()).ToUpper() : String.Empty;

            if (txttelefono.Text != "")
                entities.telefono = (txttelefono.Text != "") ? Convert.ToString(txttelefono.Text.Trim()) : String.Empty;

            if (txtdir.Text != "")
                entities.direccion = (txtdir.Text != "") ? Convert.ToString(txtdir.Text.Trim()) : String.Empty;

            if (txtemail.Text != "")
                entities.Email = (txtemail.Text != "") ? Convert.ToString(txtemail.Text.Trim()) : String.Empty;

            if (ddciudad.SelectedValue != "")
                entities.ciudad = Convert.ToInt64(ddciudad.Text.Trim());


            if (idObjeto == "")
            {
                Empresa_Sucursal pnomina = service.CrearEmpresa_Sucursal(entities, Usuario);
                entities.consecutivo = pnomina.consecutivo;
            }
            else
            {
                Empresa_Sucursal pnomina = service.ModificarEmpresa_Sucursal(entities, Usuario);
            }

           // if (entities.consecutivo != 0)
            //{
                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);

                mvPrincipal.SetActiveView(viewGuardado);

                Session.Remove(service.CodigoPrograma + ".id");
            //}
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid ||
            string.IsNullOrWhiteSpace(txtemail.Text) ||
            string.IsNullOrWhiteSpace(txtnombre.Text) ||
            string.IsNullOrWhiteSpace(txttelefono.Text) ||
            string.IsNullOrWhiteSpace(txtdir.Text)||
            string.IsNullOrWhiteSpace(ddciudad.SelectedValue))
        {
            VerError("Faltan Campos por validar");
        }
        return true;
    }

    Empresa_Sucursal ObtenerDatosGuardar()
    {
        Empresa_Sucursal pnomina = new Empresa_Sucursal();
        if (!string.IsNullOrWhiteSpace(idObjeto))
        {
            pnomina.consecutivo = ConsultarCaja(Convert.ToInt64(idObjeto)).consecutivo;
        }

        pnomina.consecutivo = Convert.ToInt64(txtcodigo.Text);
        pnomina.direccion = txtdir.Text;
        pnomina.Email = txtemail.Text;
        pnomina.descripcion = txtnombre.Text;
        pnomina.telefono = txttelefono.Text;
        pnomina.direccion = txtdir.Text;

        return pnomina;

    }


    #endregion
}
