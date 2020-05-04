using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;


public partial class PageAsesoresEstadoCuentaNegocioDetalle : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    NegocioService servicenegocio = new NegocioService();
    Producto entityProducto;
    Negocio entityNegocio;
    List<Negocio> lstConsulta = new List<Negocio>();
    List<Xpinn.Asesores.Entities.Common.Actividad> lstActividades = new List<Xpinn.Asesores.Entities.Common.Actividad>();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try{
            VisualizarOpciones(servicenegocio.CodigoPrograma, "L");
            ucImprimir.PrintCustomEvent += ucImprimir_Click;
            Site toolBar = (Site)this.Master;          
            
            toolBar.eventoRegresar += this.btnRegresar_Click;
            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[MOV_GRAL_CRED_PRODUC] != null)
                {
                    entityProducto  = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
                    entityNegocio   = new Negocio();
                    entityNegocio.Persona.IdPersona = entityProducto.Persona.IdPersona;
                    lstConsulta     = serviceEstadoCuenta.ListarNegocios(entityNegocio, (Usuario)Session["usuario"]);
                    lstActividades  = serviceEstadoCuenta.ListarActividad(new Xpinn.Asesores.Entities.Common.Actividad(), (Usuario)Session["usuario"]);
                    Actualizar();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {

        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
        
        
    }
    private void Actualizar()
    {
        try
        {
            if (lstConsulta.Count > 0) {
                var negocio = lstConsulta.First(s => s.Persona.IdPersona == entityProducto.Persona.IdPersona);
                var actividad = lstActividades.First(x => x.IdActividad == negocio.Actividad.IdActividad);

                txtNumeroIdentificacio.Text = negocio.Persona.NumeroDocumento.ToString();
                txtTipoIdentificacion.Text = negocio.Persona.TipoIdentificacion.NombreTipoIdentificacion.Trim().ToString();          
                txtNombreNegocio.Text       = negocio.NombreNegocio;
                txtDescripNegocio.Text      = negocio.Descripcion;
                txtBarrioNegocio.Text       = negocio.Localidad;
                txtDireccion.Text           = negocio.Direccion;
                txtTelefono.Text            = negocio.Telefono;
                txtActividad.Text           = actividad.NombreActividad;
                txtAntiguedad.Text          = negocio.Antiguedad.ToString();
                ChkArrend.Checked           = ((negocio.Propia == 1) ? false : true);
                ChkPropio.Checked           = ((negocio.Propia == 0) ? false : true);
                txtArrendador.Text          = negocio.Arrendador;
                txttelefArrendador.Text     = negocio.TelefonoArrendador;
                txtExperActividad.Text      = negocio.Experencia.ToString();
                txtEmpleadosPerma.Text      = negocio.EmpleadosPermanentes.ToString();
                txtEmpleadosTempo.Text      = negocio.EmpleadosTemporales.ToString();
              
                Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);            
            }
        }
        catch (Exception ex){
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = contentTable;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

   


}