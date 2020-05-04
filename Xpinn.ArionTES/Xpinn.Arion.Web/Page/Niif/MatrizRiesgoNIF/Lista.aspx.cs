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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;


public partial class Lista : GlobalWeb
{
    Xpinn.NIIF.Services.MatrizRiesgoNIFService MatrizRiesgoNIFServicio = new Xpinn.NIIF.Services.MatrizRiesgoNIFService();
    Xpinn.NIIF.Services.MatrizRiesgoFactorNIFService MatrizRiesgoFactorNIFServicio = new Xpinn.NIIF.Services.MatrizRiesgoFactorNIFService();
    Configuracion conf = new Configuracion();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(MatrizRiesgoNIFServicio.CodigoPrograma.ToString(), "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;

        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(MatrizRiesgoNIFServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboClasificacion();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                Actualizar();
            }
            if (Session[MatrizRiesgoNIFServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[MatrizRiesgoNIFServicio.CodigoPrograma + ".id"].ToString();
                Session.Remove(MatrizRiesgoNIFServicio.CodigoPrograma + ".id");                
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MatrizRiesgoNIFServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para la consulta de los scoreboards
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {     
        Page.Validate();
        ViewState["gvMatrizRiesgoRow"] = null;
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, MatrizRiesgoNIFServicio.CodigoPrograma);
            Actualizar();
            mvMatrizRiesgo.ActiveViewIndex = 0;
        }
    }

    /// <summary>
    /// Método para cuando se va a crear un score board
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        if (mvMatrizRiesgo.ActiveViewIndex == 0) //Guarda solo si esta en el multiview para adicionar nuevo registro
        {
            List<Xpinn.NIIF.Entities.MatrizRiesgoFactorNIF> lstConsulta = new List<Xpinn.NIIF.Entities.MatrizRiesgoFactorNIF>();            
            gvVariables.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvVariables.DataSource = lstConsulta;
            mvMatrizRiesgo.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarNuevo(false);
        }
        
    }

