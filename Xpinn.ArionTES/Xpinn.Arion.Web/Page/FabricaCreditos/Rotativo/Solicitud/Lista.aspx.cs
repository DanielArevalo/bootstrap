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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Entities;
using System.Globalization;

partial class Lista : GlobalWeb
{
   /// Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[creditoServicio.CodigoProgramaRotativo + ".id"] != null)
                VisualizarOpciones(creditoServicio.CodigoProgramaRotativo, "L");
            else
                VisualizarOpciones(creditoServicio.CodigoProgramaRotativo, "L");

            Site toolBar = (Site)this.Master;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            
            ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                VerError("");
                Site toolBar = (Site)this.Master;
                toolBar.MostrarConsultar(true);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativo, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }

    
    protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");

        // Determinar los datos de la persona seleccionada
        GridView gvListaAFiliados = (GridView)sender;
        String identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
        Int64 Codigo = Convert.ToInt64(gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[1].Text);
        String separador = " ";
        String Nombre = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[5].Text + separador + gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[6].Text
           + separador + gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[7].Text + separador + gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[8].Text;
        Session[creditoServicio.CodigoProgramaRotativoReporte + ".id"] = Codigo;
        Session["codigocliente"] = Codigo;

        Session["Nombre"] = Nombre.Trim().Replace("&nbsp;", "");
        Session["Identificacion"] = identificacion;
        Session["operacion"] = "N";//credito Rotativo Lista Origen de la pagina 
        // Ir a la siguiente página
        Navegar("~/Page/FabricaCreditos/Rotativo/Solicitud/Nuevo.aspx");
       
    }    


}