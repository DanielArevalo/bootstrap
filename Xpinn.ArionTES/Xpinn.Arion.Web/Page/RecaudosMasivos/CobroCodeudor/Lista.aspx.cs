using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    CobroCodeudorService _cobroCodeudorService = new CobroCodeudorService();
    Usuario _usuario;


    #region Eventos


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_cobroCodeudorService.CodigoPrograma, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cobroCodeudorService.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                LlenarGVListaCreditosCodeudores();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cobroCodeudorService.CodigoPrograma, "Page_Load", ex);
        }
    }


    private void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        LlenarGVListaCreditosCodeudores();
    }


    private void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }


    private void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtRadicacion.Text = "";
        txtFecha.Text = "";
        txtCodDeudor.Text = "";
        txtIdentDeudor.Text = "";
        txtCodCodeudor.Text = "";
        txtIdentCodeudor.Text = "";
    }


    protected void gvListaCreditosCodeudores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvListaCreditosCodeudores.PageIndex = e.NewPageIndex;
        LlenarGVListaCreditosCodeudores();
    }


    protected void gvListaCreditosCodeudores_SelectedIndexChanged(object sender, EventArgs e)
    {
        long numeroRadi = Convert.ToInt64(gvListaCreditosCodeudores.SelectedRow.Cells[3].Text);
        Session[_cobroCodeudorService.CodigoPrograma + ".numeroRadi"] = numeroRadi; // Guardo el N°Radicacion para verlo en Nuevo.aspx
        Navegar(Pagina.Nuevo);
    }


    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Select")
        {
            GridViewRow row = gvListaCreditosCodeudores.Rows[Convert.ToInt32(e.CommandArgument)];
            long idBorrar = Convert.ToInt64(row.Cells[2].Text);

            _cobroCodeudorService.EliminarCobroCodeudor(idBorrar, _usuario);

            LlenarGVListaCreditosCodeudores();
        }
    }

    protected void gvListaCreditosCodeudores_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {

    }


    #endregion


    #region Metodos para llenar y consultar GVListaCreditosCodeudores con o sin filtro


    private void LlenarGVListaCreditosCodeudores()
    {
        try
        {
            string filtro = ObtenerFiltroToQuery();

            List<CobroCodeudor> lstCodeudores = ConsultarListaCreditosConCodeudores(filtro);

            if (lstCodeudores.Count == 0)
            {
                lblInfo.Visible = true;
            }
            else
            {
                lblTotalRegs.Visible = false;
                lblTotalRegs.Text = "Se obtuvieron " + lstCodeudores.Count + " Registros!.";
            }

            gvListaCreditosCodeudores.DataSource = lstCodeudores;
            gvListaCreditosCodeudores.DataBind();
        }
        catch (Exception ex)
        {
            VerError("LlenarGVListaCreditosCodeudores, " + ex.Message);
        }
    }


    private List<CobroCodeudor> ConsultarListaCreditosConCodeudores(string filtro)
    {
        try
        {
            List<CobroCodeudor> lstCreditos = _cobroCodeudorService.ListarCobroCodeudor(filtro, _usuario);
            return lstCreditos;
        }
        catch (Exception)
        {
            return null;
        }
    }


    #endregion


    #region Metodo para obtener filtro de acuerdo a la información suministrada


    // Dependiendo de lo escrito en los campos armo el filtro para filtrar el query a realizar
    private string ObtenerFiltroToQuery()
    {
        string filtro = string.Empty;

        //Filtro numero_radicacion
        if (!string.IsNullOrWhiteSpace(txtRadicacion.Text))
        {
            filtro += " and co.numero_radicacion ='" + txtRadicacion.Text + "'";
        }

        //Filtro fecha creacion
        if (!string.IsNullOrWhiteSpace(txtFecha.Text))
        {
            filtro += " and co.fechacrea ='" + txtFecha.Text + "'";
        }

        //Filtro codigo deudor
        if (!string.IsNullOrWhiteSpace(txtCodDeudor.Text))
        {
            filtro += " and co.cod_deudor ='" + txtCodDeudor.Text + "'";
        }

        //Filtro tipo identificacion deudor
        if (!string.IsNullOrWhiteSpace(txtIdentDeudor.Text))
        {
            filtro += " and v1.identificacion like '%" + txtIdentDeudor.Text + "%'";
        }

        //Filtro tipo codigo codeudor
        if (!string.IsNullOrWhiteSpace(txtCodCodeudor.Text))
        {
            filtro += " and c.codpersona ='" + txtCodCodeudor.Text + "'";
        }

        // Filtro identificacion codeudor
        if (!string.IsNullOrWhiteSpace(txtIdentCodeudor.Text))
        {
            filtro += " and v.identificacion like '%" + txtIdentCodeudor.Text + "%'";
        }

        return filtro;
    }


    #endregion


}