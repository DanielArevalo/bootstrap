using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.FamiliaresService familiaresServicio = new Xpinn.FabricaCreditos.Services.FamiliaresService();
    
    String ListaSolicitada = "";
    List<Xpinn.FabricaCreditos.Entities.Familiares> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Familiares>();  //Lista de los menus desplegables
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(familiaresServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;            
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;               

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            
            
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";

           
                if (Session["EstadoCivil"].ToString() == "1" || Session["EstadoCivil"].ToString() == "3")  // Si es casado o union libre pone imagen "btnInformacionConyuge"
                    btnAdelante.ImageUrl = "~/Images/btnInformacionConyuge.jpg";
            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta1, familiaresServicio.GetType().Name);
                CargarListas();
                Actualizar();

                if (Session[familiaresServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta1, familiaresServicio.GetType().Name);

        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta1, familiaresServicio.GetType().Name);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta1, familiaresServicio.GetType().Name);
    }
   
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    protected void gvMiembroFamiliar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnBorrar");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {                   
            e.Row.Cells[8].Text = Convert.ToString(GetAge(Convert.ToDateTime(e.Row.Cells[8].Text)));
            e.Row.Cells[9].Text = e.Row.Cells[9].Text == "0" ? "No" : "Si";
            e.Row.Cells[10].Text = e.Row.Cells[10].Text == "0" ? "No" : "Si";            
        }
    }

    protected void gvMiembroFamiliar_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvMiembroFamiliar1.SelectedRow.Cells[0].Text;
        Session[familiaresServicio.CodigoPrograma + ".id"] = id;        
        Edicion();
    }

    protected void gvMiembroFamiliar_RowEditing(object sender, GridViewEditEventArgs e)
    { 
        String id = gvMiembroFamiliar1.SelectedRow.Cells[0].Text;
        Session[familiaresServicio.CodigoPrograma + ".id"] = id;
        Edicion();
    }


    private void Edicion()
    {

        try
        {
            if (Session[familiaresServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(familiaresServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(familiaresServicio.CodigoPrograma, "A");

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
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "A", "Page_PreInit", ex);
        }

    }



    protected void gvMiembroFamiliar_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvMiembroFamiliar1.Rows[e.RowIndex].Cells[0].Text);
            familiaresServicio.EliminarFamiliares(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.CodigoPrograma + "L", "gvMiembroFamiliar_RowDeleting", ex);
        }
    }

  
    private void CargarListas()
    {
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

    private void Guardar()
    {
        Boolean result = true;
        int vEdad = GetAge(Convert.ToDateTime(txtFechaNacimiento.Text));

        if (ddlParentesco.SelectedValue != "12" && vEdad <= 122) //Si el familiar es conyuge o tiene mas de 122 años no permite registrar.
        {
            try
            {
                Xpinn.FabricaCreditos.Entities.Familiares familiares = new Xpinn.FabricaCreditos.Entities.Familiares();

                if (idObjeto != "")
                    familiares = familiaresServicio.ConsultarFamiliares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

                familiares.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());

                if (txtNombres.Text != "") familiares.nombres = txtNombres.Text.Trim();
                familiares.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
                familiares.codparentesco = Convert.ToInt64(ddlParentesco.SelectedValue);
                familiares.sexo = rblSexo.SelectedValue;
                familiares.acargo = Convert.ToInt64(rblAcargo.SelectedValue);
                familiares.observaciones = (txtObservaciones.Text != "") ? Convert.ToString(txtObservaciones.Text.Trim()) : String.Empty;
                if (txtFechaNacimiento.Text != "") familiares.fechanacimiento = Convert.ToDateTime(txtFechaNacimiento.Text.Trim());
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
                    if (idObjeto != "")
                    {
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
                string vError = ex.ToString();
            }

        }
        else
        {
          
            String Error=("No se puede registrar porque el familiar es conyuge o tiene más de 122 años");

            this.Lblerror.Text = Error;
            result = false;
        }
      

    }


    protected void gvMiembroFamiliar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMiembroFamiliar1.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "L", "gvMiembroFamiliar_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            string codCliente = Session["Cod_persona"].ToString();
            List<Xpinn.FabricaCreditos.Entities.Familiares> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Familiares>();
            lstConsulta = familiaresServicio.ListarFamiliares(ObtenerValores(), (Usuario)Session["usuario"], codCliente);

            gvMiembroFamiliar1.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvMiembroFamiliar1.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvMiembroFamiliar1.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvMiembroFamiliar1.DataBind();
                ValidarPermisosGrilla(gvMiembroFamiliar1);
            }
            else
            {
                gvMiembroFamiliar1.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(familiaresServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Familiares ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Familiares programa = new Xpinn.FabricaCreditos.Entities.Familiares();

        return programa;
    }


    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionNegocio/Nuevo.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombres.Text != "" && txtIdentificacion.Text != "" && txtFechaNacimiento.Text != "")
             Guardar();

        if (Session["EstadoCivil"].ToString() == "1" || Session["EstadoCivil"].ToString() == "3")  // Si es casado o union libre direcciona a conyuge, sino a referencias
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/Conyuge/Nuevo.aspx");
        else if (Session["TipoCredito"].ToString() == "M")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/Referencias/Lista.aspx");
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InformacionFinancieraNegocio/Default.aspx");        
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
            if (!string.IsNullOrEmpty(familiares.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(familiares.identificacion.Trim().ToString());
            if (!string.IsNullOrEmpty(familiares.sexo))
                rblSexo.SelectedValue = HttpUtility.HtmlDecode(familiares.sexo.Trim().ToString());
            if (!string.IsNullOrEmpty(familiares.acargo.ToString()))
                rblAcargo.SelectedValue = HttpUtility.HtmlDecode(familiares.acargo.ToString());
            if (!string.IsNullOrEmpty(familiares.observaciones))
                txtObservaciones.Text = HttpUtility.HtmlDecode(familiares.observaciones.ToString());
            else txtObservaciones.Text = "";

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

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Guardar();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
    }


    protected void btnAdelante2_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombres.Text != "" && txtIdentificacion.Text != "" && txtFechaNacimiento.Text != "")
            Guardar(); 

        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/Conyuge/Nuevo.aspx");
    }

    
}