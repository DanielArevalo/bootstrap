using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Collections.Generic;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.InformacionNegocioService InformacionNegocioServicio = new Xpinn.FabricaCreditos.Services.InformacionNegocioService();
    //String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    List<Xpinn.FabricaCreditos.Entities.InformacionNegocio> lstInformacionNegocio = new List<Xpinn.FabricaCreditos.Entities.InformacionNegocio>();  //Lista de los menus desplegables
    Xpinn.FabricaCreditos.Entities.InformacionNegocio vInformacionNegocio = new Xpinn.FabricaCreditos.Entities.InformacionNegocio();
    List<Xpinn.FabricaCreditos.Entities.InformacionNegocio> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.InformacionNegocio>();
                        
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[InformacionNegocioServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(InformacionNegocioServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(InformacionNegocioServicio.CodigoPrograma, "A");

            Site1 toolBar = (Site1)this.Master;


            if (Session["negocio"] != null)
                toolBar.eventoAdelante += btnAdelante_Click2;
            else
            toolBar.eventoAdelante += btnAdelante_Click;

            toolBar.eventoAtras += btnAtras_Click;

            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            vInformacionNegocio.usuultmod = lblUsuario.Text;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            ((Label)Master.FindControl("lblCod_Cliente")).Text = Session["Cod_persona"].ToString();

            if (Session["negocio"] != null)
                toolBar.eventoAdelante += btnAdelante_Click2;
            else
                toolBar.eventoAdelante += btnAdelante_Click;

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            if (Session["negocio"] != null)
                btnAdelante.ImageUrl = "~/Images/btnGuardar.jpg";
            else
                btnAdelante.ImageUrl = "~/Images/btnGrupoFamiliar.jpg";


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            Site1 toolBar = (Site1)this.Master;

    
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            if (Session["negocio"] != null)
                btnAdelante.ImageUrl = "~/Images/btnGuardar.jpg";
            else
                btnAdelante.ImageUrl = "~/Images/btnGrupoFamiliar.jpg";
            if (!IsPostBack)
            {
                //Verifica si ya se registraron datos del negocio:

                lstConsulta = InformacionNegocioServicio.ListarInformacionNegocio(ObtenerValores(), (Usuario)Session["usuario"]);
                if (lstConsulta.Count > 0)
                {
                    idObjeto = lstConsulta[0].cod_negocio.ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    CargarListas();
                }
            }                         
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    private Xpinn.FabricaCreditos.Entities.InformacionNegocio ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.InformacionNegocio vInformacionNegocio = new Xpinn.FabricaCreditos.Entities.InformacionNegocio();

        vInformacionNegocio.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        return vInformacionNegocio;
    }


    private void CargarListas()
    {       
        try
        {
            vInformacionNegocio.ListaSolicitada = "Actividad";
            TraerResultadosLista();
            ddlActividad.DataSource = lstInformacionNegocio;
            ddlActividad.DataTextField = "ListaDescripcion";
            ddlActividad.DataValueField = "ListaId";
            ddlActividad.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lstInformacionNegocio.GetType().Name + "L", "CargarListas", ex);
        }


        try
        {
            vInformacionNegocio.ListaSolicitada = "Localidad";
            TraerResultadosLista();
            ddlLocalidad.DataSource = lstInformacionNegocio;
            ddlLocalidad.DataTextField = "ListaDescripcion";
            ddlLocalidad.DataValueField = "ListaId";
            ddlLocalidad.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lstInformacionNegocio.GetType().Name + "L", "CargarListas", ex);
        }

        try
        {
            vInformacionNegocio.ListaSolicitada = "Barrio";
            vInformacionNegocio.localidad = ddlLocalidad.SelectedValue;
            TraerResultadosLista();
            ddlBarrio.DataSource = lstInformacionNegocio;
            ddlBarrio.DataTextField = "ListaDescripcion";
            ddlBarrio.DataValueField = "ListaId";
            ddlBarrio.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void TraerResultadosLista()
    {        
        lstInformacionNegocio.Clear();
        lstInformacionNegocio = InformacionNegocioServicio.ListarInformacionNegocio(vInformacionNegocio, (Usuario)Session["usuario"]);//vInformacionNegocio.usuultmod.ToString());
    }
    

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[InformacionNegocioServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            vInformacionNegocio = InformacionNegocioServicio.ConsultarInformacionNegocio(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            
            if (vInformacionNegocio.cod_negocio != Int64.MinValue)
                txtCod_negocio.Text = HttpUtility.HtmlDecode(vInformacionNegocio.cod_negocio.ToString().Trim());
            if (vInformacionNegocio.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vInformacionNegocio.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vInformacionNegocio.direccion))
                try { direccion.Text = HttpUtility.HtmlDecode(vInformacionNegocio.direccion.ToString().Trim()); }
                catch { }
            if (!string.IsNullOrEmpty(vInformacionNegocio.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vInformacionNegocio.telefono.ToString().Trim());                        
            if (!string.IsNullOrEmpty(vInformacionNegocio.nombrenegocio))
                txtNombrenegocio.Text = HttpUtility.HtmlDecode(vInformacionNegocio.nombrenegocio.ToString().Trim());
            if (txtNombrenegocio.Text.Trim() == "")
                if (Session["Negocio"] != null)
                    txtNombrenegocio.Text = Session["Negocio"].ToString();
            if (vInformacionNegocio.valor_arriendo != Int64.MinValue && vInformacionNegocio.valor_arriendo != 0)
                txtvalorarriendo.Text = HttpUtility.HtmlDecode(vInformacionNegocio.valor_arriendo.ToString().Trim());
            if (!string.IsNullOrEmpty(vInformacionNegocio.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vInformacionNegocio.descripcion.ToString().Trim());
            if (vInformacionNegocio.antiguedad != Int64.MinValue)
                txtAntiguedad.Text = HttpUtility.HtmlDecode(vInformacionNegocio.antiguedad.ToString().Trim());
            if (vInformacionNegocio.propia != Int64.MinValue)
            {
                rblTipoLocal.SelectedValue = HttpUtility.HtmlDecode(vInformacionNegocio.propia.ToString().Trim());
                ValidaPropioArrendado();
             }
            if (!string.IsNullOrEmpty(vInformacionNegocio.arrendador))
                txtArrendador.Text = HttpUtility.HtmlDecode(vInformacionNegocio.arrendador.ToString().Trim());
            if (!string.IsNullOrEmpty(vInformacionNegocio.telefonoarrendador))
                txtTelefonoarrendador.Text = HttpUtility.HtmlDecode(vInformacionNegocio.telefonoarrendador.ToString().Trim());
            if (vInformacionNegocio.sector != Int64.MinValue)
                rblActividad.SelectedValue = HttpUtility.HtmlDecode(vInformacionNegocio.sector.ToString().Trim());
            try
            {
                if (vInformacionNegocio.codactividad != Int64.MinValue)
                    ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vInformacionNegocio.codactividad.ToString().Trim());
            }
            catch
            {
            }
            if (vInformacionNegocio.experiencia != Decimal.MinValue)
                txtExperiencia.Text = HttpUtility.HtmlDecode(vInformacionNegocio.experiencia.ToString().Trim());
            if (vInformacionNegocio.emplperm != Int64.MinValue)
                txtEmplperm.Text = HttpUtility.HtmlDecode(vInformacionNegocio.emplperm.ToString().Trim());
            if (vInformacionNegocio.empltem != Int64.MinValue)
                txtEmpltem.Text = HttpUtility.HtmlDecode(vInformacionNegocio.empltem.ToString().Trim());          

            //Despues de obtener datos se carga el valor seleccionado en las listas
            CargarListas();
            if (!string.IsNullOrEmpty(vInformacionNegocio.localidad))                
                ddlLocalidad.SelectedValue = HttpUtility.HtmlDecode(vInformacionNegocio.localidad.ToString().Trim());
            if (vInformacionNegocio.barrio != Int64.MinValue)
              ddlBarrio.SelectedValue = vInformacionNegocio.barrio.ToString().Trim();

            ValidaPropioArrendado();
        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void ddlLocalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBarrio.Enabled = true;
        vInformacionNegocio.ListaSolicitada = "Barrio";
        vInformacionNegocio.localidad = ddlLocalidad.SelectedValue;
        TraerResultadosLista();
        ddlBarrio.DataSource = lstInformacionNegocio;
        ddlBarrio.DataTextField = "ListaDescripcion";
        ddlBarrio.DataValueField = "ListaId";
        ddlBarrio.DataBind();

    }

    protected void rblTipoLocal_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidaPropioArrendado();
    }

    private void ValidaPropioArrendado()
    {
        if (rblTipoLocal.SelectedValue == "0")  // Si el local es arrendado:
            {
                txtArrendador.Enabled = true;
                txtTelefonoarrendador.Enabled = true;
                txtvalorarriendo.Enabled = true;
            }
            else
            {
                txtArrendador.Enabled = false;
                txtTelefonoarrendador.Enabled = false;
                txtvalorarriendo.Enabled = false;
                txtArrendador.Text = "";
                txtTelefonoarrendador.Text = "";
                txtvalorarriendo.Text = "0";
            }
    }


    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Session["DetalleCliente"] = "1";    // Variable que permite activar el view donde está la informacion detalle del cliente
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosCliente/DatosBasicos.aspx");
    }

    /// <summary>
    /// Método cuando se graba un nuevo negocio
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Guardar();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
    }

    /// <summary>
    /// Método para cuando se presiona el botón de guardar cuando se estan modificando datos del negocio
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdelante_Click2(object sender, ImageClickEventArgs e)
    {
        Guardar();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
    }

    private void Guardar()
    {
        try
        {

            if (idObjeto != "")
                vInformacionNegocio = InformacionNegocioServicio.ConsultarInformacionNegocio(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_negocio.Text != "") vInformacionNegocio.cod_negocio = Convert.ToInt64(txtCod_negocio.Text.Trim());
            vInformacionNegocio.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            if (txtvalorarriendo.Text == "")
                vInformacionNegocio.valor_arriendo = 0;
            else
            vInformacionNegocio.valor_arriendo =Convert.ToInt64(txtvalorarriendo.Text);
            vInformacionNegocio.direccion = (direccion.Text != "") ? Convert.ToString(direccion.Text.Trim()) : String.Empty;
            vInformacionNegocio.telefono = (txtTelefono.Text != "") ? Convert.ToString(txtTelefono.Text.Trim()) : String.Empty;
            if (ddlBarrio.Text != "") vInformacionNegocio.barrio = Convert.ToInt64(ddlBarrio.SelectedValue);
            if (ddlLocalidad.SelectedValue != "Selecccione un Item")
                vInformacionNegocio.localidad = (ddlLocalidad.Text != "") ? Convert.ToString(ddlLocalidad.SelectedValue) : String.Empty;
            else
                vInformacionNegocio.localidad = String.Empty;
            vInformacionNegocio.nombrenegocio = (txtNombrenegocio.Text != "") ? Convert.ToString(txtNombrenegocio.Text.Trim()) : String.Empty;
            vInformacionNegocio.descripcion = (txtDescripcion.Text != "") ? Convert.ToString(txtDescripcion.Text.Trim()) : String.Empty;
            if (txtAntiguedad.Text != "") vInformacionNegocio.antiguedad = Convert.ToInt64(txtAntiguedad.Text.Trim());
            if (rblTipoLocal.Text != "") vInformacionNegocio.propia = Convert.ToInt64(rblTipoLocal.SelectedValue);
            vInformacionNegocio.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim()) : String.Empty;
            vInformacionNegocio.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : String.Empty;
            if (ddlActividad.SelectedValue != "") vInformacionNegocio.codactividad = Convert.ToInt64(ddlActividad.SelectedValue);
            if (rblActividad.Text != "") vInformacionNegocio.sector = Convert.ToInt64(rblActividad.SelectedValue);
            if (txtExperiencia.Text != "") vInformacionNegocio.experiencia = Convert.ToDecimal(txtExperiencia.Text.Trim());
            if (txtEmplperm.Text != "") vInformacionNegocio.emplperm = Convert.ToInt64(txtEmplperm.Text.Trim());
            if (txtEmpltem.Text != "") vInformacionNegocio.empltem = Convert.ToInt64(txtEmpltem.Text.Trim());
            vInformacionNegocio.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(gFormatoFecha));
            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            vInformacionNegocio.usuariocreacion = lblUsuario.Text;
            vInformacionNegocio.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(gFormatoFecha));
            vInformacionNegocio.usuultmod = lblUsuario.Text;
            if (idObjeto != "")
            {
                vInformacionNegocio.cod_negocio = Convert.ToInt64(idObjeto);
                InformacionNegocioServicio.ModificarInformacionNegocio(vInformacionNegocio, (Usuario)Session["usuario"]);
            }
            else
            {
                vInformacionNegocio = InformacionNegocioServicio.CrearInformacionNegocio(vInformacionNegocio, (Usuario)Session["usuario"]);
                idObjeto = vInformacionNegocio.cod_negocio.ToString();
                Session["Cod.Negocio"] = vInformacionNegocio.cod_negocio;
            }
            Session[InformacionNegocioServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    
    }

    protected void txtEmplperm_TextChanged(object sender, EventArgs e)
    {

    }
}

