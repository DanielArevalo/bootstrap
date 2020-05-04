using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.Seguridad.Services.PerfilService OpcionesServicio2 = new Xpinn.Seguridad.Services.PerfilService();
    private Xpinn.FabricaCreditos.Services.OpcionesCreditoService OpcionesServicio = new Xpinn.FabricaCreditos.Services.OpcionesCreditoService();

    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[OpcionesServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(OpcionesServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(OpcionesServicio.CodigoPrograma, "A");
            ctlMensaje.eventoClick += btnContinuar_Click;
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGrabar_Click;
            toolBar.eventoRegresar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionesServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarModulo();
                cargarListas();
                if (Session[OpcionesServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[OpcionesServicio.CodigoPrograma + ".id"].ToString();
                   
                }
                CargarOpciones(idObjeto, ddlModulo.SelectedValue);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionesServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void CargarOpciones(string sidPerfil, string sidModulo)
    {
        Int64 idPerfil = 0;
        if (sidPerfil.Trim() != "")
            idPerfil = Convert.ToInt64(sidPerfil);
        List<OpcionesCredito> lstAccesos = new List<OpcionesCredito>();
        lstAccesos = OpcionesServicio.ListarOpciones(idPerfil, Convert.ToInt64(sidModulo), (Usuario)Session["Usuario"]);
        gvLista.DataSource = lstAccesos;
        gvLista.DataBind();
    }

    /// <summary>
    /// Crear los datos del acceso 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            OpcionesCredito vacceso = new OpcionesCredito();
            vacceso.cod_clasifica = Convert.ToInt64(ddlCod_clasifica.SelectedValue);
            Label lblcodigo;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                Acceso pAcceso = new Acceso();
                lblcodigo = (Label)rFila.FindControl("lblcodigo");
                vacceso.cod_opcion = Convert.ToInt64(lblcodigo.Text);
                CheckBox chbconsulta = (CheckBox)rFila.Cells[1].FindControl("chbconsulta");
                OpcionesCredito lstConsultaCierreMensual = new OpcionesCredito(); 
                   if (chbconsulta.Checked == true)
                   {                     
                    
                    lstConsultaCierreMensual = OpcionesServicio.CrearOpcionesCredito(vacceso, (Usuario)Session["usuario"]);
                   
                }
            
                
            }
       

            Session[OpcionesServicio.CodigoPrograma + ".id"] = idObjeto;
            ctlMensaje.MostrarMensaje("Acceso: " + idObjeto.ToString());            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionesServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

   

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    private void CargarModulo()
    {
        try
        {
            Xpinn.Seguridad.Services.ModuloService moduloServicio = new Xpinn.Seguridad.Services.ModuloService();
            List<Xpinn.Seguridad.Entities.Modulo> lstModulo = new List<Xpinn.Seguridad.Entities.Modulo>();
            lstModulo = moduloServicio.ListarModulo(null, (Usuario)Session["usuario"]);
     
            ddlModulo.DataSource = lstModulo;
            ddlModulo.DataTextField = "nom_modulo";
            ddlModulo.DataValueField = "cod_modulo";
            ddlModulo.SelectedValue = "10";
            ddlModulo.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionesServicio.CodigoPrograma, "CargarModulo", ex);
        }
    }
    private void cargarListas()
    { 
        try
        { 
            ListaSolicitada = "Cod_clasifica";
            TraerResultadosLista();
            ddlCod_clasifica.DataSource = lstDatosSolicitud;
            ddlCod_clasifica.DataTextField = "ListaDescripcion";
            ddlCod_clasifica.DataValueField = "ListaId";
            ddlCod_clasifica.DataBind();
         }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionesServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
     private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = LineasCreditoServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }
    protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarOpciones(idObjeto, ddlModulo.SelectedValue);
    }
}