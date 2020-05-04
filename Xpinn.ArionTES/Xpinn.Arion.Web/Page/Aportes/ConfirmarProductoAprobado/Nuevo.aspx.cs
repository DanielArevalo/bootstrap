using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;

public partial class Nuevo : GlobalWeb
{
    AhorroVistaServices _ahorrosService = new AhorroVistaServices();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnContinuar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ahorrosService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["solicitudProducto"] != null)
                    ObtenerDatos();
                else
                    Response.Redirect("Lista.aspx", false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ahorrosService.GetType().Name + "L", "Page_Load", ex);
        }
    }




    protected void ObtenerDatos()
    {
        try
        {
            //OBTENER LOS DATOS PARA MOSTRAR EN DETALLE
            if(Session["solicitudProducto"] != null)
            {
                AhorroVista entidad = Session["solicitudProducto"] as AhorroVista;
                txtCodPersona.Text = entidad.cod_persona.ToString();
                txtIdentificacion.Text = entidad.identificacion;
                txtNombre.Text = entidad.nombres;
                txtIdSol.Text = entidad.id_solicitud.ToString();
                txtProducto.Text = entidad.nombre_producto;
                txtEstado.Text = entidad.nom_estado;
                txtValor.Text = ((decimal)entidad.valor_cuota).ToString("C0");
                txtPlazo.Text = entidad.plazo + " meses";
                txtFechaSol.Text = entidad.fecha.ToString("dd/mm/yyyy");
                DocumentosAnexos.TablaDocumentosAnexo(entidad.id_solicitud.ToString(), 5, 1);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ahorrosService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }    

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["solicitudProducto"] = null;
        string rootCompleto = Server.MapPath("~/Page/FabricaCreditos/PlanPagos/Archivos");
        string nombreArchivo = @"\DocumentoAnexo_" + ((Usuario)Session["usuario"]).codperfil;
        DocumentosAnexos.EliminarDocumentos(rootCompleto, nombreArchivo);
        Navegar(Pagina.Lista);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("¿Seguro desea continuar el proceso para esta solicitud?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["solicitudProducto"] != null)
            {
                AhorroVista entidad = Session["solicitudProducto"] as AhorroVista;
                string tipo = Convert.ToString(entidad.tipo_producto);
                string rootCompleto = Server.MapPath("~/Page/FabricaCreditos/PlanPagos/Archivos");
                string nombreArchivo = @"\DocumentoAnexo_" + ((Usuario)Session["usuario"]).codperfil;
                DocumentosAnexos.EliminarDocumentos(rootCompleto, nombreArchivo);

                switch (tipo)
                {
                    case "3": //Redirige a creación de Ahorro
                        Response.Redirect("../../AhorrosVista/CuentasAhorro/Nuevo.aspx", false);
                        break;
                    case "5": //Redirige a creación de CDAT
                        Response.Redirect("../../CDATS/Apertura/Nuevo.aspx", false);
                        break;
                    case "9": //Redirige a creación de Aporte
                        Response.Redirect("../../Programado/Cuentas/Nuevo.aspx", false);
                        break;
                    default:
                        break;
                }
            }             
        }
        catch (Exception ex)
        {
            VerError("Error al intentar generar el retiro, " + ex.Message);
        }
    }

}
