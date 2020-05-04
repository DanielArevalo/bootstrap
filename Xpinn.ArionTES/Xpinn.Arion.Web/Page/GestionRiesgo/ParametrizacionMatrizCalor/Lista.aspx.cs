using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    MatrizServices _matrizCalor = new MatrizServices();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_matrizCalor.CodigoProgramaMC, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizCalor.CodigoProgramaMC, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
            }
            else
            {
                Session["Operacion"] = "1";
                //InicializarListado();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizCalor.CodigoProgramaMC, "Page_Load", ex);
        }
    }

    protected void ObtenerDatos()
    {
        List<Matriz> lstConsulta = new List<Matriz>();
        lstConsulta = _matrizCalor.ListarMatrizCalor((Usuario)Session["usuario"]);

        if (lstConsulta.Count > 0)
        {
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["Detalle"] = lstConsulta;
        }
    }

    #endregion
    #region Eventos Botonera


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Matriz mc = new Matriz();
            mc.lstDetalle = ObtenerListaGridView();

            // Si llega nulo es porque hubo algun error
            if (mc.lstDetalle != null)
            {
                if (mc.lstDetalle.Count > 0)
                {
                    foreach (Matriz eachCalor in mc.lstDetalle)
                    {
                        Matriz result = _matrizCalor.ModificarMatrizCalor(eachCalor, (Usuario)Session["usuario"]);
                    }
                    //Navegar(Pagina.Lista);
                    ObtenerDatos();
                }
                else
                {
                    VerError("Algo salió mal, intenta nuevamente más tarde!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizCalor.CodigoProgramaMC, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    #endregion


    #region Eventos GridView


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {}

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //impacto
            DropDownList ddlImpacto = (DropDownList)e.Row.FindControl("dllImpacto");
            if (ddlImpacto != null)
            {
                ddlImpacto.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlImpacto.Items.Insert(1, new ListItem("Insignificante", "1"));
                ddlImpacto.Items.Insert(2, new ListItem("Menor", "2"));
                ddlImpacto.Items.Insert(3, new ListItem("Moderado", "3"));
                ddlImpacto.Items.Insert(4, new ListItem("Mayor", "4"));
                ddlImpacto.Items.Insert(5, new ListItem("Catastrófico", "5"));
                ddlImpacto.DataBind();
            }

            Label lblImpacto = (Label)e.Row.FindControl("lblImpacto");
            if (lblImpacto != null)
            {
                ddlImpacto.SelectedValue = lblImpacto.Text;
            }
            //probabilidad
            DropDownList ddlProbabilidad = (DropDownList)e.Row.FindControl("dllProbabilidad");
            if (ddlProbabilidad != null)
            {
                ddlProbabilidad.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlProbabilidad.Items.Insert(1, new ListItem("Casi nunca", "1"));
                ddlProbabilidad.Items.Insert(2, new ListItem("Improbable", "2"));
                ddlProbabilidad.Items.Insert(3, new ListItem("Moderada", "3"));
                ddlProbabilidad.Items.Insert(4, new ListItem("Muy probable", "4"));
                ddlProbabilidad.Items.Insert(5, new ListItem("Siempre", "5"));
                ddlProbabilidad.DataBind();
            }

            Label lblProbabilidad = (Label)e.Row.FindControl("lblProbabilidad");
            if (lblProbabilidad != null)
            {
                ddlProbabilidad.SelectedValue = lblProbabilidad.Text;
            }
        }
    }


    #endregion


    #region Metodos De Ayuda


    List<Matriz> ObtenerListaGridView()
    {
        List<Matriz> lista = new List<Matriz>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Matriz mCalor = new Matriz();

            DropDownListGrid dllImpacto = (DropDownListGrid)rfila.FindControl("dllImpacto");
            if (dllImpacto.SelectedValue != null)
                mCalor.cod_impacto = Convert.ToInt32(dllImpacto.SelectedValue);

            DropDownListGrid dllProbabilidad = (DropDownListGrid)rfila.FindControl("dllProbabilidad");
            if (dllProbabilidad.SelectedValue != null)
                mCalor.cod_probabilidad = Convert.ToInt32(dllProbabilidad.SelectedValue);

            TextBox txtCalificacion = (TextBox)rfila.FindControl("txtCalificacion");
            if (txtCalificacion != null)
            {
                if (txtCalificacion.Visible)
                {
                    mCalor.calificacion = Convert.ToInt32(txtCalificacion.Text);
                }
            }

            lista.Add(mCalor);
        }

        return lista;
    }

    #endregion


}
