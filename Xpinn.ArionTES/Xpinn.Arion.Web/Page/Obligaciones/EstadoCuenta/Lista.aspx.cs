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

public partial class Listar : GlobalWeb
{
    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma2, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Page_PreInit", ex);
        }
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ObligacionCreditoServicio.CodigoPrograma2);
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
             BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Page_Load", ex);
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
             lstConsulta = ObligacionCreditoServicio.ListarDatosSolicitud(ObtenerValores(), (Usuario)Session["usuario"]);

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

             Session.Add(ObligacionCreditoServicio.CodigoPrograma2 + ".consulta", 1);
         }
         catch (Exception ex)
         {
             BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Actualizar", ex);
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
        String id = gvObCredito.SelectedRow.Cells[1].Text;
        Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

}