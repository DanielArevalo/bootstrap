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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;


public partial class Lista : GlobalWeb
{
    Xpinn.NIIF.Services.EstadosFinancierosNIIFService EstadosFinancierosNIFServicio = new Xpinn.NIIF.Services.EstadosFinancierosNIIFService();
    Configuracion conf = new Configuracion();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(EstadosFinancierosNIFServicio.CodigoPrograma.ToString(), "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
          

        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarListasDesplegables(TipoLista.TipoEstadoFinanciero, ddlTipoEstadoFinanciero);


                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                Actualizar();
            }
            if (Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"].ToString();
                Session.Remove(EstadosFinancierosNIFServicio.CodigoPrograma + ".id");                
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para la consulta de los scoreboards
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {     
        Page.Validate();
    
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, EstadosFinancierosNIFServicio.CodigoPrograma);
            Actualizar();
            mvEstadosFinancieros.ActiveViewIndex = 0;
        }
    }






    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        
        if (!string.IsNullOrWhiteSpace(ddlTipoEstadoFinanciero.SelectedValue) && ddlTipoEstadoFinanciero.SelectedValue!="0")
        {
            filtro += " and codigo = " + ddlTipoEstadoFinanciero.SelectedValue;
        }



        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }
    public void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<EstadosFinancierosNIIF> lstEstadosFinancieros = EstadosFinancierosNIFServicio.ListarTipoEstadoFinancieroNIIF(filtro, Usuario);

            if (lstEstadosFinancieros.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstEstadosFinancieros.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            gvEstadosFinancieros.DataSource = lstEstadosFinancieros;
            gvEstadosFinancieros.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }




    protected void gvEstadosFinancieros_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvEstadosFinancieros.SelectedRow.Cells[1].Text);

        Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"] = id;
      
        Navegar("~/Page/Niif/ConceptosNIIF/Lista.aspx");
    }
}
