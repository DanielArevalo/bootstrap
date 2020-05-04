using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    ParametrosFlujoCajaService ParamFlujoCajaServicio = new ParametrosFlujoCajaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ParamFlujoCajaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session[ParamFlujoCajaServicio.CodigoPrograma + ".id"] != null)
                {
                    Int64 idObjeto = Convert.ToInt64(Session[ParamFlujoCajaServicio.CodigoPrograma + ".id"]);
                }
                CargarValoresConsulta(panelConsulta, ParamFlujoCajaServicio.CodigoPrograma);
                //if (Session[ParamFlujoCajaServicio.CodigoPrograma + ".consulta"] != null)
                Actualizar();
                
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        GuardarValoresConsulta(panelConsulta, ParamFlujoCajaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void Actualizar()
    {
        try
        {
            List<ParametrosFlujoCaja> lstConsulta = new List<ParametrosFlujoCaja>();
            lstConsulta = ParamFlujoCajaServicio.ListarConceptos(ObtenerValores(), (Usuario)Session["usuario"]);

            gvCuentas.PageSize = pageSize;
            gvCuentas.EmptyDataText = emptyQuery;
            gvCuentas.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvCuentas.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvCuentas.DataBind();
                ValidarPermisosGrilla(gvCuentas);
            }
            else
            {
                gvCuentas.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ParamFlujoCajaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected ParametrosFlujoCaja ObtenerValores()
    {
        ParametrosFlujoCaja concepto = new ParametrosFlujoCaja();
        if (txtCodConcepto.Text.Trim() != "")
            concepto.cod_concepto = Convert.ToInt64(txtCodConcepto.Text.Trim());
        if (Convert.ToInt64(ddlTipoConcepto.SelectedValue) > 0)
            concepto.tipo_concepto = Convert.ToInt64(ddlTipoConcepto.SelectedItem.Value);
        if (txtConcepto.Text.Trim() != "")
            concepto.descripcion = txtConcepto.Text.Trim();
        return concepto;
    }
    
    protected void gvCuentas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvCuentas.Rows[e.NewEditIndex].Cells[2].Text;
        Session[ParamFlujoCajaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvCuentas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            ParamFlujoCajaServicio.EliminarConcepto(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }
    
}