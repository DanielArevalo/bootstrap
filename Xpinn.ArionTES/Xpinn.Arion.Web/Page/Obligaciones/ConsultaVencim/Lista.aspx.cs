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
            VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma3, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma3, "Page_PreInit", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ObligacionCreditoServicio.CodigoPrograma3);
        Navegar("Lista.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ImprimirGrilla();
            if (!IsPostBack)
            {
                LlenarComboEntidades(ddlEntidad);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma3, "Page_Load", ex);
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
            lstConsulta = ObligacionCreditoServicio.ListarObligacionCreditoVencido(ObtenerValores(), (Usuario)Session["usuario"]);

            gvObCredito.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvObCredito.Visible = true;
                gvObCredito.DataBind();
            }
            else
            {
                gvObCredito.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma3 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma3, "Actualizar", ex);
        }
    }

    private Xpinn.Obligaciones.Entities.ObligacionCredito ObtenerValores()
    {
        Xpinn.Obligaciones.Entities.ObligacionCredito vObligacionCredito = new Xpinn.Obligaciones.Entities.ObligacionCredito();

        vObligacionCredito.codentidad = Convert.ToInt64(ddlEntidad.SelectedValue);

        if (txtFechaIni.Text.Trim() != "")
            vObligacionCredito.fecha_inicio = txtFechaIni.Text.Trim();
        else
            vObligacionCredito.fecha_inicio = "01/01/1900";


        if (txtFechaFin.Text.Trim() != "")
            vObligacionCredito.fecha_final = txtFechaFin.Text.Trim();
        else
            vObligacionCredito.fecha_final = "01/01/1900";

        vObligacionCredito.codfiltroorderuno = Convert.ToString(ddlFiltro1.SelectedValue);
        vObligacionCredito.codfiltroorderdos = Convert.ToString(ddlFiltro2.SelectedValue);

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

    protected void ImprimirGrilla()
    {
        string printScript =
        @"function PrintGridView()
            {
             div = document.getElementById('DivButtons');
             div.style.display='none';

            var gridInsideDiv = document.getElementById('gvDiv');
            var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
            printWindow.document.write(gridInsideDiv.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();}";
        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);
        btnImprimir.Attributes.Add("onclick", "PrintGridView();");
    }
}