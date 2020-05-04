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
    Xpinn.Scoring.Services.ParametroService parametroServicio = new Xpinn.Scoring.Services.ParametroService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(parametroServicio.CodigoPrograma.ToString(), "L");
            Site toolBar = (Site)this.Master;

        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(parametroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ActualizarPar();
                HabilitarCampos(gvParametros.FooterRow, "F");
            }
            if (Session[parametroServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[parametroServicio.CodigoPrograma + ".id"].ToString();                
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(parametroServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

  

    //-------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------   Parametros   --------------------------------------------
    //-------------------------------------------------------------------------------------------------------------

    protected List<Modelo> ListaTipoPar()
    {
        Modelo vModelo;
        List<Modelo> LstModelo = new List<Modelo>();
        vModelo = new Modelo();
        vModelo.Codigo = 0;
        vModelo.Nombre = "Variable";
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 1;
        vModelo.Nombre = "Campo";
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 2;
        vModelo.Nombre = "Sentencia";
        LstModelo.Add(vModelo);
        vModelo = new Modelo();
        vModelo.Codigo = 3;
        vModelo.Nombre = "Fórmula";
        LstModelo.Add(vModelo);
        return LstModelo;
    }

    protected void gvParametros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvParametros.EditIndex = -1;
        long conseID = Convert.ToInt32(gvParametros.DataKeys[e.RowIndex].Values[0].ToString());
        String consecutivo = conseID.ToString();
        MostrarParametros(consecutivo, gvParametros, "Lineas");
        ActualizarPar();
    }


    protected void gvParametros_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        TextBox txtNombreF = (TextBox)gvParametros.FooterRow.FindControl("txtNombreF");
        if (e.CommandName.Equals("AddNew") && txtNombreF.Text != "")
        {
            DropDownList ddlVariableF = (DropDownList)gvParametros.FooterRow.FindControl("ddlVariableF");
            DropDownList ddlTipoF = (DropDownList)gvParametros.FooterRow.FindControl("ddlTipoF");
            DropDownList ddlCampoF = (DropDownList)gvParametros.FooterRow.FindControl("ddlCampoF");
            TextBox txtFormulaF = (TextBox)gvParametros.FooterRow.FindControl("txtFormulaF");
            TextBox txtSentenciaF = (TextBox)gvParametros.FooterRow.FindControl("txtSentenciaF");

            Parametro vParametro = new Parametro();
            vParametro.idvariable = 0;
            vParametro.idvariable = ddlVariableF.Text.Trim() != "" ? Convert.ToInt64(ddlVariableF.Text.Trim()) : 0;
            vParametro.nombre = txtNombreF.Text.Trim() != "" ? txtNombreF.Text.Trim() : string.Empty;
            vParametro.tipo = ddlTipoF.Text.Trim() != "" ? Convert.ToString(ddlTipoF.SelectedIndex) : "0";
            vParametro.formula = txtFormulaF.Text.Trim() != "" ? txtFormulaF.Text.Trim() : string.Empty;
            vParametro.sentencia = txtSentenciaF.Text.Trim() != "" ? txtSentenciaF.Text.Trim() : string.Empty;
            vParametro.campo = ddlCampoF.SelectedItem.ToString() != "" ? Convert.ToString(ddlCampoF.SelectedItem) : string.Empty;
            if (ValidarDatosParametro(ref vParametro) == false)
            {
                VerError("Se presento error al validar los datos");
                return;
            }
            parametroServicio.CrearParametro(vParametro, (Usuario)Session["usuario"]);
            ActualizarPar();
        }
    }

    protected Boolean ValidarDatosParametro(ref Parametro vParametro)
    {
        if (vParametro.tipo == "0")
        {
            vParametro.formula = " ";
            vParametro.sentencia = " ";
            vParametro.campo = " ";
            if (vParametro.idvariable <= 0)
                return false;
        }
        if (vParametro.tipo == "1")
        {
            vParametro.formula = " ";
            vParametro.sentencia = " ";
            vParametro.idvariable = 0;
            if (vParametro.campo == null || vParametro.campo == "")
                return false;
        }
        if (vParametro.tipo == "2")
        {
            vParametro.formula = " ";
            vParametro.campo = " ";
            vParametro.idvariable = 0;
            if (vParametro.sentencia == null || vParametro.sentencia == "")
                return false;
        }
        if (vParametro.tipo == "3")
        {
            vParametro.campo = " ";
            vParametro.idvariable = 0;
            vParametro.sentencia = " ";
            if (vParametro.formula == null || vParametro.formula == "")
                return false;
        }
        return true;
    }

    protected void gvParametros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Al actualizar el gridview, cambia el codigo de la columna "Tipo" por la respectiva descripcion
        Label lblTipo = (Label)e.Row.FindControl("lblTipo");
        if (lblTipo != null)
        {
            switch (lblTipo.Text)
            {
                case "0":
                    lblTipo.Text = "Variable";
                    break;
                case "1":
                    lblTipo.Text = "Campo";
                    break;
                case "2":
                    lblTipo.Text = "Sentencia";
                    break;
                case "3":
                    lblTipo.Text = "Fórmula";
                    break;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlTipoE = (DropDownList)e.Row.FindControl("ddlTipo");
            if (ddlTipoE != null)
            {
                ddlTipoE.SelectedIndexChanged += ddlTipoE_SelectedIndexChanged;
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlTipoF = (DropDownList)e.Row.FindControl("ddlTipoF");
            if (ddlTipoF != null)
            {
                ddlTipoF.SelectedIndexChanged += ddlTipoF_SelectedIndexChanged;
            }            
        }
    }

    protected void gvParametros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvParametros.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            AsignarEventoConfirmar();
            parametroServicio.EliminarParametro(ID, (Usuario)Session["usuario"]);
            ActualizarPar();
        }
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    private void MostrarParametros(String consecutivo, GridView gvParametros, String Var)
    {
        //Muestra el gridview con sus datos
        Parametro pScoringBoard = new Parametro();
        List<Parametro> LstScoringBoard = new List<Parametro>();
        LstScoringBoard = parametroServicio.ListarParametro(ObtenerValoresPar(), (Usuario)Session["usuario"]);

        gvParametros.DataSource = LstScoringBoard;
        gvParametros.DataBind();
        Session[Var] = LstScoringBoard;
    }

    private void ConsultarParametros()
    {
        MostrarParametros("consecutivo", gvParametros, "Lineas");

    }

    protected void gvParametros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvParametros.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvParametros.EditIndex = e.NewEditIndex;
            this.ConsultarParametros();
        }
        HabilitarCampos(gvParametros.Rows[gvParametros.EditIndex], "");
    }

    protected void gvParametros_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Guarda (Modifica) los valores que han sido editados desde el gridview
        long conseID = Convert.ToInt32(gvParametros.DataKeys[e.RowIndex].Values[0].ToString());
        DropDownList ddlTipo = (DropDownList)gvParametros.Rows[e.RowIndex].FindControl("ddlTipo");
        TextBox txtNombre = (TextBox)gvParametros.Rows[e.RowIndex].FindControl("txtNombre");
        DropDownList ddlVariable = (DropDownList)gvParametros.Rows[e.RowIndex].FindControl("ddlVariable");
        TextBox txtFormula = (TextBox)gvParametros.Rows[e.RowIndex].FindControl("txtFormula");
        DropDownList ddlCampo = (DropDownList)gvParametros.Rows[e.RowIndex].FindControl("ddlCampo");
        TextBox txtSentencia = (TextBox)gvParametros.Rows[e.RowIndex].FindControl("txtSentencia");

        Parametro pParametro = new Parametro();
        pParametro.idparametro = conseID;
        pParametro.tipo = ddlTipo.Text.Trim() != "" ? Convert.ToString(ddlTipo.SelectedIndex) : string.Empty;
        pParametro.nombre = txtNombre.Text.Trim() != "" ? txtNombre.Text.Trim() : string.Empty;
        pParametro.idvariable = ddlVariable.SelectedValue.Trim() != "" ? Convert.ToInt64(ddlVariable.SelectedValue.Trim()) : 0;
        pParametro.formula = txtFormula.Text.Trim() != "" ? txtFormula.Text.Trim() : string.Empty;
        pParametro.sentencia = txtSentencia.Text.Trim() != "" ? txtSentencia.Text.Trim() : string.Empty;
        pParametro.campo = ddlCampo.SelectedItem.ToString() != "" ? Convert.ToString(ddlCampo.SelectedItem) : string.Empty;
        if (ValidarDatosParametro(ref pParametro) == false)
        {
            VerError("Se presento error al validar los datos");
            return;
        }

        gvParametros.EditIndex = -1;
        if (txtNombre.Text != "") parametroServicio.ModificarParametro(pParametro, (Usuario)Session["usuario"]);
        ActualizarPar();
    }

    public void ActualizarPar()
    {
        try
        {
            List<Xpinn.Scoring.Entities.Parametro> lstConsulta = new List<Xpinn.Scoring.Entities.Parametro>();
            lstConsulta = parametroServicio.ListarParametro(ObtenerValoresPar(), (Usuario)Session["usuario"]);
            gvParametros.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvParametros.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvParametros.Visible = true;
                lblInfo1.Visible = false;
                lblTotalRegs1.Visible = true;
                lblTotalRegs1.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvParametros.DataBind();
            }
            else
            {   //Permite visualizar el footer del gridview cuando no hay ningun registro para mostrar
                lblTotalRegs1.Text = "<br/> Registros encontrados: 0 ";
                Parametro vParametro = new Parametro();
                vParametro.idvariable = 0;
                vParametro.nombre = "0";
                vParametro.tipo = "0";
                vParametro.sentencia = null;
                lstConsulta.Add(vParametro);
                gvParametros.DataBind();
                gvParametros.Rows[0].Visible = false;
            }
            HabilitarCampos(gvParametros.FooterRow, "F");
            Session.Add(parametroServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(parametroServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Scoring.Entities.Parametro ObtenerValoresPar()
    {
        Xpinn.Scoring.Entities.Parametro programa = new Xpinn.Scoring.Entities.Parametro();
        return programa;
    }

    protected List<Variables> ListaVariable()
    {
        VariablesService VariableServicio = new VariablesService();
        Variables eVariables = new Variables();
        return VariableServicio.ListarDefinirVariables(eVariables, (Usuario)Session["Usuario"]);
    }

    protected List<Modelo> ListaCampo()
    {
        Modelo eCampos = new Modelo();
        return parametroServicio.ListarCampos(eCampos, (Usuario)Session["Usuario"]);
    }

    protected void ddlTipoE_SelectedIndexChanged(object sender, EventArgs e)
    {
        HabilitarCampos(gvParametros.Rows[gvParametros.EditIndex], "");
    }

    protected void ddlTipoF_SelectedIndexChanged(object sender, EventArgs e)
    {
        HabilitarCampos(gvParametros.FooterRow, "F");
    }

    protected void HabilitarCampos(GridViewRow grFila, string sTipo)
    {
        DropDownList ddlTipoF = (DropDownList)grFila.FindControl("ddlTipo" + sTipo);
        if (ddlTipoF != null)
        {
            if (ddlTipoF.SelectedIndex == 0)
            {
                DropDownList ddlVariableF = (DropDownList)grFila.FindControl("ddlVariable" + sTipo);
                DropDownList ddlCampoF = (DropDownList)grFila.FindControl("ddlCampo" + sTipo);
                TextBox txtFormulaF = (TextBox)grFila.FindControl("txtFormula" + sTipo);
                TextBox txtSentenciaF = (TextBox)grFila.FindControl("txtSentencia" + sTipo);
                ddlCampoF.Visible = false;
                ddlVariableF.Visible = true;
                txtFormulaF.Visible = false;
                txtSentenciaF.Visible = false;
            }
            if (ddlTipoF.SelectedIndex ==1)
            {
                DropDownList ddlVariableF = (DropDownList)grFila.FindControl("ddlVariable" + sTipo);
                DropDownList ddlCampoF = (DropDownList)grFila.FindControl("ddlCampo" + sTipo);
                TextBox txtFormulaF = (TextBox)grFila.FindControl("txtFormula" + sTipo);
                TextBox txtSentenciaF = (TextBox)grFila.FindControl("txtSentencia" + sTipo);
                ddlCampoF.Visible = true;
                ddlVariableF.Visible = false;
                txtFormulaF.Visible = false;
                txtSentenciaF.Visible = false;
            }
            if (ddlTipoF.SelectedIndex == 2)
            {
                DropDownList ddlVariableF = (DropDownList)grFila.FindControl("ddlVariable" + sTipo);
                DropDownList ddlCampoF = (DropDownList)grFila.FindControl("ddlCampo" + sTipo);
                TextBox txtFormulaF = (TextBox)grFila.FindControl("txtFormula" + sTipo);
                TextBox txtSentenciaF = (TextBox)grFila.FindControl("txtSentencia" + sTipo);
                ddlCampoF.Visible = false;
                ddlVariableF.Visible = false;
                txtFormulaF.Visible = false;
                txtSentenciaF.Visible = true;
            }
            if (ddlTipoF.SelectedIndex == 3)
            {
                DropDownList ddlVariableF = (DropDownList)grFila.FindControl("ddlVariable" + sTipo);
                DropDownList ddlCampoF = (DropDownList)grFila.FindControl("ddlCampo" + sTipo);
                TextBox txtFormulaF = (TextBox)grFila.FindControl("txtFormula" + sTipo);
                TextBox txtSentenciaF = (TextBox)grFila.FindControl("txtSentencia" + sTipo);
                ddlCampoF.Visible = false;
                ddlVariableF.Visible = false;
                txtFormulaF.Visible = true;
                txtSentenciaF.Visible = false;
            }
        }       
    }

}
