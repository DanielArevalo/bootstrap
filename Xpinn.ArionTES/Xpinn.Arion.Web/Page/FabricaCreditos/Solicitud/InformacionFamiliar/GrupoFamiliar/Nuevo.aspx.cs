using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.FamiliaresService familiaresServicio = new Xpinn.FabricaCreditos.Services.FamiliaresService();
    String ListaSolicitada = "";
    List<Xpinn.FabricaCreditos.Entities.Familiares> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Familiares>();  //Lista de los menus desplegables
    
    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[familiaresServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(familiaresServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(familiaresServicio.CodigoPrograma, "A");

            //((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            //((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
                                
            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;  
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[familiaresServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[familiaresServicio.CodigoPrograma + ".id"].ToString();
                    if (idObjeto != null)
                    {
                        ObtenerDatos(idObjeto);
                        Session.Remove(familiaresServicio.CodigoPrograma + ".id");
                    }
                 
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
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }
    
    


   private void Guardar()
    {
     try
        {
            Xpinn.FabricaCreditos.Entities.Familiares familiares = new Xpinn.FabricaCreditos.Entities.Familiares();

            if (idObjeto != "")
                familiares = familiaresServicio.ConsultarFamiliares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            familiares.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
           
            if (txtNombres.Text != "") familiares.nombres = txtNombres.Text.Trim();
            familiares.codparentesco = Convert.ToInt64(ddlParentesco.SelectedValue);
            familiares.sexo = rblSexo.SelectedValue;
            familiares.acargo = Convert.ToInt64(rblAcargo.SelectedValue);
            familiares.observaciones = (txtObservaciones.Text != "") ? Convert.ToString(txtObservaciones.Text.Trim()) : String.Empty;
            //if (txtFechaNacimiento.Text != "") familiares.fechanacimiento = Convert.ToDateTime(txtFechaNacimiento.Text.Trim());
            familiares.fechanacimiento = DateTime.Now;
         familiares.estudia = (rblEstudia.Text != "") ? Convert.ToInt64(rblEstudia.SelectedValue) : 0;

            
            if (idObjeto != "")
            {
                familiares.codfamiliar = Convert.ToInt64(idObjeto);
                familiares.UsuarioEdita = "Admin";  // Modificar por usuario en sesion
                familiaresServicio.ModificarFamiliares(familiares, (Usuario)Session["usuario"]);
            }
            else
            {
                
                
                familiares.UsuarioCrea = "Admin";  // Modificar por usuario en sesion
                familiares = familiaresServicio.CrearFamiliares(familiares, (Usuario)Session["usuario"]);
                idObjeto = familiares.codfamiliar.ToString();
            }

            Session[familiaresServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }


     private void CargarListas()
    {
        //txtCodigoPersona.Text = Session[familiaresServicio.CodigoPrograma + ".idCliente"].ToString();  //idObjeto.ToString();
                
        // Carga la lista de parentescos
        ListaSolicitada = "Parentescos";
        TraerResultadosLista();
        ddlParentesco.DataSource = lstDatosSolicitud;
        ddlParentesco.DataTextField = "ListaDescripcion";
        ddlParentesco.DataValueField = "ListaId";
        ddlParentesco.DataBind();
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = familiaresServicio.ListasDesplegables(ObtenerValores(), (Usuario)Session["usuario"], ListaSolicitada);

    }


    private Xpinn.FabricaCreditos.Entities.Familiares ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Familiares familiares = new Xpinn.FabricaCreditos.Entities.Familiares();
        //datosCliente.numero = txtIdentificacion.Text;
        return familiares;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Session[familiaresServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[familiaresServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }



    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionNegocio/Nuevo.aspx");
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Guardar();
        // Valida estado civil
        if (Session["EstadoCivil"].ToString() == "1" || Session["EstadoCivil"].ToString() == "3")
        {
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/Conyuge/Nuevo.aspx");
        }
        else
        {
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Familiares familiares = new Xpinn.FabricaCreditos.Entities.Familiares();
            familiares = familiaresServicio.ConsultarFamiliares(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(familiares.cod_persona.ToString()))
                txtCodigoPersona.Text = HttpUtility.HtmlDecode(familiares.cod_persona.ToString());            
            if (!string.IsNullOrEmpty(familiares.nombres))
                txtNombres.Text = HttpUtility.HtmlDecode(familiares.nombres.Trim().ToString());
           
            if (!string.IsNullOrEmpty(familiares.sexo))
                rblSexo.SelectedValue = HttpUtility.HtmlDecode(familiares.sexo.Trim().ToString());
            if (!string.IsNullOrEmpty(familiares.acargo.ToString()))
                rblAcargo.SelectedValue = HttpUtility.HtmlDecode(familiares.acargo.ToString());
            if (!string.IsNullOrEmpty(familiares.observaciones))
                txtObservaciones.Text = HttpUtility.HtmlDecode(familiares.observaciones.ToString());
            else txtObservaciones.Text = "";

            //if (!string.IsNullOrEmpty(familiares.fechanacimiento.ToString()))
            //    txtFechaNacimiento.Text = HttpUtility.HtmlDecode(familiares.fechanacimiento.ToString());

            if (familiares.fechanacimiento != DateTime.MinValue)
                txtFechaNacimiento.Text = HttpUtility.HtmlDecode(familiares.fechanacimiento.ToShortDateString());

            if (!string.IsNullOrEmpty(familiares.estudia.ToString()))
                rblEstudia.Text = HttpUtility.HtmlDecode(familiares.estudia.ToString());

            //Despues de obtener datos se carga el valor seleccionado en las listas
            CargarListas();
            if (!string.IsNullOrEmpty(familiares.codparentesco.ToString()))
                ddlParentesco.SelectedValue = HttpUtility.HtmlDecode(familiares.codparentesco.ToString());           
        

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Guardar();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
    }
}