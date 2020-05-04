using System;
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

    Xpinn.Caja.Data.OficinaData oficinaData = new Xpinn.Caja.Data.OficinaData();
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
    Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    Xpinn.Caja.Services.CajaService cajaServicio = new Xpinn.Caja.Services.CajaService();
    Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
    Xpinn.Caja.Entities.ProcesoOficina procesoOficina = new Xpinn.Caja.Entities.ProcesoOficina();
    Xpinn.Caja.Services.ProcesoOficinaService procesoOficinaService = new Xpinn.Caja.Services.ProcesoOficinaService();
    Usuario user = new Usuario();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(cajaServicio.CodigoPrograma2, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

            if (Session[cajaServicio.CodigoCaja + ".id"] != null)
                toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "E", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["codCaj"] = cajero.cod_cajero;
                ObtenerDatos();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "E", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos()
    {
        string[] listcaja = (string[])Session["ListCaja"];
        caja = cajaServicio.ConsultarCaja(Convert.ToInt64(listcaja[0]), (Usuario)Session["usuario"]);
        oficina = oficinaService.ConsultarXOficina(Convert.ToInt64(listcaja[1]), (Usuario)Session["usuario"]);
        txtFecha.Text = caja.fecha_creacion.ToString("dd/MM/yyyy");
        Usuario codusu = (Usuario)Session["usuario"];
        procesoOficina.cod_usuario = codusu.codusuario;

        Session["codusuario"] = procesoOficina.cod_usuario;
        procesoOficina.fecha_proceso = oficina.fechaproceso;// fecha maxima que puede ser la de apertura o cierre
        procesoOficina.cod_oficina = Convert.ToInt64(listcaja[1]);
        procesoOficina.tipo_proceso = oficina.tipo_proceso;



      
        int anio, dia, mes;
        DateTime dtFecha;
        anio = procesoOficina.fecha_proceso.Year;
        mes = procesoOficina.fecha_proceso.Month;
        dia = procesoOficina.fecha_proceso.Day;

        dtFecha = new DateTime(anio, mes, dia, 23, 59, 0);

        tbxFechaNuevoProceso.Text = dtFecha.ToString("dd/MM/yyyy");

   
        if (Convert.ToDateTime(tbxFechaNuevoProceso.Text) == Convert.ToDateTime(txtFecha.Text))
        {
            tbxFechaNuevoProceso.Text = oficina.fecha_nuevo_proceso.ToString("dd/MM/yyyy");
        }


        Session["fechaproceso"] = procesoOficina.fecha_proceso;

        procesoOficina = procesoOficinaService.ConsultarXProcesoOficina(procesoOficina, (Usuario)Session["usuario"]);



        // Cierre de Caja   
        if (int.Parse(caja.estado.ToString()) == 1)//caso de que la caja este activa; caja.estado es el estado actual de la caja a aperturar no del procesoOficina 
        {
            txtEstadoAct.Text = "Activo";
            chkNuevoEstado.Text = "Inactivo";
        }
        else // Apertura de Oficina - caso de que la oficina este inactiva
        {
            txtEstadoAct.Text = "Inactivo";
            chkNuevoEstado.Text = "Activo";
        }
        txtCodigo.Text = listcaja[0];
        txtOficina.Text = listcaja[2];
        txtNombre.Text = listcaja[3];
        Session["cod_oficina"] = Convert.ToInt64(listcaja[1]);
        // validacion de caja principal  
        if (int.Parse(caja.esprincipal.ToString()) == 1)//caso de que la caja es principal se activa; caja.esprincipal 
        {
            chkPrincipal.Checked = true;
        }
        else
        {
            chkPrincipal.Checked = false;
        }
    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../..//Page/CajaFin/AperturaCierreCaja/Lista.aspx");
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {   
            if(chkNuevoEstado.Checked)
            {
                Usuario codusu = (Usuario)Session["usuario"];
                List<Xpinn.Caja.Entities.Caja> lstConsulta = new List<Xpinn.Caja.Entities.Caja>();
                List<Xpinn.Caja.Entities.Caja> lista = new List<Xpinn.Caja.Entities.Caja>();
                Xpinn.Caja.Entities.Caja variable = new Xpinn.Caja.Entities.Caja();
                Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
                variable.cod_caja = txtCodigo.Text;
                variable.cod_oficina = Convert.ToInt64(Session["cod_oficina"].ToString());
                variable.nombre = txtNombre.Text;
                variable.fecha_creacion = Convert.ToDateTime(tbxFechaNuevoProceso.Text);
                variable.estado = txtEstadoAct.Text == "Activo" ? 1 : txtEstadoAct.Text == "Inactivo" ? 0 : 2;
                variable.esprincipal = Convert.ToInt64(chkPrincipal.Checked);              
                lstConsulta.Add(variable);
                int resultado = 0;
                resultado = procesoOficinaService.CrearCajasAbrir(lstConsulta, Session["codCaj"].ToString(), codusu);
                variable.fecha_creacion = Convert.ToDateTime(txtFecha.Text);
                variable.estado = txtEstadoAct.Text == "Activo" ? 0 : txtEstadoAct.Text == "Inactivo" ? 1 : 2;
                caja = cajaServicio.ModificarCaja(variable,new GridView(), new GridView(), codusu);
                if (resultado == 1)
                {
                    VerError("");
                    Navegar("Lista.aspx");
                }
                else
                {
                    VerError("No se pudo realizar los cambios de la caja");
                    return;
                }
            }
            else
            {
                VerError("No se puede guardar sin haber seleccionado el nuevo estado");
                return;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "E", "btnGuardar_Click", ex);
        }
    }
}