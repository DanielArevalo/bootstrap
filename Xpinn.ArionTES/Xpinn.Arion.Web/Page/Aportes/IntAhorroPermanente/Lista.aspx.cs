using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Aportes.Services;

public partial class Lista : GlobalWeb
{
    AporteServices apoServicio = new AporteServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(apoServicio.CodigoProgramaPagoIntAPermanente, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarCombo();
                VerError("");
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "Page_Load", ex);
        }
    }

    protected void CargarCombo()
    {
        try
        {
            PoblarListas poblar = new PoblarListas();
            poblar.PoblarListaDesplegable("empresa_recaudo", "cod_empresa, nom_empresa", "","1",ddlEmpresa,(Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "CargarEmpresa", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, "");
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session["num_recaudo"] = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Navegar(Pagina.Nuevo);
    }

    private void Actualizar()
    {
        try
        {
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();
            List<RecaudosMasivos> lista = new List<RecaudosMasivos>();
            RecaudosMasivos pRecaudo = new RecaudosMasivos();
            RecaudosMasivosService RecaudosMasivosServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            lstConsulta = RecaudosMasivosServicio.ListarRecaudo(ObtenerValores(), (Usuario)Session["usuario"]);
            lstConsulta = lstConsulta.OrderByDescending(x => x.numero_recaudo).ToList();

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                pLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                pLista.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> La consulta no obtuvo resultados";
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "Actualizar", ex);
        }
    }

    private RecaudosMasivos ObtenerValores()
    {
        RecaudosMasivos vRecaudosMasivos = new RecaudosMasivos();

        if (txtNumRecaudo.Text.Trim() != "")
            vRecaudosMasivos.numero_recaudo = Convert.ToInt64(txtNumRecaudo.Text.Trim());
        if (txtFecPeriodo.TieneDatos == true)
            vRecaudosMasivos.periodo_corte = txtFecPeriodo.ToDateTime;
        if (ddlEmpresa.SelectedValue != "")
            vRecaudosMasivos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
        vRecaudosMasivos.estado = "2";
        return vRecaudosMasivos;
    }
}