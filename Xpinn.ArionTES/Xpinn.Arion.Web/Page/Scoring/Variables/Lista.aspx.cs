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
    Xpinn.Scoring.Services.VariablesService DefinirVariablesServicio = new Xpinn.Scoring.Services.VariablesService();   

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {          
            Site toolBar = (Site)this.Master; 
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(DefinirVariablesServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ActualizarVar();
            }
            if (Session[DefinirVariablesServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[DefinirVariablesServicio.CodigoPrograma + ".id"].ToString();
                Session.Remove(DefinirVariablesServicio.CodigoPrograma + ".id");
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DefinirVariablesServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    //-------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------   Variables   --------------------------------------------
    //-------------------------------------------------------------------------------------------------------------
    
    protected List<Modelo> ListaTipo()
    {
        Modelo vModelo;
        List<Modelo> LstModelo = new List<Modelo>();
        vModelo = new Modelo();
        vModelo.Codigo = 0;
        vModelo.Nombre = "Sentencia";
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 1;
        vModelo.Nombre = "Usuario";
        LstModelo.Add(vModelo);
        return LstModelo;
    }

    protected void gvVar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvVar.EditIndex = -1;
        long conseID = Convert.ToInt32(gvVar.DataKeys[e.RowIndex].Values[0].ToString());
        String consecutivo = conseID.ToString();
        MostrarCalificaciones(consecutivo, gvVar, "Lineas");
        ActualizarVar(); 
    }


    protected void gvVar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtVariableF = (TextBox)gvVar.FooterRow.FindControl("txtVariableF");
            TextBox txtNombreF = (TextBox)gvVar.FooterRow.FindControl("txtNombreF");
            DropDownList ddlTipoF = (DropDownList)gvVar.FooterRow.FindControl("ddlTipoF");
            TextBox txtSentenciaF = (TextBox)gvVar.FooterRow.FindControl("txtSentenciaF");           
           
            Variables vDefinirVariables = new Variables();
            vDefinirVariables.idvariable = 0;
            vDefinirVariables.variable = txtVariableF.Text.Trim() != "" ? txtVariableF.Text.Trim() : string.Empty;
            vDefinirVariables.nombre = txtNombreF.Text.Trim() != "" ? txtNombreF.Text.Trim() : string.Empty;
            vDefinirVariables.tipo = ddlTipoF.Text.Trim() != "" ? Convert.ToInt64(ddlTipoF.SelectedIndex) : 0;
            vDefinirVariables.sentencia = txtSentenciaF.Text.Trim() != "" ? txtSentenciaF.Text.Trim() : string.Empty;
            
            if (vDefinirVariables.tipo == 0 && vDefinirVariables.sentencia == string.Empty)
            {
                VerError("Debe ingresar la sentencia");
                return;
            }
            gvVar.EditIndex = -1;
            DefinirVariablesServicio.CrearDefinirVariables(vDefinirVariables, (Usuario)Session["usuario"]);
            ActualizarVar();                                  
        }
    }
    protected void gvVar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Al actualizar el gridview, cambia el codigo de la columna "tIPO" por la respectiva descripcion
        Label lblTipo = (Label)e.Row.FindControl("lblTipo");
        if (lblTipo != null)
        {
            switch (lblTipo.Text)
            {
                case "0":
                    lblTipo.Text = "Sentencia";
                    break;
                case "1":
                    lblTipo.Text = "Usuario";
                    break;      
            }
        }  
    }

    protected void gvVar_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvVar.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            AsignarEventoConfirmar();
            DefinirVariablesServicio.EliminarDefinirVariables(ID, (Usuario)Session["usuario"]);
            ActualizarVar();
        }
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    private void MostrarCalificaciones(String consecutivo, GridView gvVar, String Var)
    {
        //Muestra el gridview con sus datos
        Variables pScoringBoard = new Variables();
        List<Variables> LstScoringBoard = new List<Variables>();
        LstScoringBoard = DefinirVariablesServicio.ListarDefinirVariables(ObtenerValoresVar(), (Usuario)Session["usuario"]);

        gvVar.DataSource = LstScoringBoard;
        gvVar.DataBind();
        Session[Var] = LstScoringBoard;
    }

    private void ConsultarCalificaciones()
    {
        MostrarCalificaciones("consecutivo", gvVar, "Lineas");

    }

    protected void gvVar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvVar.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvVar.EditIndex = e.NewEditIndex;
            this.ConsultarCalificaciones();
        } 
    }

    protected void gvVar_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        VerError("");
        // Guarda (Modifica) los valores que han sido editados desde el gridview
        long conseID = Convert.ToInt32(gvVar.DataKeys[e.RowIndex].Values[0].ToString());
        TextBox txtVariableE = (TextBox)gvVar.Rows[e.RowIndex].FindControl("txtVariableE");
        TextBox txtNombreE = (TextBox)gvVar.Rows[e.RowIndex].FindControl("txtNombreE");
        DropDownList ddlTipoE = (DropDownList)gvVar.Rows[e.RowIndex].FindControl("ddlTipoE");
        TextBox txtSentenciaE = (TextBox)gvVar.Rows[e.RowIndex].FindControl("txtSentenciaE");

        Variables pDefinirVariables = new Variables();
        pDefinirVariables.idvariable = conseID;
        pDefinirVariables.variable = txtVariableE.Text.Trim() != "" ? txtVariableE.Text.Trim() : string.Empty;
        pDefinirVariables.nombre = txtNombreE.Text.Trim() != "" ? txtNombreE.Text.Trim() : string.Empty;
        pDefinirVariables.tipo = ddlTipoE.Text.Trim() != "" ? Convert.ToInt64(ddlTipoE.SelectedIndex) : 0;
        pDefinirVariables.sentencia = txtSentenciaE.Text.Trim() != "" ? txtSentenciaE.Text.Trim() : string.Empty;

        if (pDefinirVariables.tipo == 0 && pDefinirVariables.sentencia == string.Empty)
        {
            VerError("Debe ingresar la sentencia");
            return;
        }
        gvVar.EditIndex = -1;
        DefinirVariablesServicio.ModificarDefinirVariables(pDefinirVariables, (Usuario)Session["usuario"]);
        ActualizarVar();
                
    }

    public void ActualizarVar()
    {
        try
        {
            List<Xpinn.Scoring.Entities.Variables> lstConsulta = new List<Xpinn.Scoring.Entities.Variables>();
            lstConsulta = DefinirVariablesServicio.ListarDefinirVariables(ObtenerValoresVar(), (Usuario)Session["usuario"]);
            gvVar.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvVar.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvVar.Visible = true;
                lblInfo0.Visible = false;
                lblTotalRegs0.Visible = true;
                lblTotalRegs0.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvVar.DataBind();
            }
            else
            {   //Permite visualizar el footer del gridview cuando no hay ningun registro para mostrar
                lblTotalRegs0.Text = "<br/> Registros encontrados: 0 ";
                Variables vDefinirVariables = new Variables();
                vDefinirVariables.idvariable = 0;
                vDefinirVariables.variable = "0";
                vDefinirVariables.nombre = "0";
                vDefinirVariables.tipo = 0;
                vDefinirVariables.sentencia = null;
                lstConsulta.Add(vDefinirVariables);
                gvVar.DataBind();
                gvVar.Rows[0].Visible = false;
            }

            Session.Add(DefinirVariablesServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DefinirVariablesServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Scoring.Entities.Variables ObtenerValoresVar()
    {       
        Xpinn.Scoring.Entities.Variables programa = new Xpinn.Scoring.Entities.Variables();
        //programa.idvariable = Convert.ToInt32(ViewState["gvScoringRow"].ToString());
        return programa;
    }

 }
