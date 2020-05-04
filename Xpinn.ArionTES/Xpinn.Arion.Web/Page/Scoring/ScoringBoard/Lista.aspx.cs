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

using Xpinn.Scoring.Services;
using Xpinn.Scoring.Entities;

using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;


public partial class Lista : GlobalWeb
{
    Xpinn.Scoring.Services.ScScoringBoardService ScScoringBoardServicio = new Xpinn.Scoring.Services.ScScoringBoardService();
    Xpinn.Scoring.Services.ScScoringBoardVarService ScScoringBoardVarServicio = new Xpinn.Scoring.Services.ScScoringBoardVarService();
    Xpinn.Scoring.Services.ScScoringBoardCalService ScScoringBoardCalServicio = new Xpinn.Scoring.Services.ScScoringBoardCalService();
    Configuracion conf = new Configuracion();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ScScoringBoardServicio.CodigoPrograma.ToString(), "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(ScScoringBoardServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                LlenarComboLineas();
                LlenarComboClasificacion();
                Actualizar();
            }
            if (Session[ScScoringBoardServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[ScScoringBoardServicio.CodigoPrograma + ".id"].ToString();
                Session.Remove(ScScoringBoardServicio.CodigoPrograma + ".id");                
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScScoringBoardServicio.GetType().Name + "A", "Page_Load", ex);
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
        ViewState["gvScoringRow"] = null;
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ScScoringBoardServicio.CodigoPrograma);
            Actualizar();
            mvScoringBoard.ActiveViewIndex = 0;
        }
    }

    /// <summary>
    /// Método para cuando se va a crear un score board
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        if (mvScoringBoard.ActiveViewIndex == 0) //Guarda solo si esta en el multiview para adicionar nuevo registro
        {
            ActualizarCal();
            ActualizarVar();
            mvScoringBoard.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        
    }

    private void CrearScoringBoard()
    {
        this.Lblerror.Text = "";
        ScScoringBoard vScScoringBoard = new ScScoringBoard();
        vScScoringBoard.idscore = 0;
        vScScoringBoard.cod_clasifica = Convert.ToInt64(ddlClasificacionC.SelectedValue);
        vScScoringBoard.cod_linea_credito = Convert.ToString(ddlLineasC.SelectedValue);
        vScScoringBoard.aplica_a = Convert.ToInt64(ddlAPLICA_AC.SelectedIndex);
        vScScoringBoard.modelo = Convert.ToInt64(ddlMODELOC.SelectedIndex);
        try
        {
            if (txtBeta0.Text != "")
                vScScoringBoard.beta0 = Convert.ToDecimal(txtBeta0.Text);
            else
                vScScoringBoard.beta0 = 0;
        }
        catch
        {
            VerError("Error al convertir el Beta 0");
        }
        try
        {
            if (txtScoreMaximo.Text != "")
                vScScoringBoard.score_maximo = Convert.ToDecimal(txtScoreMaximo.Text);
            else
                vScScoringBoard.score_maximo = 0;
        }
        catch
        {
            VerError("Error al convertir el Beta 0");
        }
        vScScoringBoard.fechacreacion = DateTime.Now;
        Usuario pUsuario = (Usuario)Session["usuario"];
        vScScoringBoard.usuariocreacion = pUsuario.nombre;
        vScScoringBoard.fecultmod = DateTime.Now;
        vScScoringBoard.usuultmod = pUsuario.nombre;
        gvScoring.EditIndex = -1;
        ScScoringBoardServicio.CrearScScoringBoard(vScScoringBoard, (Usuario)Session["usuario"]);
        lblCodigo.Text = vScScoringBoard.idscore.ToString();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {        
        if (mvScoringBoard.ActiveViewIndex == 1) //Guarda solo si esta en el multiview para adicionar nuevo registro
        {
            // Actualiza el registro creado
            long conseID = 0;
            if (lblCodigo.Text.Trim() != "")
                conseID = Convert.ToInt32(lblCodigo.Text);
            ScScoringBoard pScScoringBoard = new ScScoringBoard();
            pScScoringBoard.idscore = conseID;
            pScScoringBoard.cod_clasifica = Convert.ToInt64(ddlClasificacionC.SelectedValue.ToString());
            pScScoringBoard.cod_linea_credito = ddlLineasC.SelectedValue.ToString();
            pScScoringBoard.aplica_a = Convert.ToInt64(ddlAPLICA_AC.SelectedIndex.ToString());
            pScScoringBoard.modelo = Convert.ToInt64(ddlMODELOC.SelectedIndex.ToString());
            pScScoringBoard.fecultmod = DateTime.Now;
            pScScoringBoard.beta0 = txtBeta0.Text.Trim() != "" ? Convert.ToDecimal(txtBeta0.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
            pScScoringBoard.score_maximo = txtScoreMaximo.Text.Trim() != "" ? Convert.ToDecimal(txtScoreMaximo.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
            pScScoringBoard.clase = Convert.ToInt64(ddlCLASESCORE.SelectedIndex.ToString());
            Usuario usuario = (Usuario)Session["usuario"];
            pScScoringBoard.usuultmod = usuario.nombre;
            if (conseID == 0)
                CrearScoringBoard();
            else
                ScScoringBoardServicio.ModificarScScoringBoard(pScScoringBoard, (Usuario)Session["usuario"]);
            Actualizar();
            mvScoringBoard.ActiveViewIndex = 0; //Muestra el gridviwe
        }
        else
        {
            mvScoringBoard.ActiveViewIndex = 0;
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
    /// <param name="gvScoring"></param>
    /// <param name="Var"></param>
    private void MostrarScoringBoard(String consecutivo, GridView gvScoring, String Var)
    {   
        //Muestra el gridview con sus datos
        ScScoringBoard pScoringBoard = new ScScoringBoard();
        List<ScScoringBoard> LstScoringBoard = new List<ScScoringBoard>();
        LstScoringBoard = ScScoringBoardServicio.ListarScScoringBoard(ObtenerValoresSb(), (Usuario)Session["usuario"]);
        if (LstScoringBoard.Count == 0)
            {
                LstScoringBoard = (List<ScScoringBoard>)Session["Clasificaciones"];
            }
        gvScoring.DataSource = LstScoringBoard;
        gvScoring.DataBind();
        Session[Var] = LstScoringBoard;
    }
   
    /// <summary>
    /// Método para obtener los datos de consulta
    /// </summary>
    /// <returns></returns>
    private Xpinn.Scoring.Entities.ScScoringBoard ObtenerValoresSb()
    {
        Xpinn.Scoring.Entities.ScScoringBoard vScScoringBoard = new Xpinn.Scoring.Entities.ScScoringBoard();

        vScScoringBoard.filtro = "";

        if (ddlClasificacionf.SelectedValue != "0")
        {
            vScScoringBoard.filtro = " and sc.COD_CLASIFICA =" + ddlClasificacionf.SelectedValue;
        }

        if (ddlLineasf.SelectedValue != "0")
        {
            vScScoringBoard.filtro = vScScoringBoard.filtro + " and sc.cod_linea_credito =" + ddlLineasf.SelectedValue;
        }

        if (ViewState["gvScoringRow"] != null)
        {
            vScScoringBoard.filtro = vScScoringBoard.filtro + " and sc.idscore =" + ViewState["gvScoringRow"];
            
        }

        return vScScoringBoard;
    }

    public void Actualizar()
    {
        try
        {
            List<Xpinn.Scoring.Entities.ScScoringBoard> lstConsulta = new List<Xpinn.Scoring.Entities.ScScoringBoard>();
            lstConsulta = ScScoringBoardServicio.ListarScScoringBoard(ObtenerValoresSb(), (Usuario)Session["usuario"]);
            gvScoring.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvScoring.DataSource = lstConsulta;

            if (lstConsulta.Count > 0 && ViewState["gvScoringRow"] == null)
            {
                gvScoring.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvScoring.DataBind();
            }
            else if (lstConsulta.Count > 0 && ViewState["gvScoringRow"] != null) //Esta condicion se cumple solo cuando se oprime el boton de seleccion del gridview
            {    
                //Selecciona los indices segun valores del gridview
                lblCodigo.Text = lstConsulta[0].idscore.ToString();
                ddlClasificacionC.SelectedValue = lstConsulta[0].cod_clasifica.ToString();             
                ddlLineasC.SelectedValue = lstConsulta[0].cod_linea_credito.ToString();
                ddlAPLICA_AC.SelectedIndex = Convert.ToInt32(lstConsulta[0].aplica_a.ToString());
                ddlMODELOC.SelectedIndex = Convert.ToInt32(lstConsulta[0].modelo.ToString());
                txtBeta0.Text = lstConsulta[0].beta0.ToString();
                txtScoreMaximo.Text = lstConsulta[0].score_maximo.ToString();

                // Items en modo lectura
                ddlClasificacionC.Enabled = false;
                ddlLineasC.Enabled = false;
                ddlAPLICA_AC.Enabled = false;
                ddlMODELOC.Enabled = false;
                gvVariables.Enabled = false;
                gvCalificaciones.Enabled = false;
                txtBeta0.Enabled = false;
                txtScoreMaximo.Enabled = false;

                //Muestra las calificaciones y variables propias del scoring seleccionado
                ActualizarCal();
                ActualizarVar();
                mvScoringBoard.ActiveViewIndex = 1; // Habilita el segundo view                
            }
            else
            {
                gvScoring.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;               
            }

            Session.Add(ScScoringBoardServicio.CodigoPrograma + ".consulta", 1);
                  
            gvScoring.EditIndex = -1;
            gvScoring.DataBind();

        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(ScScoringBoardServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
   
    protected void LlenarComboLineas()
    {
        Lineas vLineas = new Lineas();
        List<Lineas> LstLineas = new List<Lineas>();
        LstLineas = ScScoringBoardServicio.ListarLineas(vLineas, (Usuario)Session["Usuario"]);
        vLineas.Codigo = "0";
        vLineas.Nombre = "";
        LstLineas.Add(vLineas);
        Lineas vLineas1 = new Lineas();
        vLineas1.Codigo = "";
        vLineas1.Nombre = "";
        LstLineas.Add(vLineas1);

        //Lista para adicionar nuevo registro
        ddlLineasC.DataSource = LstLineas;
        ddlLineasC.DataTextField = "Nombre";
        ddlLineasC.DataValueField = "codigo";
        ddlLineasC.DataBind();
        ddlLineasC.SelectedValue = "0";

        //Lista para realizar filtro
        ddlLineasf.DataSource = LstLineas;
        ddlLineasf.DataTextField = "Nombre";
        ddlLineasf.DataValueField = "codigo";
        ddlLineasf.DataBind();
        ddlLineasf.SelectedValue = "0";       

    }

    /// <summary>
    /// Método para llenar la lista de clasificaciones
    /// </summary>
    protected void LlenarComboClasificacion()
    {
        Clasificacion vClasificacion = new Clasificacion();
        List<Clasificacion> LstClasificacion = new List<Clasificacion>();
        LstClasificacion = ScScoringBoardServicio.ListarClasificacion(vClasificacion, (Usuario)Session["Usuario"]);
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
            int conseID = Convert.ToInt32(gvScoring.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvScoring.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvScoring.Rows[0].Cells[1].Visible = false;
                this.gvScoring.Rows[0].Cells[2].Visible = false;
                this.gvScoring.Rows[0].Cells[3].Visible = false;
                this.gvScoring.Rows[0].Cells[4].Visible = false;
            }
        }
        catch
        {
        }

    }
    
    protected void gvScoring_RowDataBound(object sender, GridViewRowEventArgs e)
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

            List<Lineas> LstLineas;
            DropDownList ddlLineas = (DropDownList)e.Row.FindControl("ddlLineas");
            if (ddlLineas != null)
            {
                LstLineas = this.ListaLineas();
                ddlLineas.DataSource = LstLineas;
                ddlLineas.DataTextField = "Nombre";
                ddlLineas.DataValueField = "Codigo";
                ddlLineas.SelectedValue = "";
            }

            List<Lineas> LstAplica;
            DropDownList ddlAplica = (DropDownList)e.Row.FindControl("ddlAplica");
            if (ddlAplica != null)
            {
                LstAplica = this.ListaLineas();
                ddlAplica.DataSource = LstAplica;
                ddlAplica.DataTextField = "Nombre";
                ddlAplica.DataValueField = "Codigo";
                ddlAplica.SelectedValue = "";
            }

            // Al actualizar el gridview, cambia el codigo de la columna "Aplica_A" por la respectiva descripcion
            Label lblAPLICA_A = (Label)e.Row.FindControl("lblAPLICA_A");  
            if (lblAPLICA_A != null)
            {               
                switch (lblAPLICA_A.Text)
                   {
                      case "0":
                         lblAPLICA_A.Text = "";
                         break;
                      case "1":
                         lblAPLICA_A.Text = "Ambos";
                         break;
                      case "2":
                         lblAPLICA_A.Text = "Deudor";
                         break;
                      case "3":
                         lblAPLICA_A.Text = "Codeudor";
                         break;
                   }
            }

            // Al actualizar el gridview, cambia el codigo de la columna "Modelo" por la respectiva descripcion
            Label lblMODELO = (Label)e.Row.FindControl("lblMODELO");
            if (lblMODELO != null)
            {
                switch (lblMODELO.Text)
                {
                    case "0":
                        lblMODELO.Text = "";
                        break;
                    case "1":
                        lblMODELO.Text = "Credit Scoring";
                        break;
                    case "2":
                        lblMODELO.Text = "Regresión Logística";
                        break;
                }
            }

            Control ctrl = e.Row.FindControl("lblCLASE");
            if (ctrl != null)
            {
                Label lblCLASE = ctrl as Label;
                if (lblCLASE.Text == "0")
                    lblCLASE.Text = "Sin Clase";
                if (lblCLASE.Text == "1")
                    lblCLASE.Text = "De Aprobación";
                if (lblCLASE.Text == "2")
                    lblCLASE.Text = "De Seguimiento";

            }

        }
    }

    private void ConsultarScoringBoard()
    {
        MostrarScoringBoard("consecutivo", gvScoring, "Lineas");
    }

    protected void gvScoring_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Colocar los items en modo edicion
        ddlClasificacionC.Enabled = true;
        ddlLineasC.Enabled = true;
        ddlAPLICA_AC.Enabled = true;
        ddlMODELOC.Enabled = true;
        gvVariables.Enabled = true;
        gvCalificaciones.Enabled = true;
        txtBeta0.Enabled = true;
        
        // Determinar la id del item que se esta modificando
        int conseID = Convert.ToInt32(gvScoring.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvScoring.EditIndex = e.NewEditIndex;
            this.ConsultarScoringBoard();
        }

        //Obtiene los componentes que se habilitan en modo edicion en el gridview
        lblCodigo.Text = Convert.ToString(conseID);
        Label lblCOD_CLASIFICA = (Label)gvScoring.Rows[e.NewEditIndex].FindControl("lblCOD_CLASIFICA");
        if (lblCOD_CLASIFICA != null)
            ddlClasificacionC.SelectedValue = lblCOD_CLASIFICA.Text;
        Label lblCOD_LINEA_CREDITO = (Label)gvScoring.Rows[e.NewEditIndex].FindControl("lblCOD_LINEA_CREDITO");
        if (lblCOD_LINEA_CREDITO != null)
            ddlLineasC.SelectedValue = lblCOD_LINEA_CREDITO.Text;
        Label lblAPLICA_A = (Label)gvScoring.Rows[e.NewEditIndex].FindControl("lblAPLICA_A");
        if (lblAPLICA_A != null)
        {
            if (lblAPLICA_A.Text == "Ambos")
                ddlAPLICA_AC.SelectedIndex = 1;
            if (lblAPLICA_A.Text == "Deudor")
                ddlAPLICA_AC.SelectedIndex = 2;
            if (lblAPLICA_A.Text == "Codeudor")
                ddlAPLICA_AC.SelectedIndex = 3;
        }
        Label lblMODELO = (Label)gvScoring.Rows[e.NewEditIndex].FindControl("lblMODELO");
        if (lblMODELO != null)
        {
            if (lblMODELO.Text == "Credit Scoring")
                ddlMODELOC.SelectedIndex = 1;
            if (lblMODELO.Text == "Regresión Logística")
                ddlMODELOC.SelectedIndex = 2;
        }
        Label lblBETA0 = (Label)gvScoring.Rows[e.NewEditIndex].FindControl("lblBETA0");
        txtBeta0.Text = lblBETA0.Text;

        Label lblSCORE = (Label)gvScoring.Rows[e.NewEditIndex].FindControl("lblSCORE");
        txtScoreMaximo.Text = lblSCORE.Text;

        Label lblCLASE = (Label)gvScoring.Rows[e.NewEditIndex].FindControl("lblCLASE");
        if (lblCLASE != null)
        {
            if (lblCLASE.Text == "Sin Clase")
                ddlCLASESCORE.SelectedIndex = 0;
            if (lblCLASE.Text == "De Aprobación")
                ddlCLASESCORE.SelectedIndex = 1;
            if (lblCLASE.Text == "De Seguimiento")
                ddlCLASESCORE.SelectedIndex = 2;
        }

        ViewState["gvScoringRow"] = Convert.ToString(conseID);
        ActualizarCal();
        ActualizarVar();

        mvScoringBoard.ActiveViewIndex = 1; // Habilita el segundo view para editar el registro seleccionado      
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
    }
 
    protected void gvScoring_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {        
        long ID = Convert.ToInt32(gvScoring.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            AsignarEventoConfirmar();
            ScScoringBoardServicio.EliminarScScoringBoard(ID, (Usuario)Session["usuario"]);
            Actualizar();            
        }
    }   

    protected List<Clasificacion> ListaClasificacion()
    {
        Clasificacion vClasificacion = new Clasificacion();
        List<Clasificacion> LstClasificacion = new List<Clasificacion>();
        Usuario usuario = new Usuario();
        LstClasificacion = ScScoringBoardServicio.ListarClasificacion(vClasificacion, usuario);
        LstClasificacion.Add(vClasificacion);
        return LstClasificacion;
    }

    protected List<Lineas> ListaLineas()
    {
        Lineas vLineas = new Lineas();
        List<Lineas> LstLineas = new List<Lineas>();
        Usuario usuario = new Usuario();
        LstLineas = ScScoringBoardServicio.ListarLineas(vLineas, usuario);
        LstLineas.Add(vLineas);
        return LstLineas;
    }

    protected List<Modelo> ListaAplica()
    {
        Modelo aplica;
        List<Modelo> LsAplica = new List<Modelo>();
        aplica = new Modelo();
        aplica.Codigo = 0;
        aplica.Nombre = "";
        LsAplica.Add(aplica);
        aplica = new Modelo();
        aplica.Codigo = 1;
        aplica.Nombre = "Ambos";
        LsAplica.Add(aplica);
        aplica = new Modelo();
        aplica.Codigo = 2;
        aplica.Nombre = "Deudor";
        LsAplica.Add(aplica);
        aplica = new Modelo();
        aplica.Codigo = 3;
        aplica.Nombre = "Codeudor";
        LsAplica.Add(aplica);
        return LsAplica;
    }

    protected List<Modelo> ListaModelo()
    {
        Modelo vModelo;
        List<Modelo> LstModelo = new List<Modelo>();
        vModelo = new Modelo();
        vModelo.Codigo = 0;
        vModelo.Nombre = string.Empty;
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 1;
        vModelo.Nombre = "Credit Scoring";
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 2;
        vModelo.Nombre = "Regresión Logística";
        LstModelo.Add(vModelo);
        return LstModelo;
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
        VerError("");

        if (e.CommandName.Equals("AddNew"))
        {
            if (lblCodigo.Text.Trim() == "")
            {
                VerError("Debe primero guardar la hoja de evaluación");
                return;
            }

            DropDownList ddlVariableF = (DropDownList)gvVariables.FooterRow.FindControl("ddlVariableF");
            TextBox txtMinimoF = (TextBox)gvVariables.FooterRow.FindControl("txtMinimoF");
            TextBox txtMaximoF = (TextBox)gvVariables.FooterRow.FindControl("txtMaximoF");
            TextBox txtValorF = (TextBox)gvVariables.FooterRow.FindControl("txtValorF");
            TextBox txtBetaF = (TextBox)gvVariables.FooterRow.FindControl("txtBetaF");

            if (ddlVariableF.SelectedItem.Text == "")
            {
                String Error = "Por favor diligenciar los datos";
                this.Lblerror.Text = Error;
                QuitarFilaInicialScoreBoard();
            }
            else
            {                
                this.Lblerror.Text = "";
                ScScoringBoardVar vScScoringBoardVar = new ScScoringBoardVar();
                vScScoringBoardVar.idscorevar = -1;
                vScScoringBoardVar.idscore = Convert.ToInt64(lblCodigo.Text);
                vScScoringBoardVar.idparametro = Convert.ToInt64(ddlVariableF.SelectedValue);
                vScScoringBoardVar.minimo = txtMinimoF.Text.Trim() != "" ? Convert.ToDecimal(txtMinimoF.Text) : 0;
                vScScoringBoardVar.maximo = txtMaximoF.Text.Trim() != "" ? Convert.ToDecimal(txtMaximoF.Text) : 0;
                vScScoringBoardVar.valor = txtValorF.Text.Trim() != "" ? Convert.ToInt64(txtValorF.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
                vScScoringBoardVar.beta = txtBetaF.Text.Trim() != "" ? Convert.ToDecimal(txtBetaF.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
                gvVariables.EditIndex = -1;
                ScScoringBoardVarServicio.CrearScScoringBoardVar(vScScoringBoardVar, (Usuario)Session["usuario"]);
                ActualizarVar();
            }

        }
    }

    protected void gvVariables_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox ctrl = (TextBox)e.Row.FindControl("txtBetaE");
            if (ctrl != null)
            {
                ctrl.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
            TextBox lctrl = (TextBox)e.Row.FindControl("lblBeta");
            if (lctrl != null)
            {
                lctrl.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox ctrl = (TextBox)e.Row.FindControl("txtBetaF");
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
            ScScoringBoardVarServicio.EliminarScScoringBoardVar(ID, (Usuario)Session["usuario"]);
            ActualizarVar();
        }
    }

    protected void gvVariables_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Guarda (Modifica) los valores que han sido editados desde el gridview
        long conseID = Convert.ToInt32(gvVariables.DataKeys[e.RowIndex].Values[0].ToString());
        DropDownList ddlVariable = (DropDownList)gvVariables.Rows[e.RowIndex].FindControl("ddlVariable");
        TextBox txtMinimoE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtMinimoE");
        TextBox txtMaximoE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtMaximoE");
        TextBox txtValorE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtValorE");
        TextBox txtBetaE = (TextBox)gvVariables.Rows[e.RowIndex].FindControl("txtBetaE");

        ScScoringBoardVar pScScoringBoardVar = new ScScoringBoardVar();
        pScScoringBoardVar.idscorevar = conseID;
        pScScoringBoardVar.idscore = Convert.ToInt64(lblCodigo.Text);
        pScScoringBoardVar.idparametro = Convert.ToInt64(ddlVariable.SelectedValue);
        pScScoringBoardVar.minimo = txtMinimoE.Text.Trim() != "" ? Convert.ToDecimal(txtMinimoE.Text) : 0;
        pScScoringBoardVar.maximo = txtMaximoE.Text.Trim() != "" ? Convert.ToDecimal(txtMaximoE.Text) : 0;
        pScScoringBoardVar.valor = txtValorE.Text.Trim() != "" ? Convert.ToInt64(txtValorE.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
        pScScoringBoardVar.beta = txtBetaE.Text.Trim() != "" ? Convert.ToDecimal(txtBetaE.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;

        gvVariables.EditIndex = -1;
        ScScoringBoardVarServicio.ModificarScScoringBoardVar(pScScoringBoardVar, (Usuario)Session["usuario"]);
        ActualizarVar();
    }

    private void MostrarVariables(String consecutivo, GridView gvVariables, String Var)
    {
        //Muestra el gridview con sus datos
        ScScoringBoardVar pScoringBoard = new ScScoringBoardVar();
        List<ScScoringBoardVar> LstScoringBoard = new List<ScScoringBoardVar>();
        LstScoringBoard = ScScoringBoardVarServicio.ListarScScoringBoardVar(ObtenerValoresSbVar(), (Usuario)Session["usuario"]);
        gvVariables.DataSource = LstScoringBoard;
        gvVariables.DataBind();
        Session[Var] = LstScoringBoard;
    }

    protected List<Parametro> ListaVariable()
    {
        ParametroService ParametroServicio = new ParametroService();
        Parametro eParametro = new Parametro();
        return ParametroServicio.ListarParametro(eParametro, (Usuario)Session["Usuario"]);
    }

    protected List<Modelo> ListaTipo()
    {
        Modelo vModelo;
        List<Modelo> LstModelo = new List<Modelo>();
        vModelo = new Modelo();
        vModelo.Codigo = 0;
        vModelo.Nombre = string.Empty;
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 1;
        vModelo.Nombre = "Negación";
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 2;
        vModelo.Nombre = "Reevaluación";
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 3;
        vModelo.Nombre = "Aprobación";
        LstModelo.Add(vModelo);
        return LstModelo;
    }

    public void ActualizarVar()
    {
        try
        {
            List<Xpinn.Scoring.Entities.ScScoringBoardVar> lstConsulta = new List<Xpinn.Scoring.Entities.ScScoringBoardVar>();
            lstConsulta = ScScoringBoardVarServicio.ListarScScoringBoardVar(ObtenerValoresSbVar(), (Usuario)Session["usuario"]);
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
                ScScoringBoardVar vScScoringBoardVar = new ScScoringBoardVar();
                vScScoringBoardVar.idscorevar = 0;
                vScScoringBoardVar.idscore = 0;
                vScScoringBoardVar.idparametro = 0;
                vScScoringBoardVar.minimo = 0;
                vScScoringBoardVar.maximo = 0;
                vScScoringBoardVar.valor = 0;
                vScScoringBoardVar.beta = 0;

                lstConsulta.Add(vScScoringBoardVar);
                gvVariables.DataBind();
                gvVariables.Rows[0].Visible = false;
            }

            Session.Add(ScScoringBoardVarServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(ScScoringBoardVarServicio.CodigoPrograma, "ActualizarVar", ex);
        }
    }

    private Xpinn.Scoring.Entities.ScScoringBoardVar ObtenerValoresSbVar()
    {
        Xpinn.Scoring.Entities.ScScoringBoardVar programa = new Xpinn.Scoring.Entities.ScScoringBoardVar();
        if (ViewState["gvScoringRow"] == null)
            return programa;
        programa.idscore = Convert.ToInt32(ViewState["gvScoringRow"].ToString());
        return programa;
    }


    //-------------------------------------------------------------------------------------------------------------
    //---------------------------------------------   Calificaciones   --------------------------------------------
    //-------------------------------------------------------------------------------------------------------------



    protected void gvCalificaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCalificaciones.EditIndex = -1;
        long conseID = Convert.ToInt32(gvCalificaciones.DataKeys[e.RowIndex].Values[0].ToString());
        String consecutivo = conseID.ToString();
        MostrarCalificaciones(consecutivo, gvCalificaciones, "Lineas");
        ActualizarCal(); 
    }


    protected void gvCalificaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            decimalesGrid txtMinimoF0 = (decimalesGrid)gvCalificaciones.FooterRow.FindControl("txtMinimoF0");
            decimalesGrid txtMaximoF0 = (decimalesGrid)gvCalificaciones.FooterRow.FindControl("txtMaximoF0");
            decimalesGrid txtCalificacionF = (decimalesGrid)gvCalificaciones.FooterRow.FindControl("txtCalificacionF");
            DropDownList ddlTipoF = (DropDownList)gvCalificaciones.FooterRow.FindControl("ddlTipoF");
            TextBox txtObservacionF = (TextBox)gvCalificaciones.FooterRow.FindControl("txtObservacionF");

            if (txtMinimoF0.Text == "" || txtMaximoF0.Text == "")
            {
                String Error = "Por favor diligenciar los datos";
                this.Lblerror.Text = Error;
                //quitarfilainicialGarantiasReales();
            }
            else
            {
                this.Lblerror.Text = "";
                ScScoringBoardCal vScScoringBoardCal = new ScScoringBoardCal();
                vScScoringBoardCal.idscorecal = 0;
                vScScoringBoardCal.idscore = Convert.ToInt64(lblCodigo.Text);
                vScScoringBoardCal.cal_minimo = txtMinimoF0.Text.Trim() != "" ? Convert.ToDecimal(txtMinimoF0.Text) : 0;
                vScScoringBoardCal.cal_maximo = txtMaximoF0.Text.Trim() != "" ? Convert.ToDecimal(txtMaximoF0.Text) : 0;
                vScScoringBoardCal.calificacion = txtCalificacionF.Text.Trim() != "" ? txtCalificacionF.Text.Trim().Replace(conf.ObtenerSeparadorMilesConfig(), "") : string.Empty;
                vScScoringBoardCal.tipo = Convert.ToInt64(ddlTipoF.SelectedIndex);
                vScScoringBoardCal.observacion = txtObservacionF.Text.Trim() != "" ? txtObservacionF.Text.Trim() : string.Empty;
                gvCalificaciones.EditIndex = -1;
                ScScoringBoardCalServicio.CrearScScoringBoardCal(vScScoringBoardCal, (Usuario)Session["usuario"]);
                ActualizarCal();
            }
        }
    }
    protected void gvCalificaciones_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Al actualizar el gridview, cambia el codigo de la columna "Modelo" por la respectiva descripcion
        Label lblTipo = (Label)e.Row.FindControl("lblTipo");
        if (lblTipo != null)
        {
            switch (lblTipo.Text)
            {
                case "0":
                    lblTipo.Text = "";
                    break;
                case "1":
                    lblTipo.Text = "Negación";
                    break;
                case "2":
                    lblTipo.Text = "Reevaluación";
                    break;
                case "3":
                    lblTipo.Text = "Aprobación";
                    break;
                default:
                    lblTipo.Text = "";
                    break;

            }
        }  
    }
    protected void gvCalificaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvCalificaciones.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            AsignarEventoConfirmar();
            ScScoringBoardCalServicio.EliminarScScoringBoardCal(ID, (Usuario)Session["usuario"]);
            ActualizarCal();
        }
    }

    private void MostrarCalificaciones(String consecutivo, GridView gvCalificaciones, String Var)
    {
        //Muestra el gridview con sus datos
        ScScoringBoardCal pScoringBoard = new ScScoringBoardCal();
        List<ScScoringBoardCal> LstScoringBoard = new List<ScScoringBoardCal>();
        LstScoringBoard = ScScoringBoardCalServicio.ListarScScoringBoardCal(ObtenerValoresSbCal(), (Usuario)Session["usuario"]);

        gvCalificaciones.DataSource = LstScoringBoard;
        gvCalificaciones.DataBind();
        Session[Var] = LstScoringBoard;
    }

    private void ConsultarCalificaciones()
    {
        MostrarCalificaciones("consecutivo", gvCalificaciones, "Lineas");

    }
    protected void gvCalificaciones_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvCalificaciones.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvCalificaciones.EditIndex = e.NewEditIndex;
            this.ConsultarCalificaciones();
        } 
    }
    protected void gvCalificaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Guarda (Modifica) los valores que han sido editados desde el gridview
        long conseID = Convert.ToInt32(gvCalificaciones.DataKeys[e.RowIndex].Values[0].ToString());
        decimalesGrid txtMinimoE0 = (decimalesGrid)gvCalificaciones.Rows[e.RowIndex].FindControl("txtMinimoE0");
        decimalesGrid txtMaximoE0 = (decimalesGrid)gvCalificaciones.Rows[e.RowIndex].FindControl("txtMaximoE0");
        decimalesGrid txtCalificacionE = (decimalesGrid)gvCalificaciones.Rows[e.RowIndex].FindControl("txtCalificacionE");
        DropDownList ddlTipoE = (DropDownList)gvCalificaciones.Rows[e.RowIndex].FindControl("ddlTipoE");
        TextBox txtObservacionE = (TextBox)gvCalificaciones.Rows[e.RowIndex].FindControl("txtObservacionE");

        ScScoringBoardCal pScScoringBoardCal = new ScScoringBoardCal();
        pScScoringBoardCal.idscorecal = conseID;
        pScScoringBoardCal.idscore = Convert.ToInt64(lblCodigo.Text);
        pScScoringBoardCal.cal_minimo = txtMinimoE0.Text.Trim() != "" ? Convert.ToInt64(txtMinimoE0.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
        pScScoringBoardCal.cal_maximo = txtMaximoE0.Text.Trim() != "" ? Convert.ToInt64(txtMaximoE0.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")) : 0;
        pScScoringBoardCal.calificacion = txtCalificacionE.Text.Trim() != "" ? txtCalificacionE.Text.Replace(conf.ObtenerSeparadorMilesConfig(), "") : string.Empty;
        pScScoringBoardCal.tipo = Convert.ToInt64(ddlTipoE.SelectedIndex);
        
        pScScoringBoardCal.observacion = txtObservacionE.Text.Trim() != "" ? txtObservacionE.Text : string.Empty;

        gvCalificaciones.EditIndex = -1;
        ScScoringBoardCalServicio.ModificarScScoringBoardCal(pScScoringBoardCal, (Usuario)Session["usuario"]);
        ActualizarCal();
    }

    public void ActualizarCal()
    {
        try
        {
            List<Xpinn.Scoring.Entities.ScScoringBoardCal> lstConsulta = new List<Xpinn.Scoring.Entities.ScScoringBoardCal>();
            lstConsulta = ScScoringBoardCalServicio.ListarScScoringBoardCal(ObtenerValoresSbCal(), (Usuario)Session["usuario"]);
            gvCalificaciones.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvCalificaciones.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvCalificaciones.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvCalificaciones.DataBind();
                //ValidarPermisosGrilla(gvCalificaciones);
            }
            else
            {   //Permite visualizar el footer del gridview cuando no hay ningun registro para mostrar
                lblTotalRegs.Text = "<br/> Registros encontrados: 0 ";
                ScScoringBoardCal vScScoringBoardCal = new ScScoringBoardCal();
                vScScoringBoardCal.idscorecal = 0;
                vScScoringBoardCal.idscore = 0;
                vScScoringBoardCal.cal_minimo = 0;
                vScScoringBoardCal.cal_maximo = 0;
                vScScoringBoardCal.calificacion = null;
                vScScoringBoardCal.tipo = 0;
                vScScoringBoardCal.observacion = null;

                lstConsulta.Add(vScScoringBoardCal);
                gvCalificaciones.DataBind();
                gvCalificaciones.Rows[0].Visible = false;
            }

            Session.Add(ScScoringBoardCalServicio.CodigoPrograma + ".consulta", 1);
            //      this.creargarantiasinicial(0, "GarantiasReales");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScScoringBoardCalServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Scoring.Entities.ScScoringBoardCal ObtenerValoresSbCal()
    {       
        Xpinn.Scoring.Entities.ScScoringBoardCal programa = new Xpinn.Scoring.Entities.ScScoringBoardCal();
        if (ViewState["gvScoringRow"] == null)
            return programa;
        programa.idscore = Convert.ToInt32(ViewState["gvScoringRow"].ToString());
        return programa;
    }


    protected void gvScoring_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void gvScoring_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["gvScoringRow"] = gvScoring.SelectedDataKey.Value.ToString();
        Actualizar();
    }
    
    //-------------------------------------------------------------------------------------------------------------
    //------------------------------------------------   Reporte   ------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------

    protected void btnReporte_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindReportViewer(); 
            mvScoringBoard.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            throw ex;
        }       
    }
    private void BindReportViewer()
    {
        rvScoring.Visible = true;
        
        //Parametros
        ReportParameter[] param = new ReportParameter[5];
        param[0] = new ReportParameter("Codigo", lblCodigo.Text);  
        param[1] = new ReportParameter("Clasificacion", ddlClasificacionC.SelectedItem.Text);
        param[2] = new ReportParameter("Linea", ddlLineasC.SelectedItem.Text);
        param[3] = new ReportParameter("Aplica", ddlAPLICA_AC.SelectedItem.Text);
        param[4] = new ReportParameter("Modelo", ddlMODELOC.SelectedItem.Text);
        rvScoring.LocalReport.SetParameters(param);
        
        //Lista de calificaciones
        IList<Xpinn.Scoring.Entities.ScScoringBoardCal> CalificacionesList = new List<Xpinn.Scoring.Entities.ScScoringBoardCal>();
        CalificacionesList = ScScoringBoardCalServicio.ListarScScoringBoardCal(ObtenerValoresSbCal(), (Usuario)Session["usuario"]);

        foreach (Xpinn.Scoring.Entities.ScScoringBoardCal CalificacionesList1 in CalificacionesList)
        {
            switch (CalificacionesList1.tipo)
            {
                case 0:
                    CalificacionesList1.descripcionTipo = "";
                    break;
                case 1:
                    CalificacionesList1.descripcionTipo = "Negación";
                    break;
                case 2:
                    CalificacionesList1.descripcionTipo = "Reevaluación";
                    break;
                default:
                    CalificacionesList1.descripcionTipo = "Aprobación";
                    break;
            }
        }

        ReportDataSource rds = new ReportDataSource("DataSet1", CalificacionesList);        

        //Lista de variables
        IList<Xpinn.Scoring.Entities.ScScoringBoardVar> VariablesList = new List<Xpinn.Scoring.Entities.ScScoringBoardVar>();
        VariablesList = ScScoringBoardVarServicio.ListarScScoringBoardVar(ObtenerValoresSbVar(), (Usuario)Session["usuario"]);

        foreach (Xpinn.Scoring.Entities.ScScoringBoardVar VariablesList1 in VariablesList)
        {
            switch (VariablesList1.idparametro)
            {
                case 0:
                    VariablesList1.descripcionParametro = "V0";
                    break;
                case 1:
                    VariablesList1.descripcionParametro = "V1";
                    break;
                case 2:
                    VariablesList1.descripcionParametro = "V2";
                    break;
                default:
                    VariablesList1.descripcionParametro = "V3";
                    break;
            }
        }

        ReportDataSource rdsVar = new ReportDataSource("DataSet2", VariablesList);   

        rvScoring.LocalReport.DataSources.Clear();
        rvScoring.LocalReport.DataSources.Add(rds);
        rvScoring.LocalReport.DataSources.Add(rdsVar);
        rvScoring.LocalReport.Refresh();
    }

    
}
