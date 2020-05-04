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

partial class Nuevo : GlobalWeb
{
    String descripcion;
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private Xpinn.FabricaCreditos.Services.ProcesosService ProcesosServicio = new Xpinn.FabricaCreditos.Services.ProcesosService(); // Permite iniciar la consulta del historial (Segundo GridView)
    List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
    String operacion = "";

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ProcesosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ProcesosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ProcesosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlEstados.Visible = false;
                CargarListas();
                if (Session[ProcesosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ProcesosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ProcesosServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                operacion = (String)Session["operacion"];
                if (operacion == null)
                {
                    operacion = "";
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    Boolean ValidarDatos()
    {
        if (ddlTipoProceso.SelectedIndex == 0)
        {
            VerError("Seleccine un Tipo de Proceso");
            return false;
        }
        if (ddlEstados.Visible == true)
        {
            if (ddlEstados.SelectedIndex == 0)
            {
                VerError("Seleccine un estado");
                return false;
            }
        }
        else
        {
            if (txtDescripcion.Text == "")
            {
                VerError("Ingrese una descripción del estado");
                return false;
            }
        }
        return true;
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Xpinn.FabricaCreditos.Entities.Procesos vProcesos = new Xpinn.FabricaCreditos.Entities.Procesos();

                vProcesos.tipo_proceso = Convert.ToInt32(ddlTipoProceso.SelectedValue);
                if (ddlAntecesor.SelectedValue != "")
                    vProcesos.cod_proceso_antec = Convert.ToInt64(ddlAntecesor.SelectedValue);
                if (ddlTipoProceso.SelectedValue == "1")
                    vProcesos.descripcion = this.ddlEstados.SelectedItem.Text;
                else if (ddlTipoProceso.SelectedValue == "2")
                    vProcesos.descripcion = txtDescripcion.Text;

                if (ddlEstados.Visible == true)
                    if (ddlEstados.SelectedIndex != 0)
                        vProcesos.estado = ddlEstados.SelectedValue;
                    else
                        vProcesos.estado = null;
                else
                    vProcesos.estado = null;

                if (idObjeto != "")
                {
                    vProcesos.cod_proceso = Convert.ToInt64(idObjeto);
                    vProcesos = ProcesosServicio.ModificarProcesos(vProcesos, (Usuario)Session["usuario"]);
                }
                else
                {
                    vProcesos.cod_proceso = 0;
                    vProcesos = ProcesosServicio.CrearProcesos(vProcesos, (Usuario)Session["usuario"]);
                    idObjeto = vProcesos.cod_proceso.ToString();
                }
                if (!string.IsNullOrEmpty(vProcesos.Perror))
                {
                    VerError("No se pudo crear el proceso: " + vProcesos.Perror);
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
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Procesos vProcesos = new Xpinn.FabricaCreditos.Entities.Procesos();
            vProcesos = ProcesosServicio.ConsultarProcesos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vProcesos.cod_proceso.ToString()))
                txtCodProceso.Text = HttpUtility.HtmlDecode(vProcesos.cod_proceso.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesos.tipo_proceso.ToString()))
                ddlTipoProceso.SelectedValue = HttpUtility.HtmlDecode(vProcesos.tipo_proceso.ToString().Trim());

            ddlTipoProceso_SelectedIndexChanged(ddlTipoProceso, null);
            if (vProcesos.tipo_proceso == 1)
            {
                if (vProcesos.estado != null && vProcesos.estado != "")
                    ddlEstados.SelectedValue = vProcesos.estado;
            }
            else if (vProcesos.tipo_proceso == 2)
            {
                if (vProcesos.descripcion != null)
                    txtDescripcion.Text = HttpUtility.HtmlDecode(vProcesos.descripcion.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(vProcesos.cod_proceso_antec.ToString()))
                ddlAntecesor.SelectedValue = HttpUtility.HtmlDecode(vProcesos.cod_proceso_antec.ToString().Trim());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void ObtenerProcesosAutomaticos()
    {

        try
        {
            Xpinn.FabricaCreditos.Entities.Procesos vProcesos = new Xpinn.FabricaCreditos.Entities.Procesos();

            if (!string.IsNullOrEmpty(vProcesos.descripcion.ToString()))

                this.ddlEstados.SelectedItem.Text = HttpUtility.HtmlDecode(vProcesos.descripcion.ToString().Trim());
            descripcion = vProcesos.descripcion;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "ObtenerProcesosAutomaticos", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    private void CargarListas()
    {
        try
        {
            ddlAntecesor.Items.Clear();
            ddlAntecesor.Items.Add(new ListItem("Ninguno", "0"));
            ListaSolicitada = "EstadoProceso";
            TraerResultadosLista();
            ddlAntecesor.DataSource = lstDatosSolicitud;
            ddlAntecesor.DataTextField = "ListaDescripcion";
            ddlAntecesor.DataValueField = "ListaId";
            ddlAntecesor.DataBind();

            Xpinn.FabricaCreditos.Services.ProcesosService vProcesoServicio = new Xpinn.FabricaCreditos.Services.ProcesosService();
            Xpinn.FabricaCreditos.Entities.Procesos vProceso = new Xpinn.FabricaCreditos.Entities.Procesos();
            ddlEstados.DataSource = vProcesoServicio.ListarProcesosAutomaticos(vProceso, (Usuario)Session["usuario"]);
            ddlEstados.DataTextField = "descripcion";
            ddlEstados.DataValueField = "estado";
            ddlEstados.DataBind();
            ddlEstados.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    protected void ddlTipoProceso_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoProceso.SelectedValue == "2")
        {
            txtDescripcion.Visible = true;
            ddlEstados.Visible = false;
        }
        else
        {
            txtDescripcion.Visible = false;
            ddlEstados.Visible = true;
        }
    }

}