    private void CrearNIIFBoard()
    {
        this.Lblerror.Text = "";
        MatrizRiesgoNIF vMatrizRiesgoNIF = new MatrizRiesgoNIF();
        vMatrizRiesgoNIF.idmatriz = 0;
        vMatrizRiesgoNIF.cod_clasifica = Convert.ToInt64(ddlClasificacionC.SelectedValue);
        vMatrizRiesgoNIF.tipo_persona = Convert.ToString(ddlTipoPersonaC.SelectedValue);
        vMatrizRiesgoNIF.fechacreacion = DateTime.Now;
        Usuario pUsuario = (Usuario)Session["usuario"];
        vMatrizRiesgoNIF.usuariocreacion = pUsuario.codusuario;
        vMatrizRiesgoNIF.fecultmod = DateTime.Now;
        vMatrizRiesgoNIF.usuultmod = pUsuario.codusuario;
        gvMatrizRiesgo.EditIndex = -1;
        MatrizRiesgoNIFServicio.CrearMatrizRiesgoNIF(vMatrizRiesgoNIF, (Usuario)Session["usuario"]);
        lblCodigo.Text = vMatrizRiesgoNIF.idmatriz.ToString();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {        
        if (mvMatrizRiesgo.ActiveViewIndex == 1) //Guarda solo si esta en el multiview para adicionar nuevo registro
        {
            // Actualiza el registro creado
            int conseID = 0;
            if (lblCodigo.Text.Trim() != "")
                conseID = Convert.ToInt32(lblCodigo.Text);
            MatrizRiesgoNIF pMatrizRiesgoNIF = new MatrizRiesgoNIF();
            pMatrizRiesgoNIF.idmatriz = conseID;
            pMatrizRiesgoNIF.cod_clasifica = Convert.ToInt64(ddlClasificacionC.SelectedValue.ToString());
            pMatrizRiesgoNIF.tipo_persona = ddlTipoPersonaC.SelectedValue.ToString();
            pMatrizRiesgoNIF.fecultmod = DateTime.Now;
            Usuario usuario = (Usuario)Session["usuario"];
            pMatrizRiesgoNIF.usuultmod = usuario.codusuario;
            if (conseID == 0)
                CrearNIIFBoard();
            else
                MatrizRiesgoNIFServicio.ModificarMatrizRiesgoNIF(pMatrizRiesgoNIF, (Usuario)Session["usuario"]);
            Actualizar();
            mvMatrizRiesgo.ActiveViewIndex = 0; //Muestra el gridviwe
        }
        else
        {
            mvMatrizRiesgo.ActiveViewIndex = 0;
        }
        
    } 

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    /// <summary>
    /// Método para mostrar los datos de un score board seleccionado para edición
    /// </summary>
    /// <param name="consecutivo"></param>
    /// <param name="gvMatrizRiesgo"></param>
    /// <param name="Var"></param>
    private void MostrarNIIFBoard(String consecutivo, GridView gvMatrizRiesgo, String Var)
    {   
        //Muestra el gridview con sus datos
        MatrizRiesgoNIF pNIIFBoard = new MatrizRiesgoNIF();
        List<MatrizRiesgoNIF> LstNIIFBoard = new List<MatrizRiesgoNIF>();
        LstNIIFBoard = MatrizRiesgoNIFServicio.ListarMatrizRiesgoNIF(ObtenerValoresSb(), (Usuario)Session["usuario"]);
        if (LstNIIFBoard.Count == 0)
            {
                LstNIIFBoard = (List<MatrizRiesgoNIF>)Session["Clasificaciones"];
            }
        gvMatrizRiesgo.DataSource = LstNIIFBoard;
        gvMatrizRiesgo.DataBind();
        Session[Var] = LstNIIFBoard;
    }
   
    /// <summary>
    /// Método para obtener los datos de consulta
    /// </summary>
    /// <returns></returns>
    private Xpinn.NIIF.Entities.MatrizRiesgoNIF ObtenerValoresSb()
    {
        Xpinn.NIIF.Entities.MatrizRiesgoNIF vMatrizRiesgoNIF = new Xpinn.NIIF.Entities.MatrizRiesgoNIF();

        vMatrizRiesgoNIF.filtro = "";

        if (ddlClasificacionf.SelectedValue != "0" && ddlClasificacionf.SelectedValue != "")
        {
            vMatrizRiesgoNIF.filtro = " and sc.COD_CLASIFICA =" + ddlClasificacionf.SelectedValue;
        }

        if (ddlTipoPersonaf.SelectedValue != "0")
        {
            vMatrizRiesgoNIF.filtro = vMatrizRiesgoNIF.filtro + " and sc.tipo_persona = '" + ddlTipoPersonaf.SelectedValue + "' ";
        }

        if (ViewState["gvMatrizRiesgoRow"] != null)
        {
            vMatrizRiesgoNIF.filtro = vMatrizRiesgoNIF.filtro + " and sc.idmatriz = " + ViewState["gvMatrizRiesgoRow"];
            
        }

        return vMatrizRiesgoNIF;
    }

    public void Actualizar()
    {
        try
        {
            List<Xpinn.NIIF.Entities.MatrizRiesgoNIF> lstConsulta = new List<Xpinn.NIIF.Entities.MatrizRiesgoNIF>();
            lstConsulta = MatrizRiesgoNIFServicio.ListarMatrizRiesgoNIF(ObtenerValoresSb(), (Usuario)Session["usuario"]);
            gvMatrizRiesgo.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvMatrizRiesgo.DataSource = lstConsulta;

            if (lstConsulta.Count > 0 && ViewState["gvMatrizRiesgoRow"] == null)
            {
                gvMatrizRiesgo.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvMatrizRiesgo.DataBind();
            }
            else if (lstConsulta.Count > 0 && ViewState["gvMatrizRiesgoRow"] != null) //Esta condicion se cumple solo cuando se oprime el boton de seleccion del gridview
            {    
                //Selecciona los indices segun valores del gridview
                lblCodigo.Text = lstConsulta[0].idmatriz.ToString();
                ddlClasificacionC.SelectedValue = lstConsulta[0].cod_clasifica.ToString();
                ddlTipoPersonaC.SelectedValue = lstConsulta[0].tipo_persona.ToString();

                // Items en modo lectura
                ddlClasificacionC.Enabled = false;
                ddlTipoPersonaC.Enabled = false;
                gvVariables.Enabled = false;

                //Muestra las calificaciones y variables propias del NIIF seleccionado                
                ActualizarVar();
                mvMatrizRiesgo.ActiveViewIndex = 1; // Habilita el segundo view                
            }
            else
            {
                gvMatrizRiesgo.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;               
            }

            Session.Add(MatrizRiesgoNIFServicio.CodigoPrograma + ".consulta", 1);
                  
            gvMatrizRiesgo.EditIndex = -1;
            gvMatrizRiesgo.DataBind();

        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(MatrizRiesgoNIFServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
   

    /// <summary>
    /// Método para llenar la lista de clasificaciones
    /// </summary>
    protected void LlenarComboClasificacion()
    {
        Clasificacion vClasificacion = new Clasificacion();
        List<Clasificacion> LstClasificacion = new List<Clasificacion>();
        LstClasificacion = MatrizRiesgoNIFServicio.ListarClasificacion(vClasificacion, (Usuario)Session["Usuario"]);
        vClasificacion.Codigo = 0;
        vClasificacion.Nombre = "";
        LstClasificacion.Add(vClasificacion);

        //Lista para adicionar nuevo registro
        ddlClasificacionC.DataSource = LstClasificacion;
        ddlClasificacionC.DataTextField = "Nombre";
        ddlClasificacionC.DataValueField = "Codigo";
        ddlClasificacionC.DataBind();
        ddlClasificacionC.SelectedValue = "0";

        //Lista para realizar filtro
        ddlClasificacionf.DataSource = LstClasificacion;
        ddlClasificacionf.DataTextField = "Nombre";
        ddlClasificacionf.DataValueField = "codigo";
        ddlClasificacionf.DataBind();
        ddlClasificacionf.SelectedValue = "0";        
    }

    private void QuitarFilaInicialScoreBoard()
    {
        try
        {
            int conseID = Convert.ToInt32(gvMatrizRiesgo.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvMatrizRiesgo.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvMatrizRiesgo.Rows[0].Cells[1].Visible = false;
                this.gvMatrizRiesgo.Rows[0].Cells[2].Visible = false;
                this.gvMatrizRiesgo.Rows[0].Cells[3].Visible = false;
                this.gvMatrizRiesgo.Rows[0].Cells[4].Visible = false;
            }
        }
        catch
        {
        }

    }
    
    protected void gvMatrizRiesgo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            List<Clasificacion> LstClasificacion;
            DropDownList ddlClasificacion = (DropDownList)e.Row.FindControl("ddlClasificacion");
            if (ddlClasificacion != null)
            {
                LstClasificacion = this.ListaClasificacion();
                ddlClasificacion.DataSource = LstClasificacion;
                ddlClasificacion.DataTextField = "Nombre";
                ddlClasificacion.DataValueField = "Codigo";
                ddlClasificacion.SelectedValue = "";
            }
        }
    }

    private void ConsultarNIIFBoard()
    {
        MostrarNIIFBoard("consecutivo", gvMatrizRiesgo, "Lineas");
    }

    protected void gvMatrizRiesgo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Colocar los items en modo edicion
        ddlClasificacionC.Enabled = true;
        ddlTipoPersonaC.Enabled = true;
        gvVariables.Enabled = true;
        
        // Determinar la id del item que se esta modificando
        int conseID = Convert.ToInt32(gvMatrizRiesgo.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvMatrizRiesgo.EditIndex = e.NewEditIndex;
            this.ConsultarNIIFBoard();
        }

        //Obtiene los componentes que se habilitan en modo edicion en el gridview
        lblCodigo.Text = Convert.ToString(conseID);
        Label lblCOD_CLASIFICA = (Label)gvMatrizRiesgo.Rows[e.NewEditIndex].FindControl("lblCOD_CLASIFICA");
        if (lblCOD_CLASIFICA != null)
            ddlClasificacionC.SelectedValue = lblCOD_CLASIFICA.Text;
        Label lblTIPO_PERSONA = (Label)gvMatrizRiesgo.Rows[e.NewEditIndex].FindControl("lblTIPO_PERSONA");
        if (lblTIPO_PERSONA != null)
            ddlTipoPersonaC.SelectedValue = lblTIPO_PERSONA.Text;
        ViewState["gvMatrizRiesgoRow"] = Convert.ToString(conseID);
        ActualizarVar();

        mvMatrizRiesgo.ActiveViewIndex = 1; // Habilita el segundo view para editar el registro seleccionado      
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
    }
 
    protected void gvMatrizRiesgo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {        
        long ID = Convert.ToInt32(gvMatrizRiesgo.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            AsignarEventoConfirmar();
            MatrizRiesgoNIFServicio.EliminarMatrizRiesgoNIF(ID, (Usuario)Session["usuario"]);
            Actualizar();            
        }
    }   

    protected List<Clasificacion> ListaClasificacion()
    {
        Clasificacion vClasificacion = new Clasificacion();
        List<Clasificacion> LstClasificacion = new List<Clasificacion>();
        Usuario usuario = new Usuario();
        LstClasificacion = MatrizRiesgoNIFServicio.ListarClasificacion(vClasificacion, usuario);
        LstClasificacion.Add(vClasificacion);
        return LstClasificacion;
    }

    

    //-------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------   Variables   --------------------------------------------
    //-------------------------------------------------------------------------------------------------------------

    protected void gvVariables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvVariables.EditIndex = -1;
        long conseID = Convert.ToInt32(gvVariables.DataKeys[e.RowIndex].Values[0].ToString());

        String consecutivo = conseID.ToString();


        MostrarVariables(consecutivo, gvVariables, "Lineas");
        ActualizarVar();
        
    }

