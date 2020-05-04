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
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Detalle : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TerceroServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TerceroServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TerceroServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[TerceroServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TerceroServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TerceroServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {                    
                    Navegar(Pagina.Lista);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
            vTercero = TerceroServicio.ConsultarTercero(Convert.ToInt64(pIdObjeto), null, (Usuario)Session["usuario"]);
            
            txtCodigo.Text = HttpUtility.HtmlDecode(vTercero.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vTercero.identificacion.ToString().Trim());
            if (vTercero.digito_verificacion.ToString() != "")
                txtDigitoVerificacion.Text = HttpUtility.HtmlDecode(vTercero.digito_verificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.primer_apellido))
                txtSigla.Text = HttpUtility.HtmlDecode(vTercero.primer_apellido.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.razon_social))
                txtRazonSocial.Text = HttpUtility.HtmlDecode(vTercero.razon_social.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vTercero.direccion.ToString().Trim());
            if (vTercero.codciudadexpedicion.ToString() != "")
                ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codciudadexpedicion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vTercero.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.email))
                txtEmail.Text = HttpUtility.HtmlDecode(vTercero.email.ToString().Trim());
            if (vTercero.fechaexpedicion.ToString() != "")
                txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vTercero.fechaexpedicion.ToString()));
            if (vTercero.codactividad.ToString() != "")
                ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codactividad.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.regimen))
                ddlRegimen.SelectedValue = HttpUtility.HtmlDecode(vTercero.regimen.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private void CargarListas()
    {
        try
        {
            // Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaId";
            ddlCiudad.DataSource = TraerResultadosLista("Ciudades");
            ddlCiudad.DataBind();

            // Llenar la actividad
            ddlActividad.DataTextField = "ListaDescripcion";
            ddlActividad.DataValueField = "ListaIdStr";
            ddlActividad.DataSource = TraerResultadosLista("Actividad_Negocio");
            ddlActividad.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }



}