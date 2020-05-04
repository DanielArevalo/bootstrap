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

public partial class Lista : GlobalWeb
{
    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma6, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Page_PreInit", ex);
        }
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ObligacionCreditoServicio.CodigoPrograma6);
        Navegar("Lista.aspx");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboEntidades(ddlEntidad);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();
            lstConsulta = ObligacionCreditoServicio.ListarDatosSolicitudAprobacion(ObtenerValores(), (Usuario)Session["usuario"]);

            gvObCredito.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Label2.Visible = false;
                lblTotalReg.Visible = true;
                lblTotalReg.Text = "<br/> Registros encontrados " + lstConsulta.Count;
                gvObCredito.Visible = true;
                gvObCredito.DataBind();
            }
            else
            {
                Label2.Visible = true;
                lblTotalReg.Visible = false;
                gvObCredito.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma6 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Actualizar", ex);
        }
    }

    private Xpinn.Obligaciones.Entities.ObligacionCredito ObtenerValores()
    {
        Xpinn.Obligaciones.Entities.ObligacionCredito vObligacionCredito = new Xpinn.Obligaciones.Entities.ObligacionCredito();

        if (txtNumeObl.Text.Trim() != "")
            vObligacionCredito.codobligacion = Convert.ToInt64(txtNumeObl.Text.Trim());

        vObligacionCredito.codentidad = Convert.ToInt64(ddlEntidad.SelectedValue);

        if (txtMotoSolicitado.Text.Trim() != "")
            vObligacionCredito.montosolicitud = decimal.Parse(txtMotoSolicitado.Text);

        return vObligacionCredito;
    }

    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();
        ddlEntidades.Items.Insert(0, new ListItem("Todos", "0"));
    }

    protected void gvObCredito_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvObCredito.SelectedRow.Cells[2].Text;
        Session[ObligacionCreditoServicio.CodigoPrograma6 + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvObCredito_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int cod = Convert.ToInt32(gvObCredito.DataKeys[e.RowIndex].Values[0].ToString());
        Session["cod"] = cod;
        ctlMensaje.MostrarMensaje("Desea Realizar la eliminación?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ObligacionCreditoServicio.EliminarObligacionCredito(Convert.ToInt64(Session["cod"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

}