    protected void gvVariables_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            DropDownList ddlParametroF = (DropDownList)gvVariables.FooterRow.FindControl("ddlParametroF");
            TextBox txtMinimoF = (TextBox)gvVariables.FooterRow.FindControl("txtMinimoF");
            TextBox txtMaximoF = (TextBox)gvVariables.FooterRow.FindControl("txtMaximoF");
            TextBox txtFactorF = (TextBox)gvVariables.FooterRow.FindControl("txtFactorF");
            TextBox txtVariableF = (TextBox)gvVariables.FooterRow.FindControl("txtVariableF");
            TextBox txtCalificacionF = (TextBox)gvVariables.FooterRow.FindControl("txtCalificacionF");
            if (ddlParametroF.SelectedItem.Text == "")
            {
                String Error = "Por favor diligenciar los datos";
                this.Lblerror.Text = Error;
                QuitarFilaInicialScoreBoard();
            }
            else
            {                
                this.Lblerror.Text = "";
                MatrizRiesgoFactorNIF vMatrizRiesgoFactorNIF = new MatrizRiesgoFactorNIF();
                vMatrizRiesgoFactorNIF.idfactorpondera = -1;
                vMatrizRiesgoFactorNIF.idmatriz = lblCodigo.Text != "" ? Convert.ToInt32(lblCodigo.Text) : 0;
                vMatrizRiesgoFactorNIF.idparametro = Convert.ToInt32(ddlParametroF.SelectedValue);
                vMatrizRiesgoFactorNIF.minimo = txtMinimoF.Text;
                vMatrizRiesgoFactorNIF.maximo = txtMaximoF.Text;
                vMatrizRiesgoFactorNIF.factor = txtFactorF.Text.Trim() != "" ? Convert.ToDecimal(txtFactorF.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
                vMatrizRiesgoFactorNIF.variable = txtVariableF.Text;
                vMatrizRiesgoFactorNIF.calificacion = txtCalificacionF.Text.Trim() != "" ? Convert.ToDecimal(txtCalificacionF.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
                gvVariables.EditIndex = -1;
                MatrizRiesgoFactorNIFServicio.CrearMatrizRiesgoFactorNIF(vMatrizRiesgoFactorNIF, (Usuario)Session["usuario"]);
                ActualizarVar();
            }

        }
    }
    protected void gvVariables_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox ctrl = (TextBox)e.Row.FindControl("txtCalificacionE");
            if (ctrl != null)
            {
                ctrl.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
            TextBox lctrl = (TextBox)e.Row.FindControl("lblCalificacion");
            if (lctrl != null)
            {
                lctrl.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox ctrl = (TextBox)e.Row.FindControl("txtCalificacionF");
            if (ctrl != null)
            {
                ctrl.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
    }

    protected void gvVariables_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvVariables.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvVariables.EditIndex = e.NewEditIndex;
            this.ConsultarVariables();
        }      
    }

