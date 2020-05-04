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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;

partial class Lista : GlobalWeb
{
    private TransicionSegmentoNIFService TranServicios = new TransicionSegmentoNIFService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TranServicios.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TranServicios.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, TranServicios.CodigoProgramaoriginal);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TranServicios.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, TranServicios.CodigoProgramaoriginal);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, TranServicios.CodigoProgramaoriginal);
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[TranServicios.CodigoProgramaoriginal + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[TranServicios.CodigoProgramaoriginal + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TranServicios.CodigoProgramaoriginal, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<TransicionSegmentoNIF> lstConsulta = new List<TransicionSegmentoNIF>();
            lstConsulta = TranServicios.ListarTransicionSegmento(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(TranServicios.CodigoProgramaoriginal + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TranServicios.CodigoProgramaoriginal, "Actualizar", ex);
        }
    }

    private TransicionSegmentoNIF ObtenerValores()
    {
        TransicionSegmentoNIF vSegmento = new TransicionSegmentoNIF();

        if (txtCodigo.Text.Trim() != "")
            vSegmento.codsegmento = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            vSegmento.nombre = Convert.ToString(txtDescripcion.Text.Trim().ToUpper());

        return vSegmento;
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(e.Keys[0]);

            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea eliminar el registro seleccionado?");
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ID"].ToString() != "")
            {
                TranServicios.EliminarTransicionSegmentoNIF(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TranServicios.CodigoProgramaoriginal, "btnContinuarMen_Click", ex);
        }
    }
   

}