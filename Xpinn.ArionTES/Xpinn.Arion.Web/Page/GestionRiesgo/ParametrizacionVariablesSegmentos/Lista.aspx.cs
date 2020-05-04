using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    HistoricoSegmentacionService _historicoService = new HistoricoSegmentacionService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_historicoService.CodigoPrograma5, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma5, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatos(idObjeto);   
            }
            else
            {
                Session["Operacion"] = "1";
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma5, "Page_Load", ex);
        }
    }

    void ObtenerDatos(String idObjeto)
    {
        List<tipoVariable> lstConsulta = new List<tipoVariable>();
        lstConsulta = _historicoService.ListarTiposVariable(ObtenerValores(), Usuario);

        if (lstConsulta.Count > 0)
        {
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["Detalle"] = lstConsulta;
        }
    }
    tipoVariable ObtenerValores()
    {
        tipoVariable variable = new tipoVariable();


        /*if (txtCodigo.Text.Trim() != "")
            rango.cod_rango_perfil = Convert.ToInt32(txtCodigo.Text.Trim());*/

        return variable;
    }

    #endregion


    #region Eventos Botonera


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            tipoVariable vVariables = new tipoVariable();
            vVariables.lstVariables = ObtenerListaGridView(true);

            // Si llega nulo es porque hubo algun error
            if (vVariables.lstVariables != null)
            {
                if (vVariables.lstVariables.Count > 0)
                {
                    foreach (tipoVariable eachVariable in vVariables.lstVariables)
                    {
                        tipoVariable result = _historicoService.ModificarVariable(eachVariable, Usuario);
                    }
                    //Navegar(Pagina.Lista);
                    ObtenerDatos("true");
                }
                else
                {
                    VerError("Algo salió mal, intenta nuevamente más tarde!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma5, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    #endregion


    #region Eventos GridView


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
        }
    }

    #endregion


    #region Metodos De Ayuda


    List<tipoVariable> ObtenerListaGridView(bool validarGridView = false)
    {
        List<tipoVariable> lista = new List<tipoVariable>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            tipoVariable sVariable = new tipoVariable();

            Label lblcodigoVariable = (Label)rfila.FindControl("lblcodigoVariable");
            if (lblcodigoVariable != null && !string.IsNullOrWhiteSpace(lblcodigoVariable.Text))
                sVariable.cod_variable = Convert.ToInt32(lblcodigoVariable.Text);

            TextBox txtRiesgoBajo = (TextBox)rfila.FindControl("txtRiesgoBajo");
            if (txtRiesgoBajo != null)
            {
                if (txtRiesgoBajo.Visible)
                {
                    sVariable.riesgo_bajo = Convert.ToInt32(txtRiesgoBajo.Text);
                }
            }

            TextBox txtRiesgoModerado = (TextBox)rfila.FindControl("txtRiesgoModerado");
            if (txtRiesgoModerado != null)
            {
                if (txtRiesgoModerado.Visible)
                {
                    sVariable.riesgo_moderado = Convert.ToInt32(txtRiesgoModerado.Text);
                }
            }

            TextBox txtRiesgoAlto = (TextBox)rfila.FindControl("txtRiesgoAlto");
            if (txtRiesgoAlto != null)
            {
                if (txtRiesgoAlto.Visible)
                {
                    sVariable.riesgo_alto = Convert.ToInt32(txtRiesgoAlto.Text);
                }
            }

            TextBox txtRiesgoExtremo = (TextBox)rfila.FindControl("txtRiesgoExtremo");
            if (txtRiesgoExtremo != null)
            {
                if (txtRiesgoExtremo.Visible)
                {
                    sVariable.riesgo_extremo = Convert.ToInt32(txtRiesgoExtremo.Text);
                }
            }

            lista.Add(sVariable);
            //Session["Detalle"] = lista;
        }

        return lista;
    }

    #endregion


}