    private void ConsultarVariables()
    {
        MostrarVariables("consecutivo", gvVariables, "Lineas");
    }

    protected void gvVariables_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvVariables.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            AsignarEventoConfirmar();
            MatrizRiesgoFactorNIFServicio.EliminarMatrizRiesgoFactorNIF(ID, (Usuario)Session["usuario"]);
            ActualizarVar();
        }
    }

    protected void gvVariables_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Guarda (Modifica) los valores que han sido editados desde el gridview
        long conseID = Convert.ToInt32(gvVariables.DataKeys[e.RowIndex].Values[0].ToString());
        DropDownList ddlParametroE = (DropDownList)gvVariables.Rows[e.RowIndex].FindControl("ddlParametro");
        TextBox txtMinimoE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtMinimoE");
        TextBox txtMaximoE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtMaximoE");
        TextBox txtFactorE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtFactorE");
        TextBox txtVariableE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtVariableE");
        TextBox txtCalificacionE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtCalificacionE");

        MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF = new MatrizRiesgoFactorNIF();
        pMatrizRiesgoFactorNIF.idfactorpondera = conseID;
        pMatrizRiesgoFactorNIF.idmatriz = Convert.ToInt64(lblCodigo.Text);
        pMatrizRiesgoFactorNIF.idparametro = Convert.ToInt64(ddlParametroE.SelectedValue);
        pMatrizRiesgoFactorNIF.minimo = txtMinimoE.Text;
        pMatrizRiesgoFactorNIF.maximo = txtMaximoE.Text;
        pMatrizRiesgoFactorNIF.factor = txtFactorE.Text.Trim() != "" ? Convert.ToDecimal(txtFactorE.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
        pMatrizRiesgoFactorNIF.variable = txtVariableE.Text;
        pMatrizRiesgoFactorNIF.calificacion = txtCalificacionE.Text.Trim() != "" ? Convert.ToDecimal(txtCalificacionE.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;        

        gvVariables.EditIndex = -1;
        MatrizRiesgoFactorNIFServicio.ModificarMatrizRiesgoFactorNIF(pMatrizRiesgoFactorNIF, (Usuario)Session["usuario"]);
        ActualizarVar();
    }

    private void MostrarVariables(String consecutivo, GridView gvVariables, String Var)
    {
        //Muestra el gridview con sus datos
        MatrizRiesgoFactorNIF pNIIFBoard = new MatrizRiesgoFactorNIF();
        List<MatrizRiesgoFactorNIF> LstNIIFBoard = new List<MatrizRiesgoFactorNIF>();
        LstNIIFBoard = MatrizRiesgoFactorNIFServicio.ListarMatrizRiesgoFactorNIF(ObtenerValoresSbVar(), (Usuario)Session["usuario"]);
        gvVariables.DataSource = LstNIIFBoard;
        gvVariables.DataBind();
        Session[Var] = LstNIIFBoard;
    }

    protected List<Parametro> ListaParametros()
    {
        ParametroService ParametroServicio = new ParametroService();
        Parametro eParametro = new Parametro();
        return ParametroServicio.ListarParametro(eParametro, (Usuario)Session["Usuario"]);
    }

    
    public void ActualizarVar()
    {
        try
        {
            List<Xpinn.NIIF.Entities.MatrizRiesgoFactorNIF> lstConsulta = new List<Xpinn.NIIF.Entities.MatrizRiesgoFactorNIF>();
            lstConsulta = MatrizRiesgoFactorNIFServicio.ListarMatrizRiesgoFactorNIF(ObtenerValoresSbVar(), (Usuario)Session["usuario"]);
            gvVariables.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvVariables.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvVariables.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvVariables.DataBind();
                //ValidarPermisosGrilla(gvVariables);
            }
            else
            {
                lblTotalRegs.Text = "<br/> Registros encontrados: 0 ";
                MatrizRiesgoFactorNIF vMatrizRiesgoFactorNIF = new MatrizRiesgoFactorNIF();
                vMatrizRiesgoFactorNIF.idfactorpondera = 0;
                vMatrizRiesgoFactorNIF.idmatriz = 0;
                vMatrizRiesgoFactorNIF.idparametro = 0;
                vMatrizRiesgoFactorNIF.minimo = "0";
                vMatrizRiesgoFactorNIF.maximo = "0";
                vMatrizRiesgoFactorNIF.factor = 0;
                vMatrizRiesgoFactorNIF.variable = "";
                vMatrizRiesgoFactorNIF.calificacion = 0;

                lstConsulta.Add(vMatrizRiesgoFactorNIF);
                gvVariables.DataBind();
                gvVariables.Rows[0].Visible = false;
            }

            Session.Add(MatrizRiesgoFactorNIFServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(MatrizRiesgoFactorNIFServicio.CodigoPrograma, "ActualizarVar", ex);
        }
    }

    private Xpinn.NIIF.Entities.MatrizRiesgoFactorNIF ObtenerValoresSbVar()
    {
        Xpinn.NIIF.Entities.MatrizRiesgoFactorNIF programa = new Xpinn.NIIF.Entities.MatrizRiesgoFactorNIF();
        if (ViewState["gvMatrizRiesgoRow"] == null)
            return programa;
        programa.idmatriz = Convert.ToInt32(ViewState["gvMatrizRiesgoRow"].ToString());
        return programa;
    }

   
    protected void gvMatrizRiesgo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }

    protected void gvMatrizRiesgo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["gvMatrizRiesgoRow"] = gvMatrizRiesgo.SelectedDataKey.Value.ToString();
        Actualizar();
    }
            
}
