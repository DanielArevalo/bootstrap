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
    MatrizServices _matrizService = new MatrizServices();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_matrizService.CodigoProgramaPMR, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizService.CodigoProgramaPMR, "Page_PreInit", ex);
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
            BOexcepcion.Throw(_matrizService.CodigoProgramaPMR, "Page_Load", ex);
        }
    }

    void ObtenerDatos()
    {
        List<Matriz> lstConsulta = new List<Matriz>();
        lstConsulta = _matrizService.ListarRangosMatrizRiesgo((Usuario)Session["usuario"]);

        if (lstConsulta.Count > 0)
        {
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["Detalle"] = lstConsulta;
        }
        else
        {
            //InicializarListado();
        }
    }


    #endregion


    #region Eventos Botonera


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Matriz vRangoRiesgo = new Matriz();
            vRangoRiesgo.lstDetalle = ObtenerListaGridView(true);

            // Si llega nulo es porque hubo algun error
            if (vRangoRiesgo.lstDetalle != null)
            {
                if (vRangoRiesgo.lstDetalle.Count > 0)
                {
                    foreach (Matriz eachRango in vRangoRiesgo.lstDetalle)
                    {
                        Matriz result = _matrizService.ModificarRangoMatrizRiesgo(eachRango, Usuario);
                    }
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
            BOexcepcion.Throw(_matrizService.CodigoProgramaPMR, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        
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
            Label lblCalificacion = (Label)e.Row.FindControl("lblCalificacion");
            if (Convert.ToInt32(lblCalificacion.Text) > 0)
                switch (Convert.ToInt32(lblCalificacion.Text))
                {
                    case 1: lblCalificacion.Text = "Bajo"; break;
                    case 2: lblCalificacion.Text = "Moderado"; break;
                    case 3: lblCalificacion.Text = "Alto"; break;
                    case 4: lblCalificacion.Text = "Extremo"; break;
                }
        }
    }

    protected void ddlOperador_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlCondicion_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }


    #endregion


    #region Metodos De Ayuda


    List<Matriz> ObtenerListaGridView(bool validarGridView = false)
    {
        List<Matriz> lista = new List<Matriz>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Matriz sRango = new Matriz();

            Label lblcodigoRango = (Label)rfila.FindControl("lblcodigoRango");

            Label lblCalificacion = (Label)rfila.FindControl("lblCalificacion");
            if (lblCalificacion.Text != null)
                switch (Convert.ToString(lblCalificacion.Text))
                {
                    case "Bajo": sRango.calificacion = 1; break;
                    case "Moderado": sRango.calificacion = 2; break;
                    case "Alto": sRango.calificacion = 3; break;
                    case "Extremo": sRango.calificacion = 4; break;
                }

            if (lblcodigoRango != null && !string.IsNullOrWhiteSpace(lblcodigoRango.Text))
                sRango.cod_matriz = Convert.ToInt32(lblcodigoRango.Text);

            TextBox txtRangoMinimo = (TextBox)rfila.FindControl("txtRangoMinimo");
            if (txtRangoMinimo != null)
            {
                if (txtRangoMinimo.Visible)
                {
                    sRango.rango_minimo = Convert.ToInt32(txtRangoMinimo.Text);
                }
            }

            TextBox txtRangoMaximo = (TextBox)rfila.FindControl("txtRangoMaximo");
            if (txtRangoMinimo != null)
            {
                if (txtRangoMaximo.Visible)
                {
                    sRango.rango_maximo = Convert.ToInt32(txtRangoMaximo.Text);
                }
            }
            lista.Add(sRango);
        }

        return lista;
    }

    #endregion


}
