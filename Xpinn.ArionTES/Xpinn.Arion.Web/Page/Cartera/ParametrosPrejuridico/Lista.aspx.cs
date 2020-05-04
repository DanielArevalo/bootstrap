using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    ParametroCobroPrejuridicoService _cobroService = new ParametroCobroPrejuridicoService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_cobroService.CodigoPrograma, "E");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += CtlMensaje_eventoClick;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cobroService.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        _usuario = (Usuario)Session["usuario"];
        if (!IsPostBack)
        {
            ctlMensaje.Visible = false;
            btnAgregar.Visible = false;
        }
    }


    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (string.IsNullOrWhiteSpace(ddlTipoCobro.SelectedValue))
        {
            VerError("Selecciona un tipo de Cobro");
            return;
        }
        ctlMensaje.Visible = true;
        ctlMensaje.MostrarMensaje("Desea realizar los cambios?");
    }


    private void CtlMensaje_eventoClick(object sender, EventArgs e)
    {
        try
        {
            List<ParametroCobroPrejuridico> lstParametro = LlenarListaParaGuardar();

            if (lstParametro.Count != 0)
            {
                foreach (var parametro in lstParametro)
                {
                    _cobroService.ModificarParametroCobroPrejuridico(parametro, _usuario);
                }

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                mvParametro.ActiveViewIndex = 1;
            }
            else
            {
                VerError("No hay nada que guardar");
            }
        }
        catch (Exception ex)
        {
            VerError("Error intentando guardar los parametros, " + ex.Message);
        }
    }

    private List<ParametroCobroPrejuridico> LlenarListaParaGuardar()
    {
        List<ParametroCobroPrejuridico> lstParametro = new List<ParametroCobroPrejuridico>();

        foreach (GridViewRow row in gvLista.Rows)
        {
            string minimo = string.Empty;
            string maximo = string.Empty;
            string porcentaje = string.Empty;
            string valorCobro = string.Empty;

            TextBox txtMinimo = row.FindControl("txtMinimo") as TextBox;
            TextBox txtMaximo = row.FindControl("txtMaximo") as TextBox;
            TextBox txtPorcentaje = row.FindControl("txtPorcentaje") as TextBox;
            TextBox txtValor = row.FindControl("txtValor") as TextBox;

            if (txtMinimo != null && txtMinimo.Enabled)
            {
                minimo = txtMinimo.Text;
            }
            if (txtMaximo != null && txtMaximo.Enabled)
            {
                maximo = txtMaximo.Text;
            }
            if (txtPorcentaje != null && txtPorcentaje.Enabled)
            {
                porcentaje = txtPorcentaje.Text;
            }
            if (txtValor != null && txtValor.Enabled)
            {
                valorCobro = txtValor.Text;
            }

            ParametroCobroPrejuridico parametro = new ParametroCobroPrejuridico();
            parametro.idparametro = Convert.ToInt32(gvLista.DataKeys[row.TabIndex].Value);
            parametro.tipo_cobro = Convert.ToInt32(ddlTipoCobro.SelectedValue);
            parametro.forma_cobro = ddlFormaCobro.SelectedIndex;
            parametro.minimo = minimo;
            parametro.maximo = maximo;
            parametro.porcentaje = !string.IsNullOrWhiteSpace(porcentaje) ? Convert.ToInt32(porcentaje) : default(int?);
            parametro.valor = !string.IsNullOrWhiteSpace(valorCobro) ? Convert.ToDecimal(valorCobro) : default(decimal?);

            lstParametro.Add(parametro);
        }

        return lstParametro;
    }


    protected void ddlTipoCobro_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlCobro = sender as DropDownList;

            if (ddlCobro != null)
            {
                btnAgregar.Visible = true;
                List<ParametroCobroPrejuridico> lstCobroPre = new List<ParametroCobroPrejuridico>(1);
                ParametroCobroPrejuridico cobro = new ParametroCobroPrejuridico();

                string selectedValue = ddlCobro.SelectedValue;

                if (!string.IsNullOrWhiteSpace(selectedValue))
                {
                    cobro.forma_cobro = Convert.ToInt32(selectedValue);
                    lstCobroPre = _cobroService.ListarParametroCobroPrejuridico(cobro, _usuario);
                }
                if (lstCobroPre.Count <= 0)
                {                    
                    ParametroCobroPrejuridico item = new ParametroCobroPrejuridico();
                    lstCobroPre.Add(item);
                } 
                gvLista.DataSource = lstCobroPre;
                gvLista.DataBind();
            }
            else
            {
                btnAgregar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            VerError("Error al cambiar de tipo de cobro, " + ex.Message);
        }
    }

    protected void ddlFormaCobro_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlFormaCobro = sender as DropDownList;

            if (ddlFormaCobro != null)
            {
                List<ParametroCobroPrejuridico> lstCobroPre = new List<ParametroCobroPrejuridico>();
                lstCobroPre = LlenarListaParaGuardar();
                gvLista.DataSource = lstCobroPre;
                gvLista.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError("Error al cambiar de tipo de cobro, " + ex.Message);
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        VerError("");
        lblMensaje.Text = "";
        List<ParametroCobroPrejuridico> lstParametro = new List<ParametroCobroPrejuridico>();
        lstParametro = LlenarListaParaGuardar();
        ParametroCobroPrejuridico item = new ParametroCobroPrejuridico();
        lstParametro.Add(item);
        gvLista.DataSource = lstParametro;
        gvLista.DataBind();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtPorcentaje = (TextBox)e.Row.FindControl("txtPorcentaje");
            TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
            if (ddlFormaCobro.SelectedIndex == 1)
            {
                if (txtPorcentaje != null)
                    txtPorcentaje.Visible = false;
                if (txtValor != null)
                    txtValor.Visible = true;
            }
            else
            {
                if (txtPorcentaje != null)
                    txtPorcentaje.Visible = true;
                if (txtValor != null)
                    txtValor.Visible = false;
            }
        }
    }
}